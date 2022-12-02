using DAL.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<JobStatus> JobStatuses { get; set; }

        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }
        public MyContext() {
           
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=Tracking_System;Trusted_Connection=False;User ID=sa ; Password=reallyStrongPwd123");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name ="Admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "RegisteredUser"
                }
                );

            modelBuilder.Entity<JobStatus>().HasData(
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
            modelBuilder.Entity<Category>().HasData(
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
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Age = 45,
                    RoleId = 1,
                    Email="zheplinska@gmail.com",
                    Password="Qwerty123"
                }
            );

        }
    }
}
