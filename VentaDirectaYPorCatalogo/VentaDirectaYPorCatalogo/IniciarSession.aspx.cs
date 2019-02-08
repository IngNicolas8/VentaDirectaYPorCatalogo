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
    public partial class IniciarSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario(txtUsuario.Text, CreateMD5(txtContraseña.Text).ToLower());
                if(new OrganizarUsuario().IniciarSession(usuario))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El usuario inicio sessión correctamente');", true);
                    Session["Session"] = true;
                    Response.Redirect("Defaul.aspx");
                }
                else
                {
                    txtContraseña.Text = "";
                    txtUsuario.Text = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El usuario no pudo iniciar sessión correctamente');", true);
                }
            }
            catch(Exception ex)
            {
                txtContraseña.Text = "";
                txtUsuario.Text = "";
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