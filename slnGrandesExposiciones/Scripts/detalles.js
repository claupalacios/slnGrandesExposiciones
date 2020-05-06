var opcion_botonera = null;
var array_detalles = new Array();
//var json_detalles = null;
var JS_ERROR = null;

var $fila_seleccionada = null;

function OBJ_Detalle(nombre, valor) {
    this.Nombre = nombre;
    this.Valor = valor;

    this.IsMonto = function() {
        for (var i = 0; i < NombresMontos.length; i++) {
            if (this.Nombre == NombresMontos[i]) {
                return true;
            }
        }
        return false;
    }

    const NombresMontos = new Array(
        "PORC_CCF", "DEUDA_FIN_MES", "PREV_TOTAL", "OP_NO_ID", "OP_DERIVADAS", "DEUDA_ID_TR",
        "TR_COBRANZAS_ONDARA_FCI", "TR_COBRANZAS_ONDARA_SEC_PUB", "GALVAL_TRIM_FCI", "GALVAL_TRIM_SEC_PUB",
        "CONTROLANTE_GFG_GAF", "FORMULA_EXP_BRUTA_IND_NETO_PREV", "FORMULA_EXP_BRUTA_CONSOL_MEN_NETO_PREV",
        "FORMULA_EXP_BRUTA_CONSOL_TRIM_NETO_PREV", "FORMULA_EXP_BRUTA_CONSOL_CONT_NETO_PREV",
        "GTIAS_1_6_301_306", "FORMULA_PORC_1_6_301_306", "GTIAS_7_307", "FORMULA_PORC_7_307",
        "GTIAS_2_5_31_302_305_331", "FORMULA_PORC_2_5_31_302_305_331", "GTIAS_16_316",
        "FORMULA_PORC_16_316", "GTIAS_23_62_63_323", "FORMULA_PORC_23_62_63_323", "GTIAS_10_53_310",
        "FORMULA_PORC_10_53_310", "GTIAS_54", "FORMULA_PORC_54", "GTIAS_25_325", "FORMULA_PORC_25_325",
        "GTIAS_42", "FORMULA_PORC_42", "FORMULA_EXP_IND_NETA_CRC_PREV", "FORMULA_EXP_CONSOL_MEN_NETA_CRC_PREV",
        "FORMULA_EXP_CONSOL_TRIM_NETA_CRC_PREV", "FORMULA_EXP_CONSOL_CONT_NETA_CRC_PREV", "OTRAS_GTIAS_PREF",
        "FORMULA_EXP_NETA_CRC_OTRAS_PREF_PREV_IND", "FORMULA_EXP_NETA_CRC_OTRAS_PREF_PREV_CONSOL_MEN",
        "FORMULA_EXP_NETA_CRC_OTRAS_PREF_PREV_CONSOL_TRIM", "FORMULA_EXP_NETA_CRC_OTRAS_PREF_PREV_CONSOL_CONT"
    );
}

function MostrarOpcionesTablaDetalles(seleccion, obj_mouse) {
    $("#tabdetopc").remove();

    GenerarObjDetalles(seleccion.sectionRowIndex);

    if ((seleccion != undefined) && (seleccion != null)) {
        var contenedor = document.createElement("div");
        var boton_insertar = document.createElement("button");
        var boton_editar = document.createElement("button");
        var boton_eliminar = document.createElement("button");
        var boton_cierre = document.createElement("button");

        $(contenedor)
            .attr("id", "tabdetopc")
            .addClass("alert")
            .addClass("alert-success")
            .addClass("alert-dismissable")
            .addClass("show")
            .css("position", "fixed")
            .css("left", String(obj_mouse.clientX - 4 + "px"))
            .css("top", String(obj_mouse.clientY - 4 + "px"))
            .css("padding", "4px 28px 4px 4px")
            .css("width", "auto")
            .css("z-index", "1")
            .mouseleave(function () {
                $(this).remove();
            });
        //.css("opacity", "0.8")

        $(boton_insertar)
            .attr("type", "button")
            .attr("href", "#")
            .attr("data-toggle", "modal")
            .attr("data-target", "#modal_nuevo_deudor")
            .css("opacity", "0.7")
            .click(function (data, fn) {
                opcion_botonera = 1;

                $("#tabdetopc").remove();
                $("#modal_nuevo_deudor > div > div > div > h4 > img")
                    .attr("src", "/Content/Images/copy.png")
                    .css("height", "32px")
                    .css("width", "auto;");
                $("#modal_nuevo_deudor > div > div > div > h4 > span").text("Insertar Deudor");
            })
            [0].innerHTML = "<img src='../Content/Images/copy.png' title='Insertar' style='height: 24px; width: auto; padding-top: 0px;' />";

        $(boton_editar)
            .attr("type", "button")
            .attr("href", "#")
            .attr("data-toggle", "modal")
            .attr("data-target", "#modal_nuevo_deudor")
            .css("opacity", "0.7")
            .click(function (data, fn) {
                opcion_botonera = 2;

                $("#tabdetopc").remove();
                $("#modal_nuevo_deudor > div > div > div > h4 > img")
                    .attr("src", "/Content/Images/edit.png")
                    .css("height", "32px")
                    .css("width", "auto;");
                $("#modal_nuevo_deudor > div > div > div > h4 > span").text("Modificar Deudor");
                CopiarObjDetalles();
            })
            [0].innerHTML = "<img src='../Content/Images/edit.png' title='Modificar' style='height: 24px; width: auto; padding-top: 0px;' />";

        $(boton_eliminar)
            .attr("type", "button")
            .attr("href", "#")
            .attr("data-toggle", "modal")
            .attr("data-target", "#modal_eliminar_deudor")
            .css("opacity", "0.7")
            .click(function (data, fn) {
                opcion_botonera = 3;

                $("#tabdetopc").remove();
            })
            [0].innerHTML = "<img src='../Content/Images/delete.png' title='Eliminar' style='height: 24px; width: auto; padding-top: 0px;' />";

        $(boton_cierre)
            .attr("type", "button")
            .attr("class", "close")
            .attr("data-dismiss", "alert")
            [0].innerHTML = "<span class='glyphicon glyphicon-remove'></span>";

        contenedor.appendChild(boton_insertar);
        contenedor.appendChild(boton_editar);
        contenedor.appendChild(boton_eliminar);
        contenedor.appendChild(boton_cierre);
        document.body.appendChild(contenedor);
        contenedor.focus();
    }
}

