using BusinessLayer.Concrete;
using DataAccessLayer.Context;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DataContext db = new DataContext();
        ProductRepository productRepository = new ProductRepository();
        public PartialViewResult PopularProduct()
        {
            var product = productRepository.GetPopularProduct();
            ViewBag.popular = product;

            return PartialView();
        }
        public ActionResult ProductDetails(int id)
        {
            var details = productRepository.GetById(id);
            var yorum = db.Comments.Where(x => x.ProductId == id).ToList();
            ViewBag.yorum = yorum;  
            return View(details);
        }
        [HttpPost]
        public ActionResult Comment(Comment data)
        {
            if (User.Identity.IsAuthenticated)
            {
                db.Comments.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            var details = productRepository.GetById(data.ProductId);
            return RedirectToAction("ProductDetails", details);
        }
    }
}