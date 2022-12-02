using System;
using DAL.EF;
using DAL.Repositories;
using DAL.Entities;
using Xunit;
using System.Linq;

namespace TrackingSystemTests.DAL_Tests
{
    public class JobStatusRepositoryTest
    {
        private DbHelper helper;
        private JobStatusRepository jobStatusRepository;

        [Fact]
        public void JobStatusRepository_GetAll_GetsAllItems()
        {
            using (helper = new DbHelper())
            {
                jobStatusRepository = new JobStatusRepository(helper.Db);

                var statuses = jobStatusRepository.GetAll();
                var expected = helper.Db.JobStatuses.Count();

                Assert.Equal(expected, statuses.Count());
                Assert.Equal(3, statuses.Count());
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void JobStatusRepository_GetById_ReturnsItem(int id)
        {
            using (helper = new DbHelper())
            {
                jobStatusRepository = new JobStatusRepository(helper.Db);
                var actual = jobStatusRepository.Get(id);
                var expected = helper.Db.JobStatuses.FirstOrDefault(x => x.Id == id);

                Assert.NotNull(actual);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Tasks, actual.Tasks);
            }
        }

        [Fact]
        public void JobStatusRepository_Add_AddsItem()
        {
            using (helper = new DbHelper())
            {
                jobStatusRepository = new JobStatusRepository(helper.Db);

                var expected = jobStatusRepository.GetAll().Count() + 1;

                JobStatus job = new JobStatus
                {
                    Id = 1001,
                    Name = "New Status"
                };
                jobStatusRepository.Add(job);
                helper.Db.SaveChanges();

                var actualJob = jobStatusRepository.Get(1001);
                Assert.NotNull(actualJob);
                Assert.Equal(expected, jobStatusRepository.GetAll().Count());
                Assert.Equal(1001, actualJob.Id);
                Assert.Equal("New Status", actualJob.Name);

            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void JobStatusRepository_Delete_DeletesItem(int id)
        {
            using (helper = new DbHelper())
            {
                jobStatusRepository = new JobStatusRepository(helper.Db);
                var expected = jobStatusRepository.GetAll().Count() - 1;

                jobStatusRepository.Delete(id);
                helper.Db.SaveChanges();

                var actual = jobStatusRepository.Get(id);
                Assert.Null(actual);
                Assert.Equal(expected, jobStatusRepository.GetAll().Count());
            }

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void JobStatusRepository_Update_UpdatesItem(int id)
        {
            using (helper = new DbHelper())
            {
                jobStatusRepository = new JobStatusRepository(helper.Db);
                var expected = jobStatusRepository.Get(id);
                expected.Name = "Updated name";
                jobStatusRepository.Update(expected);
                helper.Db.SaveChanges();

                var actual = jobStatusRepository.Get(id);
                Assert.NotNull(actual);
                Assert.Equal(expected.Name, actual.Name);
            }
        }

        [Fact]
        public void JobStatusRepository_Find_FindsCorrectItems()
        {
            using (helper = new DbHelper())
            {
                jobStatusRepository = new JobStatusRepository(helper.Db);

                var actual = jobStatusRepository.Find(x => x.Id != 1);

                Assert.Equal(2, actual.Count());
                foreach (var item in actual)
                {
                    Assert.NotEqual(1,item.Id);
                }
            }
        }

    }
}
