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
                                <asp:Button ID="btnBuscarProducto" runat="server" CssClass="btn btn-default" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-5 col-md-offset-1">
                    <asp:DropDownList ID="ddlOrdenar" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1" id="filtros">
                    <h5>Filtros de Busqueda:</h5>
                </div>
                <div class="col-md-10">
                    <ul class="thumbnails">
                        <li class="span4">
                            <a href="#" class="thumbnail">
                                <img src="#" alt="">
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
