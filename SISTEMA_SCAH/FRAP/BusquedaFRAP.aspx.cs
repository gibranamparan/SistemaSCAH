using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Funciones = DataAccessUsuarios.FuncionesUsuario;
using pRANV = DataAccessFRAP.PrioridadRANV;

public partial class _Default : System.Web.UI.Page
{
    private DSFrap.condicionMedicaDataTable dtCondicionMedica;
    private DSFrap.motivoAtencionDataTable dtMotivoAtencion;
    private DSFrap.traumaPacienteDataTable dttraumaPaciente;
    private int idxColumnaMotivoAtencion;
    private int idxColumnaCondicionMedica;
    private int idxColumnaTraumaPaciente;
    private int idxColumnaPrioridad;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["idUser"] == null)
        {
            Response.Redirect("~/Error.aspx");
        }
        else
        {
            DSUsuarios ds = (DSUsuarios)(Session["DSUser"]);
            DSUsuarios.PermisosRow pr = ds.Permisos.FindByidFuncion((int)Funciones.BusquedaFRAP);
            if (pr == null)
            {
                Response.Redirect("~/Error.aspx");
            }
            else
            {
                int returnVal;
                dttraumaPaciente = DataAccessFRAP.traumaPaciente(out returnVal).traumaPaciente;
                dtCondicionMedica = DataAccessFRAP.condicionMedica(out returnVal).condicionMedica;
                dtMotivoAtencion = DataAccessFRAP.motivoAtencion(out returnVal).motivoAtencion;
                if (!IsPostBack)
                {
                    ddlTrauma.DataSource = dttraumaPaciente;
                    ddlTrauma.DataBind();
                }
                else
                {
                }
            }
        }
    }

    protected void grvFRAP_RowDataBound(object sender, GridViewRowEventArgs e)
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
            //Se toma la celda de truma.
            TableCell cellTrauma = gvr.Cells[idxColumnaTraumaPaciente];

            //Se transforman los indices separados por comas de estas columnas en las condiciones y motivos que representan.
            cellCondicionMedica.Text = Utilidades.numerosAElementosDeTabla(cellCondicionMedica.Text, dtCondicionMedica);
            cellMotivoAtencion.Text = Utilidades.numerosAElementosDeTabla(cellMotivoAtencion.Text, dtMotivoAtencion);
            cellTrauma.Text = Utilidades.numerosAElementosDeTabla(cellTrauma.Text, dttraumaPaciente);
            //Se interpreta el indice de prioridad por el RANV y se le da estilo a las celdas segun sea la prioridad
            idxAPrioridadRANV(ref cellPrioridad);
        }

    }

    private void idxAPrioridadRANV(ref TableCell cellPrioridad)
    {
        String p = cellPrioridad.Text;
        int RANV;
        if (int.TryParse(p, out RANV))
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

    protected void grvFRAP_DataBinding(object sender, EventArgs e)
    {
        //Se detectan las posiciones de las columnas de motivoAtencion y condicionMedica
        idxColumnaMotivoAtencion = Utilidades.GetColumnIndexByDBName(grvFRAP, "motivoAtencion");
        idxColumnaCondicionMedica = Utilidades.GetColumnIndexByDBName(grvFRAP, "condicionMedica");
        idxColumnaTraumaPaciente = Utilidades.GetColumnIndexByDBName(grvFRAP, "Trauma");
        idxColumnaPrioridad = Utilidades.GetColumnIndexByDBName(grvFRAP, "prioridad");
    }
    protected void btBuscar_Click(object sender, EventArgs e)
    {
        String nombre = tbNombrePaciente.Text;
        DateTime fechaInicial = new DateTime(), fechaFinal = new DateTime();
        int returnVal;

        DateTime.TryParse(tbFechaInicial.Text,out fechaInicial);
        DateTime.TryParse(tbFechaFinal.Text, out fechaFinal);

        DSFrap ds = DataAccessFRAP.busquedaConFiltros(fechaInicial, fechaFinal, nombre, ddlTrauma.SelectedIndex, out returnVal);

        grvFRAP.DataSource = ds.vFrapYPacientes;
        grvFRAP.DataBind();
    }
}