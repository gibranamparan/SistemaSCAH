<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Ingreso.aspx.cs" Inherits="Paginas_Cuenta_Ingreso" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="Contenido" runat="server">
    <asp:Panel CssClass="panelContenido" ID="pnlIngreso" runat="server" Width="35%" DefaultButton="btIngresar">
        <p>Introduzca nombre de usuario y contraseña para ingresar al sistema.</p>
        <table>
            <tr>
                <td>Nombre de Usuario</td>
                <td><asp:TextBox ID="tbNombreUsuario" MaxLength="25" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Contraseña</td>
                <td><asp:TextBox ID="tbContraseña" TextMode="Password" MaxLength="50" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:LinkButton ID="lnkCrearCuenta" Text="Crear Cuenta" PostBackUrl="~/Cuenta/AltaCuenta.aspx" runat="server"></asp:LinkButton></td>
                <td><asp:Button ID="btIngresar" OnClientClick="" runat="server" Text="Ingresar" OnClick="btIngresar_Click" /></td>
            </tr>
            <tr>
                <td></td>
                <td> <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
