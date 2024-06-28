var pool = require('../../Database/index');

const registraUsuario = async (rut_empleado, username, password, pnombre, snombre, papellido, sapellido) => {
  const client = await pool.connect();
  try {
    await client.query('BEGIN');

    // Insertar en la tabla Usuario
    const insertUsuario = 
      'INSERT INTO Usuario (nombre_usuario, rut_empleado, pnombre_empleado, snombre_empleado, papellido_empleado, sapellido_empleado) VALUES ($1, $2, $3, $4, $5, $6) RETURNING id_usuario';
    const resultUsuario = await client.query(insertUsuario, [username, rut_empleado, pnombre, snombre, papellido, sapellido]);
    const userId = resultUsuario.rows[0].id_usuario;

    // Insertar en la tabla Contraseña
    const insertContrasena = 
      'INSERT INTO Contraseña (id_usuario, contrasenia_hash) VALUES ($1, $2)';
    await client.query(insertContrasena, [userId, password]);

    await client.query('COMMIT');
    return userId;
  } catch (error) {
    await client.query('ROLLBACK');
    throw error; 
  } finally {
    client.release();
  }
};

/*
{
  "username": "prueba",
  "rut": "12345678-9",
  "password": "12345",
  "pnombre" : "pnombrePrueba",
  "snombre" : "snombrePrueba",
  "papellido": "papellidoPrueba",
  "sapellido": "sapellidoPrueba"
}
*/

const getUserByUsername = async (username) => {
  const client = await pool.connect();
  const queryString =
      'SELECT u.id_usuario, u.nombre_usuario, c.contrasenia_hash ' +
      'FROM usuario u ' +
      'INNER JOIN contraseña c ON u.id_usuario = c.id_usuario ' +
      'WHERE u.nombre_usuario = $1';
  console.log(queryString); // Imprime la consulta SQL
  const result = await client.query(queryString, [username]);
  console.log(result.rows); // Imprime el resultado de la consulta
  return result.rows[0];
};

const generateToken = (userId) => {
  const secretKey = process.env.JWT_SECRET;
  return jwt.sign({ userId }, secretKey, { expiresIn: '1h' });
};

module.exports = {
  registraUsuario,
  getUserByUsername,
  generateToken,
};
