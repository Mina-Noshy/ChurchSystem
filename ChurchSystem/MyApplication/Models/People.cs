using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class People
    {
        // المواطنين
        [Key]
        public int Id { get; set; }
        [Required]
        public string PeopleName { get; set; }
        public int HouseId { get; set; }
        public int CollageId { get; set; }
        public int LevelId { get; set; }
        public DateTime Birthdate { get; set; }
        public string Work { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        public string SocialStatus { get; set; }
        public string ImagePath { get; set; }
        public string Note { get; set; }

        [ForeignKey("HouseId")]
        public virtual House House { get; set; }

        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }

        [ForeignKey("CollageId")]
        public virtual Collage Collage { get; set; }
        public virtual ICollection<Confession> Confessions { get; set; }
        public virtual ICollection<Server> Servers { get; set; }
    }
}
