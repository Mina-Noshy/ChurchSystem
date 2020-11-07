using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class Stage
    {
        // مراحل
        [Key]
        public int Id { get; set; }
        [Required]
        public string StageName { get; set; }
        public virtual ICollection<Level> Levels { get; set; }
        public virtual ICollection<Graduate> Graduates { get; set; }
    }
}
