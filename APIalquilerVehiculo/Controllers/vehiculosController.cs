using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIalquilerVehiculo.Data;
using APIalquilerVehiculo.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIalquilerVehiculo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class vehiculosController : ControllerBase
    {
        private readonly AplicationData _context;

        public vehiculosController(AplicationData context)
        {
            _context = context;
        }

        // GET: api/vehiculos
        [HttpGet]
        //[Authorize]
        public object Getvehiculo()
        {
            if (_context.cliente == null)
            {
                return NotFound();
            }
            var dat = (from v in _context.vehiculo
                       join t in _context.tipoVehiculo on v.idTipo_vh equals t.idTipo_vh
                       join e in _context.estadoVehiculo on v.idEst_vh equals e.idEst_vh
                       select new
                       {
                           marca = v.marca,
                           plazas = v.plazas,
                           cambios = v.cambios,
                           kilometraje = v.kilometraje,
                           tipo = t.descripcion,
                           costo = v.precioAlquiler,
                           estado = e.descripcion,
                       }).ToList();

            return dat;
        }
        /* public async Task<ActionResult<IEnumerable<vehiculo>>> Getvehiculo()
         {
           if (_context.vehiculo == null)
           {
               return NotFound();
           }
             return await _context.vehiculo.ToListAsync();
         }*/

        // GET: api/vehiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<vehiculo>> Getvehiculo(int? id)
        {
          if (_context.vehiculo == null)
          {
              return NotFound();
          }
            var vehiculo = await _context.vehiculo.FindAsync(id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return vehiculo;
        }

        // PUT: api/vehiculos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putvehiculo(int? id, vehiculo vehiculo)
        {
            if (id != vehiculo.idAuto)
            {
                return BadRequest();
            }

            _context.Entry(vehiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vehiculoExists(id))
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

        // POST: api/vehiculos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<vehiculo>> Postvehiculo(vehiculo vehiculo)
        {
          if (_context.vehiculo == null)
          {
              return Problem("Entity set 'AplicationData.vehiculo'  is null.");
          }
            _context.vehiculo.Add(vehiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getvehiculo", new { id = vehiculo.idAuto }, vehiculo);
        }

        // DELETE: api/vehiculos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletevehiculo(int? id)
        {
            if (_context.vehiculo == null)
            {
                return NotFound();
            }
            var vehiculo = await _context.vehiculo.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            _context.vehiculo.Remove(vehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool vehiculoExists(int? id)
        {
            return (_context.vehiculo?.Any(e => e.idAuto == id)).GetValueOrDefault();
        }
    }
}
