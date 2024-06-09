namespace Hola
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Precio { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        // Constructor que inicializa todas las propiedades
        public Producto(int id, string nombre, int precio, string descripcion)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Descripcion = descripcion;
        }

      
        public Producto() { }
    }
}
