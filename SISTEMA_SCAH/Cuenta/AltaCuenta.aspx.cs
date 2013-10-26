using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Paginas_Cuenta_AltaCuenta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int returnval;
        DSUsuarios ds=DataAccessUsuarios.consultaTiposUsuarios(out returnval);

        if(returnval>0)
            foreach (DSUsuarios.TipoUsuariosRow renglon in ds.TipoUsuarios)
                ddlTipoUsuario.Items.Add(new ListItem(renglon.NombreTipoUsuario, renglon.IdTipoUsuario.ToString()));

    }
    protected void btAceptar_Click(object sender, EventArgs e)
    {        
        lblErrorAltaCuenta.ForeColor = Color.Red;
        if(tbCContraseña.Text.Trim().Equals(tbContraseña.Text.Trim()))
        {
            //Declarar algunas variables auxiliares
            int returnValue = 0;

            //Creo el objeto que me permite tener acceso a la BD
            DataAccessUsuarios connUsuaro = new DataAccessUsuarios();

            //Capturar la informacion que el usuario introdujo en la
            //capa de presentacion
            String nombre=tbNombreUsuario.Text.Trim();
            String pass=tbContraseña.Text.Trim();
            int tipoUser= int.Parse(ddlTipoUsuario.SelectedValue);
            
            //Aqui se manda los datos del nuevo usuario a la base de datos
           
            returnValue = connUsuaro.altaUsuario(nombre, pass, tipoUser);


            //Validar si la operacion en la BD fue exitosa
            /* return value -1: Ya existe usuario
             return value -2: Problema de conexino o creacion nuevo registro en tabla*/
        
            if (returnValue > 0)
            {
                lblErrorAltaCuenta.ForeColor = Color.Green;
                lblErrorAltaCuenta.Text = "Cuenta dada de alta";
            }
            else
            {
                switch ((DataAccessUsuarios.errorAltaUsuario) returnValue)
                {
                    case DataAccessUsuarios.errorAltaUsuario.UsuarioYaExiste:
                        lblErrorAltaCuenta.Text = "El nombre de usuario ya se encuentra registrado.";
                        break;
                    case DataAccessUsuarios.errorAltaUsuario.ErrorBaseDatos:
                        lblErrorAltaCuenta.Text = "Problemas con la base de datos, intentalo nuevamente.";
                        break;
                }
            }
        }
        else
            lblErrorAltaCuenta.Text="Verifique que la confirmacion de contraseña sea correcta.";
        
    }
}