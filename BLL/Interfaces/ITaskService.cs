using BLL.DTO;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface ITaskService:ICrud<TaskDTO>
    {
        public IEnumerable<TaskDTO> FindTasksByUserEmail(string email);
        public IEnumerable<TaskDTO> TasksToCheck();
    }
}
