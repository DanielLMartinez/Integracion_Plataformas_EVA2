const userDetails = JSON.parse(localStorage.getItem('userDetails'));

if (userDetails) {
    document.getElementById('boleta-details').innerHTML = `
        <div class="detail"><strong>Nombre:</strong> ${userDetails.userName}</div>
        <div class="detail"><strong>RUT:</strong> ${userDetails.userRut}</div>
        <div class="detail"><strong>Apellido:</strong> ${userDetails.userLastName}</div>
        <div class="detail"><strong>Dirección:</strong> ${userDetails.userAddress}</div>
        <div class="detail"><strong>Producto:</strong> ${userDetails.productName}</div>
        <div class="detail"><strong>Precio:</strong> ${userDetails.productPrice}</div>
        <div class="detail"><strong>Cantidad:</strong> ${userDetails.productQuantity}</div>
        <div class="detail"><strong>Total:</strong> ${userDetails.total}</div>
    `;
} else {
    document.getElementById('boleta-details').innerHTML = '<p>No hay detalles disponibles.</p>';
}
