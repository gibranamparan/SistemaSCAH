using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DataAccessFRAP
/// </summary>
/// 
using DSTAFrap = DSFrapTableAdapters.FRAPTableAdapter;
using DSTAVistaFRAPYPacientes = DSFrapTableAdapters.vFrapYPacientesTableAdapter;
using DSTAPaciente = DSFrapTableAdapters.PacientesTableAdapter;
using DSTACondicionMedica = DSFrapTableAdapters.condicionMedicaTableAdapter;
using DSTAMotivoAtencion = DSFrapTableAdapters.motivoAtencionTableAdapter;

using DSTATraumaPaciente = DSFrapTableAdapters.traumaPacienteTableAdapter;

public class DataAccessFRAP
{
    public enum PrioridadRANV { R = 0, A = 1, N=2, V=3 };
    public enum EstadoFRAP { Terminado = 1, Cancelado = 2 };
    public enum FRAPError { ErrorAccesoBD = -1, ErrorAltaNuevoRegistro = -2 };

	public DataAccessFRAP()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Recibe un conjunto de datos de un registro FRAP para darlo de alta en la tabla de FRAPs en la base de datos SCA-H,
    /// el proceso dá como resultado un entero que indica la clave ID de indetificación del registro creado. En caso de haber
    /// un error, se indica con un numero negativo el cual puede interpretarse con el enum FRAPError de esta clase.
    /// </summary>
    public static int altaFRAP(int prioridad,DateTime fechaHoraSalidaServicio, string callesNumero, string colonia, string municipio, int lugarOcurrenciaID, string lugarOcurrenciaOtro,
  string motivoAtencion, string Trauma, int pacienteID, string sopVitAvanzadoAID, string sopVitAvanzadoBID, string sopVitAvanzadoCID,
  int estVitRespiracionNum, int estVitRespiracionTipo, int estVitPulsoNum, int estVitPulsoTipo, int presionNum, int presionDen, double glicemia, int temperatura,
  int glasgowAO, int glasgowRV, int glasgowRM, int estadoPaciente, string condicionMedica, string lesiones, string antecedentesAlergias,
  string antecedentesMedicamentos, string antecedentesPatologias, string antecedentesCirugias, string sopVitBasicoA, string sopVitBasicoB,
  string sopVitBasicoC, int DEA1Tipo, int DEA1Valor, int DEA2Valor, int DEA2Tipo, int DEA3Valor, int DEA3Tipo, string impresionDiagnostica,
  EstadoFRAP estado, DateTime fechaFinLlenado)
    {
        int idAlta;
        DSTAFrap dsta = new DSTAFrap();
        
        //Se transforman los datos de fecha a String con el formato indicado.
        /*FORMATO: yyyy-mm-dd hh:mm:ss (24h)*/
        String salidaAlServicio = fechaHoraSalidaServicio.ToString("yyyy-MM-dd hh:mm:ss");
        String fechaFin = fechaFinLlenado.ToString("yyyy-MM-dd hh:mm:ss");
        try
        {
            idAlta = (int)dsta.spAltaFRAP((byte)prioridad,salidaAlServicio, callesNumero, colonia, municipio, (byte)lugarOcurrenciaID, lugarOcurrenciaOtro, motivoAtencion, Trauma, pacienteID,
                sopVitAvanzadoAID, sopVitAvanzadoBID, sopVitAvanzadoCID, (byte)estVitRespiracionNum, (byte)estVitRespiracionTipo, (byte)estVitPulsoNum, (byte)estVitPulsoTipo,
                (byte)presionNum, (byte)presionDen, glicemia, (byte)temperatura, (byte)glasgowAO, (byte)glasgowRV, (byte)glasgowRM, (byte)estadoPaciente, condicionMedica,
                lesiones, antecedentesAlergias, antecedentesMedicamentos, antecedentesPatologias, antecedentesCirugias, sopVitBasicoA, sopVitBasicoB,
                sopVitBasicoC, (byte)DEA1Tipo, (byte)DEA1Valor, (byte)DEA2Valor, (byte)DEA2Tipo, (byte)DEA3Valor, (byte)DEA3Tipo, impresionDiagnostica,
                (byte)estado, fechaFin);
        }
        catch (Exception exc)
        {
            idAlta = (int)FRAPError.ErrorAccesoBD;
        }
        return idAlta;
    }

