using System.Collections.Generic;
namespace BLL.DTO
{
    public class JobStatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> TasksIds { get; set; }
    }
}
