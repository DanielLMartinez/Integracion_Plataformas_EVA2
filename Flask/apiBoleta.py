from flask import Flask, request, jsonify, render_template
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
def principal():
    return render_template('Principal.html')

@app.route('/index.html')
def cliente():
    return render_template('index.html')

@app.route('/Login.html')
def trabajador():
    return render_template('Login.html')

if __name__ == '__main__':
    app.run(debug=True, port=5001)
