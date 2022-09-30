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
    public class pagosController : ControllerBase
    {
        private readonly AplicationData _context;

        public pagosController(AplicationData context)
        {
            _context = context;
        }

        // GET: api/pagos
        [HttpGet("historialReserva")]
        //[Authorize]
        public object GetHistorial()
        {
            if (_context.cliente == null)
            {
                return NotFound();
            }
            var dat = (from cl in _context.cliente
                       join p in _context.pago on cl.idCliente equals p.id_client
                       join a in _context.vehiculo on p.idAuto equals a.idAuto
                       join e in _context.estadoPago on p.idEst_pago equals e.idEstado_pag
                       select new
                       {
                           cedula = cl.cedula,
                           nombres = cl.nombres,
                           apellidos = cl.apellidos,
                           auto = a.marca,
                           costo = p.costo,
                           estado = e.descripcion,
                       }).ToList();

            return dat;
        }
        // GET: api/pagos
        [HttpGet("Pagos")]
        public async Task<ActionResult<IEnumerable<reserva>>> Getpago()
        {
          if (_context.reserva == null)
          {
              return NotFound();
          }
            return await _context.reserva.ToListAsync();
        }

        [HttpGet("buscarbyCedula")]
        public object GetPagos(string cedula)
        {
            if (_context.cliente == null)
            {
                return NotFound();
            }
            var dat = (from cl in _context.cliente
                       join p in _context.pago on cl.idCliente equals p.id_client
                       join a in _context.vehiculo on p.idAuto equals a.idAuto
                       join e in _context.estadoPago on p.idEst_pago equals e.idEstado_pag
                       where (cl.cedula == cedula)
                       select new
                       {
                           cedula = cl.cedula,
                           nombres = cl.nombres,
                           apellidos = cl.apellidos,
                           auto = a.marca,
                           costo = p.costo,
                           estado = e.descripcion,
                       }).ToList();

            return dat;
        }
        // GET: api/pagos/5
        [HttpGet("{id}")]
       
        public async Task<ActionResult<pago>> Getpago(int? id)
        {
          if (_context.pago == null)
          {
              return NotFound();
          }
            var pago = await _context.pago.FindAsync(id);

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        // PUT: api/pagos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpago(int? id, pago pago)
        {
            if (id != pago.idPago)
            {
                return BadRequest();
            }

            _context.Entry(pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pagoExists(id))
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

        // POST: api/pagos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<pago>> Postpago(pago pago)
        {
          if (_context.pago == null)
          {
              return Problem("Entity set 'AplicationData.pago'  is null.");
          }
            _context.pago.Add(pago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getpago", new { id = pago.idPago }, pago);
        }

        // DELETE: api/pagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepago(int? id)
        {
            if (_context.pago == null)
            {
                return NotFound();
            }
            var pago = await _context.pago.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.pago.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool pagoExists(int? id)
        {
            return (_context.pago?.Any(e => e.idPago == id)).GetValueOrDefault();
        }
    }
}
