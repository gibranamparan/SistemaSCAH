using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Funciones = DataAccessUsuarios.FuncionesUsuario;

public partial class Cuenta_VerUsuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["idUser"] == null)
        {
            Response.Redirect("~/Error.aspx");
        }
        else
        {
            DSUsuarios DSUser = (DSUsuarios)(Session["DSUser"]);
            DSUsuarios.PermisosRow pr = DSUser.Permisos.FindByidFuncion((int)Funciones.VerUsuarios);
            if (pr != null)
            {
                bool operacionExitosa;
                DataAccessUsuarios dau = new DataAccessUsuarios();
                DataSet dsListaUsuarios = dau.consultaUsuarios(out operacionExitosa);

                grvUsuarios.DataSource = dsListaUsuarios;
                grvUsuarios.DataBind();
            }
            else
                Response.Redirect("~/Error.aspx");

        }
    }
}