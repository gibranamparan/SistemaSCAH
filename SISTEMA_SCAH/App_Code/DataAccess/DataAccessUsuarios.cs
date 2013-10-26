using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for DAUsuarios
/// </summary>
public class DataAccessUsuarios
{
    //Miembros de datos
    SqlConnection sqlc;

    //Se enlistan los nombres de las secciones a las que pueden tener acceso los usuarios.
    public enum FuncionesUsuario { VerUsuarios=1, AltaFRAP, BusquedaFRAP, UltimosRegistrosFRAP };

    //Se enlistan los nombres de los tipos de usuarios en el sistema.
    public enum TipoUsuario { Médico=1, Paramédico, Administrador};

    //Se enlistan los posibles errores al dar de alta un usuario
    public enum errorAltaUsuario{ UsuarioYaExiste=-1, ErrorBaseDatos=-2};

    //Se enlistan los posibles errores al buscar un usuario
    public enum errorConsultaUsuario { UsuarioNoEncontrado = -1, ErrorBaseDatos = -2 };

    //ConnectionString es las instrucciones para conectar a la base de datos
    String connStr = ConfigurationManager.ConnectionStrings["SCA-HConnectionString"].ConnectionString;

	public DataAccessUsuarios()
	{
        //Se crea una conexion utilizando el connection String.
        sqlc = new SqlConnection(connStr);
	}

    private void abrirConexion()
    {
        sqlc.Open();
    }

    private void cerrarConexion()
    {
        sqlc.Close();
    }

    /*METODOS PARA ALTAS*/
    /*Crea un nuevo usuario en la tabla de Usuarios, regresa un entero que indica cuantos registros se crearon o
     * el tipo de error numerado
     */
    public int altaUsuario(String nombre, String contraseña, int tipoUsuario)
    {
        //Abrir la conexion
        abrirConexion();
        
        //Declarar variables auxilires
        Object returnValue = new Object();

        //Diseñar el query que hace la funcion que deseamos
        //String query = "insert into Usuarios (NombreUsuario,Contraseña,TipoUsuario) values ('" + nombre + "','" + contraseña + "'," + tipoUsuario.ToString() + ")";


        //Alternativa usando Stored Procedures
        String storedProcedure = "spAltaUsuario";

        //Declarar el objeto para ejecutar el query
        SqlCommand sqlcomm = new SqlCommand(storedProcedure, sqlc);
        sqlcomm.CommandType = CommandType.StoredProcedure;

        //Se agregan parametros de entrada y salida al procedimiento
        sqlcomm.Parameters.AddWithValue("@NombreUsuario", nombre);
        sqlcomm.Parameters.AddWithValue("@Contraseña", contraseña);
        sqlcomm.Parameters.AddWithValue("@TipoUsuario", tipoUsuario);
        sqlcomm.Parameters.Add("@RETURN_VALUE",SqlDbType.SmallInt).Direction=ParameterDirection.ReturnValue; 
        
        /* return value -1: Ya existe usuario
         * return value -2: Problema de conexino o creacion nuevo registro en tabla
         */
        try
        {
            //EJECUTAR Procedimiento almacenado
            returnValue = sqlcomm.ExecuteScalar();

            //EJECUTAR EL QUERY!
            //returnValue = (int)sqlcomm.ExecuteNonQuery();
        }
        catch(Exception exc)
        {
            //Deteccion de problemas en la ejecucion
            returnValue = -2;
            String excString = exc.Message;
        }

        //Cerrar conexion
        cerrarConexion();

        return (int)returnValue;
    }

    
    /*METODOS PARA CONSULTAS*/
    //Consulta de todos los usuarios
    /*
    public DataSet consultaUsuarios(String nombreUsuario, String contraseña, out int returnVal)
    {
      
        //Declarar variables auxilires
        returnVal = 0;
        DataSet ds = new DataSet();


        //Diseñar el query que hace la funcion que deseamos
        String query = "select Usuarios.NombreUsuario, Usuarios.Contraseña, TipoUsuarios.NombreTipoUsuario from usuarios inner join TipoUsuarios on Usuarios.TipoUsuario=TipoUsuarios.IdTipoUsuario order by NombreUsuario";

        
        //Declarar el objeto para ejecutar el query
        SqlCommand sqlcomm = new SqlCommand(query, sqlc);

        try
        {
            //Abrir la conexion
            abrirConexion();

            //EJECUTAR EL QUERY!
            //SqlDataReader sdr = sqlcomm.ExecuteReader();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            sda.Fill(ds);

            //Se cuenta el numero de usuarios regresados por la consulta
            returnVal = ds.Tables[0].Rows.Count;
        }
        catch
        {
            //Deteccion de problemas en la ejecucion
            returnVal = -1;
        }

        //Cerrar conexion
        cerrarConexion();

        return ds;
    }*/

    /*METODOS PARA CONSULTAS*/
    //Consulta de todos los usuarios
    
