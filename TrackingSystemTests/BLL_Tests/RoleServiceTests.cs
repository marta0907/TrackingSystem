using System.Collections.Generic;
using BLL.Services;
using Xunit;
using DAL;
using BLL.DTO;
using DAL.Entities;
using System.Linq;
namespace TrackingSystemTests.BLL_Tests
{
    public class RoleServiceTests
    {
        private DbHelper helper;
        private RoleService roleService;

        [Fact]
        public void RoleService_GetRoles_GetsAllRoles()
        {
            using(helper = new DbHelper())
            {
                UnitOfWork unitOfWork = new UnitOfWork(helper.Db);
                roleService = new RoleService(unitOfWork);

                var roles = roleService.GetRoles();
                Assert.Equal(2, roles.Count());
                Assert.Contains(roles, x => x.Name == "RegisteredUser");
                Assert.Contains(roles, x => x.Name == "Admin");
            }
        }
    }
}
