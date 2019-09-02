using BaseDeDatos;
using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VentaDirectaYPorCatalogo
{
    public partial class AdministrarProductosPorCatalogo : System.Web.UI.Page
    {
        LinkButton lbIniciarSession;
        LinkButton lblNombreDelUsuario;
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
                CargarDatosEnComboCatalogo();
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
        }

        /// <summary>
        /// Carga los catalogos en el combo
        /// </summary>
        private void CargarDatosEnComboCatalogo()
        {
            OrganizarCatalogo organizarCatalogo = new OrganizarCatalogo();
            var auxiliar = new Catalogo();
            auxiliar.Nombre = "";
            DataTable tabla = organizarCatalogo.BuscarCatalogos(auxiliar);
            ddlCatalogo.AppendDataBoundItems = true;
            ddlCatalogo.Items.Add(new ListItem("-- Seleccione --", "0", true));
            ddlCatalogo.DataSource = tabla;
            ddlCatalogo.DataValueField = "idCatalogo";
            ddlCatalogo.DataTextField = "nombre";
            ddlCatalogo.DataBind();
        }

        protected void ddlCatalogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlCatalogo.SelectedValue.Equals("0"))
            {
                OrganizarProducto organizarProducto = new OrganizarProducto();
                Catalogo catalogo = new Catalogo();
                catalogo.IdCatalogo = Convert.ToInt32(ddlCatalogo.SelectedValue.ToString());
                DataTable tabla = organizarProducto.BuscarProductosPorCatalogo(catalogo);
                lbProductosDelCatalogo.DataSource = tabla;
                lbProductosDelCatalogo.DataTextField = "nombre";
                lbProductosDelCatalogo.DataValueField = "Codigo";
                lbProductosDelCatalogo.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('Debe seleccionar un catalogo');", true);
            }
        }
        
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();
                OrganizarProducto organizarProducto = new OrganizarProducto();
                producto.Nombre = txNombre.Text;
                Limpiar();
                DataTable catalogos = organizarProducto.BuscarProductos(producto);
                if (catalogos.Rows.Count != 0)
                {
                    Limpiar();
                    
                    lbProducto.DataSource = catalogos;
                    lbProducto.DataTextField = "nombre";
                    lbProducto.DataValueField = "Codigo";
                    lbProducto.DataBind();
                }
                else
                {
                    Limpiar();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('No se encontro el Producto');", true);
                }
                //Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('No se encontro el catalogo o  ocurrio una situacion, pruebe mas tarde');", true);
            }
        }

        /// <summary>
        /// Metodo que limpia los campos
        /// </summary>
        private void Limpiar()
        {
            lbProducto.DataSource = null;
            lbProducto.DataBind();
            txNombre.Text = "";
            lbProductosDelCatalogo.DataSource = null;
            lbProductosDelCatalogo.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (lbProducto.SelectedItem != null)
            {
                if (!lbProductosDelCatalogo.Items.Contains(lbProducto.SelectedItem))
                {
                    lbProductosDelCatalogo.Items.Add(lbProducto.SelectedItem);
                    lbProducto.Items.Remove(lbProducto.SelectedItem);
                }
                else
                {
                    lbProducto.Items.Remove(lbProducto.SelectedItem);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('Debe seleccionar el Producto a agregar');", true);
            }
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            if(lbProductosDelCatalogo.SelectedItem != null)
            {
                if (!lbProducto.Items.Contains(lbProductosDelCatalogo.SelectedItem))
                {
                    lbProducto.Items.Add(lbProductosDelCatalogo.SelectedItem);
                    lbProductosDelCatalogo.Items.Remove(lbProductosDelCatalogo.SelectedItem);
                }
                else
                {
                    lbProductosDelCatalogo.Items.Remove(lbProductosDelCatalogo.SelectedItem);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('Debe seleccionar el producto a quitar el Producto');", true);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Producto> productos = new List<Producto>();
                foreach (ListItem item in lbProductosDelCatalogo.Items)
                {
                    Producto producto = new Producto();
                    producto.Nombre = item.Text;
                    producto.IdProducto = Convert.ToInt32(item.Value);
                    productos.Add(producto);
                }
                Catalogo catalogo = new Catalogo();
                catalogo.IdCatalogo = Convert.ToInt32(ddlCatalogo.SelectedValue.ToString());
                catalogo.Productos = productos;
                OrganizarCatalogo.ModificarProductosDeCatalogo(catalogo);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('No se pudo modificar los productos del catalogo o  ocurrio una situacion, pruebe mas tarde');", true);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Response.Redirect("Default.aspx");
        }

        protected void lbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lbProductosDelCatalogo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}