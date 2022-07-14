using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Info { get; set; }
        public double Price { get; set; }
        public byte[] Photo { get; set; }
    }
}
