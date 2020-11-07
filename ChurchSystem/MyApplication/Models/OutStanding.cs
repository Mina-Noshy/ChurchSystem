using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class OutStanding
    {
        // متفوقين
        [Key]
        public int Id { get; set; }
        public string StudentName{ get; set; }
        public int GraduateYear { get; set; }
        public string Grade { get; set; }
        public int Percent { get; set; }
        public int CollageId { get; set; }
        public int LevelId { get; set; }
        public string Mobile { get; set; }
        public string Addres { get; set; }
        public string Note { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey("CollageId")]
        public virtual Collage Collage { get; set; }

        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }
    }
}
