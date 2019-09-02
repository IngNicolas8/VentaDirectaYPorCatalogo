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
    public partial class ModificarOBorrarCatalogo : System.Web.UI.Page
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
                Catalogo catalogo = new Catalogo();
                OrganizarCatalogo organizarCatalogo = new OrganizarCatalogo();
                catalogo.Nombre = txNombre.Text;
                if(txtFecha.Text != "")
                    catalogo.Fecha = Convert.ToDateTime(txtFecha.Text);
                Limpiar();
                DataTable catalogos = organizarCatalogo.BuscarCatalogos(catalogo);
                if (catalogos.Rows.Count != 0)
                {
                    Limpiar();
                    gvCatalogo.DataSource = catalogos;
                    gvCatalogo.DataKeyNames = new string[] { "nombre" };
                    gvCatalogo.DataBind();
                }
                else
                {
                    Limpiar();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('No se encontro el catalogo');", true);
                }
                //Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('No se encontro el catalogo o  ocurrio una situacion, pruebe mas tarde');", true);
            }
        }

        /// <summary>
        /// Metodo que limpia los campos
        /// </summary>
        private void Limpiar()
        {
            gvCatalogo.DataSource = null;
            gvCatalogo.DataBind();
            txNombre.Text = "";
            txtFecha.Text = Convert.ToString(DateTime.Now);
        }

        protected void gvCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["catalogoAModificar"] = gvCatalogo.SelectedValue.ToString();
            Response.Redirect("RegistrarCatalogo.aspx");
        }
    }
}