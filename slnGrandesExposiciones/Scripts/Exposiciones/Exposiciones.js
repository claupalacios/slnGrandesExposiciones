$(document).on("click", ".delete", function () {
    var elementId = $(this).data('id');
    $(".modal-body #elementId").val(elementId);
});


$(document).on("click", "#aceptarBoton", function () {
    var archivoId = $(".modal-body #elementId").val();
    var exposicionId = $(".modal-body #elementId").val();

    $.ajax({
        url: "/Exposiciones/Nuevo/" + archivoId + "/" + exposicionId,
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