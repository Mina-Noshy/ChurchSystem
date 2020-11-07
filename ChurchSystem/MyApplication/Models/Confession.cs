using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class Confession
    {
        // اعترافات
        [Key]
        public int Id { get; set; }
        public int PeopleId { get; set; }
        public DateTime LastConfessionDate { get; set; }
        public string Note { get; set; }

        [ForeignKey("PeopleId")]
        public virtual People People { get; set; }
    }
}
