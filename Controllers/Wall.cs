using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DojoActivityNew.Models;

namespace Wall.Controllers 
{
    public class WallController : Controller
    {
        private DojoActivityNewContext dbContext;
        public WallController(DojoActivityNewContext context)
        {
            dbContext = context;
        }

        [HttpGet("home")]
        public IActionResult Home()
        {
            int? id = HttpContext.Session.GetInt32("userId");
            if (id is null)
            {
                ModelState.AddModelError("Email", "Please Log In");
                return RedirectToAction("Index" , "Dojo");
            }
            ViewBag.User = dbContext.users.FirstOrDefault(p => p.UserId == id);
            ViewBag.Activities= dbContext.activities
                .Include(m=>m.User)
                .Include(c => c.Guest)
                .ThenInclude(u => u.User).ToList();
            ViewBag.AllGuests = dbContext.guests.ToList();
            Dashboard dash = new Dashboard
            {
                Activities = ViewBag.Activities,
                User = dbContext.users.FirstOrDefault(u => u.UserId == id),
                Guests = dbContext.guests.ToList()
            };
            return View("home" , dash);
        }

        [HttpGet("new")]
        public IActionResult newActivity()
        { 
            ViewBag.Id = HttpContext.Session.GetInt32("userId");
            return View("newactivity");
        }

        [HttpPost("createActivity")]
        public IActionResult CreateActivity(Activities activity)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(activity);
                dbContext.SaveChanges();
                Activities One = dbContext.activities.FirstOrDefault(p => p.Title == activity.Title);
                return Redirect($"info/{One.ActivityId}");
            }
            else
            {
                return View("newactivity");
            }
        }

        [HttpGet("deleteactivity/{activityId}")]
        public IActionResult DeleteWedding(int activityId)
        {
        Activities One = dbContext.activities.FirstOrDefault(p => p.ActivityId == activityId);
        dbContext.Remove(One);
        dbContext.SaveChanges();
        return RedirectToAction("Home");
        }

        [HttpGet("info/{activityId}")]
        public IActionResult ActivityInfo(int activityId)
        {
           ViewBag.Activity = dbContext.activities
                            .Include(u => u.User)
                            .Include(p=> p.Guest)
                            .ThenInclude(u=> u.User)
                            .FirstOrDefault(p=> p.ActivityId == activityId);
            int? id = HttpContext.Session.GetInt32("userId");
            One one =  new One 
            {
               Activity = ViewBag.Activity,
               User = dbContext.users.FirstOrDefault(u => u.UserId == id),
               Guests = dbContext.guests.ToList()
            } ;              
           return View("actinfo" , one);
        }

        [HttpGet("addguest/{userId}/{activityId}")]
        public IActionResult AddGuest(int userId, int activityId)
        {
            Guest one = new Guest(userId, activityId);
            dbContext.Add(one);
            dbContext.SaveChanges();
            return RedirectToAction("Home");
        }
        [HttpGet("onejoin/{activityId}/{userId}")]
        public IActionResult OneJoin(int userId, int activityId)
        {
            Guest hey = new Guest(userId, activityId);
            dbContext.Add(hey);
            dbContext.SaveChanges();
            return Redirect($"/info/{activityId}");
        }

        [HttpGet("removeguest/{userId}/{activityId}")]
        public IActionResult RemoveGuest(int userId, int activityId)
        {
            Guest one = dbContext.guests.FirstOrDefault(u => u.UserId == userId && u.ActivityId ==activityId );
            dbContext.Remove(one);
            dbContext.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpGet("oneleft/{activityId}/{userId}")]
        public IActionResult OneLeft(int userId, int activityId)
        {
            Guest one = dbContext.guests.FirstOrDefault(u => u.UserId == userId && u.ActivityId ==activityId );
            dbContext.Remove(one);
            dbContext.SaveChanges();
            return Redirect($"/info/{activityId}");
        }



        [HttpGet("logout")]
        public IActionResult LogOut()
        {
        HttpContext.Session.Clear();
        return RedirectToAction("Index" , "Dojo");
        }


    }

}