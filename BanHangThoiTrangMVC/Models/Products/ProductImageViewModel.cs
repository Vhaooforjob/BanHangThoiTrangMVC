using BanHangThoiTrangMVC.Models.EF;

public class ProductImageViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Image { get; set; }
    public bool IsDefault { get; set; }

    //public virtual ProductViewModel Product { get; set; }
}