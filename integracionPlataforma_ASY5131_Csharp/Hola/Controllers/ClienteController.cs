using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Hola.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private static IList<Cliente> _clientes = new List<Cliente>();

        // GET: api/clientes
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            // Devuelve la lista de clientes
            return Ok(_clientes);
        }

        // GET: api/clientes/{Rut}
        [HttpGet("{Rut}")]
        public ActionResult<Cliente> Get(int Rut)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Rut == Rut);
            if (cliente == null)
            {
                // Devuelve un error 404 si el cliente no se encuentra
                return NotFound("Cliente no encontrado");
            }
            return Ok(cliente);
        }

        // POST: api/clientes
        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            if (_clientes.Any(c => c.Rut == cliente.Rut))
            {
                // Devuelve un error 400 si el cliente ya existe
                return BadRequest("Ya existe un cliente con el mismo RUT.");
            }

            // Agrega el cliente a la lista
            _clientes.Add(cliente);

            // Devuelve un código 201 (Created) y la URL de la nueva entidad
            return CreatedAtAction(nameof(Get), new { Rut = cliente.Rut }, cliente);
        }

        // PUT: api/clientes/{Rut}
        [HttpPut("{Rut}")]
        public IActionResult Put(int Rut, [FromBody] Cliente cliente)
        {
            var clienteExistente = _clientes.FirstOrDefault(c => c.Rut == Rut);
            if (clienteExistente == null)
            {
                // Devuelve un error 404 si el cliente no se encuentra
                return NotFound("Cliente no encontrado");
            }

            // Actualiza el nombre y apellido del cliente
            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Apellido = cliente.Apellido;

            // Devuelve un código 204 (NoContent) indicando que la actualización fue exitosa
            return NoContent();
        }

        // DELETE: api/clientes/{Rut}
        [HttpDelete("{Rut}")]
        public IActionResult Delete(int Rut)
        {
            var clienteExistente = _clientes.FirstOrDefault(c => c.Rut == Rut);
            if (clienteExistente == null)
            {
                // Devuelve un error 404 si el cliente no se encuentra
                return NotFound("Cliente no encontrado");
            }

            // Elimina el cliente de la lista
            _clientes.Remove(clienteExistente);

            // Devuelve un código 204 (NoContent) indicando que la eliminación fue exitosa
            return NoContent();
        }
    }
}
