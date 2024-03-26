using AutoMapper;
using BanHangThoiTrangMVC.Models;
using BanHangThoiTrangMVC.Models.EF;
using BanHangThoiTrangMVC.Repositories.Interfaces;
using BanHangThoiTrangMVC.Services.Interfaces;
using System.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using BanHangThoiTrangMVC.Models.Payments;

namespace BanHangThoiTrangMVC.Services.Implement
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        
        public ShoppingCartService(IProductRepository productRepository, IMapper mapper, IOrderRepository orderRepository, IUnitOfWorkRepository unitOfWorkRepository)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
            this._orderRepository = orderRepository;
            this._unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<ShoppingCart> AddToCart(int id, int quantity, ShoppingCart cart)
        {
            if (cart == null)
            {
                cart = new ShoppingCart();
            }
            Product product = await _productRepository.FindAsync(id);

            ShoppingCartItem item = _mapper.Map<ShoppingCartItem>(product);
            item.Quantity = quantity;
            item.ProductImg = product.ProductImages.FirstOrDefault(x => x.IsDefault)?.Image;

            cart.AddToCart(item, quantity);
            return cart;
        }

        public async Task<(CartCode, Order)> Checkout(OrderViewModel req, ShoppingCart cart)
        {
            Order order = _mapper.Map<Order>(req);
            order.Status = 1;//chưa thanh toán / 2/đã thanh toán, 3/Hoàn thành, 4/hủy
            cart.Items.ForEach(x => order.OrderDetails.Add(new OrderDetail
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Price
            }));
            
            order.TotalAmount = cart.Items.Sum(x => (x.Price * x.Quantity));
            order.CreateDate = DateTime.Now;
            order.ModifiedDate = DateTime.Now;
            Random rd = new Random();
            order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
            _orderRepository.Add(order);
            await _unitOfWorkRepository.SaveChangesAsync();
            
            CartCode code = new CartCode { Success = true, Code = req.TypePayment, Url = "" };
            //var url = "";
            if (req.TypePayment == 2)
            {
                var url = await UrlPayment(req.TypePaymentVN, order.Code);
                code = new CartCode { Success = true, Code = req.TypePayment, Url = url };
            }

            return (code, order);
        }

        #region Thanh toán vnpay
        public async Task<string> UrlPayment(int TypePaymentVN, string orderCode)
        {
            var urlPayment = "";
            var order = await _orderRepository.FirstOrDefaultAsync(x => x.Code == orderCode);
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            var Price = (long)order.TotalAmount * 100;
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.CreateDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng :" + order.Code);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.Code); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return urlPayment;
        }
        #endregion
    }
}