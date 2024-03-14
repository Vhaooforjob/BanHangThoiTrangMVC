using BanHangThoiTrangMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace BanHangThoiTrangMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BaseController<T> : Controller where T : CommonAbstract
    {   
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected virtual ActionResult CustomIndex()
        {
            var items = db.Set<T>().ToList();
            return View("Index", items);
        }

        protected virtual ActionResult CustomAdd(T model)
        {
            return View("Add", model);
        }

        protected virtual ActionResult CustomEdit(T model)
        {
            return View("Edit", model);
        }

        protected virtual ActionResult CustomDelete(int id)
        {
            var item = db.Set<T>().Find(id);
            if (item != null)
            {
                db.Set<T>().Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        protected virtual ActionResult CustomDeleteAll(string ids, Action<int> deleteAction)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemIds = ids.Split(',');

                if (itemIds != null && itemIds.Any())
                {
                    foreach (var itemId in itemIds)
                    {
                        var id = Convert.ToInt32(itemId);
                        deleteAction.Invoke(id);
                    }

                    db.SaveChanges();
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }
    }
}