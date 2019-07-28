using System;
using System.Collections.Generic;
using System.Text;

namespace Clases
{
    public class TipoDeProducto
    {
        private int idTipoDeProducto;
        private string descripcion;
        private string nombre;

        /// <summary>
        /// id del tipo de producto
        /// </summary>
        public int IdTipoDeProducto { get => idTipoDeProducto; set => idTipoDeProducto = value; }

        /// <summary>
        /// descripcion del tipo de producto
        /// </summary>
        public string Descripcion { get => descripcion; set => descripcion = value; }

        /// <summary>
        /// nombre del tipo de´producto
        /// </summary>
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
