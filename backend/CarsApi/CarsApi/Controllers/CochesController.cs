using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsApi.Context;
using CarsApi.Model;

namespace CarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CochesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CochesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Coches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coche>>> GetCoches()
        {
            return await _context.Coches.ToListAsync();
        }

        // GET: api/Coches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coche>> GetCoche(int id)
        {
            var coche = await _context.Coches.FindAsync(id);

            if (coche == null)
            {
                return NotFound();
            }

            return coche;
        }

        // PUT: api/Coches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoche(int id, Coche coche)
        {
            if (id != coche.Id)
            {
                return BadRequest();
            }

            _context.Entry(coche).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CocheExists(id))
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

        // POST: api/Coches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coche>> PostCoche(Coche coche)
        {
            _context.Coches.Add(coche);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoche", new { id = coche.Id }, coche);
        }

        // DELETE: api/Coches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoche(int id)
        {
            var coche = await _context.Coches.FindAsync(id);
            if (coche == null)
            {
                return NotFound();
            }

            _context.Coches.Remove(coche);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CocheExists(int id)
        {
            return _context.Coches.Any(e => e.Id == id);
        }
    }
}