    public static DSFrap consultaFRAPs(out int returnVal)
    {

        DSTAFrap dsta = new DSTAFrap();
        DSFrap ds = new DSFrap();

        try
        {
            returnVal = dsta.Fill(ds.FRAP);
        }
        catch (Exception exc)
        {
            returnVal = (int)FRAPError.ErrorAccesoBD;
        }

        return ds;
    }

    public static DSFrap consultaFRAPs(DateTime fechaLimiteInferior,out int returnVal)
    {
        returnVal = 0;
        DSTAFrap dsta = new DSTAFrap();
        DSFrap ds = new DSFrap();

        try
        {
            returnVal=dsta.FillByMayorFechaLimiteInferior(ds.FRAP, fechaLimiteInferior);
        }
        catch (Exception exc)
        {
            returnVal = (int)FRAPError.ErrorAccesoBD;
        }

        return ds;
    }

    public static DSFrap consultaVistaFRAPsYPacientes(DateTime fechaLimiteInferior, out int returnVal)
    {
        returnVal = 0;
        DSTAVistaFRAPYPacientes dsta = new DSTAVistaFRAPYPacientes();
        DSFrap ds = new DSFrap();

        try
        {
            returnVal = dsta.FillByMayorFechaLimiteInferior(ds.vFrapYPacientes, fechaLimiteInferior);
            
        }
        catch (Exception exc)
        {
            returnVal = (int)FRAPError.ErrorAccesoBD;
        }

        return ds;
    }

    public static DSFrap busquedaConFiltros(DateTime fechaInicio, DateTime fechaFin, String nombrePaciente, int traumabuscado, out int returnVal)
    {
        returnVal = 0;
        DSTAVistaFRAPYPacientes dsta = new DSTAVistaFRAPYPacientes();
        DSFrap ds = new DSFrap();

        if (fechaInicio.Year < 1753)
        {
            fechaInicio = fechaInicio.AddYears(1753 - fechaInicio.Year);
        }
        if (fechaFin.Year < 1753)
        {
            fechaFin = fechaFin.AddYears(1753 - fechaFin.Year);
        }
        
        try
        {
            returnVal = dsta.FillBySPBuscarFRAPPorFiltros(ds.vFrapYPacientes, fechaInicio, fechaFin, nombrePaciente, traumabuscado.ToString());

        }
        catch (Exception exc)
        {
            returnVal = (int)FRAPError.ErrorAccesoBD;
        }

        return ds;
    }

    public static DSFrap consultaFRAPPorId(int ID, out int returnVal)
    {
        returnVal = 0;
        DSTAFrap dsta = new DSTAFrap();
        DSTAPaciente dstapac=new DSTAPaciente();
        DSFrap ds = new DSFrap();

        try
        {
            //Se toma el FRAP por ID
            returnVal = dsta.FillById(ds.FRAP,ID);
            //Si la consulta fue exitosa
            if (returnVal > 0)
            {
                //Se toma el paciente asociado al FRAP
                int idPaciente = ds.FRAP[0].pacienteID;
                returnVal=dstapac.FillByID(ds.Pacientes, idPaciente);
            }
        }
        catch (Exception exc)
        {
            returnVal = (int)FRAPError.ErrorAccesoBD;
        }

        return ds;
    }

    public static DSFrap motivoAtencion(out int returnVal)
    {

        returnVal = 0;
        DSTAMotivoAtencion dsta = new DSTAMotivoAtencion();
        DSFrap ds = new DSFrap();

        try
        {
            returnVal = dsta.Fill(ds.motivoAtencion);
        }
        catch (Exception exc)
        {
            returnVal = (int)FRAPError.ErrorAccesoBD;
        }

        return ds;
    }

    public static DSFrap condicionMedica(out int returnVal)
    {

        returnVal = 0;
        DSTACondicionMedica dsta = new DSTACondicionMedica();
        DSFrap ds = new DSFrap();

        try
        {
            returnVal = dsta.Fill(ds.condicionMedica);
        }
        catch (Exception exc)
        {
            returnVal = (int)FRAPError.ErrorAccesoBD;
        }

        return ds;
    }



    public static DSFrap traumaPaciente(out int returnVal)
    {

        returnVal = 0;
        DSTATraumaPaciente dsta = new DSTATraumaPaciente();
        DSFrap ds = new DSFrap();

        try
        {
            returnVal = dsta.Fill(ds.traumaPaciente);
        }
        catch (Exception exc)
        {
            returnVal = (int)FRAPError.ErrorAccesoBD;
        }

        return ds;
    }
}