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
    public class ClientesController : ControllerBase
    {
        private readonly AplicationData _context;

        public ClientesController(AplicationData context)
        {
            _context = context;
        }
        [HttpGet("buscarbyCedula")]
        public object GetDatos(string cedula)
        {
            if (_context.cliente == null)
            {
                return NotFound();
            }
            var dat = (from cl in _context.cliente
                       join u in _context.Usuario on cl.idUsuario equals u.idUsuario
                       where (cl.cedula == cedula)
                       select new
                       {
                           cedula = cl.cedula,
                           nombres = cl.nombres,
                           apellidos = cl.apellidos,
                           correo = u.correo,
                           direccion = cl.direccion,
                           edad = cl.edad,
                       }).ToList();

            return dat;
        }

        
        // GET: api/Clientes
        [HttpGet]
        public object GetClientes()
        {
            if (_context.cliente == null)
            {
                return NotFound();
            }
            var dat = (from cl in _context.cliente
                       join c in _context.ciudad on cl.idCiudad equals c.idCiudad
                       join u in _context.Usuario on cl.idUsuario equals u.idUsuario
                       select new
                       {
                           cedula = cl.cedula,
                           nombres = cl.nombres,
                           apellidos = cl.apellidos,
                           edad = cl.edad,
                           correo = u.correo,
                           direccion = cl.direccion,
                           ciudad = c.nombre,
                       }).ToList();

            return dat;
        }
        //GET CLIENTES
        [HttpGet("listaClientes")]
         public async Task<ActionResult<IEnumerable<clientes>>> Getclientes()
         {
           if (_context.clientes == null)
           {
               return NotFound();
           }
             return await _context.clientes.ToListAsync();
         }

        
        [HttpPost ("reservas")]
        public async Task<ActionResult<Cliente>> PostReseva(reserva reser)
        {
            if (_context.reserva == null)
            {
                return Problem("Entity set 'AplicationData.cliente'  is null.");
            }
            _context.reserva.Add(reser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = reser.idReserva }, reser);
        }
        // GET: api/Clientes/5
      /*  [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClientes(int? id)
        {
          if (_context.cliente == null)
          {
              return NotFound();
          }
            var cliente = await _context.cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }*/

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}Actualizar")]
        public async Task<IActionResult> PutClientes(int? id, clientes client)
        {
            if (id != client.idClientes)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("GuardarClientes")]
        public async Task<ActionResult<clientes>> PostClientes(clientes cl)
        {
            if (_context.clientes == null)
            {
                return Problem("Entity set 'AplicationData.cliente'  is null.");
            }
            _context.clientes.Add(cl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cl.idClientes }, cl);
        }
        /*[HttpPost("Cliente")]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
         {
           if (_context.cliente == null)
           {
               return Problem("Entity set 'AplicationData.cliente'  is null.");
           }
             _context.cliente.Add(cliente);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetCliente", new { id = cliente.idCliente }, cliente);
         }*/

        // DELETE: api/Clientes/5
        [HttpDelete("EliminarCliente")]
        public async Task<IActionResult> DeleteCliente(int? id)
        {
            if (_context.clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int? id)
        {
            return (_context.clientes?.Any(e => e.idClientes == id)).GetValueOrDefault();
        }
    }
}
