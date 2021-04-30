using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace emarket.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage =" You have to enter product's name !")]
        public string name { get; set; }
        [Required(ErrorMessage = " You have to enter product's price !")]
        public int price { get; set; }
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        [DataType(DataType.ImageUrl)]
        public string image { get; set; }
        [Required]
        [StringLength(3000,MinimumLength = 200, ErrorMessage = " You have to enter the product's description and must be more than 200 character.")]
        public string description { get; set; }
        [ForeignKey("Category")]
        public int category_id { get; set; }
        public virtual Category Category { get; set; }
        public virtual Cart Cart { get; set; }
    }
}