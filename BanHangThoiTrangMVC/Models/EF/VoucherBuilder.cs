using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHangThoiTrangMVC.Models.EF
{
    public class VoucherBuilder : IVoucherBuilder
    {
        private string _code;
        private int _value;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _quantity;

        public IVoucherBuilder SetCode(string code)
        {
            _code = code;
            return this;
        }

        public IVoucherBuilder SetValue(int value)
        {
            _value = value;
            return this;
        }

        public IVoucherBuilder SetStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        public IVoucherBuilder SetEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        public IVoucherBuilder SetQuantity(int quantity)
        {
            _quantity = quantity;
            return this;
        }

        public Voucher Build()
        {
            return new Voucher(_code, _value, _startDate, _endDate, _quantity, null, DateTime.Now, DateTime.Now, null);
        }
    }

}