/*function GenerarJsonDetalles(indice_fila) {
    try {
        var fila = $("#table > tbody > tr")[indice_fila];
        var str_detalles = "";

        if (fila != undefined) {
            var campos = $(fila).children();

            //str_detalles += "[{";
            str_detalles += "{";

            for (var i = 0; i < (campos.length) ; i++) {
                if (campos[i].attributes.id.value != "") {
                    var detalle = new OBJ_Detalle(
                        campos[i].attributes.id.value,
                        campos[i].textContent.trim()
                    );

                    if (detalle.IsMonto()) {
                        while (detalle.Valor.indexOf(".") > -1) {
                            detalle.Valor = detalle.Valor.replace(".", "");
                        }
                    }
                    
                    array_detalles.push(detalle);

                    str_detalles += '"' + detalle.Nombre + '"' + ':' + '"' + detalle.Valor + '"' + ',';
                }
            }

            str_detalles = str_detalles.substring(0, str_detalles.length - 1);
            str_detalles += "}";
            //str_detalles += "}]";

            json_detalles = JSON.parse(str_detalles);
        }
    } catch (ex) {
        JS_ERROR = ex;
    }
}*/

function GenerarObjDetalles(indice_fila) {
    try {
        var fila = $("#table > tbody > tr")[indice_fila];
        
        if (fila != undefined) {
            var $campos = $(fila).children();

            for (var i = 0; i < ($campos.length) ; i++) {
                if ($campos[i].attributes.id.value != "") {
                    var detalle = new OBJ_Detalle(
                        $campos[i].attributes.id.value,
                        $campos[i].textContent.trim()
                    );

                    if (detalle.IsMonto()) {
                        while (detalle.Valor.indexOf(".") > -1) {
                            detalle.Valor = detalle.Valor.replace(".", "");
                        }
                    }

                    array_detalles.push(detalle);
                }
            }
        }
    } catch (ex) {
        JS_ERROR = ex;
    }
}

function CopiarObjDetalles() {
    try {
        if (array_detalles != null) {
            for (var i = 0; i < array_detalles.length; i++) {
                var campo = new String().concat("*[name='", array_detalles[i].Nombre, "']")

                if ($(campo).length > 0) {
                    $(campo).val(array_detalles[i].Valor);
                }
            }
        }
    } catch (ex) {
        JS_ERROR = ex;
    }
}

function PintarFila(fila) {
    if ($fila_seleccionada != null) {
        $fila_seleccionada
            .css("background-color", "transparent")
            .css("color", "#333333");
    }
    $(fila)
        .css("background-color", "lightblue")
        .css("color", "#666666");
}

function EnviarControlador() {
    var $opcion = $("#TIPO_OPERACION_CONTROLLER");

    if ($opcion.length > 0) {
        $opcion.val(opcion_botonera);
    }

    $("form").submit();
}

/*function EnviarJsonController() {
    $.ajax(
        {
            url: '/Exposiciones/Detalles?op=2',
            type: 'post',
            data: JSON.stringify(json_detalles),
            contentType: 'application/json; charset=utf-8;',
            dataType: 'json',
            success: function (response) {
                alert(response);
            
                return;
            },
            error: function (err) {
                if (err.statusText.trim() != "OK") {
                    JS_ERROR = err;
                }
            
                return;
            }
        }
    );
}*/

$(document).ready(function () {
    $("#table > tbody > tr")
        .click(function (n, i) {
            PintarFila(this);

            $fila_seleccionada = $(this);

            MostrarOpcionesTablaDetalles(this, n);
        });
    }
);