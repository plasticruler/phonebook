using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PhoneBook.API.Models;
using PhoneBook.API.Models.DTO;
using PhoneBook.API.Options;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactController : BaseController
    {
        private readonly AppDbContext _context;

        public IOptionsSnapshot<AppSettings> _appSettings { get; }

        public ContactController(AppDbContext context, IOptionsSnapshot<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }

        // GET: api/Contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        // GET: api/Contact/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(long id)
        {
            var contact = await _context.Contacts
                .Include(c=>c.PhoneNumbers)
                .FirstAsync(c=>c.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contact/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(long id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }
            
            

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                var cnt = _context.Contacts.Find(contact.Id);
                var pb = _context.PhoneBook.Find(contact.PhoneBookId);
                if (cnt == null || pb.UserId != UserId)
                {
                    return NotFound("Contact not found or user invalid.");
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Contact
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            var pb = _context.PhoneBook.Find(contact.PhoneBookId);

            if (pb == null || pb.UserId != UserId)
                return NotFound("PhoneBook not found or user invalid.");

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        [HttpPost("CreateForContact")]
        public async Task<ActionResult<Contact>> CreateForContact(ContactForCreate contactForCreate)
        {
            var pb = _context.PhoneBook.Find(contactForCreate.PhoneBookId);
            if (pb == null || pb.UserId != UserId)
                return NotFound("PhoneBook not found or user invalid.");
            var contact = new Contact()
            {
                Lastname = contactForCreate.LastName,
                FirstName = contactForCreate.FirstName,
                PhoneBookId = contactForCreate.PhoneBookId
            };
            _context.Contacts.Add(contact);
            _context.SaveChanges();

            _context.TelephoneNumber.Add(new TelephoneNumber()
            {
                ContactId = contact.Id,
                Number = contactForCreate.PhoneNumber,
                NumberType = contactForCreate.PhoneNumberType
            });
            await _context.SaveChangesAsync();
                
            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contact/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(long id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        private bool ContactExists(long id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
