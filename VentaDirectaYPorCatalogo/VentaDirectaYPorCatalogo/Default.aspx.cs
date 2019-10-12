using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDeDatos;
using Clases;

namespace VentaDirectaYPorCatalogo
{
    public partial class _Default : Page
    {
        LinkButton lbIniciarSession;
        LinkButton lblNombreDelUsuario;
        DataTable tabla;
        List<Producto> productos = new List<Producto>();
        protected void Page_Load(object sender, EventArgs e)
        {
            lbIniciarSession = (LinkButton)Master.FindControl("lbIniciarSession");
            lblNombreDelUsuario = (LinkButton)Master.FindControl("lblNombreDelUsuario");
            if (!Page.IsPostBack)
            {
                if (Session["Session"] != null && Session["Usuario"] != null)
                {
                    lbIniciarSession.Text = "Cerrar sessión";
                    var usuario = (Usuario)Session["Usuario"];
                    lblNombreDelUsuario.Text = usuario.User;
                }
                else
                {
                    lbIniciarSession.Text = "Iniciar sessión";
                    lblNombreDelUsuario.Text = "";
                }
                CargarTiposDeProducto();
                CargarCatalogos();
                BuscarProductos(null, null, "Asc");
            }
            else
            {
                if (Session["Session"] != null && Session["Usuario"] != null)
                {
                    lbIniciarSession.Text = "Cerrar sessión";
                    var usuario = (Usuario)Session["Usuario"];
                    lblNombreDelUsuario.Text = usuario.User;
                }
                else
                {
                    lbIniciarSession.Text = "Iniciar sessión";
                    lblNombreDelUsuario.Text = "";
                }
            }
            Mostrar();
        }

        private void BuscarProductos(float? precioDesde, float? precioHasta, string orden)
        {
            Session["imagenes"] = null;
            OrganizarProducto organizarProducto = new OrganizarProducto();
            Producto producto = new Producto();
            producto.Nombre = txtNombreDelProducto.Text;
            if(ddlTipoDeProducto.SelectedValue != "0")
            {
                producto.TipoDeProducto = new TipoDeProducto();
                producto.TipoDeProducto.IdTipoDeProducto = Convert.ToInt32(ddlTipoDeProducto.SelectedValue);
            }
            else{
                producto.TipoDeProducto = null;
            }
            if (ddlCatalogo.SelectedValue != "0")
            {
                producto.Catalogo = new Catalogo();
                producto.Catalogo.IdCatalogo = Convert.ToInt32(ddlCatalogo.SelectedValue);
            }
            else
            {
                producto.Catalogo = null;
            }
            tabla = organizarProducto.BuscarProductos(producto, precioDesde, precioHasta, ddlOrdenar.SelectedValue);
            foreach(DataRow fila in tabla.Rows)
            {
                producto = new Producto();
                producto.IdProducto = Convert.ToInt32(fila["Codigo"]);
                producto.Nombre = (string)fila["nombre"];
                producto.Precio = float.Parse(fila["precio"].ToString());
                if(fila["imagen"] != DBNull.Value)
                    producto.Imagen = (string)fila["imagen"];
                productos.Add(producto);
            }
            Session["imagenes"] = productos;
        }

        /// <summary>
        /// Carga los Catalogos en el combo
        /// </summary>
        private void CargarCatalogos()
        {
            OrganizarCatalogo organizarCatalogo = new OrganizarCatalogo();
            var auxiliar = new Catalogo();
            auxiliar.Nombre = "";
            DataTable tabla = organizarCatalogo.BuscarCatalogos(auxiliar);
            ddlCatalogo.AppendDataBoundItems = true;
            ddlCatalogo.Items.Add(new ListItem("-- Seleccione --", "0", true));
            ddlCatalogo.DataSource = tabla;
            ddlCatalogo.DataValueField = "codigo";
            ddlCatalogo.DataTextField = "nombre";
            ddlCatalogo.DataBind();
        }

