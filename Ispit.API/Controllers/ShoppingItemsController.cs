using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ispit.API.Data;
using Ispit.API.Models;

namespace Ispit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShoppingItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingItem>>> GetShoppingItem()
        {
            if (_context.ShoppingItem == null)
            {
                return NotFound();
            }
            return await _context.ShoppingItem.ToListAsync();
        }

        // GET: api/ShoppingItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingItem>> GetShoppingItem(int id)
        {
            if (_context.ShoppingItem == null)
            {
                return NotFound();
            }
            var shoppingItem = await _context.ShoppingItem.FindAsync(id);

            if (shoppingItem == null)
            {
                return NotFound();
            }

            return shoppingItem;
        }

        // PUT: api/ShoppingItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingItem(int id, ShoppingItem shoppingItem)
        {
            if (id != shoppingItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingItemExists(id))
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

        // POST: api/ShoppingItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingItem>> PostShoppingItem(ShoppingItem shoppingItem)
        {
            if (_context.ShoppingItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ShoppingItem'  is null.");
            }
            _context.ShoppingItem.Add(shoppingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingItem", new { id = shoppingItem.Id }, shoppingItem);
        }

        // DELETE: api/ShoppingItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingItem(int id)
        {
            if (_context.ShoppingItem == null)
            {
                return NotFound();
            }
            var shoppingItem = await _context.ShoppingItem.FindAsync(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            _context.ShoppingItem.Remove(shoppingItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingItemExists(int id)
        {
            return (_context.ShoppingItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
