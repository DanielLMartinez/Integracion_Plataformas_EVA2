from flask import Flask, request, jsonify, send_from_directory
from flask_restful import Api, Resource
import requests
import random
from flask_cors import CORS
import os

app = Flask(__name__)
api = Api(app)
cors = CORS(app, resources={r"/api/*": {"origins": "*"}})

url_cliente = "http://localhost:5009/api/Cliente"
url_producto = "http://localhost:5009/api/Producto"

def generar_id_boleta():
    return str(random.randint(10000000, 99999999))

class Factura(Resource):
    def post(self):
        objRespuesta = {
            "id_boleta": generar_id_boleta(),
            "items": [],
            "cliente": {},
            "total_boleta": 0
        }

        data = request.get_json()
        required_keys = ["Rut", "id_producto", "cantidad"]
        if not all(key in data for key in required_keys):
            return {"error": "Faltan claves en la solicitud. Se requieren 'Rut', 'id_producto', y 'cantidad'."}, 400

        cliente_response = requests.get(url_cliente + "/" + str(data["Rut"]))
        if cliente_response.status_code != 200:
            return {"error": f"No se pudo obtener el cliente con Rut {data['Rut']}"}, cliente_response.status_code

        cliente_json = cliente_response.json()
        producto_response = requests.get(url_producto + "/" + str(data["id_producto"]))
        if producto_response.status_code != 200:
            return {"error": f"No se pudo obtener el producto con id {data['id_producto']}"}, producto_response.status_code

        producto_json = producto_response.json()

        item = {
            "id_producto": producto_json["id"],
            "nombre_producto": producto_json["nombre"],
            "precio_unitario": producto_json["precio"],
            "cantidad": data["cantidad"],
            "total_item": int(producto_json["precio"]) * int(data["cantidad"])
        }
        objRespuesta["items"].append(item)

        objRespuesta["cliente"] = {
            "id_cliente": cliente_json["rut"],
            "nombre_cliente": cliente_json["nombre"] + " " + cliente_json["apellido"],
            "direccion_cliente": cliente_json["direccion"]
        }

        objRespuesta["total_boleta"] = sum(item["total_item"] for item in objRespuesta["items"])

        return jsonify(objRespuesta)

api.add_resource(Factura, "/api/boleta")

@app.route('/')
def index():
    return """
    <!DOCTYPE html>
    <html lang="es">

    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Cliente y Productos</title>
        <style>
        </style>
    </head>

    <body>
        <div class="form-section">
            <h2>Cliente</h2>
            <label for="Cli_Rut">RUT:</label>
            <input type="text" id="Cli_Rut">

            <label for="Cli_Nombre">Nombre:</label>
            <input type="text" id="Cli_Nombre">

        </div>

        <div class="form-section">
            <h2>Producto</h2>
            <label for="id_producto">ID del Producto:</label>
            <input type="number" id="id_producto">

            <label for="prod_cantidad">Cantidad:</label>
            <input type="number" id="prod_cantidad">

            <button id="submit">Generar Boleta</button>
        </div>

        <div class="form-section" id="resultado">
            <h2>Boleta</h2>
            <div id="boleta"></div>
        </div>

        <script>
            document.getElementById("submit").addEventListener("click", function () {
                const rut = document.getElementById("Cli_Rut").value;
                const idProducto = document.getElementById("id_producto").value;
                const cantidad = document.getElementById("prod_cantidad").value;

                if (!rut || !idProducto || !cantidad) {
                    alert("Por favor complete todos los campos");
                    return;
                }

                const data = {
                    Rut: rut,
                    id_producto: idProducto,
                    cantidad: cantidad
                };

                fetch("http://localhost:5001/api/boleta", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(data)
                })
                    .then(response => response.json())
                    .then(data => {
                        const boletaElement = document.getElementById("boleta");
                        boletaElement.innerHTML = `
                            <p><strong>ID Boleta:</strong> ${data.id_boleta}</p>
                            <p><strong>Cliente:</strong></p>
                            <ul>
                                <li><strong>ID Cliente:</strong> ${data.cliente.id_cliente}</li>
                                <li><strong>Nombre Cliente:</strong> ${data.cliente.nombre_cliente}</li>
                                <li><strong>Dirección Cliente:</strong> ${data.cliente.direccion_cliente}</li>
                            </ul>
                            <p><strong>Items:</strong></p>
                            <ul>
                                ${data.items.map(item => `
                                    <li>
                                        <strong>ID Producto:</strong> ${item.id_producto}<br>
                                        <strong>Nombre Producto:</strong> ${item.nombre_producto}<br>
                                        <strong>Precio Unitario:</strong> ${item.precio_unitario}<br>
                                        <strong>Cantidad:</strong> ${item.cantidad}<br>
                                        <strong>Total Item:</strong> ${item.total_item}
                                    </li>
                                `).join('')}
                            </ul>
                            <p><strong>Total Boleta:</strong> ${data.total_boleta}</p>
                        `;
                        document.getElementById("resultado").style.display = "block";
                    })
                    .catch(error => {
                        console.error("Error:", error);
                    });
            });
        </script>
    </body>

    </html>
    """

@app.route('/api/boleta', methods=['POST'])
def generate_bill():
    data = request.json
    return jsonify({"example": "response"})

if __name__ == '__main__':
    app.run(debug=True)
