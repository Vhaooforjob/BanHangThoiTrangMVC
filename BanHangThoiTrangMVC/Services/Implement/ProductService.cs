using AutoMapper;
using BanHangThoiTrangMVC.HelperModels.Paging;
using BanHangThoiTrangMVC.Models.EF;
using BanHangThoiTrangMVC.Repositories.Interfaces;
using BanHangThoiTrangMVC.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Services.Implement
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper) 
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ProductViewModel>> GetListProductAsync(PagingModel<ProductFilterModel> request)
            => await this._productRepository.PagingAsync(request, (items) => this._mapper.Map<List<ProductViewModel>>(items));

        public async Task<ProductViewModel> GetByIdAndCountAsync(int id)
            => _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAndCountAsync(id));
        
    }
}