using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Funciones = DataAccessUsuarios.FuncionesUsuario;
using pRANV = DataAccessFRAP.PrioridadRANV;

public partial class _Default : System.Web.UI.Page
{
    private DSFrap.condicionMedicaDataTable dtCondicionMedica;
    private DSFrap.motivoAtencionDataTable dtMotivoAtencion;
    private int idxColumnaMotivoAtencion;
    private int idxColumnaCondicionMedica;
    private int idxColumnaPrioridad;

    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        if (Session["idUser"] == null)
        {
            Response.Redirect("~/Error.aspx");
        }
        else
        {
            DSUsuarios ds = (DSUsuarios)(Session["DSUser"]);
            DSUsuarios.PermisosRow pr = ds.Permisos.FindByidFuncion((int)Funciones.UltimosRegistrosFRAP);
            if (pr == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {*/
        //Se cargan las listas de Motivo de Atencion y de Condicion Medica
                int returnVal, rvCondMedica, rvMotAtencion;
                dtCondicionMedica = DataAccessFRAP.condicionMedica(out rvCondMedica).condicionMedica;
                dtMotivoAtencion = DataAccessFRAP.motivoAtencion(out rvMotAtencion).motivoAtencion;

        //Si las listas se han cargado satisfactoriamente
                if (rvCondMedica > 0 && rvMotAtencion > 0)
                {
                    //TEMPORAL
                    DateTime hoy = DateTime.Today.Subtract(new TimeSpan(7, 0, 0, 0));
                    //*******

                    //Se consultan los ultimos FRAPs
                    DSFrap ultimosDSF = DataAccessFRAP.consultaVistaFRAPsYPacientes(hoy, out returnVal);
                    //DSFrap ultimosDSF = DataAccessFRAP.consultaFRAPs(hoy, out returnVal);
                    grvUltimosFRAP.DataSource = ultimosDSF.vFrapYPacientes;
                    grvUltimosFRAP.DataBind();
                }
           /* }
        }*/
    }
    protected void grvUltimosFRAP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow gvr = e.Row;
        //Para cada renglon
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Se toma la celda de condicionMedica.
            TableCell cellCondicionMedica = gvr.Cells[idxColumnaCondicionMedica];
            //Se toma la celda de motivoAtencion.
            TableCell cellMotivoAtencion = gvr.Cells[idxColumnaMotivoAtencion];
            //Se toma la celda de prioridad.
            TableCell cellPrioridad = gvr.Cells[idxColumnaPrioridad];

            //Se transforman los indices separados por comas de estas columnas en las condiciones y motivos que representan.
            cellCondicionMedica.Text = Utilidades.numerosAElementosDeTabla(cellCondicionMedica.Text, dtCondicionMedica);
            cellMotivoAtencion.Text = Utilidades.numerosAElementosDeTabla(cellMotivoAtencion.Text, dtMotivoAtencion);
            if(String.IsNullOrEmpty(cellCondicionMedica.Text.Trim()))
                cellCondicionMedica.Text="No se reportó su condición médica.";
            if (String.IsNullOrEmpty(cellMotivoAtencion.Text.Trim()))
                cellCondicionMedica.Text = "No se reportó su el motivo de atención.";
            //Se interpreta el indice de prioridad por el RANV y se le da estilo a las celdas segun sea la prioridad
            idxAPrioridadRANV(ref cellPrioridad);
        }
        
    }

    private void idxAPrioridadRANV(ref TableCell cellPrioridad)
    {
        String p = cellPrioridad.Text;
        int RANV;
        if (int.TryParse(p,out RANV))
        {
            switch ((pRANV)RANV)
            {
                case pRANV.A:
                    cellPrioridad.Text = "A";
                    cellPrioridad.CssClass = "RANVA";
                    break;
                case pRANV.N:
                    cellPrioridad.Text = "N";
                    cellPrioridad.CssClass = "RANVN";
                    break;
                case pRANV.R:
                    cellPrioridad.Text = "R";
                    cellPrioridad.CssClass = "RANVR";
                    break;
                case pRANV.V:
                    cellPrioridad.Text = "V";
                    cellPrioridad.CssClass = "RANVV";
                    break;
            }
        }
    }

    protected void grvUltimosFRAP_DataBinding(object sender, EventArgs e)
    {
        //Se detectan las posiciones de las columnas de motivoAtencion y condicionMedica
        idxColumnaMotivoAtencion = Utilidades.GetColumnIndexByDBName(grvUltimosFRAP, "motivoAtencion");
        idxColumnaCondicionMedica = Utilidades.GetColumnIndexByDBName(grvUltimosFRAP, "condicionMedica");
        idxColumnaPrioridad = Utilidades.GetColumnIndexByDBName(grvUltimosFRAP, "prioridad");
    }
}