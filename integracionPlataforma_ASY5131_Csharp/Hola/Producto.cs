namespace Hola
{
    public class Producto
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }

        // Constructor para inicializar propiedades
        public Producto()
        {
            Nombre = ""; // Valor por defecto para Nombre
            Descripcion = ""; // Valor por defecto para Descripcion
        }
    }
}
