using BanHangThoiTrangMVC.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Facade
{
    public interface IExcelExportService
    {
        void ExportToExcel(List<Order> orders);
    }
}
