using System.Collections.Generic;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Entities;
using AutoMapper;
using BLL.Validation;
using System.Linq;

namespace BLL.Services
{
    public class TaskService :ITaskService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public TaskService( IUnitOfWork unitOfWork)
        {
            _mapper = MapperInitializer.CreateMapperProfile();
            _unitOfWork = unitOfWork;
        }

        public void Add(TaskDTO model)
        {
            var task = _mapper.Map<TaskDTO,Task>(model);
            _unitOfWork.Tasks.Add(task);
            _unitOfWork.Save();
        }

        public void DeleteById(int modelId)
        {
            _unitOfWork.Tasks.Delete(modelId);
            _unitOfWork.Save();
        }


        public IEnumerable<TaskDTO> GetAll()
        {
            var tasks = _unitOfWork.Tasks.GetAll();
            return _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(tasks);
        }

        public TaskDTO GetById(int id)
        {
            var task = _unitOfWork.Tasks.Get(id);
            return _mapper.Map<Task, TaskDTO>(task);
        }

        public void Update(TaskDTO model)
        {
            var task = _unitOfWork.Tasks.Get(model.Id);
            task.Name = model.Name;
            task.Description = model.Description;
            task.Deadline = model.Deadline;
            task.Answer = model.Answer;
            task.CategoryId = model.CategoryId;
            task.JobStatusId = model.JobStatusId;
            task.UserId = model.UserId;
            task.Percentage = model.Percentage;
            task.Mark = model.Mark;
            _unitOfWork.Tasks.Update(task);
            _unitOfWork.Save();
        }

        public IEnumerable<TaskDTO> FindTasksByUserEmail(string email)
        {
            var tasks = _unitOfWork.Tasks.Find(x => x.User.Email == email);
           
            return _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(tasks);
        }

        public IEnumerable<TaskDTO> TasksToCheck()
        {
            var tasks = _unitOfWork.Tasks.Find(x => x.JobStatusId == 1 );
            return _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(tasks);
        }
    }
}
