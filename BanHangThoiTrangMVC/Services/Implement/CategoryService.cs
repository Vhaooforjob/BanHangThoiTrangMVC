using AutoMapper;
using BanHangThoiTrangMVC.Models.Category;
using BanHangThoiTrangMVC.Repositories.Interfaces;
using BanHangThoiTrangMVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.Services.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public List<CategoryViewModel> GetCategoriesList()
            => _mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetCategoriesOrderByPosition());
    }
}