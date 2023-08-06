using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
	[AllowAnonymous]
	public class denemeController : Controller
	{

		// GET: deneme
		
		public ActionResult Index()
		{
			return View();
		}
	}
}