using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIalquilerVehiculo.Data;
using APIalquilerVehiculo.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace APIalquilerVehiculo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ciudadController : ControllerBase
    {
        private readonly AplicationData _context;

        public ciudadController(AplicationData context)
        {
            _context = context;
        }

        // GET: api/ciudad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ciudad>>> Getciudad()
        {
          if (_context.ciudad == null)
          {
              return NotFound();
          }
            return await _context.ciudad.ToListAsync();
        }
       [HttpGet("buscarciudad")]
        /*public async Task<ActionResult<IEnumerable<ciudad>>> GetCliente(string ciudad)
        {
            IQueryable<ciudad> query = _context.ciudad;
            query = query.Where(ci => ci.nombre.StartsWith(ciudad));

            if (query == null)
            {
                return null;
            }
            return await query.ToListAsync();
        }*/

        public object ciudad()
         {
             //string nombre;
           //  IQueryable<ciudad> query = _context.ciudad;
             var query = (from c in _context.ciudad //where(c => c.nombre == nombre)
                                select new
                                {
                                    ciudad = c.nombre,
                                }).ToList();
          //  query = query.Where(c=> c.nombre.StartsWith(nombre));
             if(query == null)
             {
                 return NotFound();
             }
             return query;
         }
        // GET: api/ciudad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ciudad>> Getciudad(int? id)
        {
          if (_context.ciudad == null)
          {
              return NotFound();
          }
            var ciudad = await _context.ciudad.FindAsync(id);

            if (ciudad == null)
            {
                return NotFound();
            }

            return ciudad;
        }

        // PUT: api/ciudad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putciudad(int? id, ciudad ciudad)
        {
            if (id != ciudad.idCiudad)
            {
                return BadRequest();
            }

            _context.Entry(ciudad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ciudadExists(id))
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

        // POST: api/ciudad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ciudad>> Postciudad(ciudad ciudad)
        {
          if (_context.ciudad == null)
          {
              return Problem("Entity set 'AplicationData.ciudad'  is null.");
          }
            _context.ciudad.Add(ciudad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getciudad", new { id = ciudad.idCiudad }, ciudad);
        }

        // DELETE: api/ciudad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteciudad(int? id)
        {
            if (_context.ciudad == null)
            {
                return NotFound();
            }
            var ciudad = await _context.ciudad.FindAsync(id);
            if (ciudad == null)
            {
                return NotFound();
            }

            _context.ciudad.Remove(ciudad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ciudadExists(int? id)
        {
            return (_context.ciudad?.Any(e => e.idCiudad == id)).GetValueOrDefault();
        }
    }
}
