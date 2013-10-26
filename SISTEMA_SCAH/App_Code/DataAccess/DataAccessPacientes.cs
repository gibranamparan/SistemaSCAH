using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Permite el acceso a la tabla Pacientes de la base de datos SCA-H para llevar a cabo operaciones como altas, bajas, consultas y modificaciones de pacientes.
/// </summary>

using DSTAPacientes = DSFrapTableAdapters.PacientesTableAdapter;
public class DataAccessPacientes
{
    public enum PacientesError {ErrorAccesoBD=-1,ErrorAltaNuevoRegistro=-2};

	public DataAccessPacientes()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Recibe un conjunto de datos de un paciente para darlo de alta un nuevo registro en la tabla de pacientes en la base de datos SCA-H,
    /// el proceso dá como resultado un entero que indica la clave ID de indetificación del registro creado. En caso de haber un error, se indica con un numero negativo
    /// el cual puede interpretarse con el enum PacientesError de esta clase.
    /// Parameters:
    ///     nombrePaciente: String que representa el nombre del nuevo paciente.
    /// </summary>
    public static int altaPaciente(string nombrePaciente, int sexo, int edad, string domicilio, string colonia, string municipio, string derechohabiencia, string telefono, string ocupacion, string otro)
    {
        DSTAPacientes dsta = new DSTAPacientes();
        int idAlta;

        try
        {
            idAlta = (int)dsta.spAltaPaciente(nombrePaciente, (short)sexo, (short)edad, domicilio, colonia, municipio, derechohabiencia, telefono, ocupacion, otro);
        }
        catch(Exception exc)
        {
            idAlta = (int)PacientesError.ErrorAccesoBD;
        }

        return idAlta;
    }

    /// <summary>
    /// Busca en la tabla Pacientes de la base de datos SCA-H un paciente con un nombre conformado por la secuencia de caracteres de String nombreABuscar.
    /// Da como resultado un DSFrap con la tabla de pacientes conteniendo el resultado de la busqueda, además se genera un valor de regreso returnVal que indica
    /// la cantidad de registros encontrados o un numero entero negativo que reporta algun error, el cual puede hacerse un casting con el enum PacientesError de
    /// esta clase.
    /// </summary>
    public static DSFrap consultaPacientes(string nombreABuscar,out int returnVal)
    {
        DSTAPacientes dsta = new DSTAPacientes();
        DSFrap ds = new DSFrap();

        try
        {
            returnVal=dsta.Fill(ds.Pacientes);
        }
        catch (Exception exc)
        {
            returnVal = (int)PacientesError.ErrorAccesoBD;
        }

        return ds;
    }



    /// <summary>
    /// Busca en la tabla Pacientes de la base de datos SCA-H un paciente con un nombre conformado por la secuencia de caracteres de String nombreABuscar.
    /// Da como resultado un DSFrap con la tabla de pacientes conteniendo el resultado de la busqueda, además se genera un valor de regreso returnVal que indica
    /// la cantidad de registros encontrados o un numero entero negativo que reporta algun error, el cual puede hacerse un casting con el enum PacientesError de
    /// esta clase.
    /// </summary>
    public static DSFrap consultaPacientePorID(int ID, out int returnVal)
    {
        DSTAPacientes dsta = new DSTAPacientes();
        DSFrap ds = new DSFrap();

        try
        {
            returnVal = dsta.FillByID(ds.Pacientes, ID);
        }
        catch (Exception exc)
        {
            returnVal = (int)PacientesError.ErrorAccesoBD;
        }

        return ds;
    }
}