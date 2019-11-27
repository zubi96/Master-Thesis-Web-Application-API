using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterThesisWebApplication.Data.Interfaces;
using MasterThesisWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterThesisWebApplication.Data.Repositories
{
    public class MobileAuthRepository : IMobileAuthRepository
    {
        private readonly DataContext _context;
        public MobileAuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<MobileUser> Register(MobileUser user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.MobileUsers.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<MobileUser> Login(string username, string password)
        {
            var user = await _context.MobileUsers.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.MobileUsers.AnyAsync(x => x.Username == username))
            {
                return true;
            }

            return false;
        }

    }
}
