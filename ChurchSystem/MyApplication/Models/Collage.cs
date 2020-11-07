using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class Collage
    {
        // الكلية
        [Key]
        public int Id { get; set; }
        [Required]
        public string CollageName { get; set; }

        public virtual ICollection<People> Peoples { get; set; }
        public virtual ICollection<OutStanding> OutStandings { get; set; }
        public virtual ICollection<Graduate> Graduates { get; set; }
    }
}
