using System;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class JobStatusRepository: IRepository<JobStatus>
    {
        private readonly MyContext myContext;
        public JobStatusRepository(MyContext context)
        {
            myContext = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(JobStatus item)
        {
            myContext.JobStatuses.Add(item);
        }

        public void Delete(int id)
        {
            var status = myContext.JobStatuses.FirstOrDefault(x => x.Id == id);
            myContext.JobStatuses.Remove(status);
        }

        public IEnumerable<JobStatus> Find(Func<JobStatus, bool> predicate)
        {
            return myContext.JobStatuses.Include(x => x.Tasks).Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobStatus Get(int id)
        {
            return myContext.JobStatuses.Include(x => x.Tasks).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<JobStatus> GetAll()
        {
            return myContext.JobStatuses.Include(x => x.Tasks);
        }

        public void Update(JobStatus item)
        {
            myContext.Entry(item).State = EntityState.Modified;
        }
    }
}
