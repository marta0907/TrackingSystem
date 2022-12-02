﻿using System.Collections.Generic;
namespace BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<int> TasksIds { get; set; }
    }
}
