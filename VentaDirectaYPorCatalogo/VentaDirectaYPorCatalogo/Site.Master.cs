using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VentaDirectaYPorCatalogo
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Session"] != null  && (bool)Session["Session"])
            {
                lbIniciarSession.Text = "Cerrar sessión";
            }
            else
            {
                lbIniciarSession.Text = "Iniciar sessión";
            }
        }

        protected void LbIniciarSession_Click(object sender, EventArgs e)
        {
            if(lbIniciarSession.Text.Equals("Iniciar sessión"))
            {
                Response.Redirect("IniciarSession.aspx");
            }
            else
            {
                Session["Session"] = false;
                lbIniciarSession.Text = "Iniciar sessión";
            }
        }
        
        protected void lbRegistrarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarUsuario.aspx");
        }
    }
}