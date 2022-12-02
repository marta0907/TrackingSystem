using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLL.Interfaces;
using BLL.DTO;

namespace PL.Controllers
{
    [Authorize(Roles ="2")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        public UserController(IUserService userService, ITaskService taskService)
        {
            _userService = userService;
            _taskService = taskService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
                var tasks = _taskService.FindTasksByUserEmail(email);
                ViewBag.Title = "";
                if (tasks.Count() == 0)
                {
                    tasks = new List<TaskDTO>() { };

                }
                return View(tasks);
            }
            catch
            {
                return View();
            }
        }
        

        public IActionResult Task(int? id)
        {
            if (id != null)
            {
                try
                {
                    var task = _taskService.GetById((int)id);
                    return View(task);
                }
                catch           
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }


            [HttpPost]
        public IActionResult Answer(TaskDTO task)
        {
            try
            {
                //var taskDTO = _taskService.GetById(task.Id);
                task.Answer = task.Answer;
                task.JobStatusId = 1;
                _taskService.Update(task);
                ViewBag.Message = "Your answer was saved";
                return RedirectToAction("Task", task.Id);
            }
            catch(Exception e)
            {
               Console.Write(e.Message);
            }
            return RedirectToAction("Index");

        }
    }
}
