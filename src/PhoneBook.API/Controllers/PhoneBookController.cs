using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Models;

namespace PhoneBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhoneBookController : BaseController
    {
        private readonly AppDbContext _context;

        public PhoneBookController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PhoneBook
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPhoneBook>>> GetPhoneBook()
        {
            return await _context.PhoneBook.Where(p=>p.UserId==UserId).ToListAsync();
        }
        [HttpGet("GetUserPhoneBooks/{id}")]
        public async Task<ActionResult<IEnumerable<UserPhoneBook>>> GetUserPhoneBooks(long id)
        {
            long userId = UserId.Value;
            return await _context.PhoneBook.Where(x => x.UserId == userId)
                .ToListAsync();
        }
        // GET: api/PhoneBook/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPhoneBook>> GetUserPhoneBook(long id)
        {
            var userPhoneBook = await _context.PhoneBook
                .Include(c => c.Contacts)
                .FirstAsync(x => x.Id == id);

            if (userPhoneBook == null || userPhoneBook.UserId != UserId)
            {
                return NotFound();
            }

            return userPhoneBook;
        }

        // PUT: api/PhoneBook/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPhoneBook(long id, UserPhoneBook userPhoneBook)
        {
            if (id != userPhoneBook.Id || userPhoneBook.UserId != UserId)
            {
                return BadRequest();
            }

            _context.Entry(userPhoneBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPhoneBookExists(id))
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

        // POST: api/PhoneBook
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserPhoneBook>> PostUserPhoneBook(UserPhoneBook userPhoneBook)
        {
            userPhoneBook.UserId = UserId.Value;            
            _context.PhoneBook.Add(userPhoneBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPhoneBook", new { id = userPhoneBook.Id }, userPhoneBook);
        }

        // DELETE: api/PhoneBook/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserPhoneBook>> DeleteUserPhoneBook(long id)
        {
            var userPhoneBook = await _context.PhoneBook.FindAsync(id);
            if (userPhoneBook == null || userPhoneBook.UserId != UserId)
            {
                return NotFound();
            }

            _context.PhoneBook.Remove(userPhoneBook);
            await _context.SaveChangesAsync();

            return userPhoneBook;
        }

        private bool UserPhoneBookExists(long id)
        {
            return _context.PhoneBook.Any(e => e.Id == id);
        }
    }
}
