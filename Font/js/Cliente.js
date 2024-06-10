document.getElementById('finalizar').addEventListener('click', function() {
    // Obtener valores del formulario
    const Cli_Nombre = document.getElementById('Cli_Nombre').value;
    const Cli_Rut = document.getElementById('Cli_Rut').value;
    const Cli_Apellido = document.getElementById('Cli_Apellido').value;
    const Cli_Direccion = document.getElementById('Cli_Direccion').value;
    const prod_Nombre = document.getElementById('prod_Nombre').value;
    const prod_precio = parseFloat(document.getElementById('prod_precio').value);
    const prod_cantida = parseInt(document.getElementById('prod_cantida').value);

    // Validar que los campos obligatorios no estén vacíos
    if (!Cli_Nombre|| !Cli_Rut || !prod_Nombre || isNaN(prod_precio) || isNaN(prod_cantidad)) {
        alert('Por favor, complete todos los campos obligatorios y asegúrese de que el precio y la cantidad sean números válidos.');
        return
    }

    // Calcular el total
    const total = prod_precio * prod_cantida;

    // Crear objeto de detalles del usuario
    const Detalle_cli = {
        Cli_Nombre,
        Cli_Rut,
        Cli_Apellido,
        Cli_Direccion,

        prod_Nombre,
        prod_precio,
        prod_cantidad,
        total
    };

    // Guardar en el almacenamiento local
    localStorage.setItem('Detalle_cli', JSON.stringify(Detalle_cli));

    // Redirigir a la página de boleta
    window.location.href="Boleta.html"
});
