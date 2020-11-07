using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class Area
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AreaName { get; set; }
        public int TownId { get; set; }

        [ForeignKey("TownId")]
        public virtual Town Towns { get; set; }

        public virtual ICollection<House> Houses { get; set; }
    }
}
