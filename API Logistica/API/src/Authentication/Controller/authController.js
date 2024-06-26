const authModel = require('../Model/authModel');
const bcrypt = require('bcrypt');
const e = require('express');
const express = require('express')
const jwt = require('jsonwebtoken');
const RONDA_HASH = 10;

const encriptaPassword = async (password) => {
  return await bcrypt.hash(password, RONDA_HASH);
};

const registraUsuario = async (req, res) => {
  const { rut, username, password, pnombre, snombre, papellido, sapellido, email } = req.body;

  const passEncriptada = await encriptaPassword(password);

  const userId = await authModel.registraUsuario(rut, username, passEncriptada, pnombre, snombre, papellido, sapellido, email);(
    rut,
    username,
    passEncriptada,
    pnombre,
    snombre,
    papellido,
    sapellido,
    email
  );

  res.status(200).send({ id: userId });
};

const login = async (req, res) => {
  const { username, password } = req.body;

  try {
    const resultado = await authModel.getUserByUsername(username);
    const matchPass = await bcrypt.compare(password, resultado.contrasenia_hash);
    if (matchPass) {
      const token = jwt.sign({ username }, process.env.JWT_SECRET, { expiresIn: '1h' });
      res.cookie('token', token, { httpOnly: true })
      return res.status(200).send({ message: "Inicio de sesión exitoso"});
    }

    return res.status(401).send({ error: "No autorizado" });
  } catch (error) {
    console.error('Error en la autenticación:', error.message);
    return res.status(500).send({ error: "Error interno del servidor" });
  }
};

const authenticateToken = (req, res, next) => {
  const token = req.headers.authorization?.split(' ')[1];

  if (!token) {
    return res.status(401).send({ error: "Acceso denegado. Token no encontrado" });
  }

  jwt.verify(token, process.env.JWT_SECRET, (err, user) => {
    if (err) {
      if (err.name === 'TokenExpiredError') {
        return res.status(401).send({ error: "Acceso denegado. Token expirado" });
      }
      return res.status(403).send({ error: "Acceso denegado. Token inválido" });
    }

    req.user = user;
    next();
  });
};

module.exports = {
  login: login,
  registraUsuario: registraUsuario,
  authenticateToken: authenticateToken
};
