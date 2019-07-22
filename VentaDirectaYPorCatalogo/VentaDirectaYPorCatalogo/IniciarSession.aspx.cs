using BaseDeDatos;
using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Clases;

namespace VentaDirectaYPorCatalogo
{
    public partial class IniciarSession : System.Web.UI.Page
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

        protected void BtnIniciar_Click(object sender, EventArgs e)
        {
            LinkButton lbIniciarSession = (LinkButton)Master.FindControl("lbIniciarSession");
            lblNombreDelUsuario = (LinkButton)Master.FindControl("lblNombreDelUsuario");
            try
            {
                Usuario usuario = new Usuario(txtUsuario.Text, CreateMD5(txtContraseña.Text).ToLower(), new Persona());
                OrganizarUsuario organizacionUsuario = new OrganizarUsuario();
                string rol = organizacionUsuario.IniciarSession(usuario);
                if(rol != "desconocido")
                {
                    lbIniciarSession.Text = "Cerrar session";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El usuario inicio sessión correctamente');", true);
                    Session["Session"] = rol;
                    organizacionUsuario.BuscarUsuario(ref usuario);
                    Session["Usuario"] = usuario;
                    lblNombreDelUsuario.Text = usuario.User;
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    txtContraseña.Text = "";
                    txtUsuario.Text = "";
                    Session["Session"] = rol;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El usuario no pudo iniciar sessión correctamente');", true);
                }
                //Response.Redirect("Default.aspx");
            }
            catch(Exception ex)
            {
                txtContraseña.Text = "";
                txtUsuario.Text = "";
                Session["Session"] = "desconocido";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El usuario no pudo iniciar sessión correctamente');", true);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        /// <summary>
        /// Crea un hash de la contraseña
        /// </summary>
        /// <param name="input">contraseña</param>
        /// <returns>hash</returns>
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}