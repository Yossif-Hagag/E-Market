using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emarket.Models;

namespace emarket.ViewModels
{
    public class CategoryProduct
    {
        public Product Product { get; set; }
        public IEnumerable<Category> Category { get; set; }
    }
}