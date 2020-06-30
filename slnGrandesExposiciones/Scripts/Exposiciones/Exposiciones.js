//$(document).on("click", ".delete", function () {
//    var elementId = $(this).data('id');
//    $(".modal-body #elementId").val(elementId);
//});

$(document).on("click", "#validar", function () {
    var idExposicion = $(this).attr("data-id")
    $(".modal-body #exposicion").val(idExposicion);

});


$(document).on("click", "#aceptarValidacionBoton", function () {
    var elementId = $(".modal-body #exposicion").val();

    $.ajax({
        url: "/Exposiciones/Validar/" + elementId,
        success: function (data) {
            $('#myAlertSuccess').show('fade');
            setTimeout(function () {
                location.reload();
            }, 3000);

        },
        error: function () {
            $("#buttonError").addClass('show');
            setTimeout(function () {
                location.reload();
            }, 3000);
        }
    });

});

$(document).on("click", "#descargarBoton", function () {
    var elementId = $(this).attr("data-id")

    $.ajax({
        url: "/Exposiciones/Descargar/" + elementId,
        success: function (data) {
            $('#myAlertSuccess').show('fade');
            setTimeout(function () {
                location.reload();
            }, 3000);

        },
        error: function () {
            $("#buttonError").addClass('show');
            setTimeout(function () {
                location.reload();
            }, 3000);
        }
    });

});