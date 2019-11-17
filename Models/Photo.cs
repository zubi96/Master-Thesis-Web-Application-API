using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterThesisWebApplication.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
