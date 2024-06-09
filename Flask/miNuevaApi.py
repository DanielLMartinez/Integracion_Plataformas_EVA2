from flask import Flask, request
from flask_restful import Api, Resource

app = Flask(__name__)
api = Api(app)

clientes = {}

class Cliente(Resource):
    def get(self, cliente_id):
        return {'cliente_id': clientes[cliente_id]}
    def put(self, cliente_id):
        clientes[cliente_id] = request.get_json()
        return {'cliente_id': clientes[cliente_id]}
    
api.add_resource(Cliente, '/<string:cliente_id>')

if __name__ == '__main__':
    app.run(debug=True)