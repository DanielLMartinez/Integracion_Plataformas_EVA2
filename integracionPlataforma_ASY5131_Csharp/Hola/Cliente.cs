namespace Hola
{
    public class Cliente
    {
        public int Id { get; set; }
        public int Rut { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public int Telefono { get; set; }

        // Constructor que inicializa todas las propiedades
        public Cliente(int id, int rut, string nombre, string apellido, string direccion, int telefono)
        {
            Id = id;
            Rut = rut;
            Nombre = nombre;
            Apellido = apellido;
            Direccion = direccion;
            Telefono = telefono;
        }

        
        public Cliente() { }
    }
}
