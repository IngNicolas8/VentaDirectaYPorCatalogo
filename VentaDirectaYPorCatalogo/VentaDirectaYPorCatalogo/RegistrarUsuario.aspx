<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarUsuario.aspx.cs" Inherits="VentaDirectaYPorCatalogo.RegistrarUsuario" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtUsuario" runat="server" TextMode="SingleLine" CssClass="form-control" Text="Ejemplo@mail.com"></asp:TextBox>
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
            <asp:TextBox ID="txtContraseña" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-4">
            <asp:RequiredFieldValidator ID="rfvContraseña" runat="server" ErrorMessage="Debe ingresar una contraseña valida" Display="Dynamic" ValidationGroup="1" ControlToValidate="txtContraseña" ForeColor="Red"></asp:RequiredFieldValidator><br />
            <asp:CompareValidator ID="cvContraseña" runat="server" ErrorMessage="La contraseña no coincide" ControlToCompare="txtContraseña" ControlToValidate="txtConfirmarContraseña" ValidationGroup="1" ForeColor="Red"></asp:CompareValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblConfirmarContraseña" runat="server" Text="Confirmar contraseña"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtConfirmarContraseña" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-4">
            <asp:RequiredFieldValidator ID="rfvConfirmarContraseña" runat="server" ErrorMessage="Debe ingresar una contraseña valida" Display="Dynamic" ValidationGroup="1" ControlToValidate="txtConfirmarContraseña" ForeColor="Red"></asp:RequiredFieldValidator><br />
            <asp:CompareValidator ID="cvConfirmarContraseña" runat="server" ErrorMessage="La contraseña no coincide" ControlToCompare="txtConfirmarContraseña" ControlToValidate="txtContraseña" ValidationGroup="1" ForeColor="Red"></asp:CompareValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-5 col-md-1">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="BtnAceptar_Click" CssClass="btn btn-default" ValidationGroup="1" />
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" CssClass="btn btn-default"/>
        </div>
        <div class="col-md-4">

        </div>
    </div>
</asp:Content>
