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
            
        }

        protected void LbIniciarSession_Click(object sender, EventArgs e)
        {
            if(lbIniciarSession.Text.Equals("Iniciar sessión"))
            {
                Response.Redirect("IniciarSession.aspx");
            }
            else
            {
                Session["Session"] = null;
                lbIniciarSession.Text = "Iniciar sessión";
            }
        }
    }
}