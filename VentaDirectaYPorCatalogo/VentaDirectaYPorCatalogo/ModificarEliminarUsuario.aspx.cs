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
    public partial class ModificarEliminarUsuario : System.Web.UI.Page
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
                Usuario usuario = new Usuario();
                OrganizarUsuario organizacionUsuario = new OrganizarUsuario();
                usuario.User = txtUsuarioABuscar.Text;
                DataTable usuarios = organizacionUsuario.BuscarUsuarios(usuario);
                if (usuarios.Rows.Count != 0)
                {
                    txtUsuarioABuscar.Text = "";
                    Limpiar();
                    
                    gvUsuarios.DataSource = usuarios;
                    gvUsuarios.DataKeyNames = new string[] { "usuario" };
                    gvUsuarios.DataBind();
                }
                else
                {
                    Limpiar();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('No se encontro el usuario');", true);
                }
                //Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('No se encontro el usuario o  ocurrio una situacion, pruebe mas tarde');", true);
            }
        }

        /// <summary>
        /// Metodo que limpia los campos
        /// </summary>
        private void Limpiar()
        {
            gvUsuarios.DataSource = null;
            gvUsuarios.DataBind();
            txtUsuarioABuscar.Text = "";
        }

        protected void gvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["UsuarioAModificar"] = gvUsuarios.SelectedValue.ToString();
        }
    }
}