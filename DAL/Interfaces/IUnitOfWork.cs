using System;
using DAL.Entities;
namespace DAL.Interfaces
{
    public interface IUnitOfWork 
    {
        IRepository<Category> Categories { get; }
        IRepository<JobStatus> JobStatuses { get; }
        IRepository<Role> Roles { get; }
        IRepository<Task> Tasks { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
