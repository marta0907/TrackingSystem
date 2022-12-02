using System.Collections.Generic;
using BLL.Services;
using Xunit;
using DAL;
using BLL.DTO;
using DAL.Entities;
using System.Linq;
namespace TrackingSystemTests.BLL_Tests
{
    public class TaskServiceTests
    {
        private DbHelper helper;
        private TaskService taskService;

        [Fact]
        public void TaskServise_GetAll_GetsAllTasks()
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unitOfWork = new UnitOfWork(helper.Db);
                taskService = new TaskService(unitOfWork);

                var actual = taskService.GetAll();
                var expected = helper.Mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(unitOfWork.Tasks.GetAll());

                Assert.IsAssignableFrom<IEnumerable<TaskDTO>>(actual);
                Assert.Equal(expected.Count(), actual.Count());
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TaskService_GetById_GetsTaskById(int id)
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unitOfWork = new UnitOfWork(helper.Db);
                taskService = new TaskService(unitOfWork);

                var actual = taskService.GetById(id);
                var expected = helper.Mapper.Map<Task, TaskDTO>(unitOfWork.Tasks.Get(id));

                Assert.IsType<TaskDTO>(actual);
                Assert.Equal(id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.UserId, actual.UserId);
                Assert.Equal(expected.Deadline, actual.Deadline);
                Assert.Equal(expected.Mark, actual.Mark);
                Assert.Equal(expected.Answer, actual.Answer);
                Assert.Equal(expected.Description, actual.Description);
            }
        }
        [Fact]
        public void TaskService_Add_AddsNewTask()
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unitOfWork = new UnitOfWork(helper.Db);
                taskService = new TaskService(unitOfWork);

                var expected = new TaskDTO
                {
                    Id = 1001,
                    Name = "New Name",
                    Description = "New Description",
                    Deadline = new System.DateTime(2021, 09, 09),
                    Answer = "Answer",
                    Mark = 21,
                    UserId = 1,
                    JobStatusId = 1,
                    CategoryId = 1
                };

                taskService.Add(expected);
                var actual = taskService.GetById(1001);

                Assert.Equal(4, taskService.GetAll().Count());
                Assert.Equal(1001, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.UserId, actual.UserId);
                Assert.Equal(expected.Deadline, actual.Deadline);
                Assert.Equal(expected.Mark, actual.Mark);
                Assert.Equal(expected.Answer, actual.Answer);
                Assert.Equal(expected.Description, actual.Description);

            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TaskService_Update_UpdatesTaskById(int id)
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unitOfWork = new UnitOfWork(helper.Db);
                taskService = new TaskService(unitOfWork);

                var expected = taskService.GetById(id);
                expected.Answer = "Update answer";
                expected.Name = "Update name";
                expected.Mark = 99;
                expected.Deadline = System.DateTime.Today;

                taskService.Update(expected);

                var actual = taskService.GetById(id);

                Assert.Equal(id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.UserId, actual.UserId);
                Assert.Equal(expected.Deadline, actual.Deadline);
                Assert.Equal(expected.Mark, actual.Mark);
                Assert.Equal(expected.Answer, actual.Answer);
                Assert.Equal(expected.Description, actual.Description);
            }
        }

        [Fact]
        public void TaskService_Add_AddsTask()
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unitOfWork = new UnitOfWork(helper.Db);
                taskService = new TaskService(unitOfWork);

                var expected = new TaskDTO
                {
                    Id = 1001,
                    Name = "Task5",
                    Description = "Description 5",
                    Deadline = new System.DateTime(2021, 10, 08),
                    JobStatusId = 1,
                    CategoryId = 1,
                    UserId = 2,
                    Answer = "answer5",
                    Mark = 55
                };

                taskService.Add(expected);
                var actual = taskService.GetById(1001);

                Assert.Equal(4, taskService.GetAll().Count());
                Assert.Equal(1001, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.UserId, actual.UserId);
                Assert.Equal(expected.Deadline, actual.Deadline);
                Assert.Equal(expected.Mark, actual.Mark);
                Assert.Equal(expected.Answer, actual.Answer);
                Assert.Equal(expected.Description, actual.Description);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TaskService_Delete_DeletesTaskById(int id)
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unitOfWork = new UnitOfWork(helper.Db);
                taskService = new TaskService(unitOfWork);

                taskService.DeleteById(id);
                var actual = taskService.GetById(id);

                Assert.Null(actual);
                Assert.Equal(2, taskService.GetAll().Count());
               
            }
        }

        [Theory]
        [InlineData("martuha@gmail.com")]
        [InlineData("new@gmail.com")]
        public void TaskServise_FindTasksByUserEmail_FindsCorrectTasks(string email)
        {

            using (helper = new DbHelper())
            {
                UnitOfWork unitOfWork = new UnitOfWork(helper.Db);
                taskService = new TaskService(unitOfWork);

                var tasks = taskService.FindTasksByUserEmail(email);
                var user = helper.Db.Users.FirstOrDefault(x => x.Email == email).Id;
                foreach (var item in tasks)
                {
                    Assert.Equal(user, item.UserId);
                }
            }
        }

        [Fact]
        public void TaskService_TasksToCheck_GetsTasksToCheck()
        {
            using (helper = new DbHelper())
            {
                UnitOfWork unitOfWork = new UnitOfWork(helper.Db);
                taskService = new TaskService(unitOfWork);

                var tasks = taskService.TasksToCheck();
                foreach (var item in tasks)
                {
                    Assert.Equal(1, item.JobStatusId);
                }
            }
        }

    }
}
