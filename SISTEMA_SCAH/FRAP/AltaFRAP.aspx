<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AltaFRAP.aspx.cs" Inherits="_Default" %>

<asp:Content ContentPlaceHolderID="Contenido" ID="Contenido" runat="server">
    <style>        
        select[disabled="disabled"],input[disabled="disabled"] {
          color:black;
        }   

        input[checked="checked"]+label {
          border:1px solid black;
          font-weight:bold;
        }
    </style>
    <!-- SE CARGA CSS DE LA HOJA DE FRAP -->
    <link href="../Estilos/FRAPEstilo.css" runat="server" type="text/css" rel="stylesheet" />

    <!-- SE CARGA CSS JQUERY -->
    <asp:ScriptManager runat="server" ID="smScriptManager"></asp:ScriptManager>
    <link href="../jquery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../jquery/jquery-ui-timepicker-addon.css" rel="stylesheet" type="text/css" />


    <!--SE CARGA CODIGO JAVASCRIPT-->
    <script src="../jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery/jquery-ui.js" type="text/javascript"></script>
    <script src="../jquery/jquery.maskedinput.js" type="text/javascript"></script>
    <script src="../JavaScripts/JavaScript.js" type="text/javascript"></script>
    <script src="../jquery/jquery-ui-timepicker-addon.js" type="text/javascript"></script>

    <!-- INCIA TABLA PRINCIPAL DE FRAP -->
    <table  class="tableTituloFRAP">
        <!-- INDICACION DE PRIORIDAD POR COLORES -->
        <tr>
            <td style="width:20%"><span class="nombreCampos">Prioridad</span><br />
            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" CellSpacing="0" CellPadding="0" ClientIDMode="Static" ID="rblRANV">
                <asp:ListItem Text="R" Value="R" id="R"></asp:ListItem>
                <asp:ListItem Text="A" Value="A" id="A"></asp:ListItem>
                <asp:ListItem Text="N" Value="N" id="N"></asp:ListItem>
                <asp:ListItem Text="V" Value="V" id="V"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="tituloFRAP">
            <br />
            Formato de Registro de Atención Prehospitalaria</tr>
    </table>
    <br />

    <table class="cuerpoFomulario">
        <!-- CONTROLES PARA FECHA Y HORA DE ALTA DEL FRAP -->
        <tr>
            <td>
                <asp:Label ID="lblFechasHoraSalidaServicio" Text="Fecha y Hora: " runat="server" CssClass="nombreCampos"/>
                <asp:TextBox ID="tbFechaHorasSalidaaServicio" ClientIDMode="Static" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <!-- CONTROLES PARA INTRODUCCION DE UBICACION DEL SERVICIO-->
            <td style="width:50%">
                <span class="nombreCampos">Ubicacion del servicio</span><br />
                <table class="tablaUbicacionServicio">
                    <tr>
                        <td>Calle(s) y Número:</td><td><asp:TextBox runat="server" ID="tbCalleYNumero"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Colonia:</td><td><asp:TextBox runat="server" ID="tbColoniaServicio"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td>Municipio:</td><td><asp:TextBox runat="server" ID="tbMunicipioServicio"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
            <td rowspan="3">
                <table class="tablaInterna" id="Trauma">
                    <!-- CONTROLES PARA INTRODUCCION TRAUMA-->
                    <tr class="subtitulosFRAP"><th>Trauma</th></tr>
                    <tr><td>
                        <asp:CheckBoxList ID="cblTraumas" DataTextField="nombreTrauma" RepeatColumns="2" runat="server">
                        </asp:CheckBoxList></td></tr>
                </table>
            </td>
        </tr>
        <tr>
            <td >
                <!-- CONTINUACION DE CONTROLES PARA INTRODUCCION DE UBICACION DEL SERVICIO-->
                <span class="nombreCampos">Lugar de Ocurrencia</span><br />
                <asp:RadioButtonList ID="rblLugarOcurrencia" ClientIDMode="Static" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Text="Casa" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Vía Pública" Value="1" ></asp:ListItem>
                    <asp:ListItem Text="Trabajo" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Otro" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr style="display:none" id="renglonOtroLugarOcurrencia">
            <td>
                Otro lugar de ocurrencia: <br />
                <asp:TextBox ID="tbOtroLugarOcurrencia" runat="server" MaxLength="200" TextMode="MultiLine" Height="30px" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <!-- CONTROLES PARA INTRODUCCION DE MOTIVO DE ATENCION -->
                <span class="nombreCampos">Motivo de Atencion</span>
                <asp:CheckBoxList ID="cblMotivoDeAtencion" runat="server">
                    <asp:ListItem Text="Traumático" Value="Traumático"></asp:ListItem>
                    <asp:ListItem Text="Enfermedad" Value="Enfermedad"></asp:ListItem>
                    <asp:ListItem Text="Ginecobstétrico" Value="Ginecobstétrico"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr><td colspan="2"><div class="tableTituloFRAP"><br /></div></td></tr>
        <tr>
            <td>
                <!-- CONTROLES PARA INTRODUCCION DE DATOS PERSONALES DEL PACIENTE -->
                <table id="tablaDatosPersonales">
                    <tr><td>Nombre o Media Afiliación:</td><td><asp:TextBox ID="tbNombreOAfiliacion" MaxLength="80"  Width="100%" runat="server"></asp:TextBox></td></tr>
                    <tr><td style="vertical-align:top">
                        Sexo:<asp:DropDownList ID="ddlSexo" runat="server">
                                <asp:ListItem Text="Masculino" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Femenino"  Value="2"></asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Edad: <asp:TextBox ID="tbEdad" ClientIDMode="Static" Width="50px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr><td>Domicilio:</td><td><asp:TextBox ID="tbDomicilio" TextMode="MultiLine" MaxLength="150" Height="50px" Width="100%" runat="server"></asp:TextBox></td></tr>
                    <tr><td>Colonia:</td><td><asp:TextBox ID="tbColonia" Width="100%" MaxLength="30"  runat="server"></asp:TextBox></td></tr>
                    <tr><td>Municipio:</td><td><asp:TextBox ID="tbMunicipio" MaxLength="30" Width="100%" runat="server"></asp:TextBox></td></tr>
                    <tr><td>Derechohabiencia:</td><td><asp:TextBox ID="tbDerechohabiencia" MaxLength="50" Width="100%" runat="server"></asp:TextBox></td></tr>
                    <tr><td>Teléfono:</td><td><asp:TextBox ID="tbTelefono" ClientIDMode="Static" MaxLength="15" Width="100%" runat="server"></asp:TextBox></td></tr>
                    <tr><td>Ocupación:</td><td><asp:TextBox ID="tbOcupación" Width="100%" MaxLength="30" runat="server"></asp:TextBox></td></tr>
                    <tr><td>Otro:</td><td><asp:TextBox Height="50px" ID="tbOtro" MaxLength="150" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox></td></tr>
                </table>
            </td>
            <td rowspan="4">
                <table class="tablaInterna" id="SoporteVital">
                    <!-- CONTROLES PARA INTRODUCCION DE SOPORTE VITAL -->
                    <tr><th colspan="2" class="subtitulosFRAP">Soporte Vital Avanzado</th></tr>
                    <tr class="subindicesTablasInternasSoporteVitalAvanzado">
                        <td>
                            <div>A</div>
                            <asp:CheckBoxList ID="cblSoporteVitalAvanzadoA" runat="server">
                                <asp:ListItem Text="Int. Orotraqueal"></asp:ListItem>
                                <asp:ListItem Text="Masc. Laringea"></asp:ListItem>
                                <asp:ListItem Text="Combitubo"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td rowspan="3">
                            <div>C</div>
                            <asp:CheckBoxList ID="cblSoporteVitalAvanzadoC" runat="server">
                                <asp:ListItem Text="Acc. Ven. Perif"></asp:ListItem>
                                <asp:ListItem Text="I.E.V."></asp:ListItem>
                                <asp:ListItem Text="Perfusion Osea"></asp:ListItem>
                                <asp:ListItem Text="Hartmman"></asp:ListItem>
                                <asp:ListItem Text="SSN 0.9%"></asp:ListItem>
                                <asp:ListItem Text="Dextrosa"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr class="subindicesTablasInternasSoporteVitalAvanzado">
                        <td rowspan="2">
                            <div>B</div>
                            <asp:CheckBoxList ID="cblSoporteVitalAvanzadoB" runat="server">
                            <asp:ListItem Text="B.V.M."></asp:ListItem>
                            <asp:ListItem Text="Descompresion"></asp:ListItem>
                            <asp:ListItem Text="Torácica"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                <br />
                
                <!-- CONTENEDOR DE LESIONES -->
                <table class="contenedorImagenLesiones">
                    <tr class="subtitulosFRAP"><th>Lesiones</th></tr>
                    <tr>
                        <td>
                            <canvas width="290" height="338" id="canvasLesiones">

                            </canvas>
                        </td>
                    </tr>
                </table>

                <!-- SELECCION DE LESIONES -->
                <asp:RadioButtonList CssClass="cssRblSeleccionLesiones" ID="rblSeleccionLesiones" ClientIDMode="Static" RepeatColumns="2" runat="server">
                    <asp:ListItem Text="1. Abrasión" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2. Amputación" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3. Aplastamiento" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4. Avulsión" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5. Contusión" Value="5"></asp:ListItem>
                    <asp:ListItem Text="6. Dolor" Value="6"></asp:ListItem>
                    <asp:ListItem Text="7. Esguince" Value="7"></asp:ListItem>
                    <asp:ListItem Text="8. Fx. Abierta" Value="8"></asp:ListItem>
                    <asp:ListItem Text="9. Fx. Cerrada" Value="9"></asp:ListItem>
                    <asp:ListItem Text="10. Hx. A. Fuego" Value="10"></asp:ListItem>
                    <asp:ListItem Text="11. Hx. A. Blaca" Value="11"></asp:ListItem>
                    <asp:ListItem Text="12. Hemorragia" Value="12"></asp:ListItem>
                    <asp:ListItem Text="13. Laceración" Value="13"></asp:ListItem>
                    <asp:ListItem Text="14. Luxación" Value="14"></asp:ListItem>
                    <asp:ListItem Text="15. Mord. o Picadura" Value="15"></asp:ListItem>
                    <asp:ListItem Text="16. Punción" Value="16"></asp:ListItem>
                    <asp:ListItem Text="17. Quemadura" Value="17"></asp:ListItem>
                    <asp:ListItem Text="18. Trauma cerrado" Value="18"></asp:ListItem>
                    <asp:ListItem Text="19. Trauma penetrante" Value="19"></asp:ListItem>
                    <asp:ListItem Text="20. Herida" Value="20"></asp:ListItem>
                    <asp:ListItem Text="21. Hematoma" Value="21"></asp:ListItem>
                    <asp:ListItem Text="Borrar" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
                <!--Guardado auxiliar de lesion seleccionada -->
                <span style="display:none" id="auxLesionSeleccionada">0</span>

                <!-- Historial de operaciones sobre canvas de lesiones-->
                <!--<span style="display:none" id="historialAccionesCanvas"></span>-->
                
                <asp:HiddenField ID="historialAccionesCanvas" ClientIDMode="Static" runat="server"></asp:HiddenField>

                <br />
                <br />
                <!-- CONTROL PARA INTRODUCCION IMPRESION DIAGNOSTICA -->
                <table class="tablaAntecedentes">
                    <tr class="subtitulosFRAP"><th>Impresión Diagnóstica</th></tr>
                    <tr><td style="text-align:center"><asp:TextBox ID="tbImpresionDiagnostica" runat="server" TextMode="MultiLine" Width="95%" Height="100px" ></asp:TextBox></td></tr>
                </table>
                <table style="width:100%">
                    <!--BOTONES PARA ENVIAR EL FRAP O CANCELARLO-->
                    <tr>
                        <td colspan="4" style="text-align:right">
                            <asp:Button runat="server" ID="btCancelarFRAP" Width="80" Height="40" Text="Cancelar" OnClick="btCancelarFRAP_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btAceptarFRAP" Width="80" Height="40" Text="Enviar" OnClick="btAceptarFRAP_Click" />
                        </td>
                    </tr>
                    <tr>
                        <asp:Label ID="lblErrorPaciente" Visible="false" ForeColor="Red" runat="server" Text="Error en la introducción de información del paciente, verifique los campos marcados en rojo."></asp:Label>
                        <asp:Label ID="lblErrorFRAP" Visible="false" ForeColor="Red" runat="server" Text="Error en la introducción de información del FRAP, verifique los campos marcados en rojo."></asp:Label>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="tablaInterna">
                    <tr style="vertical-align:middle;">
                        <!-- ENCABEZADOS DE LA TABLA DE ESTADOS VITALES -->
                        <th style="width:auto">Signos Vitales</th><th>Glasgow<br /></th><th  style="width:70px">Estado del Paciente</th>
                    </tr>
                    <tr>
                        <td style="width:auto">
                            <!--TABLA INTRODUCCION DE ESTADOS VITALES -->
                            <table>
                                <!-- CONTROLES DE RESPIRACION-->
                                <tr>
                                    <td>Respiracion:</td>
                                    <td>
                                        <asp:TextBox ID="tbRespiracion" ClientIDMode="Static" Width="20" runat="server"></asp:TextBox>
                                        <asp:DropDownList ID="ddlRespiracion" runat="server">
                                            <asp:ListItem Text="Regular"></asp:ListItem>
                                            <asp:ListItem Text="Superficial"></asp:ListItem>
                                            <asp:ListItem Text="Dificultosa"></asp:ListItem>
                                        </asp:DropDownList><br />
                                    </td>
                                </tr>

                                 <!-- CONTROLES DE PULSO-->
                                <tr>
                                    <td>Pulso: </td>
                                    <td>
                                         <asp:TextBox ID="tbPulso" ClientIDMode="Static" Width="20" runat="server"></asp:TextBox>
                                        <asp:DropDownList ID="ddlPulso" runat="server">
                                            <asp:ListItem Text="Rítmico"></asp:ListItem>
                                            <asp:ListItem Text="Arrítmico"></asp:ListItem>
                                            <asp:ListItem Text="Fuerte"></asp:ListItem>
                                            <asp:ListItem Text="Débil"></asp:ListItem>
                                            <asp:ListItem Text="Ausente"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <!-- CONTROLES DE PRESION-->
                                <tr>
                                    <td>Presion:</td>
                                    <td>
                                        <asp:TextBox ID="tbPresionNum" ClientIDMode="Static" Width="20" runat="server"></asp:TextBox>
                                        <span style="font-size:x-large;font-weight:bolder;">/</span>
                                        <asp:TextBox ID="tbPresionDen" ClientIDMode="Static" Width="20" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <!-- CONTROLES DE GLICEMIA-->
                                <tr>
                                    <td>Glicemia: </td>
                                    <td><asp:TextBox ID="tbGlicemia" ClientIDMode="Static" Width="35" runat="server"></asp:TextBox></td>
                                </tr>

                                <!-- CONTROLES DE TEMPERATURA-->
                                <tr>
                                    <td>Temperatura: </td>
                                    <td><asp:TextBox ID="tbTemperatura" ClientIDMode="Static" Width="20" runat="server"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <!-- CONTROLES PARA GLASGOW-->
                            <table>
                                <tr><td>
                                    AO: <asp:TextBox ID="tbAO" ClientIDMode="Static" Width="20" runat="server"></asp:TextBox></td></tr>
                                <tr><td>
                                    RV: <asp:TextBox ID="tbRV" ClientIDMode="Static" Width="20" runat="server"></asp:TextBox></td></tr>
                                <tr><td>
                                    RM: <asp:TextBox ID="tbRM" ClientIDMode="Static" Width="20" runat="server"></asp:TextBox></td></tr>
                                <tr><td style="vertical-align:middle"><asp:TextBox ClientIDMode="Static" ID="tbGlasgowSobre" ReadOnly="true" Width="20" runat="server"></asp:TextBox>
                                    <span style="font-size:x-large;">/15</span>
                                    </td></tr>
                            </table>

                        </td>
                        <td style="text-align:center">
                            <!-- CONTROLES PARA ESTADO DEL PACIENTE -->
                            <asp:RadioButtonList ID="rblAVDI" runat="server">
                                <asp:ListItem Text="A"></asp:ListItem>
                                <asp:ListItem Text="V"></asp:ListItem>
                                <asp:ListItem Text="D"></asp:ListItem>
                                <asp:ListItem Text="I"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <br />
                <table class="tablaInterna">
                    <!-- TABLA DE INDICACIONES DE CONCIóN MéDICA-->
                    <tr class="subtitulosFRAP"><th>Médica</th></tr>
                    <tr><td>
                        <asp:CheckBoxList runat="server" ID="cblMedica" RepeatColumns="2">
                            <asp:ListItem Text="Paro Cardiaco"></asp:ListItem>
                            <asp:ListItem Text="Neurología"></asp:ListItem>
                            <asp:ListItem Text="Organos de los sentidos"></asp:ListItem>
                            <asp:ListItem Text="Cardiovascular"></asp:ListItem>
                            <asp:ListItem Text="Gastrointestinal"></asp:ListItem>
                            <asp:ListItem Text="Genitourinario"></asp:ListItem>
                            <asp:ListItem Text="Gineco-Obstétrico"></asp:ListItem>
                            <asp:ListItem Text="Shock"></asp:ListItem>
                            <asp:ListItem Text="Metabólico"></asp:ListItem>
                            <asp:ListItem Text="Intoxicación"></asp:ListItem>
                            <asp:ListItem Text="Psiquiátrica"></asp:ListItem>
                            <asp:ListItem Text="Ovace"></asp:ListItem>
                            <asp:ListItem Text="Térmica"></asp:ListItem>
                            <asp:ListItem Text="Ent. Común"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td></tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <!-- CONTROLES PARA INTRODUCCION DE ANTECEDENTES DEL PACIENTE -->
                <table class="tablaAntecedentes">
                    <tr>
                    <th colspan="2">Antecedentes</th></tr>
                    <tr><td>Alergias:</td><td><asp:TextBox ID="tbAlergias" Width="100%" runat="server"></asp:TextBox></td></tr>
                    <tr><td>Medicamentos:</td><td><asp:TextBox ID="tbMedicamentos" Width="100%" runat="server"></asp:TextBox></td></tr>
                    <tr><td>Patologías:</td><td><asp:TextBox ID="tbPatologías" Width="100%" runat="server"></asp:TextBox></td></tr>
                    <tr><td>Cirugías:</td><td><asp:TextBox ID="tbCirugías" Width="100%" runat="server"></asp:TextBox></td></tr>
                </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="tablaInterna">
                        <!-- CONTROLES PARA INTRODUCCION DE SOPORTE VITAL DEL PACIENTE -->
                        <tr class="subtitulosFRAP"><th colspan="3">Soporte Vital Básico</th></tr>
                        <tr>
                            <td rowspan="2" style="width:33.33%">
                                <div>A</div>
                                <asp:CheckBoxList ID="cblSoporteVitalBásicoA" runat="server">
                                    <asp:ListItem Text="Evaluación"></asp:ListItem>
                                    <asp:ListItem Text="Despeje V.A."></asp:ListItem>
                                    <asp:ListItem Text="Can. Orofaríngea"></asp:ListItem>
                                    <asp:ListItem Text="Can. Nasofaríngea"></asp:ListItem>
                                    <asp:ListItem Text="Asp. Secreciones"></asp:ListItem>
                                    <asp:ListItem Text="Collar Cervical"></asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td rowspan="2" style="width:33.33%">
                                <div>B</div>
                                <asp:CheckBoxList Font-Size="Small" ID="cblSoporteVitalBásicoB" runat="server">
                                    <asp:ListItem Text="Evaluación"></asp:ListItem>
                                    <asp:ListItem Text="B.V.M."></asp:ListItem>
                                    <asp:ListItem Text="Cánula Nasal"></asp:ListItem>
                                    <asp:ListItem Text="Ventury"></asp:ListItem>
                                    <asp:ListItem Text="Masc. Simple"></asp:ListItem>
                                    <asp:ListItem Text="Masc. No reinhalación"></asp:ListItem>
                                    <asp:ListItem Text="O2 Lts."></asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td>
                                <div>C</div>
                                <asp:CheckBoxList ID="cblSoporteVitalBásicoC" runat="server">
                                    <asp:ListItem Text="Evaluación"></asp:ListItem>
                                    <asp:ListItem Text="Compresión"></asp:ListItem>
                                    <asp:ListItem Text="Hemostacia"></asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <!-- CONTROLES PARA INTRODUCCION DE D.E.A. -->
                                    <tr><th class="nombreCampos" colspan="3">D.E.A.</th></tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="tbRitmoCardiacoDEA1" ClientIDMode="Static" runat="server" Width="100%"></asp:TextBox>
                                            <asp:DropDownList runat="server" Width="100%" ID="ddlTipoRitmoDEA1">
                                                <asp:ListItem Text="FV"></asp:ListItem>
                                                <asp:ListItem Text="AESP"></asp:ListItem>
                                                <asp:ListItem Text="FA"></asp:ListItem>
                                                <asp:ListItem Text="AS"></asp:ListItem>
                                                <asp:ListItem Text="AECP"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbRitmoCardiacoDEA2" ClientIDMode="Static" runat="server" Width="100%"></asp:TextBox>
                                            <asp:DropDownList runat="server" Width="100%" ID="ddlTipoRitmoDEA2">
                                                <asp:ListItem Text="FV"></asp:ListItem>
                                                <asp:ListItem Text="AESP"></asp:ListItem>
                                                <asp:ListItem Text="FA"></asp:ListItem>
                                                <asp:ListItem Text="AS"></asp:ListItem>
                                                <asp:ListItem Text="AECP"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbRitmoCardiacoDEA3" ClientIDMode="Static" runat="server" Width="100%"></asp:TextBox>
                                            <asp:DropDownList runat="server" Width="100%" ID="ddlTipoRitmoDEA3">
                                                <asp:ListItem Text="FV"></asp:ListItem>
                                                <asp:ListItem Text="AESP"></asp:ListItem>
                                                <asp:ListItem Text="FA"></asp:ListItem>
                                                <asp:ListItem Text="AS"></asp:ListItem>
                                                <asp:ListItem Text="AECP"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
       <script type="text/javascript">
           $(document).ready(
               function(){

                   //Marcar Prioridad si el formulario se encuentra en solo lectura
                   var rblRANV = $("#rblRANV");
                   if(rblRANV.find("input").is("[disabled]"))
                   {
                       rblRANV.find("input, label").css("border", "none");
                       rblRANV.find("input, label").css("display", "none");
                       rblRANV.find("input, label").parent().css("display", "none");
                       rblRANV.find("input[checked='checked']+label").css("display", "inline").parent().css("display", "inline");
                       //rblRANV.find("input[checked='checked'], input[checked='checked']+label").addClass("display", "inline");
                   }

                   //Asignacion de scripts y estilos para calendario
                   var tbFechaHorasSalidaaServicio = $("#tbFechaHorasSalidaaServicio");
                   if (!tbFechaHorasSalidaaServicio.is("[readonly]")) {
                       tbFechaHorasSalidaaServicio.datetimepicker();
                       tbFechaHorasSalidaaServicio.mask("99/99/9999 99:99");
                   }

                   //Evento de cambio para radiobuttonlist de Ocurrencia de percance
                   var rblLugarOcurrencia = document.getElementById("rblLugarOcurrencia");
                   rblLugarOcurrencia.onchange = otroLugarOcurrencia;
                   otroLugarOcurrencia();

                   //Evento de mouse sobre canvas y click para canvas de Lesiones.
                   var rblSeleccionLesiones = $("#rblSeleccionLesiones");
           
                   if (!rblSeleccionLesiones.find("input").is("[disabled]")) {
                       var canvas = document.getElementById('canvasLesiones');
                       canvas.onmousedown = setLesionEnCanvas; //Cuando el mouse hace click
                       canvas.onmouseover = cursorImagenLesiones; //Cuando el mouse pasa por encima
                   }
                   else {
                       cargarLesiones();
                       rblSeleccionLesiones.find("input").css("visibility", "hidden");
                       rblSeleccionLesiones.find('input[value="0"] + label').css("visibility", "hidden");
                   }

                   //SE CARGA LA FECHA Y HORA AL MOMENTO DE ENTRAR A LA PAGINA
                   var lblFecha = document.getElementById("lblFecha");
                   var now = new Date();

                   //SE ESTABLECEN MASCARAS EN LOS TEXTBOX QUE LO REQUIERAN
                   //Los signos de interrogacion en las mascaras indican los caracteres de izquierda a derecha
                   //que serán obligatorios.
                   $("#tbTelefono").mask("(999)-99-99999");
                   $("#tbEdad").mask("9?99");
                   $("#tbRespiracion").mask("9?9");
                   $("#tbPulso").mask("9?9");
                   $("#tbPresionNum").mask("9?9");
                   $("#tbPresionDen").mask("9?9");
                   $("#tbGlicemia").mask("99?.99");
                   $("#tbTemperatura").mask("9?9");


                   $("#tbAO").mask("9");
                   document.getElementById("tbAO").onchange = validarGlasgow;
                   $("#tbRV").mask("9");
                   document.getElementById("tbRV").onchange = validarGlasgow;
                   $("#tbRM").mask("9");
                   document.getElementById("tbRM").onchange = validarGlasgow;

                   $("#tbRitmoCardiacoDEA1").mask("9?99");
                   $("#tbRitmoCardiacoDEA2").mask("9?99");
                   $("#tbRitmoCardiacoDEA3").mask("9?99");          
               })
           
    </script>
</asp:Content>

