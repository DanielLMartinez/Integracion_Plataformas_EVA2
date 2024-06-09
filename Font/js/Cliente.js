document.getElementById('finalize').addEventListener('click', function() {
    const userName = document.getElementById('user-name').value;
    const userRut = document.getElementById('user-rut').value;
    const userLastName = document.getElementById('user-lastname').value;
    const userAddress = document.getElementById('user-address').value;
    const productName = document.getElementById('product-name').value;
    const productPrice = document.getElementById('product-price').value;
    const productQuantity = document.getElementById('product-quantity').value;
    const total = productPrice * productQuantity;

    const userDetails = {
        userName,
        userRut,
        userLastName,
        userAddress,
        productName,
        productPrice,
        productQuantity,
        total
    };

    localStorage.setItem('userDetails', JSON.stringify(userDetails));
    window.location.href = 'boleta.html';
});
