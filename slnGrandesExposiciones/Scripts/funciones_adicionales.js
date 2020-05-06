function MostrarAlerta(titulo, mensaje, elemento_focus) {
    $("#alerta").remove();

    if (elemento_focus != undefined) {
        if (typeof (mensaje) == "string") {
            if (mensaje != "") {
                var div_alerta = document.createElement("div");
                var boton_cierre = document.createElement("button");
                var contenedor = null;

                contenedor = elemento_focus.parentNode;

                $(div_alerta)
                    .attr("id", "alerta")
                    .css("position", "absolute")
                    .addClass("alert")
                    .addClass("alert-danger")
                    .addClass("alert-dismissable")
                    .addClass("show")
                    .css("width", "auto")
                    .css("z-index", "1")
                    //.css("opacity", "0.8")
                    [0].innerHTML = "<strong>" + titulo + "</strong>" + mensaje;

                $(boton_cierre)
                    .attr("type", "button")
                    .attr("class", "close")
                    .attr("data-dismiss", "alert")
                    [0].innerHTML = "<span class='glyphicon glyphicon-remove'></span>";

                div_alerta.appendChild(boton_cierre);
                contenedor.appendChild(div_alerta);

                elemento_focus.focus();
            }
        }
    }
}

function JsonController(url_get, method) {
    //url_get = "/Controller/method"
    //method = "GET" / "POST"

    $.ajax(
        {
            url: url_get,
            type: method,
            datatype: 'json',
            async: false
        }
    ).done(
        function (result) {
            return result;
        }
    );
}

