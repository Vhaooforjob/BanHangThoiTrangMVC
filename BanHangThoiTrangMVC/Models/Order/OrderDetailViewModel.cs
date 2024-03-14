using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Models.EF;
using System;

public class OrderDetailViewModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreateDate { get; set; }

    //public virtual OrderViewModel Order { get; set; }
    //public virtual ProductViewModel Product { get; set; }
}