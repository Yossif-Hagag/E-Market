using emarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emarket.ViewModels
{
    public class ProductCartViewModel
    {
           public List<Product> products { get; set; }
        public List<Cart> carts { get; set; }
    }
}