    public DataSet consultaUsuarios(out bool operacionExitosa)
    {
        //Abrir la conexion
        abrirConexion();


        //Declarar variables auxilires
        operacionExitosa = true;
        DataSet ds = new DataSet();


        //Diseñar el query que hace la funcion que deseamos
        String query = "select Usuarios.NombreUsuario, Usuarios.Contraseña, TipoUsuarios.NombreTipoUsuario from usuarios inner join "+
            "TipoUsuarios on Usuarios.TipoUsuario=TipoUsuarios.IdTipoUsuario order by NombreUsuario";

        
        //Declarar el objeto para ejecutar el query
        SqlCommand sqlcomm = new SqlCommand(query, sqlc);

        try
        {
            //EJECUTAR EL QUERY!
            //SqlDataReader sdr = sqlcomm.ExecuteReader();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            sda.Fill(ds);
        }
        catch
        {
            //Deteccion de problemas en la ejecucion
            operacionExitosa = false;
        }
        //Deteccion de problemas en la ejecucion
        if (ds.Tables.Count==0 || ds.Tables[0].Rows.Count==0)
        {
            operacionExitosa = false;
        }

        //Cerrar conexion
        cerrarConexion();

        return ds;
    }
    
    //Consulta de 1 usuarios en la base de datos mediante su NOMBRE DE USUARIO
    public DataSet consultaUnUsuarioSP(String nombreUsuario, out int returnValue)
    {
        //Abrir la conexion
        abrirConexion();

        //Declarar variables auxilires
        returnValue=0;
        DataSet ds = new DataSet();

        //Alternativa usando Stored Procedures
        String storedProcedure = "spConsultaUsuario";

        //Declarar el objeto para ejecutar el query
        SqlCommand sqlcomm = new SqlCommand(storedProcedure, sqlc);
        sqlcomm.CommandType = CommandType.StoredProcedure;

        //Se agregan parametros de entrada a el Procedimiento
        sqlcomm.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

        try
        {
            //EJECUTAR Procedimiento almacenado, guardar resultado en un DataSet y pasarlo
            //a un DSUsuario
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            sda.Fill(ds);

            //Se regresa el numero de registros que resultan del query
            returnValue = ds.Tables[0].Rows.Count;

        }
        catch (Exception exc)
        {
            //Deteccion de problemas en la ejecucion
            returnValue = -1;
            String excString = exc.Message;
        }

        //Cerrar conexion
        cerrarConexion();

        return ds;
    }

    //Consulta de 1 usuarios en la base de datos mediante su NOMBRE DE USUARIO
    public static DSUsuarios consultaUnUsuario(String nombreUsuario, out int returnValue)
    {
        DSUsuariosTableAdapters.UsuariosTableAdapter TAUsuarios = new DSUsuariosTableAdapters.UsuariosTableAdapter();
        DSUsuariosTableAdapters.PermisosTableAdapter TAPermisos = new DSUsuariosTableAdapters.PermisosTableAdapter();
        DSUsuarios ds = new DSUsuarios();

        try
        {
            //Se consulta de la base de datos el usuario que se encuentra ingresando
            ds.Usuarios.Merge(TAUsuarios.GetData(nombreUsuario));
            ds.Usuarios.AcceptChanges();

            //Se adquieren los permisos asociados al usuario
            ds.Permisos.Merge(TAPermisos.GetData(ds.Usuarios[0].TipoUsuario));
            ds.Permisos.AcceptChanges();

            returnValue = ds.Usuarios.Rows.Count;
        }
        catch (IndexOutOfRangeException exc)
        {
            returnValue = (int) errorConsultaUsuario.UsuarioNoEncontrado;
        }
        catch (Exception exc)
        {
            returnValue = (int)errorConsultaUsuario.ErrorBaseDatos;
        }

        return ds;
    }

    //Entrega una tabla de todos los tipos de usuarios en la base de datos
    public static DSUsuarios consultaTiposUsuarios(out int returnValue)
    {
        DSUsuariosTableAdapters.TipoUsuariosTableAdapter dstu = new DSUsuariosTableAdapters.TipoUsuariosTableAdapter();
        DSUsuarios ds = new DSUsuarios();

        try
        {
            ds.TipoUsuarios.Merge(dstu.GetData());
            ds.TipoUsuarios.AcceptChanges();
            returnValue = ds.TipoUsuarios.Rows.Count;
        }
        catch
        {
            returnValue = -1;
        }

        return ds;
    }

    //Consulta los permisos de uso de funciones de 1 usuarios en la base de datos mediante su idUser
    public static DSUsuarios consultaPermisoUsuario(int userId, out int returnValue)
    {
        DSUsuariosTableAdapters.PermisosTableAdapter permisosAdapter = new DSUsuariosTableAdapters.PermisosTableAdapter();
        DSUsuariosTableAdapters.UsuariosTableAdapter usuariosAdapter = new DSUsuariosTableAdapters.UsuariosTableAdapter();


        DSUsuarios ds = new DSUsuarios();
        try
        {

            ds.Usuarios.Merge(usuariosAdapter.GetDataBy(userId));
            ds.Usuarios.AcceptChanges();

            int tipoUsuario=ds.Usuarios[0].TipoUsuario;
            
            ds.Permisos.Merge(permisosAdapter.GetData(tipoUsuario));
            ds.Permisos.AcceptChanges();

            returnValue = ds.Permisos.Rows.Count;
        }
        catch
        {
            returnValue = -1;
        }

        return ds;
    }
}