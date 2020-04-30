using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Models;
using PhoneBook.API.Models.DTO;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TelephoneController : BaseController
    {
        private readonly AppDbContext _context;

        public TelephoneController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Telephone
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelephoneNumber>>> GetTelephoneNumber()
        {
            return await _context.TelephoneNumber.ToListAsync();
        }

        // GET: api/Telephone/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TelephoneNumber>> GetTelephoneNumber(long id)
        {
            var telephoneNumber = await _context.TelephoneNumber.FindAsync(id);

            if (telephoneNumber == null)
            {
                return NotFound();
            }

            return telephoneNumber;
        }

        // PUT: api/Telephone/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelephoneNumber(long id, TelephoneNumber telephoneNumber)
        {
            if (id != telephoneNumber.Id)
            {
                return BadRequest();
            }

            _context.Entry(telephoneNumber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelephoneNumberExists(id))
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

        // POST: api/Telephone
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TelephoneNumber>> PostTelephoneNumber(TelephoneNumber telephoneNumber)
        {
            _context.TelephoneNumber.Add(telephoneNumber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTelephoneNumber", new { id = telephoneNumber.Id }, telephoneNumber);
        }

        [HttpPost("CreateForTelephoneNumber")]
        public async Task<ActionResult<TelephoneNumber>> CreateForTelephoneNumber(TelephoneNumberForCreate telephoneNumberForCreate)
        {
            //check that the contact belongs to the currently logged in user
            var tn = _context.Contacts.Find(telephoneNumberForCreate.ContactId).PhoneBook;
            var telephoneNumber = new TelephoneNumber()
            {
                ContactId = telephoneNumberForCreate.ContactId,
                Number = telephoneNumberForCreate.Number,
                NumberType = telephoneNumberForCreate.NumberType
            };

            _context.TelephoneNumber.Add(telephoneNumber);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTelephoneNumber", new { id = telephoneNumber.Id }, telephoneNumber);
        }

        // DELETE: api/Telephone/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TelephoneNumber>> DeleteTelephoneNumber(long id)
        {
            var telephoneNumber = await _context.TelephoneNumber.FindAsync(id);
            if (telephoneNumber == null)
            {
                return NotFound();
            }

            _context.TelephoneNumber.Remove(telephoneNumber);
            await _context.SaveChangesAsync();

            return telephoneNumber;
        }

        private bool TelephoneNumberExists(long id)
        {
            return _context.TelephoneNumber.Any(e => e.Id == id);
        }
    }
}
