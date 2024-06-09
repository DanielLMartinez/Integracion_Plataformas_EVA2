using API1;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private static IList<Producto> _productos = new List<Producto>()
        {
            new Producto { id = 1, Nombre = "Caja de Tornillos", Precio = 3500, Descripcion = "Caja con 100 tornillos de 3ra" },
            new Producto { id = 2, Nombre = "Caja de Tuercas", Precio = 4000, Descripcion = "Caja con 100 tuercas de M16" },
            new Producto { id = 3, Nombre = "Taladro", Precio = 75000, Descripcion = "Taladro eléctrico inhalambrico" },
            new Producto { id = 4, Nombre = "Martillo", Precio = 10000, Descripcion = "Martillo para clavos" }
        };

        // GET: api/<ProductoController>
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            return _productos;
        }

        // GET api/<ProductoController>/5
        [HttpGet("{id}")]
        public Producto Get(int id)
        {
            return _productos.FirstOrDefault(p => p.id == id);
        }

        // POST api/<ProductoController>
        [HttpPost]
        public void Post([FromBody] Producto value)
        {
            _productos.Add(value);
        }

        // PUT api/<ProductoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Producto value)
        {
            Producto indiceProducto = _productos.FirstOrDefault(p => p.id == id);
            _productos[_productos.IndexOf(indiceProducto)] = value;
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productos.Remove(_productos.FirstOrDefault(p => p.id == id));
        }
    }
}