using emarket.Context;
using emarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace emarket.Controllers
{
    public class CartController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            var cart = db.Cart;

            return View();
        }

        public ActionResult AddToCart(int id)
        {
            var cart1 = new Cart();
            DateTime localDate = DateTime.Now;
            cart1.added_at = localDate;
            cart1.product_id = id;
            var cart2 = db.Cart.Find(id);
            if (cart2 != null)
            {
                return RedirectToAction("Index", "Products");
            }
            else
            {
                db.Cart.Add(cart1);
                db.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
        }
        public ActionResult Remove(int id)
        {
            var cart = db.Cart.Find(id);
            if (cart != null)
            {
                db.Cart.Remove(cart);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
    }
    
}