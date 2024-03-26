using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangThoiTrangMVC.Models.EF
{
    [Table("Vouchers")]
    public class Voucher : CommonAbstract
    {
        public Voucher()
        {
        }

        public Voucher(string code, int value, DateTime startDate, DateTime endDate, int quantity)
        {
            this.Code = code;
            this.Value = value;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Quantity = quantity;
        }
        public Voucher(string code, int value, DateTime startDate, DateTime endDate, int quantity, string createBy, DateTime createDate, DateTime modifiedDate, string modifiedBy)
           : base(createBy, createDate, modifiedDate, modifiedBy)
        {
            this.Code = code;
            this.Value = value;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Quantity = quantity;
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[Required(ErrorMessage = "Tên danh mục không để trống")]
        [StringLength(150)]
        public string Code { get; set; }
        public int Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Quantity { get; set; }
        public override CommonAbstract Clone()
        {
            return (Voucher)MemberwiseClone();
        }
    }
}