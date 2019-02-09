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
        protected void Page_Load(object sender, EventArgs e)
        {
            lbIniciarSession = (LinkButton)Master.FindControl("lbIniciarSession");
            if (!Page.IsPostBack)
            {
                if(Session["Session"] != null && Session["Usuario"] != null)
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

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario(txtUsuario.Text, CreateMD5(txtContraseña.Text));
                OrganizarUsuario organizarUsuario = new OrganizarUsuario();
                organizarUsuario.RegistraUsuario(usuario);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El usuario se registro correctamente');", true);
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('" + ex.Message + "');", true);
            }
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