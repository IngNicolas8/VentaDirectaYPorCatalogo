<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarOBorrarTipoDeProducto.aspx.cs" Inherits="VentaDirectaYPorCatalogo.ModificarOBorrarTipoDeProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-md-offset-4 col-md-1">
            <asp:Label ID="lblNombre" runat="server" Text="Tipo de producto"></asp:Label>
        </div>
        <div class="col-md-2">
            <asp:TextBox ID="txNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-default" OnClick="btnBuscar_Click"/>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-offset-4 col-md-4">
            <asp:GridView ID="gvTiposDeProducto" runat="server" AutoGenerateSelectButton="true" OnSelectedIndexChanged="gvTiposDeProducto_SelectedIndexChanged">
                <Columns>
                </Columns>
                <EmptyDataTemplate>
                    No hay registros
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
