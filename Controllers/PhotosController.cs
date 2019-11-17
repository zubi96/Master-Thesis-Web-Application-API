using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MasterThesisWebApplication.Data;
using MasterThesisWebApplication.Data.Repositories;
using MasterThesisWebApplication.Dtos;
using MasterThesisWebApplication.Helpers;
using MasterThesisWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MasterThesisWebApplication.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("locations/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IAdminRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;

        public PhotosController(IAdminRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            var acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret);

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{photoId}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int photoId)
        {
            var photoFromRepo = await _repo.GetPhoto(photoId);

            if (photoFromRepo == null)
                return NoContent();

            var photoToReturn = _mapper.Map<PhotoToReturnDto>(photoFromRepo);

            return Ok(photoToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForLocation([FromForm] PhotoForCreationDto photoForCreationDto)
        {
            var locationFromRepo = await _repo.GetLocation(photoForCreationDto.LocationId);

            if (locationFromRepo == null)
                return NoContent();

            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(1000).Height(500).Crop("fill")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            locationFromRepo.Photos.Add(photo);

            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoToReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { photoId = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add photo");
        }
    }
}