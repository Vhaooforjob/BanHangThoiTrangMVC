using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Models.EF;
using DocumentFormat.OpenXml.Wordprocessing;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangThoiTrangMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class VoucherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Voucher

        public ActionResult Index(int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Voucher> items = db.Voucher.OrderByDescending(x => x.Id);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        // GET: Admin/Voucher/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Voucher/Create
        public ActionResult Add()
        {
            return View();
        }

        // POST: Admin/Voucher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Voucher model, DateTime startDate, DateTime endDate)
        {
            if (ModelState.IsValid)
            {
                model.StartDate = startDate;
                model.EndDate = endDate;
                db.Voucher.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        // GET: Admin/Voucher/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Voucher/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Voucher/Delete/5
        public ActionResult Delete(int id)
        {
            var item = db.Voucher.Find(id);
            db.Voucher.Remove(item);
            db.SaveChanges();
            return Json(new { success = true });
        }

        // POST: Admin/Voucher/DeleteAll
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            try
            {
                if (!string.IsNullOrEmpty(ids))
                {
                    var idList = ids.Split(',').Select(int.Parse).ToList();
                    var itemsToDelete = db.Voucher.Where(v => idList.Contains(v.Id)).ToList();

                    db.Voucher.RemoveRange(itemsToDelete);
                    db.SaveChanges();

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "No voucher items selected for deletion." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting voucher items: " + ex.Message });
            }
        }

    }
}
