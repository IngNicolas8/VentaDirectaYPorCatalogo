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
                if (Session["UsuarioAModificar"] == null)
                {
                    Page.Title = "Registrar Usuario";
                    titulo.InnerText = "Registrar Usuario";
                }
                else
                {
                    Page.Title = "Modificar o Eliminar Usuario";
                    titulo.InnerText = "Modificar o Eliminar Usuario";
                    CargarDatos();
                    rfvContraseña.Enabled = false;
                    rfvConfirmarContraseña.Enabled = false;
                    btnBorrar.Visible = true;
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

        /// <summary>
        /// Carga los datos personales en los controles
        /// </summary>
        private void CargarDatos()
        {
            Usuario usuarioAModificarOEliminar = new Usuario();
            OrganizarUsuario organizarUsuario = new OrganizarUsuario();
            OrganizarPersona organizarPersona = new OrganizarPersona();
            usuarioAModificarOEliminar.User = (string)Session["usuarioAModificar"];
            if(organizarUsuario.BuscarUsuario(ref usuarioAModificarOEliminar))
            {
                organizarPersona.BuscarPersona(ref usuarioAModificarOEliminar);
            }
            txtUsuario.ReadOnly = true;
            txtUsuario.Text = usuarioAModificarOEliminar.User;
            txtNombre.Text = usuarioAModificarOEliminar.Persona.Nombre;
            txtApellido.Text = usuarioAModificarOEliminar.Persona.Apellido;
            Session["idPersona"] = usuarioAModificarOEliminar.Persona.Id;
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (Session["usuarioAModificar"] == null)
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
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('" + ex.Message + "');", true);
                }
            }
            else
            {
                try
                {
                    Usuario usuario = new Usuario();
                    OrganizarUsuario organizarUsuario = new OrganizarUsuario();
                    usuario.User = (string)Session["usuarioAModificar"];
                    usuario.Persona = new Persona();
                    usuario.Persona.Nombre = txtNombre.Text;
                    usuario.Persona.Apellido = txtApellido.Text;
                    usuario.Persona.Id = (int)Session["idPersona"];
                    if (txtContraseña.Text != "" && txtConfirmarContraseña.Text != "")
                    {
                        usuario.Contraseña = CreateMD5(txtContraseña.Text);
                    }
                    organizarUsuario.ModificarUsuario(usuario);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El usuario se actualizo correctamente');", true);
                    Limpiar();
                    Page.Title = "Registrar Usuario";
                    titulo.InnerText = "Registrar Usuario";
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('" + ex.Message + "');", true);
                }
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
            Session["usuarioAModificar"] = null;
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

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.User = (string)Session["usuarioAModificar"];
            usuario.Persona = new Persona();
            usuario.Persona.Id = (int)Session["idPersona"];
            OrganizarPersona.BorrarPersona(usuario);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El usuario se Borro correctamente');", true);
            Limpiar();
            Page.Title = "Registrar Usuario";
            titulo.InnerText = "Registrar Usuario";
            btnBorrar.Visible = false;
        }
    }
}