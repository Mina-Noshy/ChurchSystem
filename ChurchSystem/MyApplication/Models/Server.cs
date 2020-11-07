using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class Server
    {
        // الخدام
        [Key]
        public int Id { get; set; }
        public int PeopleId { get; set; }
        public string Rank { get; set; }
        public DateTime JoinDate { get; set; }
        public string ImagePath { get; set; }
        public string Note { get; set; }

        [ForeignKey("PeopleId")]
        public virtual People People { get; set; }
    }
}
