using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for DataAccessUsuario2
/// </summary>
public class DataAccessUsuario2
{
    //Se adquiere el ConnectionString configurado en el archivo web.config
    String connString = ConfigurationManager.ConnectionStrings["SCA-HConnectionString"].ConnectionString;
    SqlConnection sqlc;

	public DataAccessUsuario2()
	{
        sqlc = new SqlConnection(connString);
	}

    //TENEMOS QUE SABER QUE ES UN DATASET ANTES DE HACER
    //EL METODO DE VERIFICACION DE USUARIO
    /*
    public int verificaUsuario(String nombreUsuario)
    {

        return 0;
    }*/

    /*Se regresa returnVal en las siguientes situaciones:
     * returnVal:-1, Cuando hubo una falla en la conexino a la BD
     * returnVal:0, Cuando hay conexion exitosa, pero por alguna razon no se dio de alta el usuario
     * returnVal:1, Cuando se ha dado de alta satisfactoriamente 1 usuario
     * */
    public int altaUsuario(String nombreUsuario,String contraseña,int tipoUsuario)
    {
        sqlc.Open();
        int returnVal=0;

        //Se diseña el query para insertar un nuevo registro en la tabla de Usuarios
        String query = "INSERT INTO Usuarios (NombreUsuario,Contraseña,TipoUsuario) "+
            "VALUES ('" + nombreUsuario + "','" + contraseña + "'," + tipoUsuario.ToString()+ ")";

        try
        {
            SqlCommand sqlcomm = new SqlCommand(query, sqlc);
            returnVal = sqlcomm.ExecuteNonQuery();
        }
        catch(SqlException exec)
        {
            //Uso del programador
            String error = exec.Message;

            returnVal=-1;
        }

        sqlc.Close();

        return returnVal;
    }

    public DataSet consultaUsuarios(String nombreUsuario, String contraseña, out int returnVal)
    {

        //Declarar variables auxiliares
        returnVal = 0;
        DataSet ds = new DataSet();


        //Diseñar el query que hace la funcion que deseamos
        //String query = "select Usuarios.NombreUsuario, Usuarios.Contraseña, TipoUsuarios.NombreTipoUsuario from usuarios inner join TipoUsuarios on Usuarios.TipoUsuario=TipoUsuarios.IdTipoUsuario order by NombreUsuario";
        String query = "select * from Usuarios where Usuarios.NombreUsuario='"
            + nombreUsuario + "' AND Usuarios.Contraseña='" + contraseña + "'";


        //Declarar el objeto para ejecutar el query
        SqlCommand sqlcomm = new SqlCommand(query, sqlc);

        try
        {
            //Abrir la conexion
            sqlc.Open();

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
        sqlc.Close();

        return ds;
    }
}