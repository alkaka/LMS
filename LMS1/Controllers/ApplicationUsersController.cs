﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS1.Data;
using LMS1.Models;
using LMS1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS1.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUsersController(ApplicationDbContext context,
                                          UserManager<ApplicationUser> userManager,
                                          RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            List<UserToShow> usersToShow = new List<UserToShow>();
            var listOfUsers = await _userManager.Users.ToListAsync();

            foreach (ApplicationUser u in listOfUsers)
            {
                var uts = new UserToShow();

                uts.Id = u.Id;
                uts.UserName = u.UserName;
                if (await _userManager.IsInRoleAsync(u, "Teacher")) uts.Role = "Teacher";
                else uts.Role = "Student";

                usersToShow.Add(uts);
            }

            return View(usersToShow);
        }

        public async Task<IActionResult> Details(string id)
        {
            var appUser = await _context.ApplicationUser.FindAsync(id);

            var user2Show = new UserToShow();

            user2Show.UserName = appUser.UserName;
            user2Show.Email = appUser.Email;

            var course = await _context.Course.FindAsync(appUser.CourseId);
            // Check if the course was found
            user2Show.CourseName = course.Name;

            if (await _userManager.IsInRoleAsync(appUser, "Teacher")) user2Show.Role = "Teacher";
            else user2Show.Role = "Student";

            return View(user2Show);
        }

        // GET: ApplicationUsers/Create
        public IActionResult Create(int? id)
        {
            //I am cheating a bit by using route-id as CourseId. 
            //But it is the only integer I need. 
            ViewBag.CourseId = id; 

            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Email,CourseId,PasswordHash")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var userToStore = new ApplicationUser { UserName = user.UserName, Email = user.Email, CourseId= user.CourseId };
                var result = await _userManager.CreateAsync(userToStore, user.PasswordHash);
                var resultAddRole = await _userManager.AddToRoleAsync(userToStore, "Student");

                return RedirectToAction("Details", "Courses", new { id = user.CourseId });
            }
            return View(user);
        }

        // GET: ApplicationUser/Edit/#¤%¤#"!"#)=
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ApplicationUser.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var u2e = new UserToEdit();
            u2e.Id = user.Id;
            u2e.UserName = user.UserName;
            u2e.Email = user.Email;
            return View(u2e);
        }

        // POST: ApplicationUser/Edit/#¤%¤#"!"#)=
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,UserName,Email")] UserToEdit user)
        {
            if (ModelState.IsValid)
            {
                var user2Store = await _context.ApplicationUser.FindAsync(user.Id);

                user2Store.UserName = user.UserName;
                user2Store.Email = user.Email; 

                try
                {
                    _context.Update(user2Store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // What is this for? Check in Course Edit
                    if (user.Id=="hej")
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (user2Store == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}