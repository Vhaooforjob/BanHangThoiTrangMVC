using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangThoiTrangMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class NewsController : BaseController<News>
    {
        public ActionResult Index(string Searchtext, int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = db.News.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }


        /*public ActionResult Index(string Searchtext, int page = 1, int pagesize = 5)
        {
            var items = db.News.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {
               items = (IOrderedQueryable<News>)items.Where(x => x.Alias.Equals(Searchtext) || x.Title.Contains(Searchtext));
            }
            items = items.ToPagedList(page, pagesize);
            //Chỉnh cho STT tăng dần theo trang
            ViewBag.PageSize = pagesize;
            ViewBag.Page = page;
            return View(items);
        }*/

        public ActionResult Add()
        {
            return CustomAdd(new News());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model)
        {
            News p = new News(null, DateTime.Now, DateTime.Now, null);
            CommonAbstract c = (CommonAbstract)p.Clone();

            if (ModelState.IsValid)
            {
                model.CreateDate = c.CreateDate;
                model.ModifiedDate = c.ModifiedDate;
                model.CategoryId = 5;
                model.Alias = BanHangThoiTrangMVC.Models.Common.Filter.FilterChar(model.Title);
                db.News.Add(model);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }
        public ActionResult Edit(int Id)
        {
            //var item = db.News.Find(Id);
            //return View(item);
            var item = db.Set<News>().Find(Id);
            return CustomEdit(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model)
        {
            News p = new News(null, DateTime.Now, DateTime.Now, null);
            CommonAbstract c = (CommonAbstract)p.Clone();

            if (ModelState.IsValid)
            {
                db.News.Attach(model);
                model.CreateDate = c.CreateDate;
                model.ModifiedDate = c.ModifiedDate;
                model.Alias = BanHangThoiTrangMVC.Models.Common.Filter.FilterChar(model.Title);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return CustomDelete(id);
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsActive = item.IsActive });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            return CustomDeleteAll(ids, CustomDeleteAllAction);
        }
        private void CustomDeleteAllAction(int id)
        {
            var item = db.Set<News>().Find(id);

            if (item != null)
            {
                db.Set<News>().Remove(item);
            }
        }
    }
}