using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class Sitting
    {
        [Key]
        public int Id { get; set; }
        public string MacAddress { get; set; }
        public DateTime StartDate { get; set; }
        public string DeviceName { get; set; }
        public DateTime ExpireDate { get; set; }
        public string ChurchName { get; set; }
        public string Mobile { get; set; }
        public string Note { get; set; }
    }
}
