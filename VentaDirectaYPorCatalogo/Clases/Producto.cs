using System;
using System.Collections.Generic;
using System.Text;

namespace Clases
{
    public class Producto
    {
        private int idProducto;
        private string nombre;
        private Catalogo catalogo;
        private string imagen;
        private float precio;
        private TipoDeProducto tipoDeProducto;

        /// <summary>
        /// id del producto
        /// </summary>
        public int IdProducto { get => idProducto; set => idProducto = value; }

        /// <summary>
        /// Nombre del producto
        /// </summary>
        public string Nombre { get => nombre; set => nombre = value; }

        /// <summary>
        /// Catalogoal que pertenece el producto
        /// </summary>
        public Catalogo Catalogo { get => catalogo; set => catalogo = value; }

        /// <summary>
        /// direcciones de las imagenes
        /// </summary>
        public string Imagen { get => imagen; set => imagen = value; }

        /// <summary>
        /// Precio del producto
        /// </summary>
        public float Precio { get => precio; set => precio = value; }

        /// <summary>
        /// Tipo del producto
        /// </summary>
        public TipoDeProducto TipoDeProducto { get => tipoDeProducto; set => tipoDeProducto = value; }
    }
}
