using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class House
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string HouseName { get; set; }
        public int AreaId { get; set; }
        public string Mobile { get; set; }

        [ForeignKey("AreaId")]
        public virtual Area Area { get; set; }
        public virtual ICollection<People> Peoples { get; set; }
        public virtual ICollection<Death> Deaths { get; set; }
        public virtual ICollection<Lack> Lacks { get; set; }
    }
}
