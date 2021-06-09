
$(".btnAjaxAdd").on('click', function (e) {
    e.preventDefault();
    var botonActual = $(this);
    $(botonActual).attr('disabled', true);
    $(botonActual).append('<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-loader spinercargando spin mr-2"><line x1="12" y1="2" x2="12" y2="6"></line><line x1="12" y1="18" x2="12" y2="22"></line><line x1="4.93" y1="4.93" x2="7.76" y2="7.76"></line><line x1="16.24" y1="16.24" x2="19.07" y2="19.07"></line><line x1="2" y1="12" x2="6" y2="12"></line><line x1="18" y1="12" x2="22" y2="12"></line><line x1="4.93" y1="19.07" x2="7.76" y2="16.24"></line><line x1="16.24" y1="7.76" x2="19.07" y2="4.93"></line></svg>');
    var idFormulario = $(botonActual).data("formulario");
    var formAjaxAdd = $(idFormulario);
    var valid = true;
    if (typeof CKEDITOR !== 'undefined' &&
        typeof CKEDITOR.instances.txtDescripcionSendHotel != 'undefined' &&
        typeof CKEDITOR.instances.txtDescripcionSendHotel.getData() != 'undefined') {
        $("#txtDescripcionSendHotel").val(CKEDITOR.instances.txtDescripcionSendHotel.getData());
    }

    if (typeof CKEDITOR !== 'undefined' &&
        typeof CKEDITOR.instances.txtPost != 'undefined' &&
        typeof CKEDITOR.instances.txtPost.getData() != 'undefined') {
        $("#txtPost").val(CKEDITOR.instances.txtPost.getData());
    }

    var invalid = $('.simple-example .invalid-feedback');
    var validation = Array.prototype.filter.call(formAjaxAdd, function (form) {
        if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            valid = false;
            form.reportValidity();
            $(".form-control:invalid").each(function () {
                $(this).addClass("errorjieh");
                $(this).next(".invalid-feedback").css('display', 'block');
            })
            swal({
                type: 'error',
                title: 'Oops...',
                text: 'Los datos capturados no son correctos!',
                padding: '2em'
            });
        } else {
            event.preventDefault();
            event.stopPropagation();
            invalid.css('display', 'none');
            form.classList.add('was-validated');
        }
    });

if (valid == false) {
    $(".spinercargando").remove();
    $(botonActual).attr('disabled', false);
    return;
}
formAjaxAdd.ajaxSubmit({
    success: function (response) {
        if (response.isSuccess == true) {
            if (response.funcion == "cambio de tema") {
                location.reload();
                return;
            }

            if (response.funcion == "guardadoyredirige")
            {
                swal({
                    type: 'success',
                    title: 'Felicidades...',
                    text: response.message,
                    padding: '2em'
                }).then(function (result) {
                    window.location.href = response.url;
                    return;
                })
                return;
            }

            if (response.divTabla != null && response.divTabla != "") {
                $(".modal").modal("hide");
                swal({
                    type: 'success',
                    title: 'Felicidades...',
                    text: response.message,
                    padding: '2em'
                });
                $(response.divTabla).load(response.url, function (response, status, xhr) {
                    $(".loadingCargando").hide();
                });
            } else {
                if (response.url != null && response.url != "") {
                    window.location.href = response.url;
                } else {
                    swal({
                        type: 'success',
                        title: 'Felicidades...',
                        text: response.message,
                        padding: '2em'
                    });
                    var idedit = $("#txtId").val();
                    if (response.id > 0 && (isNaN(idedit) || idedit < 1))
                        $("#txtId").val(response.id).trigger("change");

                }
            }
        } else {
            swal({
                title: "Oops...",
                text: response.message,
                type: "error",
                padding: '2em'
            });
        }
        $(".spinercargando").remove();
        $(botonActual).attr('disabled', false);
    },
    error: function (request, status, error) {
        swal({
            type: 'error',
            title: 'Oops...',
            text: "Error al conectarse al servidor",
            padding: '2em'
        });
        $(".spinercargando").remove();
        $(botonActual).attr('disabled', false);
    }
});

});