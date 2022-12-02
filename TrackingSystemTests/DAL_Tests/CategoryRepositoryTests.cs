using System;
using DAL.EF;
using DAL.Repositories;
using DAL.Entities;
using Xunit;
using System.Linq;
namespace TrackingSystemTests.DAL_Tests
{
    public class CategoryRepositoryTests
    {
        private DbHelper helper;
        private CategoryRepository categoryRepository;

        [Fact]
        public void CategoryRepository_GetAll_ReturnsAllCategories()
        {
            using(helper = new DbHelper())
            {
                categoryRepository = new CategoryRepository(helper.Db);
                var categories = categoryRepository.GetAll();

                Assert.Equal(5, categories.Count());
                Assert.Equal(helper.Db.Categories.Count(), categories.Count());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void CategoryRepository_GetById_ReturnsCategory(int id)
        {
            using (helper = new DbHelper())
            {
                categoryRepository = new CategoryRepository(helper.Db);
                var actual = categoryRepository.Get(id);
                var expected = helper.Db.Categories.FirstOrDefault(x => x.Id == id);

                Assert.NotNull(actual);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Tasks, actual.Tasks);
            }
        }

        [Fact]
        public void CategoryRepository_Add_AddsItem()
        {
            using (helper = new DbHelper())
            {
                categoryRepository = new CategoryRepository(helper.Db);
                var category = new Category
                {
                    Id=1001,
                    Name="PHP"
                };

                var expected = helper.Db.Categories.Count() + 1;
                categoryRepository.Add(category);
                helper.Db.SaveChanges();
                var actual = categoryRepository.GetAll().Count();
                var actualCat = categoryRepository.Get(1001);


                Assert.Equal(expected, actual);
                Assert.Equal(1001, actualCat.Id);
                Assert.Equal("PHP", actualCat.Name);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void CategoryRepository_Delete_DeletesItem(int id)
        {
            using (helper = new DbHelper())
            {
                categoryRepository = new CategoryRepository(helper.Db);

                var expected = helper.Db.Categories.Count() - 1;
                categoryRepository.Delete(id);
                helper.Db.SaveChanges();
                var actual = categoryRepository.GetAll().Count();

                Assert.Equal(expected, actual);
                Assert.Null(categoryRepository.Get(id));
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void CategoryRepository_Update_UpdatesItem(int id)
        {
            using (helper = new DbHelper())
            {
                categoryRepository = new CategoryRepository(helper.Db);
                var category = categoryRepository.Get(id);
                category.Name = "Updated Name";
                categoryRepository.Update(category);
                helper.Db.SaveChanges();

                var actual = categoryRepository.Get(id);

                Assert.Equal(id, actual.Id);
                Assert.Equal("Updated Name", actual.Name);

            }
        }
        [Fact]
        public void CategoryRepository_Find_FindsCorrectDAta()
        {
            using (helper = new DbHelper())
            {
                categoryRepository = new CategoryRepository(helper.Db);

                var categories = categoryRepository.Find(x => x.Name != "Java" && x.Id != 2);

                Assert.Equal(3, categories.Count());
                foreach(var item in categories)
                {
                    Assert.NotEqual("Java", item.Name);
                    Assert.NotEqual(2, item.Id);
                }
            }
        }
    }
    
}
