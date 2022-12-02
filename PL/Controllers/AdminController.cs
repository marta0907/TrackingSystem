using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PL.Models;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PL.Controllers
{
    [Authorize(Roles = "1")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IEmailService _emailService;
        private readonly IRoleService _roleService;
        private readonly ITaskService _taskService;

        public AdminController( IUserService userService,
                                IEmailService emailService,
                                ICategoryService categoryService,
                                IRoleService roleServece,
                                ITaskService taskService
                               )
        {

            _userService = userService;
            _emailService = emailService;
            _categoryService = categoryService;
            _roleService = roleServece;
            _taskService = taskService;
        } 

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var users = _userService.GetAll();
                if (users != null)
                    return View(users);
            }
            catch
            {
                ViewBag.Message = "Something was wrong";
            }
            return View();
        }




        [HttpGet]
        public IActionResult CreateNewTask()
        {
            SelectList users = new SelectList(_userService.GetAll().Where(x => x.RoleId != 1),"Id","Email");
            ViewBag.Users = users;
            ViewBag.Categories = new SelectList(_categoryService.GetCategories(),"Id","Name");
            return View("Task");
        }

        [HttpPost]
        public IActionResult CreateNewTask(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TaskDTO taskDTO = new TaskDTO()
                    {
                        Name = task.Name,
                        Description = task.Description,
                        Deadline = task.Deadline,
                        UserId = task.User,
                        CategoryId = task.CategoryId,
                        Percentage = 0.0f,
                        JobStatusId = 2
                    };
                    _taskService.Add(taskDTO);
                    var user = _userService.GetAll().FirstOrDefault(x => x.Id == task.User);
                    _emailService.SendMessageAboutNewTask(user);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "Something was wrong";
                }
            }
            return View("Task",task);
        }

        public IActionResult Edit(int? id)
        {
            if(id != null)
            {
                try
                {
                    var user = _userService.GetAll().FirstOrDefault(x => x.Id == id);
                    ViewBag.Roles = new SelectList(_roleService.GetRoles(), "Id", "Name");
                    return View(user);
                }
                catch
                {
                    ViewBag.Message = "Something was wrong";
                }
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult Edit(UserDTO user)
        {
            try
            {
                _userService.Update(user);
                ViewBag.Message = "User was successfully managed";
            }
            catch
            {
                ViewBag.Message = "Something was wrong";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult TasksToCheck()
        {
            var tasks = _taskService.TasksToCheck();
            return View(tasks);
        }
        [HttpGet]
        public IActionResult CheckTask(int? id)
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
                    ViewBag.Message = "Something was wrong";
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult CheckTask(TaskDTO taskDTO)
        {
            try
            {
                taskDTO.JobStatusId = 3;
                _taskService.Update(taskDTO);
                ViewBag.Message = "Task is checked";
            }
            catch
            {
                ViewBag.Message = "Something was wrong";
            }
            return RedirectToAction("Index");
        }
    }
} 
