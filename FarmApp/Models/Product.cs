using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }

        public string FarmerId { get; set; }
        public virtual ApplicationUser Farmer { get; set; }
    }
}