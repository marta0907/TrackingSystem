using System.Linq;
using Xunit;
using TrackingSystemTests;
using DAL.Repositories;
using DAL.Entities;
using System;

namespace TrackingSystemTests.DAL_Tests
{
    public class RoleRepositoryTests
    {
        private DbHelper helper;
        private RoleRepository roleRepository;

        [Fact]
        public void RoleRepository_GetAll_GetsAllRoles()
        {
            using(helper = new DbHelper())
            {
                roleRepository = new RoleRepository(helper.Db);

                var roles = roleRepository.GetAll();

                Assert.Equal(2, roles.Count());
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void RoleRepository_Get_GetsItemById(int id)
        {
            using (helper = new DbHelper())
            {
                roleRepository = new RoleRepository(helper.Db);

                var actual = roleRepository.Get(id);
                var expected = helper.Db.Roles.FirstOrDefault(x => x.Id == id);

                Assert.NotNull(actual);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Users, actual.Users);
            }
        }
        [Fact]
        public void RoleRepositoey_Add_AddsItemToDb()
        {
            using (helper = new DbHelper())
            {
                roleRepository = new RoleRepository(helper.Db);

                var expected = helper.Db.Roles.Count() + 1;

                Role role = new Role
                {
                    Id = 1001,
                    Name = "New Role"
                };
                roleRepository.Add(role);
                helper.Db.SaveChanges();

                var actual = roleRepository.GetAll().Count();
                var actualRole = roleRepository.Get(1001);

                Assert.Equal(expected, actual);
                Assert.Equal(1001 ,actualRole.Id);
                Assert.Equal("New Role", actualRole.Name);
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void RoleRepository_Delete_DeletesRole(int id)
        {
            using (helper = new DbHelper())
            {
                roleRepository = new RoleRepository(helper.Db);

                var expected = helper.Db.Roles.Count() - 1;

                roleRepository.Delete(id);
                helper.Db.SaveChanges();

                var actual = roleRepository.GetAll().Count();
                var actualRole = roleRepository.Get(id);

                Assert.Equal(expected, actual);
                Assert.Null(actualRole);
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void RoleRepository_Update_UpdatesRole(int id)
        {
            using (helper = new DbHelper())
            {
                roleRepository = new RoleRepository(helper.Db);

                var role = roleRepository.Get(id);
                role.Name = "Updated Name";
                roleRepository.Update(role);
                helper.Db.SaveChanges();

                var actual = roleRepository.GetAll().Count();
                var actualRole = roleRepository.Get(id);

                Assert.Equal("Updated Name", actualRole.Name);
                Assert.NotNull(actualRole);
            }
        }

        [Fact]
        public void RoleRepository_Find_FindsRole()
        {
            using (helper = new DbHelper())
            {
                roleRepository = new RoleRepository(helper.Db);

                var roles = roleRepository.Find(x => x.Id != 1);

                Assert.Single(roles);

                foreach(var item in roles)
                {
                    Assert.NotEqual(1, item.Id);
                }
            }
        }
    }
}
