using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class Death
    {
        // وفيات
        [Key]
        public int Id { get; set; }
        [Required]
        public string DeceasedName { get; set; }
        [Required]
        public DateTime DeathDate { get; set; }
        public int HouseId { get; set; }
        public string Note { get; set; }

        public DateTime FifteenDate { get; set; }
        public bool Fifteen { get; set; } // 15s

        public DateTime FortyDate { get; set; }
        public bool Forty { get; set; } // 40s

        public DateTime AnnualDate { get; set; }
        public bool Annual { get; set; } // year

        [ForeignKey("HouseId")]
        public virtual House House { get; set; }
    }
}
