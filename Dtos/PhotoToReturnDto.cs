using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterThesisWebApplication.Dtos
{
    public class PhotoToReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
