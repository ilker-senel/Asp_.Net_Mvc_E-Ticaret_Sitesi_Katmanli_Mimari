using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
	public class CartController : Controller
	{
		DataContext db = new DataContext();
		public ActionResult Index(decimal? tutar)
		{
			if (User.Identity.IsAuthenticated)
			{
				var username = User.Identity.Name;
				var kullanici = db.Users.FirstOrDefault(x => x.Email == username);
				var model = db.Carts.Where(x => x.UserId == kullanici.Id).ToList();
				var kid = db.Carts.FirstOrDefault(x => x.UserId == kullanici.Id);
				if (model != null)
				{
					if (kid == null)
					{
						ViewBag.Tutar = "Sepetinizde ürün bulunamadı.";
					}
					else if (kid != null)
					{
						tutar = db.Carts.Where(x => x.UserId == kid.UserId).Sum(x => x.Product.Price * x.Quantity);
						ViewBag.Tutar ="Toplam Tutar="+ tutar+" TL";
					}
					return View(model);
				}
			}
			return HttpNotFound();
		}

		public ActionResult AddCart(int id)
		{
			if (User.Identity.IsAuthenticated)
			{
				var kullaniciAdi = User.Identity.Name;
				var model = db.Users.FirstOrDefault(x => x.Email == kullaniciAdi);
				var u = db.Products.Find(id);
				var sepet = db.Carts.FirstOrDefault(x => x.UserId == model.Id && x.ProductId == id);
				if (model != null)
				{
					if (sepet != null)
					{
						sepet.Quantity++;
						sepet.Price = u.Price * sepet.Quantity;
						db.SaveChanges();
						return RedirectToAction("Index", "Cart");
					}
					var s = new Cart()
					{
						UserId = model.Id,
						ProductId = u.Id,
						Quantity = 1,
						Price = u.Price,
						Date = DateTime.Now,
					};
					db.Carts.Add(s);
					db.SaveChanges();
					return RedirectToAction("Index", "Cart");
				}
			}
			return View();

		}
		public ActionResult TotalCount(int? count)
		{
			if (User.Identity.IsAuthenticated)
			{
				var model = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
				count = db.Carts.Where(x => x.UserId == model.Id).Count();
				ViewBag.count = count;
				if (count == 0)
				{
					ViewBag.count = "";
				}
			}
			return PartialView();
		}

		public void DinamikMiktar(int id, int miktari)
		{
			var model = db.Carts.Find(id);
			model.Quantity = miktari;
			model.Price = model.Price * model.Quantity;
			db.SaveChanges();
		}

		public ActionResult azalt(int id)
		{
			var model=db.Carts.Find(id);
			if (model.Quantity == 1)
			{
				db.Carts.Remove(model);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			model.Quantity--;
			model.Price=model.Product.Price * model.Quantity;
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult arttir(int id)
		{
			var model = db.Carts.Find(id);
			model.Quantity++;
			model.Price=model.Product.Price*model.Quantity;
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			var sil=db.Carts.Find(id);
			db.Carts.Remove(sil);
			db.SaveChanges(); 
			return RedirectToAction("Index");
		}

		public ActionResult DeleteRange()
		{
			if (User.Identity.IsAuthenticated)
			{
				var kullanici = User.Identity.Name;
				var model = db.Users.FirstOrDefault(x=>x.Email==kullanici);
				var sil = db.Carts.Where(x => x.UserId == model.Id);
				db.Carts.RemoveRange(sil);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return HttpNotFound();
		}
	}
}