using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhoneBook.UI.Infrastructure;
using PhoneBook.UI.Infrastructure.Messager;
using PhoneBook.UI.Models;

namespace PhoneBook.UI.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IPhoneBookRepository _phoneBookRepository;

        public ContactController(IPhoneBookRepository phoneBookRepository, IMessager messager,
            IConfiguration configuration,
            IHttpContextAccessor contextAccessor) : base(configuration, messager, contextAccessor)
        {
            _phoneBookRepository = phoneBookRepository;
            _phoneBookRepository.SetAuthKey(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        // GET: Contact/Details/5
        public ActionResult Details(int id)
        {
            var contact = _phoneBookRepository.GetContact(id);
            return View(contact);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, [FromForm] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {

                _phoneBookRepository.CreateContact(id, contact.FirstName, contact.Lastname, contact.PhoneNumber,
                (PhoneNumberType)Enum.Parse(typeof(PhoneNumberType),
                contact.PhoneNumberType, true));
                return RedirectToAction(nameof(Details), "UserPhoneBook", new { id = id });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int id)
        {
            var contact = _phoneBookRepository.GetContact(id);
            return View(contact);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] Contact contact)
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

        // GET: Contact/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contact/Delete/5
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