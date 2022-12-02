using System;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private MyContext context;
        public TaskRepository(MyContext mycontext)
        {
            context = mycontext;
        }
        public void Add(Task item)
        {
            context.Tasks.Add(item);
        }

        public void Delete(int id)
        {
            Task task = context.Tasks.Find(id);
            if (task != null)
            {
                context.Tasks.Remove(task);
            }
        }

        public IEnumerable<Task> Find(Func<Task, bool> predicate)
        {
            return context.Tasks.Include(x=>x.Category).Include(x=>x.JobStatus).Include(x=>x.User).Where(predicate).ToList();
        }

        public Task Get(int id)
        {
            return context.Tasks.Include(x => x.Category).Include(x => x.JobStatus).Include(x => x.User).FirstOrDefault(x=>x.Id==id);
        }

        public IEnumerable<Task> GetAll()
        {
            return context.Tasks.Include(x => x.Category).Include(x => x.JobStatus).Include(x => x.User).ToList();
        }

        public void Update(Task item)
        {

            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();

        }
    }
}
