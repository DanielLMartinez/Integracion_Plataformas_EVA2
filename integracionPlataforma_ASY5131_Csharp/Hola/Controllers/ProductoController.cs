using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Hola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private static IList<Producto> _productos = new List<Producto>();

        // GET: api/producto
        [HttpGet]
        public ActionResult<IEnumerable<Producto>> Get()
        {
            return Ok(_productos);
        }

        // GET api/producto/5
        [HttpGet("{id}")]
        public ActionResult<Producto> Get(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.id == id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }
            return Ok(producto);
        }

        // POST api/producto
        [HttpPost]
        public IActionResult Post([FromBody] Producto value)
        {
            _productos.Add(value);
            return CreatedAtAction(nameof(Get), new { id = value.id }, value);
        }

        // PUT api/producto/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Producto value)
        {
            var productoExistente = _productos.FirstOrDefault(p => p.id == id);
            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado");
            }

            _productos[_productos.IndexOf(productoExistente)] = value;
            return NoContent();
        }

        // DELETE api/producto/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var productoExistente = _productos.FirstOrDefault(p => p.id == id);
            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado");
            }

            _productos.Remove(productoExistente);
            return NoContent();
        }
    }
}
