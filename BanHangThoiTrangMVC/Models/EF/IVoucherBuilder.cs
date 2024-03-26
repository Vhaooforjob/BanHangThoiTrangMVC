using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Models.EF
{
    public interface IVoucherBuilder
    {
        IVoucherBuilder SetCode(string code);
        IVoucherBuilder SetValue(int value);
        IVoucherBuilder SetStartDate(DateTime startDate);
        IVoucherBuilder SetEndDate(DateTime endDate);
        IVoucherBuilder SetQuantity(int quantity);

        Voucher Build();
    }
}
