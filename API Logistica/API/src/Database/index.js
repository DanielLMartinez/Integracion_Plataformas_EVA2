require("dotenv").config();
const { Pool } = require("pg");

const pool = new Pool({
  host: process.env.DB_HOST,
  port: process.env.DB_PORT,
  user: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
  database: process.env.DB_NAME,
});

pool.connect()
  .then(() => console.log("Conectado a PostgreSQL..."))
  .catch((error) => {
    console.error("Error de conexión a PostgreSQL:", error.message);
  });

module.exports = pool;