const express = require('express');
const app = express();
const cors = require('cors');
const bodyParser = require('body-parser');
const pool = require('./API/src/Database/index');
const port = 4001;

// MIDDLEWARES
app.use(cors());
app.use(bodyParser.json());

// ROUTES
const authRouter = require('./API/src/Router/authRouter');
app.use('/api', authRouter);

console.log('Iniciando conexión a la base de datos...');
pool.connect();

// Iniciar el servidor
app.listen(port, () => {
  console.log(`Servidor escuchando en ${port}`);
});
