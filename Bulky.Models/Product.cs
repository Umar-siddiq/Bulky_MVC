using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        
        [Required]
        public string? ISBN { get; set; }
        
        [Required] 
        public string? Author { get; set; }

        [Display(Name = "List Price")]
        [Required]
        [Range(1, 1000)]
        public double ListPrice { get; set; }


        [Display(Name = "Price for 1-50")]
        [Required]
        [Range(1, 1000)]
        public double Price { get; set; }


        [Display(Name = "Price for 51-100")]
        [Required]
        [Range(1, 1000)]
        public double Price50 { get; set; }


        [Display(Name = "Price for 100+")]
        [Required]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        [ValidateNever]
        public int CategoryId { get; set; }
        
        [ValidateNever][ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string ImageUrl { get; set; }
    }
}
