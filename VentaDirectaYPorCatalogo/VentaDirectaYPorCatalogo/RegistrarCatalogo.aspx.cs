using BaseDeDatos;
using Clases;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VentaDirectaYPorCatalogo
{
    public partial class RegistrarCatalogo : System.Web.UI.Page
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
                if (Session["catalogoAModificar"] == null)
                {
                    Page.Title = "Registrar Catalogo";
                    titulo.InnerText = "Registrar Catalogo";
                }
                else
                {
                    Page.Title = "Modificar o Borrar Catalogo";
                    titulo.InnerText = "Modificar o Borrar Catalogo";
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
            if (Session["catalogoAModificar"] == null)
            {
                try
                {
                    Catalogo catalogo = new Catalogo();
                    catalogo.Fecha = Convert.ToDateTime(txtFecha.Text);
                    catalogo.Temporada = txtTemporada.Text;
                    catalogo.Nombre = txtNombre.Text;
                    btnBorrar.Visible = true;
                    divBorrar.Visible = true;
                    OrganizarCatalogo organizarCatalogo = new OrganizarCatalogo();
                    organizarCatalogo.RegistrarCatalogo(catalogo);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El catalogo se registro correctamente');", true);
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
                    Catalogo catalogo = new Catalogo();
                    OrganizarCatalogo organizarCatalogo = new OrganizarCatalogo();
                    catalogo.IdCatalogo = (int)Session["idCatalogo"];
                    catalogo.Fecha = Convert.ToDateTime(txtFecha.Text);
                    catalogo.Nombre = txtNombre.Text;
                    catalogo.Temporada = txtTemporada.Text;
                    organizarCatalogo.ModificarCatalogos(catalogo);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El catalogo se actualizo correctamente');", true);
                    Limpiar();
                    Page.Title = "Registrar catalogo";
                    titulo.InnerText = "Registrar catalogo";
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('" + ex.Message + "');", true);
                }
            }
        }

        private void CargarDatos()
        {
            OrganizarCatalogo organizarCatalogo = new OrganizarCatalogo();
            Catalogo catalogo = new Catalogo();
            catalogo.Nombre = (string)Session["catalogoAModificar"];
            organizarCatalogo.BuscarCatalogo(ref catalogo);
            txtFecha.Text = catalogo.Fecha.ToShortDateString();
            txtTemporada.Text = catalogo.Temporada;
            txtNombre.Text = catalogo.Nombre;
            Session["idCatalogo"] = catalogo.IdCatalogo;
        }

        private void Limpiar()
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtTemporada.Text = "";
            txtNombre.Text = "";
            Session["catalogoAModificar"] = null;
            btnBorrar.Visible = false;
            divBorrar.Visible = false;
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            Catalogo catalogo = new Catalogo();
            catalogo.Nombre = (string)Session["catalogoAModificar"];
            catalogo.IdCatalogo = (int)Session["idCatalogo"];
            OrganizarCatalogo.BorrarTipoDeProducto(catalogo);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El catalogo se Borro correctamente');", true);
            Limpiar();
            Page.Title = "Registrar catalogo";
            titulo.InnerText = "Registrar catalogo";
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Response.Redirect("Default.aspx");
        }
    }
}