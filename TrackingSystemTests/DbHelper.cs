using DAL.Entities;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;
using BLL;
namespace TrackingSystemTests
{
    public class DbHelper : IDisposable
    {
        public MyContext Db { get; private set; }
        public Mapper Mapper { get; set; }
        public DbHelper()
        {
            var options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            Db = new MyContext(options);
            SeedData();
            Mapper = MapperInitializer.CreateMapperProfile();
        }


        private void SeedData()
        {
            Db.Roles.AddRange(
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "RegisteredUser"
                }
                );
            Db.JobStatuses.AddRange(
                new JobStatus
                {
                    Id = 1,
                    Name = "Started"
                },
                new JobStatus
                {
                    Id = 2,
                    Name = "NotStarted"
                }, new JobStatus
                {
                    Id = 3,
                    Name = "Completed"
                }
                );
            Db.Categories.AddRange(
                new Category
                {
                    Id = 1,
                    Name = "CPlusPlus"
                }, new Category
                {
                    Id = 2,
                    Name = "CSharp"
                }, new Category
                {
                    Id = 3,
                    Name = "Java"
                }, new Category
                {
                    Id = 4,
                    Name = "JavaScript"
                }, new Category
                {
                    Id = 5,
                    Name = "Python"
                }
                );
            Db.Users.AddRange(
                new User
                {
                    Id = 5,
                    Name = "User5",
                    Age = 45,
                    RoleId = 2,
                    Email = "oleg23@gmail.com",
                    Password = "55123"
                },
                new User
                {
                    Id = 1,
                    Name = "User1",
                    Age = 45,
                    RoleId = 2,
                    Email = "martuha@gmail.com",
                    Password = "123"
                }, new User
                {
                    Id = 2,
                    Name = "User2",
                    Age = 25,
                    RoleId = 1,
                    Email = "new@gmail.com",
                    Password = "Qwerty123"
                }, new User
                {
                    Id = 3,
                    Name = "User3",
                    Age = 18,
                    RoleId = 2,
                    Email = "com@gmail.com",
                    Password = "Qwerty"
                }, new User
                {
                    Id = 4,
                    Name = "User4",
                    Age = 36,
                    RoleId = 1,
                    Email = "apple@gmail.com",
                    Password = "Qwer123"
                }
                );
            Db.Tasks.AddRange(
                new Task{
                    Id = 1,
                    Name="Task1",
                    Description ="Description 1",
                    Deadline = new System.DateTime(2021,09,09),
                    JobStatusId = 1,
                    CategoryId =1,
                    UserId=1,
                    Answer = "answer1",
                    Mark = 19

                },
                 new Task
                 {
                     Id = 2,
                     Name = "Task2",
                     Description = "Description 2",
                     Deadline = new System.DateTime(2021, 09, 19),
                     JobStatusId = 2,
                     CategoryId = 2,
                     UserId = 2,
                     Answer = "answer2",
                     Mark = 9

                 },
                  new Task
                  {
                      Id = 3,
                      Name = "Task3",
                      Description = "Description 3",
                      Deadline = new System.DateTime(2021, 09, 08),
                      JobStatusId = 3,
                      CategoryId = 3,
                      UserId = 1,
                      Answer = "answer4",
                      Mark = 98
                  });
            Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
