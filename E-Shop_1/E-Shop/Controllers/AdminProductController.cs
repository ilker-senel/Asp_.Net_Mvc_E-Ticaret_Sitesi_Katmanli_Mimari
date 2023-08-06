using BusinessLayer.Concrete;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace E_Shop.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminProductController : Controller
	{
		// GET: AdminProduct
		ProductRepository productRepository = new ProductRepository();
		DataContext db = new DataContext();
		public ActionResult Index(int sayfa = 1)
		{
			return View(productRepository.List().ToPagedList(sayfa, 3));
		}
		public ActionResult Create()
		{
			List<SelectListItem> deger1 = (from i in db.Categories.ToList()
										   select new SelectListItem
										   {
											   Text = i.Name,
											   Value = i.Id.ToString()
										   }).ToList();
			ViewBag.ktgr = deger1;
			return View();
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Product data, HttpPostedFileBase file)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Hata Oluştu");
			}
			string path = Path.Combine("~/Content/Image/" + file.FileName);
			file.SaveAs(Server.MapPath(path));
			data.Image = file.FileName.ToString();
			productRepository.Insert(data);
			return RedirectToAction("Index");
		}
		public ActionResult Delete(int id)
		{
			var delete = productRepository.GetById(id);
			productRepository.Delete(delete);
			return RedirectToAction("Index");
		}

		public ActionResult Update(int id)
		{
			List<SelectListItem> deger1 = (from i in db.Categories.ToList()
										   select new SelectListItem
										   {
											   Text = i.Name,
											   Value = i.Id.ToString()
										   }).ToList();
			ViewBag.ktgr = deger1;
			var update = productRepository.GetById(id);

			return View(update);



		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Update(Product data, HttpPostedFileBase file)
		{
			var update = productRepository.GetById(data.Id);
			try
			{
				//if (!ModelState.IsValid)
				//{

				if (file == null)
				{

					update.Description = data.Description;
					update.Name = data.Name;
					update.IsApproved = data.IsApproved;
					update.Popular = data.Popular;
					update.Price = data.Price;
					update.Stok = data.Stok;
					//update.Image = file.FileName.ToString();
					update.CategoryId = data.CategoryId;
					productRepository.Update(update);
					return RedirectToAction("Index");
				}
				else
				{

					update.Description = data.Description;
					update.Name = data.Name;
					update.IsApproved = data.IsApproved;
					update.Popular = data.Popular;
					update.Price = data.Price;
					update.Stok = data.Stok;
					update.Image = file.FileName.ToString();
					update.CategoryId = data.CategoryId;
					productRepository.Update(update);
					return RedirectToAction("Index");
				}
				//}
				//var errors = ModelState.Values.SelectMany(v => v.Errors)
				//						  .Select(e => e.ErrorMessage);
			}
			catch (Exception)
			{

				;
			}


			ModelState.AddModelError("", "bir hata oluştu");
			return Update(data.Id);
		}
		public ActionResult CriticalStock()
		{
			var kritik = db.Products.Where(x => x.Stok <= 50).ToList();
			return View(kritik);
		}
		public PartialViewResult StockCount()
		{
			if (User.Identity.IsAuthenticated)
			{
				var count = db.Products.Where(x => x.Stok < 50).Count();
				ViewBag.Count = count;
				var azalan = db.Products.Where(x => x.Stok == 50).Count();
				ViewBag.Azalan = azalan;
			}//dk7.16
			return PartialView();
		}

	}
}