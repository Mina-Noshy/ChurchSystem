using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class Level
    {
        // صفوف المراحل التعليمية
        [Key]
        public int Id { get; set; }
        [Required]
        public string LevelName { get; set; }
        public int StageId { get; set; }
        public virtual ICollection<People> Peoples { get; set; }
        public virtual ICollection<OutStanding> OutStandings { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
    }
}
