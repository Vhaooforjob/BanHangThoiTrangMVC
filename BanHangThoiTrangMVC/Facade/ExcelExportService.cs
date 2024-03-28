using BanHangThoiTrangMVC.Models.EF;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHangThoiTrangMVC.Facade
{
    public class ExcelExportService : IExcelExportService
    {
        public void ExportToExcel(List<Order> orders)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.AddWorksheet("Danh sách đơn hàng");

            // Thiết lập thuộc tính bảng
            worksheet.ColumnWidth = 15;
            var headerRange = worksheet.Range("A1", "J1");
            headerRange.Style.Font.Bold = true;

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
            foreach (var order in orders)
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

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=DanhSachDonHang.xlsx");
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var memoryStream = new System.IO.MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                memoryStream.Close();
            }

            HttpContext.Current.Response.End();
        }
    }
}