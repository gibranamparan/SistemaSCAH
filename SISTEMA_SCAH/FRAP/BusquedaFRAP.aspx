<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BusquedaFRAP.aspx.cs" Inherits="_Default" %>

<asp:Content ContentPlaceHolderID="Contenido" ID="Contenido" runat="server">
    <link href="../Estilos/FRAPEstilo.css" type="text/css" rel="stylesheet" />
      
    <!-- SE CARGA CSS JQUERY -->
    <asp:ScriptManager runat="server" ID="smScriptManager"></asp:ScriptManager>
    <link href="../jquery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../jquery/jquery-ui-timepicker-addon.css" rel="stylesheet" type="text/css" />

    <!--SE CARGA CODIGO JAVASCRIPT-->
    <script src="../jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery/jquery-ui.js" type="text/javascript"></script>
    <script src="../jquery/jquery.maskedinput.js" type="text/javascript"></script>
    <script src="../jquery/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
        
    <!-- PANEL DE BUSQUEDA DE FRAP -->
    <asp:Panel ID="pblFiltroFRAP" runat="server" DefaultButton="btBuscar" >
        <table>
        <tr>
            <td>
                <asp:Label ID="lblNombrePaciente" Text="Nombre:" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbNombrePaciente" MaxLength="80" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTrauma" Text="Trauma:" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTrauma" DataTextField="nombreTrauma" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFechaInicial" Text="Fecha Inicial:" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbFechaInicial" ClientIDMode="Static" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFechaFinal" Text="Fecha Final:" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbFechaFinal"  ClientIDMode="Static" runat="server"></asp:TextBox>
            </td>
        </tr>
        </table>
        <asp:Button ID="btBuscar" runat="server" Text="Buscar" OnClick="btBuscar_Click" />
    </asp:Panel>

        <!--RESULTADOS DE BUSQUEDA -->
    <asp:GridView ID="grvFRAP" ClientIDMode="Static" runat="server" CssClass="tablaAntecedentes" AutoGenerateColumns="False" 
        OnRowDataBound="grvFRAP_RowDataBound" OnDataBinding="grvFRAP_DataBinding" >
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFormatString="~/FRAP/AltaFRAP.aspx?idFrap={0}" DataTextField="idFrap" 
                HeaderText="ID FRAP" DataNavigateUrlFields="idFrap" NavigateUrl="~/FRAP/AltaFRAP.aspx" />
            <asp:BoundField DataField="prioridad" HeaderText="Prioridad" SortExpression="prioridad" />
            <asp:BoundField DataField="nombrePaciente" HeaderText="Nombre del Paciente" SortExpression="nombrePaciente" />
            <asp:BoundField DataField="edad" HeaderText="Edad" SortExpression="edad" />
            <asp:BoundField DataField="motivoAtencion" HeaderText="Motivo de Atención" SortExpression="motivoAtencion" />
            <asp:BoundField DataField="Trauma" HeaderText="Traumas" SortExpression="Traumas" />
            <asp:BoundField DataField="condicionMedica" HeaderText="Condición Médica" SortExpression="condicionMedica" />
            <asp:BoundField DataField="fechaFinLlenado" HeaderText="Fecha de Reporte" SortExpression="fechaFinLlenado" />
        </Columns>

        <EmptyDataTemplate>
            <span style="font-weight:bold">
                No se han registrado nuevos FRAP este día.
            </span>
        </EmptyDataTemplate>
    </asp:GridView>

    <script>
        $("#tbFechaInicial").datetimepicker();
        $("#tbFechaInicial").mask("99/99/9999 99:99");

        $("#tbFechaFinal").datetimepicker();
        $("#tbFechaFinal").mask("99/99/9999 99:99");
    </script>
</asp:Content>
