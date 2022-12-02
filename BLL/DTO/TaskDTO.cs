using System;
namespace BLL.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Percentage { get; set; }
        public int JobStatusId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public int Mark { get; set; }
    }
}
