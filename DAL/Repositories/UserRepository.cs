using System;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories
{
    public class UserRepository:IRepository<User>
    {
        private readonly MyContext myContext;
        public UserRepository(MyContext context)
        {
            myContext = context;
        }

        public void Add(User item)
        {
            myContext.Users.Add(item);
        }

        public void Delete(int id)
        {
            var user = myContext.Users.FirstOrDefault(x => x.Id == id);
            myContext.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
             return myContext.Users.Include(x=>x.Role).Include(x=>x.Tasks).Where(predicate);
        }

        public User Get(int id)
        {
            return myContext.Users.Include(x => x.Role).Include(x => x.Tasks).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return myContext.Users.Include(x => x.Role).Include(x => x.Tasks).AsEnumerable();
        }

        public void Update(User item)
        {
            myContext.Entry(item).State = EntityState.Modified;
        }
    }
}
