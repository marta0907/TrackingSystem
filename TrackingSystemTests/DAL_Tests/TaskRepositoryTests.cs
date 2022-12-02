using System.Linq;
using Xunit;
using TrackingSystemTests;
using DAL.Repositories;
using DAL.Entities;
using System;

namespace DAL_Tests
{
    public class TaskRepositoryTests
    {
        private  DbHelper helper;
        private static  TaskRepository taskRepository;

        [Fact]
        public void TaskRepository_Find_FindsCorrectData()
        {
            using(helper = new DbHelper())
            {
                taskRepository = new TaskRepository(helper.Db);
                var actualTasks = taskRepository.Find(x => x.Id != 1);

                Assert.Equal(2, actualTasks.Count());
                foreach (var task in actualTasks)
                {
                    Assert.NotEqual(1, task.Id);
                }
            }
          
        }
        [Fact]
        public void TaskRepository_GetAll_ShouldGetAllTasks()
        {
            using (helper = new DbHelper())
            {
                taskRepository = new TaskRepository(helper.Db);
                var tasks = taskRepository.GetAll();
                var expected = helper.Db.Tasks.Count();
                var actual = tasks.Count();

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TaskRepository_Get_ShouldGetTaskById(int id)
        {
            using (helper = new DbHelper())
            {
                taskRepository = new TaskRepository(helper.Db);
                var actual = taskRepository.Get(id);
                var expected = helper.Db.Tasks.FirstOrDefault(x => x.Id == id);

                Assert.NotNull(actual);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Deadline, actual.Deadline);
                Assert.Equal(expected.Description, actual.Description);
                Assert.Equal(expected.JobStatusId, actual.JobStatusId);
                Assert.Equal(expected.CategoryId, actual.CategoryId);
                Assert.Equal(expected.UserId, actual.UserId);
            }
        }

        [Fact]
        public void TaskRepository_Add_AddsValueToDatabase()
        {
            using (helper = new DbHelper())
            {
                taskRepository = new TaskRepository(helper.Db);
                var task = new Task
                {
                    Id = 1001,
                    Name = "Task1001",
                    Description = "Description 1001",
                    Deadline = new System.DateTime(2022, 09, 09),
                    JobStatusId = 3,
                    CategoryId = 5,
                    UserId = 1
                };
                var expected = taskRepository.GetAll().Count() + 1;
                taskRepository.Add(task);
                helper.Db.SaveChanges();

                var actualTask = taskRepository.Get(1001);

                Assert.Equal(expected, taskRepository.GetAll().Count());
                Assert.Equal("Task1001", actualTask.Name);
                Assert.Equal("Description 1001", actualTask.Description);
                Assert.Equal(new DateTime(2022, 09, 09), actualTask.Deadline);
                Assert.Equal(3, actualTask.JobStatusId);
                Assert.Equal(5, actualTask.CategoryId);
                Assert.Equal(1, actualTask.UserId);
            }

        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TaskRepository_DeleteById_ShouldDeleteTaskById(int id)
        {
            using (helper = new DbHelper())
            {
                taskRepository = new TaskRepository(helper.Db);
                var expected = taskRepository.GetAll().Count() - 1;
                taskRepository.Delete(id);
                helper.Db.SaveChanges();

                Assert.Equal(expected, taskRepository.GetAll().Count());
                Assert.DoesNotContain(taskRepository.GetAll(), x => x.Id == id);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TaskRepository_Update_UpdatesTask(int id)
        {
            using(helper = new DbHelper())
            {
                taskRepository = new TaskRepository(helper.Db);
                var task = taskRepository.Get(id);
                var date = DateTime.Now;
                
                task.Name = "Updated Name";
                task.Deadline = date;
                task.Description = "Updated Description";
                taskRepository.Update(task);
                helper.Db.SaveChanges();

                var actual = taskRepository.Get(id);
                Assert.Equal("Updated Name", actual.Name);
                Assert.Equal("Updated Description", actual.Description);
                Assert.Equal(date, actual.Deadline);
            }

        }
    }
}
