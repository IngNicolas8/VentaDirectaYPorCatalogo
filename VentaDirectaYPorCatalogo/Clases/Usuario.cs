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

        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string User { get => user; set => user = value; }

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        public string Contraseña { get => contraseña; set => contraseña = value; }
    }
}
