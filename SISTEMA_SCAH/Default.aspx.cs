using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DSUser"] != null)
        {
            DSUsuarios dsUsuario = (DSUsuarios)Session["DSUser"];
            lblNombreUsuario.Visible = true;
            lblTipoUsuario.Visible = true;
            lblNombreUsuario.Text = dsUsuario.Usuarios[0].NombreUsuario;
            lblTipoUsuario.Text = "Bienvenido "+dsUsuario.Usuarios[0].NombreTipoUsuario;
        }
        else
        {
            lblNombreUsuario.Visible = false;
            lblTipoUsuario.Visible = false;
            lblNombreUsuario.Text = String.Empty;
            lblTipoUsuario.Text = String.Empty;
        }
    }
}