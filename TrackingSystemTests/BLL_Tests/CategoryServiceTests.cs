using System.Collections.Generic;
using BLL.Services;
using Xunit;
using DAL;
using BLL.DTO;
using DAL.Entities;
using System.Linq;
namespace TrackingSystemTests.BLL_Tests
{
    public class CategoryServiceTests
    {
        private DbHelper helper;
        private CategoryService categoryService;

        [Fact]
        public void RoleService_GetRoles_GetsAllRoles()
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unitOfWork = new UnitOfWork(helper.Db);
                categoryService = new CategoryService(unitOfWork);

                var categories = categoryService.GetCategories();
                Assert.Equal(5, categories.Count());
                Assert.Contains(categories, x => x.Name == "CPlusPlus");
                Assert.Contains(categories, x => x.Name == "CSharp");
                Assert.Contains(categories, x => x.Name == "Java");
                Assert.Contains(categories, x => x.Name == "JavaScript");
                Assert.Contains(categories, x => x.Name == "Python");
            }
        }
    }
}
