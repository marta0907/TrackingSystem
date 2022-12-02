using System;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.EF;
namespace DAL
{
    public class UnitOfWork:IUnitOfWork
    {
        private CategoryRepository categoryRepository;
        private JobStatusRepository jobStatusRepository;
        private RoleRepository roleRepository;
        private TaskRepository taskRepository;
        private UserRepository userRepository;
        private readonly MyContext myContext;
        public UnitOfWork(MyContext MyContext)
        {
            myContext = MyContext;
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(myContext);
                return categoryRepository;
            }
        }

        public IRepository<JobStatus> JobStatuses
        {
            get
            {
                if (jobStatusRepository == null)
                    jobStatusRepository = new JobStatusRepository(myContext);
                return jobStatusRepository;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(myContext);
                return roleRepository;

            }
        }

        public IRepository<Task> Tasks
        {
            get
            {
                if (taskRepository == null)
                    taskRepository = new TaskRepository(myContext);
                return taskRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(myContext);
                return userRepository;
            }
        }

        public void Save()
        {
            myContext.SaveChanges();
        }
    }
}
