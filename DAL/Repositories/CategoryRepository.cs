using System;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly MyContext myContext;
        public CategoryRepository(MyContext context)
        {
            myContext = context;
        }

        public void Add(Category item)
        {
            myContext.Categories.Add(item);
        }

        public void Delete(int id)
        {
            var item = Get(id);
            myContext.Categories.Remove(item);
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return myContext.Categories.Include(x=>x.Tasks).Where(predicate);
        }

        public Category Get(int id)
        {
            return myContext.Categories.Include(x => x.Tasks).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return myContext.Categories.Include(x => x.Tasks);
        }

        public void Update(Category item)
        {
            myContext.Entry(item).State = EntityState.Modified;
        }
    }
}
