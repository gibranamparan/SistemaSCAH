<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ContentPlaceHolderID="Contenido" ID="Contenido" runat="server">
        <script src="/JavaScripts/JavaScript.js" type="text/javascript"></script>

        <p class="textoTitulo">Bienvenido al Sistema de Comunicación Ambulancia-Hospital</p>
        <asp:Label ID="lblTipoUsuario" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblNombreUsuario" runat="server"></asp:Label>

</asp:Content>

