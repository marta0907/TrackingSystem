using System.Collections.Generic;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Entities;
using BLL.DTO;
using AutoMapper;
namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _mapper = MapperInitializer.CreateMapperProfile();
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_unitOfWork.Categories.GetAll());
        }
    
    }
}
