from flask import Flask, request

app = Flask(__name__)

## APP de Prueba para API Cliente
listaFactura = []
listaCliente = []
listaProducto = []

@app.get("/cliente")
def getClientes():
    return listaCliente

@app.get("/cliente/<id>")
def getCliente(id):
    return listaCliente.index(id)

@app.post("/cliente")
def insertaCliente():
    json = request.get_json()
    
    cliente = {
        "id": json["id"],
        "razonSocial": json["razonSocial"],
        "rut": json["rut"],
        "direccion": json["direccion"] 
    }
    print(cliente)

    listaCliente.append(cliente)
    return "Cliente Creado"

@app.put("/cliente/<id>")
def actualizaCliente(id):
    index = int(id)
    json = request.get_json()

    cliente = {
        "id": json["id"],
        "razonSocial": json["razonSocial"],
        "rut": json["rut"],
        "direccion": json["direccion"] 
    }

    if listaCliente and 0 <= index < len(listaCliente):
        listaCliente.pop(index)
        listaCliente.append(cliente)
        return "Cliente Actualizado"
    else:
        return "Error al actualizar: lista vacía o índice inválido", 400

@app.delete("/cliente")
def borrarClientes():
    listaCliente.clear()
    return "Clientes Borrados"

@app.delete("/cliente/<id>")
def borrarCliente(id):
    index = int(id) - 1
    listaCliente.pop(index)
    return "Cliente " + id +  " Borrado"


### Aquí inicia Producto


@app.get("/producto")
def getProductos():
    return listaProducto

@app.get("/producto/<id>")
def getProducto(id):
    return listaProducto.index(id)


@app.post("/producto")
def insertaProducto():
    json = request.get_json()
    
    producto = {
        "id": json["id"],
        "nombre": json["nombre"],
        "categoria": json["categoria"],
        "descripcion": json["descripcion"] 
    }
    print(producto)

    listaProducto.append(producto)
    return "Producto Creado"

@app.put("/producto/<id>")
def actualizaProducto(id):
    index = int(id)
    json = request.get_json()
    
    producto = {
        "id": json["id"],
        "nombre": json["nombre"],
        "categoria": json["categoria"],
        "descripcion": json["descripcion"] 
    }
    listaProducto.pop(index)
    listaProducto.append(producto)

    return "Producto Actualizado"

@app.delete("/producto")
def borrarProductos():
    listaProducto.clear()
    return "Productos Borrados"

@app.delete("/producto/<id>")
def borrarProducto(id):
    index = int(id) - 1
    listaProducto.pop(index)
    return "Producto " + id +  " Borrado"

#AQUI INICIA FACTURA

@app.get("/factura")
def getFacturas():
    return listaFactura

@app.get("/factura/<id>")
def getFactura(id):
    factura = next((f for f in listaFactura if f["id"] == id), None)
    if factura:
        return factura
    return "Factura no encontrada", 404

@app.post("/factura")
def insertaFactura():
    json = request.get_json()
    productos_ids = json["productos"]
    productos = []
    total = 0

    for producto_id in productos_ids:
        producto = next((p for p in listaProducto if p["id"] == producto_id), None)
        if producto:
            productos.append(producto)
            total += producto["precio"]
        else:
            return f"Producto con id {producto_id} no encontrado", 404

    factura = {
        "id": json["id"],
        "cliente_id": json["cliente_id"],
        "productos": productos,
        "total": total
    }
    listaFactura.append(factura)
    return "Factura Creada"

@app.delete("/factura/<id>")
def borrarFactura(id):
    index = next((i for i, f in enumerate(listaFactura) if f["id"] == id), None)
    if index is not None:
        listaFactura.pop(index)
        return "Factura Borrada"
    return "Factura no encontrada", 404

if __name__ == "__main__":
    app.run(debug=True)