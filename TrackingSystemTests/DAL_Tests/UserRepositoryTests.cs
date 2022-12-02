using System.Linq;
using Xunit;
using TrackingSystemTests;
using DAL.Repositories;
using DAL.Entities;
using System;

namespace TrackingSystemTests.DAL_Tests
{
    public class UserRepositoryTests
    {
        private DbHelper helper;
        private UserRepository userRepository;

        [Fact]
        public void UserRepository_GetAll_ShouldGetAllUsers()
        {
            using(helper = new DbHelper())
            {
                userRepository = new UserRepository(helper.Db);

                var actual = userRepository.GetAll();
                var expected = helper.Db.Users;

                Assert.Equal(expected.Count(), actual.Count());
                Assert.Equal(5, actual.Count());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void UserRepository_GetById_ShouldGetUserById(int id)
        {
            using (helper = new DbHelper())
            {
                userRepository = new UserRepository(helper.Db);

                var actual = userRepository.Get(id);
                var expected = helper.Db.Users.FirstOrDefault(x=>x.Id ==id);

                Assert.NotNull(actual);
                Assert.Equal(expected, actual);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Age, actual.Age);
                Assert.Equal(expected.RoleId, actual.RoleId);
                Assert.Equal(expected.Email, actual.Email);
                Assert.Equal(expected.Password, actual.Password);
            }
        }

        [Fact]
        public void UserRepository_Find_ShouldFindUserWithIdNot1()
        {
            using (helper = new DbHelper())
            {
                userRepository = new UserRepository(helper.Db);

                var actual = userRepository.Find(x => x.Id != 1);

                Assert.NotNull(actual);
                Assert.Equal(4, actual.Count());

                foreach(var item in actual)
                {
                    Assert.NotEqual(1, item.Id);
                }

            }
        }


        [Fact]
        public void UserRepository_Find_ShouldFindUserOlderThan25()
        {
            using (helper = new DbHelper())
            {
                userRepository = new UserRepository(helper.Db);

                var actual = userRepository.Find(x => x.Age >25);

                Assert.NotNull(actual);
                Assert.Equal(3, actual.Count());

                foreach (var item in actual)
                {
                    Assert.True(item.Age > 25);
                }
            }
        }

        [Fact]
        public void UserRepository_Add_ShouldAddUser()
        {
            using (helper = new DbHelper())
            {
                userRepository = new UserRepository(helper.Db);

                var user = new User
                {
                    Id = 1001,
                    Name = "User1001",
                    Age = 35,
                    RoleId = 1,
                    Email = "1001@gmail.com",
                    Password = "1df23"
                };
                userRepository.Add(user);
                helper.Db.SaveChanges();

                
                Assert.Equal(6, userRepository.GetAll().Count());

                var actual = userRepository.Get(1001);

                Assert.Equal(user, actual);
               
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void UserRepository_Delete_ShouldDeleteUser(int id)
        {
            using (helper = new DbHelper())
            {
                userRepository = new UserRepository(helper.Db);

                userRepository.Delete(id);
                helper.Db.SaveChanges();
                var user = userRepository.Get(id);

                Assert.Equal(4, userRepository.GetAll().Count());
                Assert.Null(user);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void UserRepository_Update_ShouldUpdateUser(int id)
        {
            using (helper = new DbHelper())
            {
                userRepository = new UserRepository(helper.Db);

                var user = userRepository.Get(id);
                user.Name = "Updated name";
                user.Age = 90;
                user.Email = "updatedemail@gmail.com";
                user.Password = "updatedpwd";

                userRepository.Update(user);
                helper.Db.SaveChanges();

                var actual = userRepository.Get(id);

                Assert.Equal("Updated name", actual.Name);
                Assert.Equal(90,actual.Age);
                Assert.Equal("updatedemail@gmail.com",actual.Email);
                Assert.Equal("updatedpwd",actual.Password);
                Assert.Equal(5,userRepository.GetAll().Count());
            }
        }
    }
}
