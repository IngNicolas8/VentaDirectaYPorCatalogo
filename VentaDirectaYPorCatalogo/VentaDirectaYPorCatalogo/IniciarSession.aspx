<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IniciarSession.aspx.cs" Inherits="VentaDirectaYPorCatalogo.IniciarSession" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-4">
            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="Ingrese usuario" ForeColor="Red" ControlToValidate="txtUsuario" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator><br />
            <asp:RegularExpressionValidator ID="revUsuario" runat="server" ErrorMessage="Debe ser un mail valido" ControlToValidate="txtUsuario" Display="Dynamic" ValidationGroup="1" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblContraseña" runat="server" Text="Contraseña"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control" ValidationGroup="1" TextMode="Password"></asp:TextBox>
        </div>
        <div class="col-md-4">
            <asp:RequiredFieldValidator ID="rfvContraseña" runat="server" ErrorMessage="Debe ingresar una contraseña valida" Display="Dynamic" ValidationGroup="1" ControlToValidate="txtContraseña" ForeColor="Red"></asp:RequiredFieldValidator><br />
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-5 col-md-1">
            <asp:Button ID="btnIniciar" runat="server" Text="Iniciar"  CssClass="btn btn-default" OnClick="BtnIniciar_Click"/>
        </div>
        <div class="col-md-1">
            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default" OnClick="BtnCancelar_Click"/>
        </div>
    </div>
</asp:Content>
