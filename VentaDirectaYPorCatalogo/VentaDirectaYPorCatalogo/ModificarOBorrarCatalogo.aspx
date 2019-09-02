<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarOBorrarCatalogo.aspx.cs" Inherits="VentaDirectaYPorCatalogo.ModificarOBorrarCatalogo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
        </div>
        <div class="col-md-2">
            <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-default" OnClick="btnBuscar_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="row">

    </div>
    <br />
    <div class="row">
        <div class="col-md-offset-4 col-md-4">
            <asp:GridView ID="gvCatalogo" runat="server" AutoGenerateSelectButton="true" OnSelectedIndexChanged="gvCatalog_SelectedIndexChanged">
                <Columns>
                </Columns>
                <EmptyDataTemplate>
                    No hay registros
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
