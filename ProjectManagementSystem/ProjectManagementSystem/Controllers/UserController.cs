using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementSystem.Models.Interfaces;
using ProjectManagementSystem.Repository;
using ProjectManagementSystem.Views.ViewModels.UserModels;
using ProjectManagementSystem.Views.ViewModels.ProjectModels;
using Newtonsoft.Json;

namespace ProjectManagementSystem.Controllers
{
    [Route("user")]
    public class UserController : BaseController
    {
        public UserController(IMapper mapper, IRepository repository) : base(mapper, repository) { }

        [HttpGet("login")]
        public IActionResult GetAuthorizationView()
        {
            return View("Login");
        }

        [HttpPost("login")]
        public IActionResult Login(UserLogin userLogin)
        {
            var dbUser = Repository.Users
                .Search(user => user.Email == userLogin.Email && user.Password == userLogin.Password)
                .SingleOrDefault();
            if (dbUser != null)
            {
                Authorize(userLogin.Email);
                var projects = Repository.Projects
                    .Search(project => true)
                    .Select(project => Mapper.Map<Project>(project));
                var stringProjects = JsonConvert.SerializeObject(projects);
                this.Response.Cookies.Append("projects", stringProjects);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return RedirectToAction("login", "user");
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllUsersView()
        {
            // TODO: check that user is admin (everywhere)

            var allUsers = Repository.Users.Search(us => true)
                .Where(user => user.IsDeleted == false)
                .Select(user => Mapper.Map<User>(user));

            return View("AllUsers", allUsers);
        }

        [HttpGet("create")]
        public IActionResult GetCreateUserView()
        {
            var userRoles = Repository.UserRoles
                .Search((role) => true)
                .Select(role => Mapper.Map<UserRole>(role).RoleName);
            ViewBag.UserRoles = new SelectList(userRoles);
            return View("CreateUser");
        }

        [HttpPost("create")]
        public IActionResult CreateUser(User user)
        {
            var newUser = Mapper.Map<IUser>(user);
            newUser.UserRole = Repository.UserRoles.Search(role => role.RoleName == user.Role).First();
            Repository.Users.Add(newUser);
            Repository.Save();

            //using (MailMessage mm = new MailMessage())
            //{
            //    mm.To.Add(new MailAddress("sofyasosnovik2000@gmail.com"));  // replace with valid value 
            //    mm.From = new MailAddress("sofiyasosnovik2001@gmail.com");  // replace with valid value
            //    mm.Subject = "Your email subject";                mm.Body = "Hello";

            //    mm.IsBodyHtml = false;
            //    using (SmtpClient smtp = new SmtpClient())
            //    {
            //        smtp.Host = "smtp.gmail.com";
            //        smtp.EnableSsl = true;
            //        NetworkCredential NetworkCred = new NetworkCredential("sofiyasosnovik2001@gmail.com", "marvel2001");
            //        smtp.UseDefaultCredentials = true;
            //        smtp.Credentials = NetworkCred;
            //        smtp.Port = 587;
            //        smtp.Send(mm);
            //        ViewBag.Message = "Email sent.";
            //    }
            //}


            return RedirectToAction("GetAllUsersView", "User");
        }

        [HttpGet("update")]
        public IActionResult GetUpdateUserView(User user)
        {
            var currentUser = Mapper.Map<User>(Repository.Users.FindById(user.Id));
            var userRoles = Repository.UserRoles
                .Search((role) => true)
                .Select(role => Mapper.Map<UserRole>(role).RoleName);
            ViewBag.UserRoles = new SelectList(userRoles);

            return View("UpdateUser", currentUser);
        }

        //Role not mapped
        [HttpPost("update")]
        public IActionResult UpdateUser(User updatedUserView)
        {
            var updatedUser = Mapper.Map<IUser>(updatedUserView);
            var updatedRole = Repository.UserRoles
                .Search(role => role.RoleName == updatedUser.UserRole.RoleName)
                .Single();

            updatedUser.UserRole = updatedRole;
            Repository.Users.Modify(updatedUser);
            Repository.Save();

            return RedirectToAction("GetAllUsersView", "User");
        }

        [HttpGet("delete")]
        public IActionResult GetDeleteUser(User currentUser)
        {
            return DeleteUser(currentUser);
        }

        [HttpPost("delete")]
        public IActionResult DeleteUser(User currentUser)
        {
            var existingUser = Repository.Users.FindById(currentUser.Id);
            existingUser.IsDeleted = true;

            Repository.Users.Modify(existingUser);
            Repository.Save();

            return RedirectToAction("GetAllUsersView", "User");
        }

        [HttpGet("account")]
        public IActionResult Account()
        {
            if (User.Identity.IsAuthenticated)
            {
                var us = Repository.Users.Search(user => user.Email == User.Identity.Name).SingleOrDefault();
                var accountUser = Mapper.Map<User>(us);
                return View(accountUser);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            AuthContext.Logout(User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }

        private void Authorize(string name)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id)).Wait();
            var user = Mapper.Map<User>(Repository.Users.Search(dbUser => dbUser.Email == name).Single());
            AuthContext.SetCurrentUser(user);
        }
    }
}