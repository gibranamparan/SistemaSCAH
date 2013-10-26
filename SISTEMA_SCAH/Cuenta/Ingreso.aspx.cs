using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class Paginas_Cuenta_Ingreso : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btIngresar_Click(object sender, EventArgs e)
    {        
        lblInfo.ForeColor = Color.Red;
        if(datosEntradaValidos())
        {
            int registros;

            //Se busca el usuario en la tabla de Usuarios
            DSUsuarios ds = DataAccessUsuarios.consultaUnUsuario(tbNombreUsuario.Text, out registros);

            //Si el usuario se encuentra en la base de datos
            if (registros > 0)
            {
                //Si la contraseña conincide con la almacenada en la base de datos
                if (contraseñaValida(ds.Usuarios[0].Contraseña))
                {
                    //Captura y almacena el ID de usuario en el sistema
                    int userId = ds.Usuarios[0].idUsuario;

                    if (Session["idUser"] == null)
                        Session.Add("idUser", userId);
                    else
                        Session["idUser"] = userId;

                    if (Session["DSUser"] == null)
                        Session.Add("DSUser", ds);
                    else
                        Session["DSUser"] = ds;


                    //Redirecciona a la página inicial.
                    Response.Redirect("~/Default.aspx");
                }
                else
                    lblInfo.Text = "Contraseña incorrecta, inténtelo de nuevo.";
            }
            else
                switch ((DataAccessUsuarios.errorConsultaUsuario)registros)
                {
                    case DataAccessUsuarios.errorConsultaUsuario.UsuarioNoEncontrado:
                        lblInfo.Text = "El usuario no esta registrado al sistema, dirigase a la seccion de alta de cuentas o registrese con una cuenta existente.";
                        break;
                    case DataAccessUsuarios.errorConsultaUsuario.ErrorBaseDatos:
                        lblInfo.Text = "Problemas con la base de datos, intentalo nuevamente.";
                        break;
                }
        }
        else
            lblInfo.Text = "Ingrese nombre de usuario y contraseña.";
         
        
    }

    private bool contraseñaValida(String pass)
    {
        return tbContraseña.Text == pass;
    }

    private bool datosEntradaValidos()
    {
        tbContraseña.Text = tbContraseña.Text.Trim();
        tbNombreUsuario.Text = tbNombreUsuario.Text.Trim();

        //Los datos son validos el nombre de usuario y contraseña han sido llenados con laguna informacion
        return tbContraseña.Text.Length > 0 && tbContraseña.Text.Length > 0;
    }
}