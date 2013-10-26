<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VerUsuarios.aspx.cs" Inherits="Cuenta_VerUsuarios" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" Runat="Server">
            <img src="/App_GlobalResources/borrar.png" />
    <asp:Panel CssClass="panelContenido" runat="server" ID="pnlTablaUsuario">
        <asp:GridView CssClass="gridViewEstilo" HorizontalAlign="Center" AutoGenerateColumns="false" ID="grvUsuarios" runat="server">
            <Columns>
                <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
                <asp:BoundField DataField="Contraseña" HeaderText="Contraseña" />
                <asp:BoundField DataField="NombreTipoUsuario" HeaderText="Tipo de Usuario" />
                
            </Columns>

        </asp:GridView>
    </asp:Panel>
    
</asp:Content>

