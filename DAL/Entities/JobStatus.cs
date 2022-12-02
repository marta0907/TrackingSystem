using System.Collections.Generic;

namespace DAL.Entities
{
    public class JobStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Task> Tasks { get; set; }
    }
}
