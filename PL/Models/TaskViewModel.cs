using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PL.Models
{
    public class TaskViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int User { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
