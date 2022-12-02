using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PL.Models;
using BLL.Interfaces;
using BLL.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public AccountController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            if (User.Claims.Count() > 0)
            {
                if (User.IsInRole("1"))
                    return RedirectToAction("Index", "Admin");
                else if (User.IsInRole("2"))
                    return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userService.FindUserByLoginAndPassword(model.Email, model.Password);
                    if (user != null)
                    {
                        Authenticate(user);
                        if (user.RoleId == 2)
                            return RedirectToAction("Index", "User");
                        else
                            return RedirectToAction("Index", "Admin");
                    }
                }
                catch
                {
                    ViewBag.Message = "Something was wrong";
                    return View();
                }
                
            }
            ViewBag.Message = "Not correct data";

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userService.GetAll().FirstOrDefault(x => x.Email == model.Email);
                    if (user == null)
                    {
                        UserDTO userDTO = new UserDTO
                        {
                            Name = model.Name,
                            Age = model.Age,
                            Email = model.Email,
                            Password = model.Password,
                            RoleId = 2
                        };
                        _userService.Add(userDTO);
                        _emailService.SendMessageAboutSuccessfulRegistration(userDTO);
                    }
                    ViewBag.Message = "You are successfully registered";
                    return RedirectToAction("Login", "Account");
                }
                catch
                {
                    ViewBag.Message = "Something was wrong";
                }
            }else ViewBag.Message = "Incorrect data";

            return View(model);
        }


        private void Authenticate(UserDTO user)
        {
            
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie");
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}