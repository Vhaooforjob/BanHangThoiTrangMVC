namespace BanHangThoiTrangMVC.Models
{
    public class CartCode
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public int Code { get; set; }
        public int Count { get; set; }
        public string Url { get; set; } = "";
    }
}