<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarProductosPorCatalogo.aspx.cs" Inherits="VentaDirectaYPorCatalogo.AdministrarProductosPorCatalogo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblCatalogo" runat="server" Text="Catalogo"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlCatalogo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCatalogo_SelectedIndexChanged" ViewStateMode="Enabled" AutoPostBack="True">
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-default" OnClick="btnBuscar_Click" />
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-offset-1 col-md-3">
            <asp:ListBox ID="lbProducto" runat="server" OnSelectedIndexChanged="lbProducto_SelectedIndexChanged" Width="100%" SelectionMode="Multiple"></asp:ListBox>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar >>" CssClass="btn btn-default" OnClick="btnAgregar_Click" Width="100%" />
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnQuitar" runat="server" Text="<< Quitar" CssClass="btn btn-default" OnClick="btnQuitar_Click"  Width="100%" />
        </div>
        <div class="col-md-3">
            <asp:ListBox ID="lbProductosDelCatalogo" runat="server" OnSelectedIndexChanged="lbProductosDelCatalogo_SelectedIndexChanged" Width="100%" SelectionMode="Multiple"></asp:ListBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-5 col-md-1">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" CssClass="btn btn-default" ValidationGroup="1" />
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" CssClass="btn btn-default"/>
        </div>
        <div class="col-md-3">

        </div>
    </div>
</asp:Content>
