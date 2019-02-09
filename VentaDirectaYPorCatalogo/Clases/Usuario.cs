using System;
using System.Collections.Generic;
using System.Text;

namespace Clases
{
    /// <summary>
    /// Clase con los datos del usuario
    /// </summary>
    public class Usuario
    {
        private string user;
        private string contraseña;
        private string rol;

        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string User { get => user; set => user = value; }

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        public string Contraseña { get => contraseña; set => contraseña = value; }

        /// <summary>
        /// Rol del usuario
        /// </summary>
        public string Rol { get => rol; set => rol = value; }

        /// <summary>
        /// constructor sin parametros
        /// </summary>
        public Usuario() { }

        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="usuario">usuario</param>
        /// <param name="pass">cpntraseña</param>
        public Usuario(string usuario, string pass)
        {
            User = usuario;
            Contraseña = pass;
        }
    }
}
