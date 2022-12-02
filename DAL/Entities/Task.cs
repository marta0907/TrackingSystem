using System;

namespace DAL.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Percentage { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; } 
       
        public int Mark { get; set; } = 0;

        public int JobStatusId { get; set; }
        public JobStatus JobStatus { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
