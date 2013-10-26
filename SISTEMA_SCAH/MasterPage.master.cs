using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Funciones = DataAccessUsuarios.FuncionesUsuario;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //Si hay un usuario logeado y si ha habido un cambio de usuario
        if (Session["idUser"] != null)
        {
            //Se almacena el userId a una variable temporal para en el futuro si se ha cambiado de usuario
            if (Session["idUserTemp"] == null)
                Session.Add("idUserTemp", Session["idUser"]);
            else
                Session["idUserTemp"] = Session["idUser"];

            //Inicializar variables auxiliares
            int retval;
            int userId = (int)Session["idUser"];
            DSUsuarios ds = new DSUsuarios();

            //Se consulta cuales son los permisos del usuario y si tiene algun permiso, se activan en el menu.
            ds = DataAccessUsuarios.consultaPermisoUsuario(userId, out retval);

            //Se activa el panel de informacion de registro de usuario
            pnlLoginView.Visible = true;
            lblNombreUsuario.Text = ds.Usuarios[0].NombreUsuario;


            //Si existen permisos para el usuario, se activan los enlaces correspondientes en el menu
            if (retval > 0)
                activarEnlacesMenu(ds);
        }
    }
    
    private void activarEnlacesMenu(DSUsuarios ds)
    {
        String url = String.Empty;
        MenuItem temp;

        //Se borran todos los elementos del menu para agregar aquellos a los que el usuario tiene permiso.
        //Se agregan links a secciones de acceso de uso común
        menuPrincipal.Items.Clear();

        temp = new MenuItem("Inicio", "Inicio");
        temp.NavigateUrl = "~/Default.aspx";
        menuPrincipal.Items.Add(temp);

        temp = new MenuItem("Ingresar", "Ingresar");
        temp.NavigateUrl = "~/Cuenta/Ingreso.aspx";
        menuPrincipal.Items.Add(temp);

        temp = new MenuItem("FAQ", "FAQ");
        temp.NavigateUrl = "~/Ayuda.aspx";
        menuPrincipal.Items.Add(temp);

        //Se crea el elemento del submenu FRAP.
        MenuItem FRAP = new MenuItem();
        int idxMenuFrap = 0;

        //Se declara el submenu de Usuarios.
        MenuItem Usuarios=new MenuItem();
        int idxMenuAdminUsuarios = 0;

        //Se procede a activar cada una de las funciones que el tipo de usuario tiene permitidas.
        //Por cada funcion permitida
        foreach (DSUsuarios.PermisosRow row in ds.Permisos)
            //Se detecta la funcion, se crea el elemento de menu para esta.
            //Si es la primera función que corresponde a un submenu, se crea el submenu.
            switch ((Funciones)row.Funcion)
            {
                case Funciones.AltaFRAP:
                    url = "~/FRAP/AltaFRAP.aspx";
                    temp = new MenuItem(row.NombreFuncion, row.idFuncion.ToString());
                    temp.NavigateUrl = url;

                    if (!menuPrincipal.Items.Contains(FRAP))
                    {
                        FRAP = new MenuItem("FRAP", "FRAP");
                        menuPrincipal.Items.Add(FRAP);
                        idxMenuFrap = menuPrincipal.Items.IndexOf(FRAP);
                    }

                    menuPrincipal.Items[idxMenuFrap].ChildItems.Add(temp);
                    break;
                case Funciones.BusquedaFRAP:
                    url = "~/FRAP/BusquedaFRAP.aspx";
                    temp = new MenuItem(row.NombreFuncion, row.idFuncion.ToString());
                    temp.NavigateUrl = url;
                    menuPrincipal.Items[idxMenuFrap].ChildItems.Add(temp);
                    break;
                case Funciones.UltimosRegistrosFRAP:
                    url = "~/FRAP/UltimosRegistrosFRAP.aspx";
                    temp = new MenuItem(row.NombreFuncion, row.idFuncion.ToString());
                    temp.NavigateUrl = url;
                    menuPrincipal.Items[idxMenuFrap].ChildItems.Add(temp);
                    break;
                case Funciones.VerUsuarios:
                    url = "~/Cuenta/VerUsuarios.aspx";
                    temp = new MenuItem(row.NombreFuncion, row.idFuncion.ToString());
                    temp.NavigateUrl = url;

                    if (!menuPrincipal.Items.Contains(Usuarios))
                    {
                        //Se crea el elemento del submenu Administración de Usuarios
                        Usuarios = new MenuItem("Administracion de Usuarios", "AdminUsuarios");
                        menuPrincipal.Items.Add(Usuarios);
                        idxMenuAdminUsuarios = menuPrincipal.Items.IndexOf(Usuarios);
                    }

                    menuPrincipal.Items[idxMenuAdminUsuarios].ChildItems.Add(temp);
                    break;
            }
    }

    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        pnlLoginView.Visible = false;
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("~/Default.aspx");
    }
}
