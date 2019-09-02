<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarProducto.aspx.cs" Inherits="VentaDirectaYPorCatalogo.RegistrarProducto" %>

<%@ Import Namespace="System.IO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <center>
                <h1 id="titulo" runat="server">

                </h1>
            </center>
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtNombre" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-4">
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Ingrese Nombre" ForeColor="Red" ControlToValidate="txtNombre" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator><br />
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblTipoDeProducto" runat="server" Text="Tipo"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlTipoDeProducto" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-md-4">
           
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblPrecio" runat="server" Text="Precio"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtPrecio" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-4">
            <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ErrorMessage="Ingrese precio" ForeColor="Red" ControlToValidate="txtPrecio" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator><br />
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblImagen" runat="server" Text="Imagenes"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:FileUpload ID="fuImagenes" runat="server" AllowMultiple="true"/>
        </div>
        <div class="col-md-4">
            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-default" Text="Cargar imagenes" OnClick="btnAgregar_Click"/>
        </div>
    </div>
    <div class="row">
        <div class="col-md-11 col-md-offset-1" runat="server" id="imagenes">
        
         </div>
    </div>
    <div class="row">
        <div class="col-md-offset-5 col-md-1">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="BtnAceptar_Click" CssClass="btn btn-default" ValidationGroup="1" />
        </div>
        <div class="col-md-1" runat="server" visible="false" id="divBorrar">
            <asp:Button ID="btnBorrar" runat="server" Text="Borrar" OnClick="btnBorrar_Click" CssClass="btn btn-default" Visible="false" />
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" CssClass="btn btn-default"/>
        </div>
        <div class="col-md-3">

        </div>
    </div>
</asp:Content>
