<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<asp:Content ContentPlaceHolderID="Contenido" runat="server">
    <div class="contenidoPagina">
        <p class="textoTitulo">Disculpa, ha habido un error.</p>
        Su perfil de usuario no tienen los permisos para acceder a esta página, por favor, ingrese con un perfil de usuario con privilegios para acceder a esta funcionalidad o contactese con el administrador del sistema.
        <asp:LinkButton ID="LinkButton1" Text="Click para ir a la página inicial" PostBackUrl="~/Default.aspx" runat="server"></asp:LinkButton>
    </div>
    
</asp:Content>


