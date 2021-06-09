
$(".btnAjaxAdd").on('click', function (e) {
    e.preventDefault();
    var botonActual = $(this);
    $(botonActual).attr('disabled', true);
    $(botonActual).append("<img id='imgEnviandoAjax' src='" + urlLoading + "' style='margin-left: 10px;' />");
    var idFormulario = $(botonActual).data("formulario");
    var formAjaxAdd = $(idFormulario);
    var validAjaxAdd = formAjaxAdd.validate({
        //== Validate only visible fields
        ignore: ":hidden",
        //== Display error  
        invalidHandler: function (event, validAjax) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Los datos capturados no son correctos!'
            });
            $("#imgEnviandoAjax").remove();
            $(botonActual).attr('disabled', false);
        },
        //== Submit valid form
        submitHandler: function (form) {
        }
    });
    if (validAjaxAdd.form() == false) {
        return;
    }
    formAjaxAdd.ajaxSubmit({
        success: function (response) {
            if (response.isSuccess == true) {
                if (response.url != "" && response.url != null) {
                    window.location.href = response.url;
                } else {
                    if (response.funcion == "cargacomentario") {
                        $(response.divTabla).append(response.html);
                    }
                    Swal.fire({
                        icon: 'success',
                        title: 'Felicidades...',
                        text: response.message
                    });
                }                
            } else {
                swal.fire({
                    title: "Oops...",
                    text: response.message,
                    icon: "error"
                });
            }
            $("#imgEnviandoAjax").remove();
            $(botonActual).attr('disabled', false);
        },
        error: function (request, status, error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: "Error al conectarse al servidor"
            });
            $("#imgEnviandoAjax").remove();
            $(botonActual).attr('disabled', false);
        }
    });

});