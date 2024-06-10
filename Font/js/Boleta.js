// Función para obtener y mostrar productos disponibles
function obtenerProductos() {
    fetch('http://localhost:5001/api/producto')
        .then(response => response.json())
        .then(data => {
            const productosDiv = document.getElementById('productos');
            productosDiv.innerHTML = '<h2> Productos Disponibles </h2>';

            data.forEach(producto => {
                const divProducto = document.createElement('div');
                divProducto.innerHTML = `
                    <input type="checkbox" id="producto_${producto.id}" value="${producto.id}">
                    <label for="producto_${producto.id}">${producto.nombre} - ${producto.precio}</label>
                `;
                productosDiv.appendChild(divProducto);
            });
        })
        .catch(error => console.error('Error al obtener productos:', error));
}

// Función para generar la boleta
function generarBoleta() {
    const productosSeleccionados = Array.from(document.querySelectorAll('input[type="checkbox"]:checked')).map(input => input.value);
    
    fetch('http://localhost:5001/api/boleta', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ productos: productosSeleccionados })
    })
    .then(response => response.json())
    .then(data => {
        const boletaDiv = document.getElementById('boleta');
        boletaDiv.innerHTML = '<h2> Boleta Generada </h2>';
        
        // Mostrar detalles de la boleta
        const detallesBoleta = document.createElement('pre');
        detallesBoleta.textContent = JSON.stringify(data, null, 2);
        boletaDiv.appendChild(detallesBoleta);
    })
    .catch(error => console.error('Error al generar boleta:', error));
}

// Llama a la función para obtener productos al cargar la página
window.onload = obtenerProductos;

// Agregar evento al botón "Generar"
document.getElementById('Generar').addEventListener('click', function() {
    // Al hacer clic en "Generar", generamos la boleta
    generarBoleta();
});
