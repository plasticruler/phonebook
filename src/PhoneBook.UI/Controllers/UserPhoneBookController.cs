﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhoneBook.UI.Infrastructure;
using PhoneBook.UI.Models;

namespace PhoneBook.UI.Controllers
{
    public class UserPhonebookController : BaseController
    {
        private readonly IPhoneBookRepository _phonebookRepository;

        public UserPhonebookController(IPhoneBookRepository phonebookRepository, IConfiguration configuration,
            Infrastructure.Messager.IMessager messager, IHttpContextAccessor contextAccessor) : base(configuration, messager, contextAccessor)
        {
            _phonebookRepository = phonebookRepository;
            _phonebookRepository.SetAuthKey(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
        }
        // GET: UserPhoneBook
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserPhoneBook/Details/5
        public ActionResult Details(int id)
        {
            var pb = _phonebookRepository.GetPhoneBook(id);
            
            return View(pb);
        }

        // GET: UserPhoneBook/Create
        public ActionResult Create()
        {            
            return View(new UserPhonebook()
            {
                OwnerId = (int) UserId.Value
            });
        }

        // POST: UserPhoneBook/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] UserPhonebook phoneBook)
        {
            try
            {
                _phonebookRepository.CreatePhoneBook((int) UserId.Value, phoneBook.Name);
                return RedirectToAction("Details","User",new { id = UserId.Value });
            }
            catch
            {
                return View();
            }
        }

        // GET: UserPhoneBook/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserPhoneBook/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserPhoneBook/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserPhoneBook/Delete/5
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