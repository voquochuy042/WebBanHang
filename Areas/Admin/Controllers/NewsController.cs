using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        Database1Entities db = new Database1Entities();
        // GET: Admin/News
        public ActionResult Index()
        {
            var items = db.tb_News.OrderByDescending(x=>x.Id).ToList();
            return View(items);
        }
        public ActionResult Add()
        {
			return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Add(tb_News model)
		{
            if (ModelState.IsValid)
            {
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                db.tb_News.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			return View(model);
		}
        public ActionResult Edit(int id)
        {
            var model = db.tb_News.Find(id);

            return View(model);
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(tb_News news)
		{
			if (ModelState.IsValid)
			{

				db.tb_News.Attach(news);
				news.ModifiedDate = DateTime.Now;
				db.Entry(news).Property(x => x.Title1).IsModified = true;
				db.Entry(news).Property(x => x.Description).IsModified = true;
				db.Entry(news).Property(x => x.Image).IsModified = true;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(news);
		}
	}
}