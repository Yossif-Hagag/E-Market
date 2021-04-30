using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emarket.Context;
using emarket.Models;
using System.Data.Entity;
using emarket.ViewModels;

namespace emarket.Controllers
{
    public class ProductsController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET: Products
        public ActionResult Index(string searching)
        {
            var products = db.Product.Include(p => p.Category).ToList();
            products = db.Product.Where(x => x.name.Contains(searching) || searching == null).ToList();
        
            var cart = db.Cart.ToList();
            var viewModel = new ProductCartViewModel
            {
                carts = cart,
                products = products
            };
            return View(viewModel);

        }


        [HttpGet]
        public ActionResult Create()
        {
            var category = db.Category.ToList();
            CategoryProduct cp = new CategoryProduct()
            {
                Category = category,
            };
            return View(cp);
        }

        [HttpPost]
        public ActionResult Create(CategoryProduct cp , HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
               if(file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/products/"), pic);
                    file.SaveAs(path);
                    cp.Product.image = pic;
                }
                db.Product.Add(cp.Product);
                db.SaveChanges();

                var category = db.Category.SingleOrDefault(c => c.id == cp.Product.category_id);
                category.number_of_products++;

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var product = db.Product.SingleOrDefault(m => m.id == id);
            if(product != null)
            {
                return View(product);
            }
            return HttpNotFound();
        }


        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }



        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var item = db.Product.Find(id);
            if (item != null)
            {
                db.Product.Remove(item);
                var category = db.Category.ToList().SingleOrDefault(a => a.id == item.category_id);
                category.number_of_products--;

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Update(int id)
        {
            var product = db.Product.SingleOrDefault(k => k.id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var category = db.Category.ToList();
            CategoryProduct cp = new CategoryProduct()
            {
                Category = category,
                Product =product,
            };
            return View(cp);
        }


        [HttpPost]
        public ActionResult Update(CategoryProduct cp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var product = db.Product.SingleOrDefault(k => k.id == cp.Product.id);
                var categoryOld = db.Category.Find(product.category_id);
                product.name = cp.Product.name;
                product.price = cp.Product.price;
                product.description = cp.Product.description;
                if (product.category_id != cp.Product.category_id)
                {
                    categoryOld.number_of_products--;

                    product.category_id = cp.Product.category_id;

                    var categoryNew = db.Category.Find(product.category_id);
                    categoryNew.number_of_products++;
                }

                if(file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/products/"), pic);
                    file.SaveAs(path);
                    cp.Product.image = pic;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = cp.Product.id});
            }
            return RedirectToAction("Index");
        }

    }
}