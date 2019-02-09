using BaseDeDatos;
using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VentaDirectaYPorCatalogo
{
    public partial class ModificarEliminarUsuario : System.Web.UI.Page
    {
        LinkButton lbIniciarSession;

        protected void Page_Load(object sender, EventArgs e)
        {
            lbIniciarSession = (LinkButton)Master.FindControl("lbIniciarSession");
            if (!Page.IsPostBack)
            {
                if (Session["Session"] != null && Session["Usuario"] != null)
                {
                    lbIniciarSession.Text = "Cerrar sessión";
                }
                else
                {
                    lbIniciarSession.Text = "Iniciar sessión";
                }
            }
            else
            {
                if (Session["Session"] != null && Session["Usuario"] != null)
                {
                    lbIniciarSession.Text = "Cerrar sessión";
                }
                else
                {
                    lbIniciarSession.Text = "Iniciar sessión";
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                OrganizarUsuario organizacionUsuario = new OrganizarUsuario();
                usuario.User = txtUsuarioABuscar.Text;
                organizacionUsuario.BuscarUsuario(ref usuario);
                if (usuario.User != null)
                {
                    txtUsuarioABuscar.Text = usuario.User;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('No se encontro el usuario');", true);
                }
                //Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('No se encontro el usuario o  ocurrio una situacion, pruebe mas tarde');", true);
            }
        }
    }
}