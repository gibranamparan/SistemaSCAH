function lesionSeleccionada() {
    //Se extrae la lista de lesiones que se encuentra en pantalla
    var lesionesRadioButton = document.getElementById("rblSeleccionLesiones");
    var radios = lesionesRadioButton.getElementsByTagName("input");
    var lesion;

    //Se identifica cual de las lesiones ha sido seleccionada por el usuario
    for(var c = 0; c <= radios.length-1; c++) {
        if (radios[c].checked)
            lesion = radios[c].value;
    }
    var auxLesionSeleccionada = document.getElementById("auxLesionSeleccionada");
    auxLesionSeleccionada.textContent = lesion;
    return lesion;
}

//Se despliega un cursor en caso de estar seleccionada alguna lesion o el borrador.
function cursorImagenLesiones() {
    //Detectar lesion seleccionada
    var lesion = lesionSeleccionada();

    //Si ha seleccionado alguna lesion
    if (lesion != null) {
        //Si ha seleccionado la opcion borrar
        if (lesion == 0)
            canvas.style.cursor = "url('../Recursos/Imagenes/FRAP/borrador.png'),auto";//Se muestra un borrador como cursor
        else
            canvas.style.cursor = "url('../Recursos/Imagenes/FRAP/redmouse.png'),auto";//Si no, se muestra el cursor de lesiones
    }
    else {
        canvas.style.cursor = "none";//Si no ha seleccionado lesiones, desaparece el cursor.
    }
}

//Funcion para borrar canvas cuando esta seleccionado el borrador en el canvas de lesiones.
function borrarSiBorradorSeleccionado(mousePos, canvas) {
    var ctx = canvas.getContext("2d");
    var x = mousePos.x - 5;
    var y = mousePos.y-5;
    ctx.clearRect(x, y, 10,10);
}

//En caso de que el lugar de ocurrencia haya sido marcado como "Otro",
//se despliega un campo para detallar la información.
function otroLugarOcurrencia() {
    var radios = rblLugarOcurrencia.getElementsByTagName("input");
    var valSeleccionado;

    //Se identifica el lugar de accidente que ha sido seleccionado por el usuario
    for (var c = 0; c <= radios.length - 1; c++) {
        if (radios[c].checked)
            valSeleccionado = radios[c].value;
    }

    var renglonOtroLugarOcurrencia = document.getElementById("renglonOtroLugarOcurrencia");
    var jqrenglonOtroLugarOcurrencia = $("#renglonOtroLugarOcurrencia");
    //Si el lugar seleccionado ha sido otro
    if (valSeleccionado == 3)
        jqrenglonOtroLugarOcurrencia.fadeIn("slow");
    else
        jqrenglonOtroLugarOcurrencia.fadeOut("slow");
}

//Pinta una lesion sobre el cambas o borra segun se haya seleccionado alguna de las opciones de lesiones.
function setLesionEnCanvas(evt) {
    var canvas = document.getElementById('canvasLesiones');
    var historialAccionesCanvas = document.getElementById('historialAccionesCanvas');
    var mousePos = getMousePos(canvas, evt);
    var numero = lesionSeleccionada();
    if (numero != 0) 
        txtInCanvas(mousePos, canvas, numero);
    else 
        borrarSiBorradorSeleccionado(mousePos, canvas);
    
    //Se almacen en un campo escondido la cadena de caracteres que representa todas las acciones del usuario hechas en 
    //el canvas de seleccion de lesiones
    historialAccionesCanvas.value += numero + "," + mousePos.x + "," + mousePos.y + ";";
}

//Estilo y impresion de señalización de lesión sobre canvas
function txtInCanvas(mousePos, canvas, numero) {
    var ctx = canvas.getContext('2d');
    ctx.fillStyle = "red";
    ctx.font = "bold smaller Arial";

    var x = mousePos.x - 5;
    var y = mousePos.y+5;
    ctx.fillText(numero.toString(), x, y);
}

//Funcion para determinar la posicion del mouse sobre el canvas.
function getMousePos(canvas, evt) {
    var scroll = document.body.scrollTop;
    if (typeof (canvas.offsetParent) != 'undefined') {
        for (var posX = 0, posY = 0; canvas; canvas = canvas.offsetParent) {
            posX += canvas.offsetLeft;
            posY += canvas.offsetTop;
        }
        return { x: evt.clientX - posX, y: evt.clientY - (posY - scroll) };
    } else {
        return { x: evt.clientX - canvas.offsetLeft, y: evt.clientY - (canvas.offsetTop - scroll) };
    }
}

//Toma un String que representa un historial de entrada de lesiones en un FRAP y lo representa sobre
//con canvas contenedor de lesiones
function cargarLesiones() {
    var stringLesiones = document.getElementById("historialAccionesCanvas").value;
    var canvas = document.getElementById('canvasLesiones');

    //Se determinan el conjunto operaciones hechas sobre el canvas que se quiere cargar
    var arrayLesiones = stringLesiones.split(";");
    var instruccion;
    var instruccionComponentes;

    //Por cada una de las operaciones
    for (var c = 0; c < arrayLesiones.length - 1; c++) {
        //Se determina el numero de lesion señalado y su coordenada, los cuales vienen separados por comas
        instruccion = arrayLesiones[c];
        instruccionComponentes = instruccion.split(",");
        numero = instruccionComponentes[0];

        //Las coordenadas en Sintr se convierten a un conjunto de dos numeros enteros
        mousePos = toMousePos(instruccionComponentes[1], instruccionComponentes[2]);
        if (numero != 0)
            txtInCanvas(mousePos, canvas, numero);
        else
            borrarSiBorradorSeleccionado(mousePos, canvas);
    }
}

//Utilidad para convertir dos pares de string a un conjunto de coordenadas del mouse.
function toMousePos(strX, strY) {
    return { x: parseInt(strX), y: parseInt(strY) };
}

function numToMes(num) {
    switch (num) {
        case 1:
            return "Enero";
            break;
        case 2:
            return "Febrero";
            break;
        case 3:
            return "Marzo";
            break;
        case 4:
            return "Abril";
            break;
        case 5:
            return "Mayo";
            break;
        case 6:
            return "Junio";
            break;
        case 7:
            return "Julio";
            break;
        case 8:
            return "Agosto";
            break;
        case 9:
            return "Septiembre";
            break;
        case 10:
            return "Octubre";
            break;
        case 11:
            return "Noviembre";
            break;
        case 12:
            return "Diciembre";
            break;
    }
}

//Se valida que los indicadores de Glasgow no contengan numeros superiores a los 5 puntos
function validarGlasgow() {
    var v = parseInt(this.value);
    this.value = v > 5 ? 5 : v;

    var tbGlasgowSobre = document.getElementById("tbGlasgowSobre");
    var tbAO = parseInt(document.getElementById("tbAO").value);
    var tbRV = parseInt(document.getElementById("tbRV").value);
    var tbRM = parseInt(document.getElementById("tbRM").value);
    tbGlasgowSobre.value = (isNaN(tbAO) ? 0 : tbAO) + (isNaN(tbRV) ? 0 : tbRV) + (isNaN(tbRM) ? 0 : tbRM);
}
