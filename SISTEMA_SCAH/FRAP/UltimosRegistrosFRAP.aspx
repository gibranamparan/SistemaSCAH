<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UltimosRegistrosFRAP.aspx.cs" Inherits="_Default" %>

<asp:Content ContentPlaceHolderID="Contenido" ID="Contenido" runat="server">
    <!-- SE CARGA CSS DE LA HOJA DE FRAP -->
    <link href="../Estilos/FRAPEstilo.css" type="text/css" rel="stylesheet" />


        <span class="textoTitulo">ULTIMOS FRAP's REPORTADOS ESTE DIA</span>
    <asp:GridView ID="grvUltimosFRAP" ClientIDMode="Static" runat="server" CssClass="tablaAntecedentes" AutoGenerateColumns="False" 
        OnRowDataBound="grvUltimosFRAP_RowDataBound" OnDataBinding="grvUltimosFRAP_DataBinding" >
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFormatString="~/FRAP/AltaFRAP.aspx?idFrap={0}" DataTextField="idFrap" 
                HeaderText="ID FRAP" DataNavigateUrlFields="idFrap" NavigateUrl="~/FRAP/AltaFRAP.aspx" />
            <asp:BoundField DataField="prioridad" HeaderText="Prioridad" SortExpression="prioridad" />
            <asp:BoundField DataField="nombrePaciente" HeaderText="Nombre del Paciente" SortExpression="nombrePaciente" />
            <asp:BoundField DataField="edad" HeaderText="Edad" SortExpression="edad" />
            <asp:BoundField DataField="motivoAtencion" HeaderText="Motivo de Atención" SortExpression="motivoAtencion" />
            <asp:BoundField DataField="condicionMedica" HeaderText="Condición Médica" SortExpression="condicionMedica" />
            <asp:BoundField DataField="fechaFinLlenado" HeaderText="Fecha de Reporte" SortExpression="fechaFinLlenado" />
        </Columns>
        <EmptyDataTemplate>
            <span style="font-weight:bold">
                No se han registrado nuevos FRAP este día.
            </span>
        </EmptyDataTemplate>
    </asp:GridView>
        
</asp:Content>

