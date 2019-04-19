using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoActivityNew.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace DojoActivity.Controllers
{
    public class DojoController : Controller
    {
        private DojoActivityNewContext dbContext;
        public DojoController(DojoActivityNewContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("index");
        }
        [HttpPost("register")]
        public IActionResult SignUp(User user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.users.Any(u=> u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                dbContext.Add(user);
                dbContext.SaveChanges();
                string email = user.Email;
                HttpContext.Session.SetInt32("userId" ,  user.UserId);
                return RedirectToAction ("Home" , "Wall");
            }
            else
            {
                return View("index");
            }

        }

        [HttpPost("confirm")]
        public IActionResult Confirm(LoginUser userSubmission)
        {    
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.users.FirstOrDefault(u => u.Email == userSubmission.LogEmail);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email");
                    return View("index");
                }             
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LogPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "Invalid Password");
                    return View("login");
                }
                HttpContext.Session.SetInt32("userId", userInDb.UserId);
                return RedirectToAction("Home", "Wall");
            }
            else
            {
                return View("index");
            }
        }

    }
}
