using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class Nursery
    {
        // حضانه
        [Key]
        public int Id { get; set; }
        [Required]
        public string ChildName { get; set; }
        public string FatherName { get; set; }
        public string Mobile { get; set; }
        public string Addres { get; set; }
        public string Level { get; set; }
        public DateTime Birthdate { get; set; }
        public string Note { get; set; }
        public string ImagePath { get; set; }
    }
}
