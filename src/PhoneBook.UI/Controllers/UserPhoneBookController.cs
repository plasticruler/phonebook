using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PhoneBook.UI.Configuration;
using PhoneBook.UI.Infrastructure;
using PhoneBook.UI.Infrastructure.Messager;
using PhoneBook.UI.Models;

namespace PhoneBook.UI.Controllers
{
    public class UserPhonebookController : BaseController
    {
        private readonly IPhoneBookRepository _phonebookRepository;

        public UserPhonebookController(IPhoneBookRepository phonebookRepository,
            IOptionsSnapshot<AppSettings> appSettings, IMessager messager, 
            IHttpContextAccessor contextAccessor) : base(appSettings, messager, contextAccessor)
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
                UserId = (int) UserId.Value
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
            var phoneBook = _phonebookRepository.GetPhoneBook(id);
            
            return View(phoneBook);
        }

        // POST: UserPhoneBook/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] UserPhonebook phoneBook)
        {
            try
            {                
                phoneBook.UserId = UserId.Value;
                phoneBook.Id = id;
                _phonebookRepository.UpdateUserPhoneBook(phoneBook);
                return RedirectToAction("Details", "User", new {id=id});
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