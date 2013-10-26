<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AltaCuenta.aspx.cs" Inherits="Paginas_Cuenta_AltaCuenta" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="Contenido" runat="server">
    <asp:Panel CssClass="panelContenido" ID="pnlAltaCuenta" runat="server" DefaultButton="btAceptar">
        <p>Rellene el siguiente formato para crear una nueva cuenta.</p>
        <table>
            <tr>
                <td><asp:Label ID="lblNombreUsuario" runat="server" Text="Nombre de Usuario:"></asp:Label></td>
                <td><asp:TextBox ID="tbNombreUsuario" runat="server" MaxLength="25"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblContraseña" runat="server" Text="Contraseña:"></asp:Label></td>
                <td><asp:TextBox ID="tbContraseña" TextMode="Password" runat="server" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblCContraseña" runat="server" Text="Confirmar Contraseña:"></asp:Label></td>
                <td><asp:TextBox ID="tbCContraseña" TextMode="Password" runat="server" MaxLength="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo Usuario: "></asp:Label></td>
                <td><asp:DropDownList ID="ddlTipoUsuario" runat="server">

                </asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="btAceptar" runat="server" Text="Aceptar" OnClick="btAceptar_Click" /></td>
            </tr>
        </table>
        <asp:Label ID="lblErrorAltaCuenta" runat="server" Text="" ForeColor="Red" /> 
    </asp:Panel>

</asp:Content>
