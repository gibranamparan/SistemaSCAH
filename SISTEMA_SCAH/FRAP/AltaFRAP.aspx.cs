using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Funciones = DataAccessUsuarios.FuncionesUsuario;
using DSTAFRAP = DSFrapTableAdapters.FRAPTableAdapter;

public partial class _Default : System.Web.UI.Page
{
    DateTime fechaSalidaServicio;
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        lblFecha.Text = DateTime.Now.ToString();
        if (Session["idUser"] == null)
        {
            Response.Redirect("~/Error.aspx");
        }
        else
        {
            DSUsuarios ds = (DSUsuarios)(Session["DSUser"]);
            DSUsuarios.PermisosRow pr = ds.Permisos.FindByidFuncion((int)Funciones.AltaFRAP);
            if (pr != null)
            {*/

        int returnVal;
        DSFrap dsTraumas= DataAccessFRAP.traumaPaciente(out returnVal);
        if(returnVal>0)
        {
            cblTraumas.DataSource = dsTraumas.traumaPaciente;
            cblTraumas.DataBind();
        }

        if (Request.Params.Get("idFRAP") != null)
        {
            altaFRAPModoLectura(true);
            int idFrap = int.Parse(Request.Params.Get("idFRAP"));
            int returnValFRAP;
            DSFrap dsfrap = DataAccessFRAP.consultaFRAPPorId(idFrap, out returnValFRAP);
            if (returnValFRAP > 0)
            {
                llenarFRAPConRegistro(dsfrap);
            }
        }
            /*}
            else
                Response.Redirect("~/Error.aspx");
        }*/
    }

    private void llenarFRAPConRegistro(DSFrap dsfrap)
    {
        //TextBox en solo lectura.
        tbAlergias.Text = dsfrap.FRAP[0].antecedentesAlergias;
        tbCalleYNumero.Text = dsfrap.FRAP[0].callesNumero;
        tbCirugías.Text = dsfrap.FRAP[0].antecedentesCirugias;
        tbColoniaServicio.Text = dsfrap.FRAP[0].colonia;
        tbFechaHorasSalidaaServicio.Text = dsfrap.FRAP[0].fechaHoraSalidaAlServicio.ToString();
        tbGlicemia.Text = dsfrap.FRAP[0].glicemia.ToString();
        tbImpresionDiagnostica.Text = dsfrap.FRAP[0].impresionDiagnostica;
        tbMedicamentos.Text = dsfrap.FRAP[0].antecedentesMedicamentos;
        tbMunicipioServicio.Text = dsfrap.FRAP[0].municipio;
        tbOtroLugarOcurrencia.Text = dsfrap.FRAP[0].lugarOcurrenciaOtro;
        tbPatologías.Text = dsfrap.FRAP[0].antecedentesPatologias;
        tbPresionDen.Text = dsfrap.FRAP[0].presionDen.ToString();
        tbPresionNum.Text = dsfrap.FRAP[0].presionNum.ToString();
        tbPulso.Text = dsfrap.FRAP[0].estVitPulsoNum.ToString();
        ddlPulso.SelectedIndex = dsfrap.FRAP[0].estVitPulsoTipo;
        tbRespiracion.Text = dsfrap.FRAP[0].estVitRespiracionNum.ToString();
        ddlRespiracion.SelectedIndex = dsfrap.FRAP[0].estVitRespiracionTipo;
        tbRitmoCardiacoDEA1.Text = dsfrap.FRAP[0].DEA1Valor.ToString();
        ddlTipoRitmoDEA1.SelectedIndex = dsfrap.FRAP[0].DEA1Tipo;
        tbRitmoCardiacoDEA2.Text = dsfrap.FRAP[0].DEA2Valor.ToString();
        ddlTipoRitmoDEA2.SelectedIndex = dsfrap.FRAP[0].DEA2Tipo;
        tbRitmoCardiacoDEA3.Text = dsfrap.FRAP[0].DEA3Valor.ToString();
        ddlTipoRitmoDEA3.SelectedIndex = dsfrap.FRAP[0].DEA3Tipo;

        tbAO.Text = dsfrap.FRAP[0].glasgowAO.ToString();
        tbRM.Text = dsfrap.FRAP[0].glasgowRM.ToString();
        tbRV.Text = dsfrap.FRAP[0].glasgowRV.ToString();
        tbGlasgowSobre.Text=(dsfrap.FRAP[0].glasgowAO+dsfrap.FRAP[0].glasgowRM+dsfrap.FRAP[0].glasgowRV).ToString();
        tbTemperatura.Text = dsfrap.FRAP[0].temperatura.ToString();

        //Datos de Paciente
        tbColonia.Text = dsfrap.Pacientes[0].colonia;
        tbMunicipio.Text = dsfrap.Pacientes[0].municipio;
        tbNombreOAfiliacion.Text = dsfrap.Pacientes[0].nombrePaciente;
        tbOcupación.Text = dsfrap.Pacientes[0].ocupacion;
        tbOtro.Text = dsfrap.Pacientes[0].otro;
        tbTelefono.Text = dsfrap.Pacientes[0].telefono;
        tbEdad.Text = dsfrap.Pacientes[0].edad.ToString();
        tbDomicilio.Text = dsfrap.Pacientes[0].domicilio;
        ddlSexo.SelectedValue=dsfrap.Pacientes[0].sexo.ToString();
        tbDerechohabiencia.Text = dsfrap.Pacientes[0].derechohabiencia;

        //RadioButtonList deshabilitados.
        rblAVDI.SelectedIndex = dsfrap.FRAP[0].estadoPaciente;
        rblLugarOcurrencia.SelectedIndex = dsfrap.FRAP[0].lugarOcurrenciaID;
        rblRANV.SelectedIndex = dsfrap.FRAP[0].prioridad;
        historialAccionesCanvas.Value = dsfrap.FRAP[0].lesiones;

        //CheckBoxList deshabilitados.
        checarCheckBoxList(ref cblMedica,dsfrap.FRAP[0].condicionMedica);
        checarCheckBoxList(ref cblMotivoDeAtencion, dsfrap.FRAP[0].motivoAtencion);
        checarCheckBoxList(ref cblSoporteVitalAvanzadoA, dsfrap.FRAP[0].sopVitAvanzadoAID);
        checarCheckBoxList(ref cblSoporteVitalAvanzadoB, dsfrap.FRAP[0].sopVitAvanzadoBID);
        checarCheckBoxList(ref cblSoporteVitalAvanzadoC, dsfrap.FRAP[0].sopVitAvanzadoCID);
        checarCheckBoxList(ref cblSoporteVitalBásicoA, dsfrap.FRAP[0].sopVitBasicoA);
        checarCheckBoxList(ref cblSoporteVitalBásicoB, dsfrap.FRAP[0].sopVitBasicoB);
        checarCheckBoxList(ref cblSoporteVitalBásicoC, dsfrap.FRAP[0].sopVitBasicoC);
        checarCheckBoxList(ref cblTraumas, dsfrap.FRAP[0].Trauma);
        
    }

    private void checarCheckBoxList(ref CheckBoxList cbl, string p)
    {
        p = p.Trim(',');
        String[] nums = p.Split(',');
        
        /*for (int c=0;c<=cbl.Items.Count;c++)
            if (nums.Contains(c.ToString()))
                cbl.Items[c].Selected = true;*/
        foreach (String n in nums)
            cbl.Items[int.Parse(n)].Selected = true;
            
    }

    private void altaFRAPModoLectura(bool p)
    {
        //TextBox en solo lectura.
        tbAlergias.ReadOnly = p;
        tbAO.ReadOnly = p;
        tbCalleYNumero.ReadOnly = p;
        tbCirugías.ReadOnly= p;
        tbColonia.ReadOnly = p;
        tbColoniaServicio.ReadOnly = p;
        tbDerechohabiencia.ReadOnly = p;
        tbDomicilio.ReadOnly = p;
        tbEdad.ReadOnly = p;
        tbFechaHorasSalidaaServicio.ReadOnly = p;
        tbGlasgowSobre.ReadOnly = p;
        tbGlicemia.ReadOnly = p;
        tbImpresionDiagnostica.ReadOnly = p;
        tbMedicamentos.ReadOnly = p;
        tbMunicipio.ReadOnly = p;
        tbMunicipioServicio.ReadOnly = p;
        tbNombreOAfiliacion.ReadOnly = p;
        tbOcupación.ReadOnly = p;
        tbOtro.ReadOnly = p;
        tbOtroLugarOcurrencia.ReadOnly = p;
        tbPatologías.ReadOnly = p;
        tbPresionDen.ReadOnly = p;
        tbPresionNum.ReadOnly = p;
        tbPulso.ReadOnly = p;
        tbRespiracion.ReadOnly = p;
        tbRitmoCardiacoDEA1.ReadOnly = p;
        tbRitmoCardiacoDEA2.ReadOnly = p;
        tbRitmoCardiacoDEA3.ReadOnly = p;
        tbRM.ReadOnly = p;
        tbRV.ReadOnly = p;
        tbTelefono.ReadOnly = p;
        tbTemperatura.ReadOnly = p;

        //RadioButtonList deshabilitados.
        rblAVDI.Enabled= !p;
        rblLugarOcurrencia.Enabled = !p;
        rblRANV.Enabled = !p;
        rblSeleccionLesiones.Enabled = !p;

        //DropDownList deshabilitados.
        ddlPulso.Enabled = !p;
        ddlRespiracion.Enabled = !p;
        ddlSexo.Enabled = !p;
        ddlTipoRitmoDEA1.Enabled = !p;
        ddlTipoRitmoDEA2.Enabled = !p;
        ddlTipoRitmoDEA3.Enabled = !p;

        //CheckBoxList deshabilitados.
        cblMedica.Enabled = !p;
        cblMotivoDeAtencion.Enabled = !p;
        cblSoporteVitalAvanzadoA.Enabled = !p;
        cblSoporteVitalAvanzadoB.Enabled = !p;
        cblSoporteVitalAvanzadoC.Enabled = !p;
        cblSoporteVitalBásicoA.Enabled = !p;
        cblSoporteVitalBásicoB.Enabled = !p;
        cblSoporteVitalBásicoC.Enabled = !p;
        cblTraumas.Enabled = !p;

        //Desaparecen botones
        btCancelarFRAP.Visible = !p;
        btAceptarFRAP.Visible = !p;
    }
    protected void btCancelarFRAP_Click(object sender, EventArgs e)
    {

    }
    protected void btAceptarFRAP_Click(object sender, EventArgs e)
    {
        int edad;
        int IdPaciente;
        //Si los datos enteros son validos
        if(validarDatosPaciente(out edad))
        {
            //Se ingresa un nuevo registro de paciente
            IdPaciente = DataAccessPacientes.altaPaciente(tbNombreOAfiliacion.Text.Trim(), int.Parse(ddlSexo.SelectedValue), edad, tbDomicilio.Text.Trim(), tbColonia.Text.Trim(), tbMunicipio.Text.Trim(), tbDerechohabiencia.Text.Trim(),
                tbTelefono.Text.Trim(), tbOcupación.Text.Trim(), tbOtro.Text.Trim());

            //Si el paciente existe en la base de datos
            if (IdPaciente > 0)
            {
                int nuevoidFRAP,respiracion,pulso,presionNum,presionDen,temperatura,glasgowAO,glasgowRV,glasgowRM,prioridad, lugarOcurrencia;
                double glicemia;
                DateTime dtFechaSalidaServicio;

                //Se validan todos los valores numericos obligatorios.
                if (validarDatosFRAP(out prioridad,out lugarOcurrencia, out dtFechaSalidaServicio, out respiracion, out pulso,
                out presionNum, out presionDen, out glicemia, out temperatura, out glasgowAO, out glasgowRV, out glasgowRM))
                {
                    String motivoAtencion = checkedListAString(cblMotivoDeAtencion);
                    String traumas = checkedListAString(cblTraumas);
                    String sopVitAvA = checkedListAString(cblSoporteVitalAvanzadoA);
                    String sopVitAvB = checkedListAString(cblSoporteVitalAvanzadoB);
                    String sopVitAvC = checkedListAString(cblSoporteVitalAvanzadoC);
                    String condicionMedica = checkedListAString(cblMedica);
                    String historialLesiones = historialAccionesCanvas.Value;
                    String sopVitBasA = checkedListAString(cblSoporteVitalBásicoA);
                    String sopVitBasB = checkedListAString(cblSoporteVitalBásicoB);
                    String sopVitBasC = checkedListAString(cblSoporteVitalBásicoC);
                    int dea1;
                    int.TryParse(tbRitmoCardiacoDEA1.Text.Trim(),out dea1);
                    int dea2;
                    int.TryParse(tbRitmoCardiacoDEA2.Text.Trim(), out dea2);
                    int dea3;
                    int.TryParse(tbRitmoCardiacoDEA3.Text.Trim(), out dea3);

                    nuevoidFRAP = DataAccessFRAP.altaFRAP(prioridad,dtFechaSalidaServicio, tbCalleYNumero.Text.Trim(), tbColonia.Text.Trim(), tbMunicipioServicio.Text.Trim(),
                        lugarOcurrencia, tbOtroLugarOcurrencia.Text.Trim(), motivoAtencion, traumas, IdPaciente, sopVitAvA,
                        sopVitAvB, sopVitAvC, respiracion, ddlRespiracion.SelectedIndex, pulso, ddlPulso.SelectedIndex, presionNum, presionDen, glicemia, temperatura,
                        glasgowAO, glasgowRV, glasgowRM, rblAVDI.SelectedIndex, condicionMedica, historialLesiones, tbAlergias.Text.Trim(), tbMedicamentos.Text.Trim(),
                        tbPatologías.Text.Trim(), tbCirugías.Text.Trim(), sopVitBasA, sopVitBasB, sopVitBasC, ddlTipoRitmoDEA1.SelectedIndex, dea1, dea2,
                        ddlTipoRitmoDEA2.SelectedIndex, dea3, ddlTipoRitmoDEA3.SelectedIndex, tbImpresionDiagnostica.Text.Trim(), DataAccessFRAP.EstadoFRAP.Terminado,
                        DateTime.Now);

                    if (nuevoidFRAP > 0)
                    {
                        Response.Redirect("~//FRAP//UltimosRegistrosFRAP.aspx");
                    }
                    else
                    {
                        //NOTIFICAR ERROR
                    }
                }
            }

        }
    }

    //Compone un string de indices separados por comas correspondientes
    //a los CheckBox checados en un CheboxList
    private string checkedListAString(CheckBoxList cblMotivoDeAtencion)
    {
        ListItem li;
        String salida=String.Empty;
        for (int c=0; c<cblMotivoDeAtencion.Items.Count;c++)
        {
            li=cblMotivoDeAtencion.Items[c];
            if (li.Selected)
                salida += c+",";
        }

        return salida;
    }

    //Metodos para validar campos de llenado de Paciente
    private bool validarDatosPaciente(out int edad)
    {
        tbEdad.CssClass = String.Empty;
        ddlSexo.CssClass = String.Empty;
        bool valEdad = validarIntDeTextBox(ref tbEdad, out edad);

        lblErrorPaciente.Visible = !(valEdad);

        return valEdad;
    }

    //Metodos para validar campos de llenado de FRAP
    private bool validarDatosFRAP(out int prioridad, out int lugarOcurrencia, out DateTime dtFechaSalidaServicio, out int respiracion, out int pulso, out int presionNum,
        out int presionDen, out double glicemia, out int temperatura, out int glasgowAO, out int glasgowRV, out int glasgowRM)
    {
        //Se establecen todas las variables de validacion.
        bool valprioridad,valLugarOcurrencia, valFechaSalidaServicio, valRespiracion, valPulso, valPresionNum, valPresionDen,
            valGlicemia, valtemperatura, valglasgowAO, valglasgowRV, valglasgowRM;

        //Se valida cada uno de los datos acorde al control usado para ingresarlo.
        valprioridad = validarRadioButtonList(ref rblRANV,out prioridad);
        valLugarOcurrencia = validarRadioButtonList(ref rblLugarOcurrencia, out lugarOcurrencia);
        valFechaSalidaServicio = validarFechaDeTextBox(ref tbFechaHorasSalidaaServicio, out dtFechaSalidaServicio);
        valRespiracion = validarIntDeTextBox(ref tbRespiracion, out respiracion);
        valPulso = validarIntDeTextBox(ref tbPulso, out pulso);
        valPresionNum=validarIntDeTextBox(ref tbPresionNum, out presionNum);
        valPresionDen=validarIntDeTextBox(ref tbPresionDen, out presionDen);
        valGlicemia = validarDoubleDeTextBox(ref tbGlicemia, out glicemia);
        valtemperatura=validarIntDeTextBox(ref tbTemperatura, out temperatura);
        valglasgowAO=validarIntDeTextBox(ref tbAO, out glasgowAO);
        valglasgowRV=validarIntDeTextBox(ref tbRV, out glasgowRV);
        valglasgowRM=validarIntDeTextBox(ref tbRM, out glasgowRM);

        //Se valida si todos los datos son aceptables
        bool valido = valFechaSalidaServicio && valRespiracion && valPulso && valPresionNum && valPresionDen && valGlicemia
            && valtemperatura && valglasgowAO && valglasgowRV && valglasgowRM && valprioridad && valLugarOcurrencia;

        //Se indica el error al usuario en caso de haberlo.
        lblErrorFRAP.Visible = !valido;

        return valido;
    }

    private bool validarRadioButtonList(ref RadioButtonList rbl, out int valorEntero)
    {
        bool valido=true;
        rbl.CssClass="";
        valorEntero = rbl.SelectedIndex;
        if (valorEntero <0 )
        {
            rbl.CssClass = "validacionError";
            valido = false;
        }

        return valido;
    }

    //Valida y transforma String de TextBox a entero
    private bool validarIntDeTextBox(ref TextBox tb, out int valorEntero)
    {
        bool valido=true;
        tb.CssClass = String.Empty;
        valido = int.TryParse(tb.Text.Trim(), out valorEntero);

        if (!valido)
            tb.CssClass = "validacionError";

        return valido;
    }

    //Valida y transforma String de TextBox a numero flotante
    private bool validarDoubleDeTextBox(ref TextBox tb, out double valorEntero)
    {
        bool valido = true;
        tb.CssClass = String.Empty;
        valido = double.TryParse(tb.Text.Trim(), out valorEntero);

        if (!valido)
            tb.CssClass = "validacionError";

        return valido;
    }

    //Valida y transforma String de TextBox a fecha
    private bool validarFechaDeTextBox(ref TextBox tb, out DateTime valorEntero)
    {
        bool valido = true;
        tb.CssClass = String.Empty;
        valido = DateTime.TryParse(tb.Text.Trim(), out valorEntero);

        if (!valido)
            tb.CssClass = "validacionError";

        return valido;
    }
}