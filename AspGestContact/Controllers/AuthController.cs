using AspGestContact.Mappers;
using AspGestContact.Models;
using AspGestContact.Models.Forms;
using AspGestContact.Sessions;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspGestContact.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserClientRepo _userService;

        public AuthController(IUserClientRepo service)
        {
            _userService = service;
        }
        public IActionResult Index()
        {            
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            ViewBag.Title = "Login Page";
            ViewData["Year"] = DateTime.Now.Year;
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginForm form)
        {
            ViewBag.Title = "Login Page";
            ViewData["Year"] = DateTime.Now.Year;
            
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            UserAsp user = _userService.Login(form.Email, form.Passwd).ClientToAsp();
            if(user is null)
            {
                return View(form);
            }
            HttpContext.Session.SetUser(user);
            return RedirectToAction("Index", "Contact");
        }

        public IActionResult Register()
        {
            ViewBag.Title = "Register Page";
            ViewData["Year"] = DateTime.Now.Year;
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterForm form)
        {
            ViewBag.Title = "Register Page";
            ViewData["Year"] = DateTime.Now.Year;

            if(!ModelState.IsValid)
            {
                return View(form);
            }
            _userService.Register(form.RegisterToClient());
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Disconnect();
            return RedirectToAction("Login", "Auth");
        }
    }
}
