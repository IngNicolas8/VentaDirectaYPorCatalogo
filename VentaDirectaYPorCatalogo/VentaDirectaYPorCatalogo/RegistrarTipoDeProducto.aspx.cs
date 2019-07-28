using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDeDatos;
using Clases;

namespace VentaDirectaYPorCatalogo
{
    public partial class RegistrarTipoDeProducto : System.Web.UI.Page
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
                if (Session["tipoDeProductoAModificar"] == null)
                {
                    Page.Title = "Registrar Usuario";
                    titulo.InnerText = "Registrar Usuario";
                }
                else
                {
                    Page.Title = "Modificar o Borrar Tipo de Producto";
                    titulo.InnerText = "Modificar o Borrar Tipo de Producto";
                    CargarDatos();
                    btnBorrar.Visible = true;
                    divBorrar.Visible = true;
                }
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
        
        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (Session["tipoDeProductoAModificar"] == null)
            {
                try
                {
                    TipoDeProducto tipoDeProducto = new TipoDeProducto();
                    tipoDeProducto.Descripcion = txtDescripcion.Text;
                    tipoDeProducto.Nombre = txtNombre.Text;
                    btnBorrar.Visible = true;
                    divBorrar.Visible = true;
                    OrganizarTipoDeProducto organizarTipoDeProducto= new OrganizarTipoDeProducto();
                    organizarTipoDeProducto.RegistrarTipoDeProducto(tipoDeProducto);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El tipo de producto se registro correctamente');", true);
                    Limpiar();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('" + ex.Message + "');", true);
                }
            }
            else
            {
                try
                {
                    TipoDeProducto tipoDeProducto = new TipoDeProducto();
                    OrganizarTipoDeProducto organizarTipoDeProducto = new OrganizarTipoDeProducto();
                    tipoDeProducto.IdTipoDeProducto = (int)Session["idTipoDeProducto"];
                    tipoDeProducto.Nombre = txtNombre.Text;
                    tipoDeProducto.Descripcion = txtDescripcion.Text;
                    organizarTipoDeProducto.ModificarTipoDeProductoAModificar(tipoDeProducto);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El tipo de producto se actualizo correctamente');", true);
                    Limpiar();
                    Page.Title = "Registrar tipo de producto";
                    titulo.InnerText = "Registrar tipo de producto";
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('" + ex.Message + "');", true);
                }
            }
        }

        private void CargarDatos()
        {
            OrganizarTipoDeProducto organizarTipoDeProducto = new OrganizarTipoDeProducto();
            TipoDeProducto tipoDeProducto = new TipoDeProducto();
            tipoDeProducto.Nombre = (string)Session["tipoDeProductoAModificar"];
            organizarTipoDeProducto.BuscarTipoDeProducto(ref tipoDeProducto);
            txtDescripcion.Text = tipoDeProducto.Descripcion;
            txtNombre.Text = tipoDeProducto.Nombre;
            Session["idTipoDeProducto"] = tipoDeProducto.IdTipoDeProducto;
        }

        private void Limpiar()
        {
            txtDescripcion.Text = "";
            txtNombre.Text = "";
            Session["tipoDeProductoAModificar"] = null;
            btnBorrar.Visible = false;
            divBorrar.Visible = false;
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            TipoDeProducto tipoDeProducto = new TipoDeProducto();
            tipoDeProducto.Nombre = (string)Session["tipoDeProductoAModificar"];
            tipoDeProducto.IdTipoDeProducto = (int)Session["idTipoDeProducto"];
            OrganizarTipoDeProducto.BorrarTipoDeProducto(tipoDeProducto);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El tipo de producto se Borro correctamente');", true);
            Limpiar();
            Page.Title = "Registrar tipo de producto";
            titulo.InnerText = "Registrar tipo de producto";
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Response.Redirect("Default.aspx");
        }
    }
}