using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhoneBook.UI.Infrastructure;
using PhoneBook.UI.Infrastructure.Messager;
using PhoneBook.UI.Models;
using PhoneBook.UI.Models.DTO;

namespace PhoneBook.UI.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        IPhoneBookRepository _phoneBookRepository;
        public UserController(IPhoneBookRepository phoneBookRepository, IConfiguration configuration, 
            IMessager messager, IHttpContextAccessor contextAccessor) :base(configuration, messager,contextAccessor)
        {
            _phoneBookRepository = phoneBookRepository;
            
            _phoneBookRepository.SetAuthKey(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
        }
        // GET: User
        public ActionResult Index()
        {
            
            return View(_phoneBookRepository.GetUsers());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = _phoneBookRepository.GetUser(id);
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] UserForCreate userModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _phoneBookRepository.CreateUser(userModel.FirstName, userModel.Surname, userModel.Password, userModel.EmailAddress);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _phoneBookRepository.GetUser(id);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] UserModel user)
        {
            try
            {
                // TODO: Add update logic here
                var editUser = _phoneBookRepository.GetUser(id);
                editUser.EmailAddress = user.EmailAddress;
                editUser.FirstName = user.FirstName;
                editUser.Surname = user.Surname;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}