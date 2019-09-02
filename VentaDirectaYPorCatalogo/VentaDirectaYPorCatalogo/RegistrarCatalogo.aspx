<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarCatalogo.aspx.cs" Inherits="VentaDirectaYPorCatalogo.RegistrarCatalogo" %>
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
            <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
        </div>
         <div class="col-md-3">
             <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-4">
            <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="Ingrese Nombre" ForeColor="Red" ControlToValidate="txtFecha" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator><br />
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
            <asp:Label ID="lblTemporada" runat="server" Text="Temporada"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtTemporada" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-4">
            <asp:RequiredFieldValidator ID="rfvTemporada" runat="server" ErrorMessage="Ingrese Temporada" ForeColor="Red" ControlToValidate="txtTemporada" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator><br />
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-5 col-md-1">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="BtnAceptar_Click" CssClass="btn btn-default" ValidationGroup="1" />
        </div>
        <div class="col-md-1" id="divBorrar" runat="server" visible="false">
            <asp:Button ID="btnBorrar" runat="server" Text="Borrar" OnClick="btnBorrar_Click" CssClass="btn btn-default" Visible="false" />
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" CssClass="btn btn-default"/>
        </div>
        <div class="col-md-3">

        </div>
    </div>
</asp:Content>
