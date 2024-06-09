using Hola;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Hola.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private static IList<Producto> _productos = new List<Producto>()
        {
            new Producto(1, "Caja de Tornillos", 3500, "Caja con 100 tornillos de 3ra"),
            new Producto(2, "Caja de Tuercas", 4000, "Caja con 100 tuercas de M16"),
            new Producto(3, "Taladro", 75000, "Taladro eléctrico inhalambrico"),
            new Producto(4, "Martillo", 10000, "Martillo para clavos")
        };

        private static int _nextId = _productos.Max(p => p.Id) + 1;

        // GET: api/productos
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            return _productos;
        }

        // GET: api/productos/{id}
        [HttpGet("{id}")]
        public ActionResult<Producto> Get(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }

        // POST: api/productos
        [HttpPost]
        public IActionResult Post([FromBody] Producto producto)
        {
            if (producto.Precio <= 0)
            {
                return BadRequest("El precio debe ser positivo.");
            }

            producto.Id = _nextId++;
            _productos.Add(producto);
            return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
        }

        // PUT: api/productos/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Producto producto)
        {
            var productoExistente = _productos.FirstOrDefault(p => p.Id == id);
            if (productoExistente == null)
            {
                return NotFound();
            }

            if (producto.Precio <= 0)
            {
                return BadRequest("El precio debe ser positivo.");
            }

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Precio = producto.Precio;
            productoExistente.Descripcion = producto.Descripcion;

            return NoContent();
        }

        // DELETE: api/productos/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            _productos.Remove(producto);
            return NoContent();
        }
    }
}
