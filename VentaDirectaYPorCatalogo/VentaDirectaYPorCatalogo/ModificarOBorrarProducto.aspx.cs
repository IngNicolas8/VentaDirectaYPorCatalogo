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
    public partial class ModificarOBorrarProducto : System.Web.UI.Page
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
                Page.Title = "Modificar o Eliminar Usuario";
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();
                OrganizarProducto organizarProducto = new OrganizarProducto();
                producto.Nombre = txNombre.Text;
                Limpiar();
                DataTable catalogos = organizarProducto.BuscarProductos(producto, null, null, "");
                if (catalogos.Rows.Count != 0)
                {
                    Limpiar();
                    gvProducto.DataSource = catalogos;
                    gvProducto.DataKeyNames = new string[] { "nombre" };
                    gvProducto.DataBind();
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
            gvProducto.DataSource = null;
            gvProducto.DataBind();
            txNombre.Text = "";
        }

        protected void gvProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["productoAModificar"] = gvProducto.SelectedValue.ToString();
            Response.Redirect("RegistrarProducto.aspx");
        }
    }
}