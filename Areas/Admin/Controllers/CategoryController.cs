using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        Database1Entities db = new Database1Entities();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var item = db.tb_Category.ToList();

            return View(item);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Add(tb_Category category)
		{
            if(ModelState.IsValid)
            {
                category.CreateDate = DateTime.Now;
				category.ModifiedDate = DateTime.Now;
				db.tb_Category.Add(category);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(category);
		}
        public ActionResult Edit(int id)
        {
			var item = db.tb_Category.Find(id);
			return View(item);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(tb_Category category)
		{
            if (ModelState.IsValid)
            {
				
				db.tb_Category.Attach(category);
				category.ModifiedDate = DateTime.Now;
				db.Entry(category).Property(x=>x.Title).IsModified = true;
				db.Entry(category).Property(x => x.Description).IsModified = true;
				db.Entry(category).Property(x => x.SeoDescription).IsModified = true;
				db.Entry(category).Property(x => x.SeoKeyWords).IsModified = true;
				db.Entry(category).Property(x => x.SeoTitle).IsModified = true;
				db.Entry(category).Property(x => x.Position).IsModified = true;
				db.Entry(category).Property(x => x.ModifiedBy).IsModified = true;
				db.Entry(category).Property(x => x.ModifiedDate).IsModified = true;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(category);
		}
		[HttpPost]
		public ActionResult Delete(int id)
		{
			var item = db.tb_Category.Find(id);
			if(item != null)
			{
				db.tb_Category.Remove(item);
				db.SaveChanges();
				return Json(new { success = true });
			}
			return Json(new { success = false });

		}
	}
}