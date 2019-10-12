<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VentaDirectaYPorCatalogo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     
    <div class="jumbotron">
        <h1>Productos</h1>
        <p class="lead">Encuentre los mejores productos con la mejor calidad y al mejor precio</p>
    </div>

    <div class="row">
        <div class="col-md-11 col-md-offset-1">
            <div class="row">
                <div class="col-md-5 col-md-offset-1">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombreDelProducto" runat="server">Nombre de Producto:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreDelProducto" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnBuscarProducto" runat="server" CssClass="btn btn-default" Text="Buscar" OnClick="btnBuscarProducto_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-5 col-md-offset-1">
                    <asp:DropDownList ID="ddlOrdenar" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Ascendente" Value="Asc"></asp:ListItem>
                        <asp:ListItem Text="Descendente" Value="Desc" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3" id="filtros">
                    <h5>Filtros de Busqueda:</h5>
                    <div class="row">
                        <div class="col-md-3">
                            Categoria
                        </div>
                        <div class="col-md-9">
                            <asp:DropDownList ID="ddlTipoDeProducto" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlTipoDeProducto_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            Catalogo
                        </div>
                        <div class="col-md-9">
                            <asp:DropDownList ID="ddlCatalogo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCatalogo_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="lblDesde" runat="server" Text="Desde:"></asp:Label>
                        </div>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtDesde" runat="server" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="lblHasta" runat="server" Text="Hasta:"></asp:Label>
                        </div>
                         <div class="col-md-8">
                            <asp:TextBox ID="txtHasta" runat="server" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-9">
                    <div class="row">
                        <div class="col-md-11 col-md-offset-1" runat="server" id="imagenes">
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
