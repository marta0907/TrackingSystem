using System;
using System.Collections.Generic;
using BLL.DTO;
namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryDTO> GetCategories();
       
    }
}
