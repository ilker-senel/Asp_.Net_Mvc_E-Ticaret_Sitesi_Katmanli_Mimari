using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
	public class UserController : Controller
	{
		// GET: User
		DataContext db = new DataContext();
		public ActionResult Update()
		{
			var username = (string)Session["Mail"];
			var degerler = db.Users.FirstOrDefault(x => x.Email == username);
			return View(degerler);
		}
		[HttpPost]
		public ActionResult Update(User data)
		{
			var username = (string)Session["Mail"];
			var user = db.Users.Where(x => x.Email == username).FirstOrDefault();
			user.Name = data.Name;
			user.Email = data.Email;
			user.Password = data.Password;
			user.RePassword = data.RePassword;
			user.SurName = data.SurName;
			user.UserName = data.UserName;
			db.SaveChanges();
			return RedirectToAction("Index", "Home");
		}

		public ActionResult PasswordReset()
		{
			return View();
		}

		[HttpPost]
		public ActionResult PasswordReset(string eposta)
		{
			var mail = db.Users.Where(x => x.Email == eposta).SingleOrDefault();
			if (mail != null)
			{
				Random rnd = new Random();
				int yeniSifre = rnd.Next();
				User sifre = new User();
				mail.Password = (Convert.ToString(yeniSifre));
				mail.RePassword = mail.Password.Trim();
				db.SaveChanges();

				WebMail.SmtpServer = "smtp.yandex.com";
				WebMail.EnableSsl = true;

				//WebMail.UserName = "ilkersenel1057@gmail.com";
				//WebMail.Password = "mT4xcUMTahmetahmet";
				WebMail.UserName = "kurumsalhizmetdeneme@yandex.ru";
				WebMail.Password = "Lw4-MZ4-Nj8-DJr";
				WebMail.SmtpPort = 465;//465;
				WebMail.Send(eposta, "Giriş Şifreniz", "Şifreniz:" + yeniSifre);
				ViewBag.uyari = "Şifreniz başarıyla gönderilmiştir";
				/*dk 15.00*/
			}
			else
			{
				ViewBag.uyari = "Bir hata oluştu litfen tekrar deneyin";
			}
			return View();
		}
	}
}