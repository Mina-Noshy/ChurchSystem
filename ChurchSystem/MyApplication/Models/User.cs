using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Rank { get; set; }
        public bool IsActive { get; set; }
        public string UserDevice { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
