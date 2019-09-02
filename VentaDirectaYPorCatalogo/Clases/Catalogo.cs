using System;
using System.Collections.Generic;
using System.Text;

namespace Clases
{
    public class Catalogo
    {
        private int idCatalogo;
        private DateTime fecha;
        private string temporada;
        private string nombre;
        private List<Producto> productos;

        /// <summary>
        /// Id del catalogo
        /// </summary>
        public int IdCatalogo { get => idCatalogo; set => idCatalogo = value; }

        /// <summary>
        /// fecha de inicio del catalogo
        /// </summary>
        public DateTime Fecha { get => fecha; set => fecha = value; }

        /// <summary>
        /// Temporada del catalogo
        /// </summary>
        public string Temporada { get => temporada; set => temporada = value; }

        /// <summary>
        /// Nombre del catalogo
        /// </summary>
        public string Nombre { get => nombre; set => nombre = value; }

        /// <summary>
        /// Productos de un catalogo
        /// </summary>
        public List<Producto> Productos { get => productos; set => productos = value; }
    }
}
