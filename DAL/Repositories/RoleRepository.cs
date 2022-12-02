using System;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class RoleRepository: IRepository<Role>
    {
        private readonly MyContext myContext;
        public RoleRepository(MyContext context)
        {
            myContext = context;
        }

        public void Add(Role item)
        {
            myContext.Roles.Add(item);
        }

        public void Delete(int id)
        {
            var item = myContext.Roles.FirstOrDefault(x => x.Id == id);
            myContext.Roles.Remove(item);
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return myContext.Roles.Include(x=>x.Users).Where(predicate);
        }

        public Role Get(int id)
        {
            return myContext.Roles.Include(x => x.Users).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Role> GetAll()
        {
            return myContext.Roles.Include(x => x.Users);
        }

        public void Update(Role item)
        {
            myContext.Entry(item).State = EntityState.Modified;
        }
    }
}
