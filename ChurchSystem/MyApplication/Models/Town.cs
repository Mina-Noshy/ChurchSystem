using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class Town
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TownName { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
    }
}
