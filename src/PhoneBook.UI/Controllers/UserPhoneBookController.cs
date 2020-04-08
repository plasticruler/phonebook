using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.UI.Infrastructure;
using PhoneBook.UI.Models;

namespace PhoneBook.UI.Controllers
{
    public class UserPhonebookController : Controller
    {
        private readonly IPhoneBookRepository _phonebookRepository;

        public UserPhonebookController(IPhoneBookRepository phonebookRepository)
        {
            _phonebookRepository = phonebookRepository;
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
            return View();
        }

        // POST: UserPhoneBook/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody] UserPhonebook phoneBook)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
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