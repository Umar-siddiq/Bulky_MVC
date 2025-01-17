﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Max Length is 30 characters")]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage ="Display Order Must be between 1 and 100")]
        public int DisplayOrder { get; set; }


    }
}
