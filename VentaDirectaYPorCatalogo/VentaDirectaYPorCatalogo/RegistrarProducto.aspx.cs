using BaseDeDatos;
using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VentaDirectaYPorCatalogo
{
    public partial class RegistrarProducto : System.Web.UI.Page
    {
        LinkButton lbIniciarSession;
        LinkButton lblNombreDelUsuario;
        List<string> archivos = new List<string>();
        StringBuilder html = new StringBuilder();
        DataTable tabla = new DataTable();

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
                if (Session["ProductoAModificar"] == null)
                {
                    Page.Title = "Registrar producto";
                    titulo.InnerText = "Registrar producto";
                }
                else
                {
                    Page.Title = "Modificar o borrar producto";
                    titulo.InnerText = "Modificar o borrar producto";
                    CargarDatos();
                    btnBorrar.Visible = true;
                    divBorrar.Visible = true;
                    tabla = (DataTable)Session["imagenes"];
                    Mostrar();
                }
                CargarDatosEnComboTipoDeProducto();
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
                Mostrar();
            }
        }

        /// <summary>
        /// Carga los tipos de producto en el combo
        /// </summary>
        private void CargarDatosEnComboTipoDeProducto()
        {
            OrganizarTipoDeProducto organizarTipoDeProducto = new OrganizarTipoDeProducto();
            var auxiliar = new TipoDeProducto();
            auxiliar.Nombre = "";
            DataTable tabla = organizarTipoDeProducto.BuscarTiposDeProducto(auxiliar);
            ddlTipoDeProducto.DataSource = tabla;
            ddlTipoDeProducto.DataValueField = "codigo";
            ddlTipoDeProducto.DataTextField = "nombre";
            ddlTipoDeProducto.DataBind();
        }

        /// <summary>
        /// Carga los productos en los controles
        /// </summary>
        private void CargarDatos()
        {

            OrganizarProducto organizarProducto = new OrganizarProducto();
            Producto productoAModificar = new Producto();
            productoAModificar.Nombre = (string)Session["productoAModificar"];
            organizarProducto.BuscarProducto(ref productoAModificar);
            txtNombre.Text = productoAModificar.Nombre;
            ddlTipoDeProducto.SelectedValue = productoAModificar.TipoDeProducto.IdTipoDeProducto.ToString();
            txtPrecio.Text = productoAModificar.Precio.ToString();
            Session["idProducto"] = productoAModificar.IdProducto;
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Response.Redirect("Default.aspx");
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (Session["productoAModificar"] == null)
            {
                try
                {
                    Producto producto = new Producto();
                    producto.Nombre = txtNombre.Text;
                    producto.TipoDeProducto = new TipoDeProducto();
                    producto.TipoDeProducto.IdTipoDeProducto = Convert.ToInt32(ddlTipoDeProducto.SelectedValue);
                    if (archivos.Count != 0)
                    {
                        foreach (string archivo in archivos)
                            producto.Imagen += archivo + ", ";
                    }
                    else
                    {
                        producto.Imagen = "";
                    }
                    producto.Precio = Convert.ToInt32(txtPrecio.Text);
                    OrganizarProducto organizarProducto = new OrganizarProducto();
                    organizarProducto.RegistrarProducto(producto);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El producto se registro correctamente');", true);
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
                    Producto producto = new Producto();
                    OrganizarProducto organizarProducto = new OrganizarProducto();
                    producto.IdProducto = (int)Session["idProducto"];
                    producto.Nombre = txtNombre.Text;
                    producto.TipoDeProducto = new TipoDeProducto();
                    producto.TipoDeProducto.IdTipoDeProducto = Convert.ToInt32(ddlTipoDeProducto.SelectedValue);
                    producto.Precio = float.Parse(txtPrecio.Text.ToString().Replace(",", "."));
                    if (archivos.Count != 0)
                    {
                        foreach(string archivo in archivos)
                            producto.Imagen += archivo + ", ";
                    }
                    else
                    {
                        producto.Imagen = "";
                    }
                    organizarProducto.ModificarProducto(producto);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El producto se actualizo correctamente');", true);
                    Limpiar();
                    Page.Title = "Registrar producto";
                    titulo.InnerText = "Registrar producto";
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
            txtNombre.Text = "";
            //ddlTipoDeProducto.SelectedValue = "-1";
            txtPrecio.Text = "";
            Session["productoAModificar"] = null;
            btnBorrar.Visible = false;
            divBorrar.Visible = false;
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto();
            producto.Nombre = (string)Session["productoAModificar"];
            producto.Catalogo = new Catalogo();
            producto.IdProducto = (int)Session["idProducto"];
            OrganizarProducto organizarProducto = new OrganizarProducto();
            organizarProducto.BorrarProducto(producto);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensajes", "alert('El producto se borro correctamente');", true);
            Limpiar();
            Page.Title = "Registrar producto";
            titulo.InnerText = "Registrar producto";
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            agregarImagenes();
        }

        /// <summary>
        /// Agrega las imagenes al carrusel
        /// </summary>
        private void agregarImagenes()
        {
            List<string> archivos = new List<string>();
            if (Session["imagenes"] != null)
            {
                archivos = (List<string>)Session["imagenes"];
            }
            if (fuImagenes.HasFile)
            {
                if (!archivos.Contains(fuImagenes.FileName))
                {
                    fuImagenes.SaveAs(Server.MapPath("~/imagenes/") + fuImagenes.FileName);
                    archivos.Add(fuImagenes.FileName);
                    Session["imagenes"] = archivos;
                }
            }
            Mostrar();
        }

        public void Mostrar()
        {
            List<string> archivos = new List<string>();
            imagenes.Controls.Clear();
            if (Session["imagenes"] != null)
            {
                if (!archivos.Contains(fuImagenes.FileName))
                {
                    archivos = (List<string>)Session["imagenes"];
                    foreach (string fila in archivos)
                    {
                        Panel panel = new Panel();
                        panel.ID = "pnl" + fila;
                        panel.CssClass = "thumbnail";
                        panel.Height = 250;
                        panel.Width = 250;
                        Image imagen = new Image();
                        imagen.ID = "img" + fila;
                        imagen.Width = 250;
                        imagen.Height = 250;
                        imagen.ImageUrl = "~/imagenes/" + fila;
                        Button boton = new Button();
                        boton.ID = fila;
                        boton.Click += new EventHandler(this.BtnQuitar_Click);
                        boton.Text = "X";
                        boton.CssClass = "btn btn-primary";
                        panel.Controls.Add(imagen);
                        panel.Controls.Add(boton);
                        imagenes.Controls.Add(panel);
                    }
                }
            }
        }

        public void BtnQuitar_Click(object sender, EventArgs e)
        {
            List<string> archivos = (List<string>)Session["imagenes"];
            Button boton = (Button)sender;
            archivos.Remove(boton.ID);
            Session["imagenes"] = archivos;
            Mostrar();
        }
    }
}