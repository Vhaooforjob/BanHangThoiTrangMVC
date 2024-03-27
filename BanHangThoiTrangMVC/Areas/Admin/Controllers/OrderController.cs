using BanHangThoiTrangMVC.Models;
using ClosedXML.Excel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangThoiTrangMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
            var items = db.Orders.OrderByDescending(x => x.CreateDate).ToList();
            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ViewOrder(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }

        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id,int trangthai)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.Status = trangthai;
                db.Entry(item).Property(x => x.Status).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "UnSuccess", Success = false });
        }

        public ActionResult ExportFileExcel()
        {
            // Tạo file exel 
            var workbook = new XLWorkbook();

            // Đặt tên worksheet 
            var worksheet = workbook.AddWorksheet("Danh sách đơn hàng");

            // Thiết lập thuộc tính bảng
            worksheet.ColumnWidth = 15;
            var headerRange = worksheet.Range("A1", "J1");
            headerRange.Style.Font.Bold = true;

            // Lấy danh sách bảng Orders
            var tbOrder = db.Orders.ToList();

            // Tên cột
            worksheet.Cell(1, 1).Value = "STT";
            worksheet.Cell(1, 2).Value = "Mã đơn hàng";
            worksheet.Cell(1, 3).Value = "Tên khách hàng";
            worksheet.Cell(1, 4).Value = "Số điện thoại";
            worksheet.Cell(1, 5).Value = "Địa chỉ";
            worksheet.Cell(1, 6).Value = "Thành tiền";
            worksheet.Cell(1, 7).Value = "Ngày tạo";
            worksheet.Cell(1, 8).Value = "Phương thức thanh toán";
            worksheet.Cell(1, 9).Value = "Email";
            worksheet.Cell(1, 10).Value = "Trạng thái";

            // Thêm dữ liệu
            int row = 2;
            foreach (var order in tbOrder)
            {
                worksheet.Cell(row, 1).Value = order.Id;
                worksheet.Cell(row, 2).Value = order.Code;
                worksheet.Cell(row, 3).Value = order.CustomerName;
                worksheet.Cell(row, 4).Value = order.Phone;
                worksheet.Cell(row, 5).Value = order.Address;
                worksheet.Cell(row, 6).Value = order.TotalAmount;
                worksheet.Cell(row, 7).Value = order.CreateDate;
                worksheet.Cell(row, 8).Value = order.TypePayment;
                worksheet.Cell(row, 9).Value = order.Email;
                worksheet.Cell(row, 10).Value = order.Status;

                row++;
            }

            
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=DanhSachDonHang.xlsx");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            
            using (var memoryStream = new System.IO.MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                memoryStream.Close();
            }

            Response.End();


            return View();
        }
    }
}