
$(document).on("click", ".delete", function () {
    var elementId = $(this).data('id');
    $(".modal-body #elementId").val(elementId);
});

$(document).on("click", "#botonPrueba", function () {
    $('#myAlertSuccess').show('fade');
});


$(document).on("click", "#aceptarBoton", function () {
    var elementId = $(".modal-body #elementId").val();

    $.ajax({
        url: "/Parametros/Eliminar/" + elementId,
        success: function (data) {
            $('#myAlertSuccess').show('fade');
            setTimeout(function () {
                location.reload();
            }, 5000);

        },
        error: function () {
            $("#buttonError").addClass('show');
            setTimeout(function () {
                location.reload();
            }, 5000);
        }
    });

});