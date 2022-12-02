using System.Collections.Generic;
using BLL.Services;
using Xunit;
using DAL;
using BLL.DTO;
using DAL.Entities;
using System.Linq;
namespace TrackingSystemTests.BLL_Tests
{
    public class UserServiceTests
    {
        private DbHelper helper;
        private UserService service;

        [Fact]
        public void UserServise_GetAll_GetsAll()
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unit = new UnitOfWork(helper.Db);
                service = new UserService(unit);

                var users = service.GetAll();

                Assert.Equal(5, users.Count());
                Assert.IsAssignableFrom<IEnumerable<UserDTO>>(users);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void UserService_GetById_GetsById(int id)
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unit = new UnitOfWork(helper.Db);
                service = new UserService(unit);

                var actual = service.GetById(id);
                var expected = helper.Mapper.Map<User, UserDTO>(unit.Users.Get(id));

                Assert.NotNull(actual);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Age, actual.Age);
                Assert.Equal(expected.Email, actual.Email);
                Assert.Equal(expected.Password, actual.Password);
                Assert.Equal(expected.RoleId, actual.RoleId);
                Assert.Equal(expected.TasksIds, actual.TasksIds);
                Assert.Equal(expected.Name, actual.Name);

            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void UserService_DeleteById_DeletesById(int id)
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unit = new UnitOfWork(helper.Db);
                service = new UserService(unit);

                service.DeleteById(id);
                var actual = service.GetById(id);

                Assert.Null(actual);
                Assert.Equal(4, service.GetAll().Count());

            }
        }

        [Fact]
        public void UserService_Add_AddsUser()
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unit = new UnitOfWork(helper.Db);
                service = new UserService(unit);

                var expected = new UserDTO
                {
                    Id = 1001,
                    Name = "User1001",
                    Age = 1001,
                    RoleId = 2,
                    Email = "oleg1001@gmail.com",
                    Password = "551231001"
                };
                service.Add(expected);

                var actual = service.GetById(1001);

                Assert.NotNull(actual);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Age, actual.Age);
                Assert.Equal(expected.Email, actual.Email);
                Assert.Equal(expected.Password, actual.Password);
                Assert.Equal(expected.RoleId, actual.RoleId);
                Assert.Equal(expected.Name, actual.Name);

            }
        }

        [Theory]
        [InlineData("oleg23@gmail.com", "55123")]
        [InlineData("martuha@gmail.com", "123")]
        [InlineData("new@gmail.com", "Qwerty123")]
        [InlineData("com@gmail.com", "Qwerty")]
        [InlineData("apple@gmail.com", "Qwer123")]
        public void UserService_FinfUserByEmail_FindsUser(string email, string password)
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unit = new UnitOfWork(helper.Db);
                service = new UserService(unit);

                var actual = service.FindUserByLoginAndPassword(email, password);

                Assert.NotNull(actual);
                Assert.Equal(email, actual.Email);
                Assert.Equal(password, actual.Password);

            }
        }

        [Theory]
        [InlineData("oleg23@gmail.com", "5512w3")]
        [InlineData("martuha@gmail.com", "1ww23")]
        [InlineData("new@gmail.com", "Qwertwwwy123")]
        [InlineData("com@gmail.com", "Qwewwrty")]
        [InlineData("apple@gmail.com", "Qwwer123")]
        public void UserService_FinfUserByEmail_DoesNotFindUser(string email, string password)
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unit = new UnitOfWork(helper.Db);
                service = new UserService(unit);

                var actual = service.FindUserByLoginAndPassword(email, password);

                Assert.Null(actual);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void UserService_Update_UpdatesUser(int id)
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unit = new UnitOfWork(helper.Db);
                service = new UserService(unit);

                var actual = service.GetById(id);
                actual.Password = "1001Q";
                actual.Age += 1;
                service.Update(actual);

                var expected = service.GetById(id);

                Assert.NotNull(expected);
                Assert.Equal(expected.Age, actual.Age);
                Assert.Equal(expected.Password, actual.Password);
            }
        }
    }
}
