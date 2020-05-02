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
    public class TelephoneNumberController : BaseController
    {
        private readonly IPhoneBookRepository _phoneBookRepository;

        public TelephoneNumberController(IPhoneBookRepository phoneBookRepository, 
                                    IMessager messager,
                                    IConfiguration configuration,
                                    IHttpContextAccessor contextAccessor): base(configuration, messager, contextAccessor)
        {            
            _phoneBookRepository = phoneBookRepository;
            _phoneBookRepository.SetAuthKey(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
        }
        // GET: TelephoneNumber
        public ActionResult Index()
        {
            return View();
        }

        // GET: TelephoneNumber/Details/5
        public ActionResult Details(int id)
        {
            var number = _phoneBookRepository.GetTelephoneNumber(id);
            return View(number);
        }

        // GET: TelephoneNumber/Create
        public ActionResult Create(int id)
        {
            return View(new TelephoneNumber() { ContactId = id });
        }

        // POST: TelephoneNumber/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] TelephoneNumber phoneNumber)
        {
            try
            {
                _phoneBookRepository.CreatePhoneNumber(phoneNumber.ContactId, phoneNumber.Number, phoneNumber.NumberType);
                return RedirectToAction("Details","Contact",new {id = phoneNumber.ContactId });
            }
            catch
            {
                return View();
            }
        }

        // GET: TelephoneNumber/Edit/5
        public ActionResult Edit(int id)
        {
            var number = _phoneBookRepository.GetTelephoneNumber(id);
            return View(number);
        }

        // POST: TelephoneNumber/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] TelephoneNumber telephoneNumber, int id)
        {
            try
            {
                var n = _phoneBookRepository.GetTelephoneNumber(telephoneNumber.Id);
                n.Number = telephoneNumber.Number;
                n.NumberType = telephoneNumber.NumberType;
                return RedirectToAction("Details","Contact", new { id = n.ContactId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: TelephoneNumber/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TelephoneNumber/Delete/5
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