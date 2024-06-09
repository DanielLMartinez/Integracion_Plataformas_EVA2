using API1;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private static IList<Cliente> _clientes = new List<Cliente>()
        {
            new Cliente {  Rut = 12345678-9, Nombre = "Sebastian", Apellido = "Contreras", Direccion = "Av siempre viva 0110", Telefono = 1733737 },
            new Cliente {  Rut = 17524577-2, Nombre = "Augusto", Apellido = "Pfeifer", Direccion = "Av calle del piso 1234", Telefono = 693463464 },
            new Cliente {  Rut = 16345455-3, Nombre = "Sergio", Apellido = "Mellado", Direccion = "pasaje callado 584", Telefono = 618861168 },
            new Cliente {  Rut = 26756563-6, Nombre = "Daniel", Apellido = "Martinez", Direccion = "Mirador de arriba 843", Telefono = 538431 }
        };

        // GET: api/clientes
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            return _clientes;
        }

        // GET: api/clientes/{Rut}
        [HttpGet("{Rut}")]
        public Cliente Get(int Rut)
        {
            Cliente resultado = _clientes.FirstOrDefault(c => c.Rut == Rut);
            return resultado;
        }

        // POST: api/clientes
        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            if (_clientes.Any(c => c.Rut == cliente.Rut))
            {
                return BadRequest("Ya existe un cliente con el mismo RUT.");
            }

            _clientes.Add(cliente);
            return CreatedAtAction(nameof(Cliente), new { rut = cliente.Rut }, cliente);
        }

        // PUT: api/clientes/{Rut}
        [HttpPut("{Rut}")]
        public void Put(int Rut, [FromBody] Cliente cliente)
        {
            var clienteExistente = _clientes.FirstOrDefault(c => c.Rut == Rut);
            if (clienteExistente != null)
            {
                clienteExistente.Nombre = cliente.Nombre;
                clienteExistente.Apellido = cliente.Apellido;
            }
            if (clienteExistente == null)
            {
                _clientes.Add(cliente);
            }
            else
            {
                _clientes.Remove(clienteExistente);
                _clientes.Add(cliente);
            }
        }

        // DELETE: api/clientes/{Rut}
        [HttpDelete("{Rut}")]
        public void Delete(int Rut)
        {
            _clientes.Remove(_clientes.FirstOrDefault(c => c.Rut == Rut));
        }
    }
}