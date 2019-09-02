<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarOBorrarProducto.aspx.cs" Inherits="VentaDirectaYPorCatalogo.ModificarOBorrarProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
    <div class="row">

    </div>
    <br />
    <div class="row">
        <div class="col-md-offset-4 col-md-4">
            <asp:GridView ID="gvProducto" runat="server" AutoGenerateSelectButton="true" OnSelectedIndexChanged="gvProducto_SelectedIndexChanged">
                <Columns>
                </Columns>
                <EmptyDataTemplate>
                    No hay registros
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
