using System.Collections.Generic;

namespace DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Role Role { get; set; }
        public int RoleId { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<Task> Tasks { get; set; }
    }
}
