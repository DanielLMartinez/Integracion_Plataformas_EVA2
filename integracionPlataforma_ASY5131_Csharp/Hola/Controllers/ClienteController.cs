using Hola;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Hola.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private static IList<Cliente> _clientes = new List<Cliente>()
        {
            new Cliente { Id = 1, Rut = 12345678, Nombre = "Sebastian", Apellido = "Contreras", Direccion = "Av siempre viva 0110", Telefono = 1733737 },
            new Cliente { Id = 2, Rut = 17524577, Nombre = "Augusto", Apellido = "Pfeifer", Direccion = "Av calle del piso 1234", Telefono = 693463464 },
            new Cliente { Id = 3, Rut = 16345455, Nombre = "Sergio", Apellido = "Mellado", Direccion = "pasaje callado 584", Telefono = 618861168 },
            new Cliente { Id = 4, Rut = 26756563, Nombre = "Daniel", Apellido = "Martinez", Direccion = "Mirador de arriba 843", Telefono = 538431 }
        };

        private static int _nextId = _clientes.Max(c => c.Id) + 1;

        // GET: api/clientes
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            return _clientes;
        }

        // GET: api/clientes/{Rut}
        [HttpGet("{Rut}")]
        public ActionResult<Cliente> Get(int Rut)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Rut == Rut);
            if (cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        // POST: api/clientes
        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            if (_clientes.Any(c => c.Rut == cliente.Rut))
            {
                return BadRequest("Ya existe un cliente con el mismo RUT.");
            }

            if (_clientes.Any(c => c.Nombre.Equals(cliente.Nombre, System.StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("Ya existe un cliente con el mismo nombre.");
            }

            cliente.Id = _nextId++;
            _clientes.Add(cliente);
            return CreatedAtAction(nameof(Get), new { rut = cliente.Rut }, cliente);
        }

        // PUT: api/clientes/{Id}
        [HttpPut("{Rut}")]
        public IActionResult Put(int Rut, [FromBody] Cliente cliente)
        {
            var clienteExistente = _clientes.FirstOrDefault(c => c.Rut == Rut);
            if (clienteExistente == null)
            {
                return NotFound();
            }

            if (_clientes.Any(c => c.Nombre.Equals(cliente.Nombre, System.StringComparison.OrdinalIgnoreCase) && c.Rut != Rut))
            {
                return BadRequest("Ya existe un cliente con el mismo nombre.");
            }

            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Apellido = cliente.Apellido;
            clienteExistente.Direccion = cliente.Direccion;
            clienteExistente.Telefono = cliente.Telefono;

            return NoContent();
        }

        // DELETE: api/clientes/{Rut}
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == Id);
            if (cliente == null)
            {
                return NotFound();
            }

            _clientes.Remove(cliente);
            return NoContent();
        }
    }
}
