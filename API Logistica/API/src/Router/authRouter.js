const express = require("express");
const authController = require("../Authentication/Controller/authController");
const authRouter = express.Router();

authRouter.get('/', (req, res) => {
    res.send('Bienvenido a la API');
  });

authRouter.post("/registra-usuario", authController.registraUsuario);

authRouter.post("/authentication", authController.login);


module.exports = authRouter;