        /// <summary>
        /// Carga los tipos de producto en el combo
        /// </summary>
        private void CargarTiposDeProducto()
        {
            OrganizarTipoDeProducto organizarTipoDeProducto = new OrganizarTipoDeProducto();
            var auxiliar = new TipoDeProducto();
            auxiliar.Nombre = "";
            DataTable tabla = organizarTipoDeProducto.BuscarTiposDeProducto(auxiliar);
            ddlTipoDeProducto.AppendDataBoundItems = true;
            ddlTipoDeProducto.Items.Add(new ListItem("-- Seleccione --", "0", true));
            ddlTipoDeProducto.DataSource = tabla;
            ddlTipoDeProducto.DataValueField = "codigo";
            ddlTipoDeProducto.DataTextField = "nombre";
            ddlTipoDeProducto.DataBind();
        }

        /// <summary>
        /// Muestra las imagenes
        /// </summary>
        public void Mostrar()
        {
            List<Producto> productos = new List<Producto>();
            imagenes.Controls.Clear();
            if (Session["imagenes"] != null)
            {
                productos = (List<Producto>)Session["imagenes"];
                foreach (Producto producto in productos)
                {
                    Panel columna = new Panel();
                    columna.ID = "columna" + producto.IdProducto;
                    columna.CssClass = "col-md-5";
                    columna.Style.Add("padding", "20px");
                    Panel panel = new Panel();
                    panel.ID = "pnl" + producto.IdProducto;
                    panel.CssClass = "thumbnail";
                    panel.Height = 250;
                    panel.Width = 250;
                    LinkButton a = new LinkButton();
                    Image imagen = new Image();
                    imagen.ID = "img" + producto.IdProducto;
                    imagen.Width = 250;
                    imagen.Height = 250;
                    if (producto.Imagen != null)
                    {
                        string[] imagenes = producto.Imagen.Split(',');
                        imagen.ImageUrl = "~/imagenes/" + imagenes[0];
                    }
                    else
                    {
                        imagen.ImageUrl = "#";
                    }
                    Label nombre = new Label();
                    nombre.Text = producto.Nombre;
                    Label precio = new Label();
                    precio.Text = "$" + producto.Precio.ToString();
                    Table tabla = new Table();
                    tabla.ID = "tabla" + producto.IdProducto;
                    panel.Controls.Add(imagen);
                    TableRow fila = new TableRow();
                    fila.ID = "Fila1" + producto.IdProducto;
                    TableCell celda = new TableCell();
                    celda.ID = "celda1" + producto.IdProducto;
                    celda.Controls.Add(imagen);
                    fila.Controls.Add(celda);
                    TableRow fila1 = new TableRow();
                    fila1.ID = "Fila2" + producto.IdProducto;
                    TableCell celda1 = new TableCell();
                    celda1.ID = "celda2" + producto.IdProducto;
                    celda1.Controls.Add(nombre);
                    fila1.Controls.Add(celda1);
                    TableRow fila2 = new TableRow();
                    fila2.ID = "Fila3" + producto.IdProducto;
                    TableCell celda2 = new TableCell();
                    celda2.ID = "celda3" + producto.IdProducto;
                    celda2.Controls.Add(precio);
                    fila2.Controls.Add(celda2);
                    tabla.Controls.Add(fila);
                    tabla.Controls.Add(fila1);
                    tabla.Controls.Add(fila2);
                    panel.Controls.Add(tabla);
                    a.Controls.Add(panel);
                    a.ResolveUrl("#");
                    columna.Controls.Add(a);
                    imagenes.Controls.Add(columna);
                }
            }
        }

        protected void ddlTipoDeProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscarProductos(null, null, ddlOrdenar.SelectedValue);
            Mostrar();
        }

        protected void ddlCatalogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscarProductos(null, null, ddlOrdenar.SelectedValue);
            Mostrar();
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            float? desde;
            float? hasta;
            if (txtDesde.Text != "")
                desde = float.Parse(txtDesde.Text);
            else
                desde = null;
            if (txtHasta.Text != "")
                hasta = float.Parse(txtHasta.Text);
            else
                hasta = null;
            BuscarProductos(desde, hasta, ddlOrdenar.SelectedValue);
            Mostrar();
        }
    }
}