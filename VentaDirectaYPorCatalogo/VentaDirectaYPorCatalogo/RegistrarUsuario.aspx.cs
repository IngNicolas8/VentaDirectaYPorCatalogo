using BaseDeDatos;
using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VentaDirectaYPorCatalogo
{
    public partial class RegistrarUsuario : System.Web.UI.Page
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

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Persona persona = new Persona();
                persona.Apellido = txtApellido.Text;
                persona.Nombre = txtNombre.Text;
                Usuario usuario = new Usuario(txtUsuario.Text, CreateMD5(txtContraseña.Text), persona);
                OrganizarUsuario organizarUsuario = new OrganizarUsuario();
                organizarUsuario.RegistraUsuario(usuario);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El usuario se registro correctamente');", true);
                Limpiar();
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('" + ex.Message + "');", true);
            }
        }

        /// <summary>
        /// Limpia los campos
        /// </summary>
        private void Limpiar()
        {
            txtApellido.Text = "";
            txtConfirmarContraseña.Text = "";
            txtContraseña.Text = "";
            txtNombre.Text = "";
            txtUsuario.Text = "";
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