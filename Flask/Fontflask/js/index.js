// Función para obtener todos los clientes
function getClientes() {
    fetch('/cliente')
        .then(response => response.json())
        .then(data => {
            let clientesDiv = document.getElementById('clientes');
            clientesDiv.innerHTML = JSON.stringify(data, null, 2);
        })
        .catch(error => console.error('Error:', error));
}

// Función para insertar un nuevo cliente
function insertaCliente() {
    const cliente = {
        id: document.getElementById('clienteId').value,
        razonSocial: document.getElementById('razonSocial').value,
        rut: document.getElementById('rut').value,
        direccion: document.getElementById('direccion').value
    };

    fetch('/cliente', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(cliente)
    })
    .then(response => response.text())
    .then(data => {
        alert(data);
        getClientes();
    })
    .catch(error => console.error('Error:', error));
}

// Función para actualizar un cliente
function actualizaCliente() {
    const cliente = {
        id: document.getElementById('updateClienteId').value,
        razonSocial: document.getElementById('updateRazonSocial').value,
        rut: document.getElementById('updateRut').value,
        direccion: document.getElementById('updateDireccion').value
    };

    fetch(`/cliente/${cliente.id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(cliente)
    })
    .then(response => response.text())
    .then(data => {
        alert(data);
        getClientes();
    })
    .catch(error => console.error('Error:', error));
}

// Función para borrar un cliente por ID
function borrarCliente() {
    const id = document.getElementById('deleteClienteId').value;
    fetch(`/cliente/${id}`, {
        method: 'DELETE'
    })
    .then(response => response.text())
    .then(data => {
        alert(data);
        getClientes();
    })
    .catch(error => console.error('Error:', error));
}

// Inicializa la lista de clientes cuando se carga la página
window.onload = function() {
    getClientes();
}
