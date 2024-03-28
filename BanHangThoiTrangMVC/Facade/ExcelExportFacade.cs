using BanHangThoiTrangMVC.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHangThoiTrangMVC.Facade
{
    public class ExcelExportFacade
    {
        private readonly IExcelExportService _excelExportService;

        public ExcelExportFacade(IExcelExportService excelExportService)
        {
            _excelExportService = excelExportService;
        }

        public void ExportOrdersToExcel(List<Order> orders)
        {
            _excelExportService.ExportToExcel(orders);
        }
    }
}