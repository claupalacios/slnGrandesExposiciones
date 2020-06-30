
$(document).on("click", ".delete", function () {
    var elementId = $(this).data('id');
    $(".modal-body #parametroAEliminar").val(elementId);
});


$(document).on("click", "#aceptarEliminarParametroBoton", function () {
    var elementId = $(".modal-body #parametroAEliminar").val();

    $.ajax({
        url: "/Parametros/Eliminar/" + elementId,
        success: function (data) {
            $('#myAlertSuccess').show('fade');
            setTimeout(function () {
                location.reload();
            }, 2000);

        },
        error: function () {
            $("#buttonError").addClass('show');
            setTimeout(function () {
                location.reload();
            }, 2000);
        }
    });

});