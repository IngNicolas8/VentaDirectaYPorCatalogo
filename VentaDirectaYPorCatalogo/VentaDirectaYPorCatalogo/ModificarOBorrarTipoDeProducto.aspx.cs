using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDeDatos;
using System.Data;

namespace VentaDirectaYPorCatalogo
{
    public partial class ModificarOBorrarTipoDeProducto : System.Web.UI.Page
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
                TipoDeProducto tipoDeProducto = new TipoDeProducto();
                OrganizarTipoDeProducto organizarTipoDeProducto = new OrganizarTipoDeProducto();
                tipoDeProducto.Nombre = txNombre.Text;
                Limpiar();
                DataTable tiposDeProductos = organizarTipoDeProducto.BuscarTiposDeProducto(tipoDeProducto);
                if (tiposDeProductos.Rows.Count != 0)
                {
                    Limpiar();
                    gvTiposDeProducto.DataSource = tiposDeProductos;
                    gvTiposDeProducto.DataKeyNames = new string[] { "nombre" };
                    gvTiposDeProducto.DataBind();
                }
                else
                {
                    Limpiar();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('No se encontro el tipo de producto');", true);
                }
                //Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('No se encontro el tipo de producto o  ocurrio una situacion, pruebe mas tarde');", true);
            }
        }

        /// <summary>
        /// Metodo que limpia los campos
        /// </summary>
        private void Limpiar()
        {
            gvTiposDeProducto.DataSource = null;
            gvTiposDeProducto.DataBind();
            txNombre.Text = "";
        }

        protected void gvTiposDeProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["tipoDeProductoAModificar"] = gvTiposDeProducto.SelectedValue.ToString();
            Response.Redirect("RegistrarTipoDeProducto.aspx");
        }
    }
}