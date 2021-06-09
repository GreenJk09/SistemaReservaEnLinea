$.datepicker.regional['es'] = {
    closeText: 'Cerrar',
    prevText: '< Ant',
    nextText: 'Sig >',
    currentText: 'Hoy',
    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
    dayNames: ['Domingo', 'Lunes', 'Martes', 'Mi\u00E9rcoles', 'Jueves', 'Viernes', 'S\u00E1bado'],
    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mi\u00E9', 'Juv', 'Vie', 'S\u00E1b'],
    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'S\u00E1'],
    weekHeader: 'Sm',
    dateFormat: 'dd/mm/yy',
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ''
};
$.datepicker.setDefaults($.datepicker.regional['es']);


$(function () {
    "use strict";

    // for navbar background color when scrolling
     
    $(window).scroll(function () {
        var $scrolling = $(this).scrollTop();
        var bc2top = $("#back-top-btn");
        var stickytop = $(".navbar");
        if ($scrolling >= 160) {
            stickytop.addClass('sticky');
        } else {
            stickytop.removeClass('sticky');
        }
        if ($scrolling > 150) {
            bc2top.fadeIn(2000);
        } else {
            bc2top.fadeOut(2000);
        }
    });
 
  
    //animation scroll js
        var nav = $('nav'),
        navOffset = nav.offset().top,
        $window = $(window);
    /* navOffset ends */
   
    //animation scroll js
    var html_body = $('html, body');
    $('nav a').on('click', function () {
        if (location.pathname.replace(/^\//, '') === this.pathname.replace(/^\//, '') && location.hostname === this.hostname) {
            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            if (target.length) {
                html_body.animate({
                    scrollTop: target.offset().top - 80
                }, 1000);
                return false;
            }
        }
    });


    // navbar js ends here

    //BACK TO TO BUTTON JAVA SCRIPT PART START
    var bc2top = $('#back-top-btn');
    bc2top.on('click', function () {
        html_body.animate({
            scrollTop: 0
        }, 100);
    });

    // Closes responsive menu when a scroll link is clicked
    $('.nav-link').on('click', function () {
        $('.navbar-collapse').collapse('hide');
    });
    
    /* -------------------------------------
	          Preloader				
	 	-------------------------------------- */
    $('#preloader').delay(2500).fadeOut(1000);
    
    
    /* -------------------------------------
	         datepicker js				
	 	-------------------------------------- */
   $('#datepicker').datepicker({
       minDate: 0,
       onSelect: function () {
           var dt2 = $('#datepicker1');
           var nextDayDate = $(this).datepicker('getDate', '+1d');
           nextDayDate.setDate(nextDayDate.getDate() + 1);
           dt2.datepicker('setDate', nextDayDate);
           dt2.datepicker('option', 'minDate', nextDayDate);
       }
   });
    $('#datepicker1').datepicker({
        minDate: 0
    });
      
     /* -------------------------------------
	         travel js				
	 	-------------------------------------- */
    $(document).ready(function(e) {
try {
$(".get-app select").msDropDown();
} catch(e) {
alert(e.message);
}
});
    
    /* -------------------------------------
	         top-day-slick js				
	 	-------------------------------------- */
    $('.top-day-slick').slick({
        autoplay: true,
        arrows: true,
        dots: false,
        prevArrow: '<i class="fa fa-angle-right todaysNext"></i>',
        nextArrow: '<i class="fa fa-angle-left todaysprev"></i>',
        infinite: true,
        slidesToShow: 3,
        autoplaySpeed: 4000,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1,
                    infinite: true,

                }
    },
            {
                breakpoint: 950,
                settings: {
                    arrows: false,
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
    },
            {
                breakpoint: 767.98,
                settings: {
                    arrows: false,
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
    }
  ]
    });

    /* -------------------------------------
	       recomended-slick js				
	 	-------------------------------------- */
    $('.recomended-slick').slick({
        autoplay: true,
        arrows: true,
        dots: false,
        prevArrow: '<i class="fa fa-angle-right recomandNext"></i>',
        nextArrow: '<i class="fa fa-angle-left recomandprev"></i>',
        infinite: true,
        slidesToShow: 3,
        autoplaySpeed: 4000,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1,
                    infinite: true,

                }
    },
            {
                breakpoint: 910,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
    },
            {
                breakpoint: 767.98,
                settings: {
                    arrows: false,
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
    }
  ]
    });
    
    
    /* -------------------------------------
	       artical-slick js				
	 	-------------------------------------- */
    $('.artical-slick').slick({
        autoplay: true,
        arrows: true,
        dots: false,
        prevArrow: '<i class="fa fa-angle-right articalNext"></i>',
        nextArrow: '<i class="fa fa-angle-left articalprev"></i>',
        infinite: true,
        slidesToShow: 2,
        autoplaySpeed: 4000,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                    infinite: true,

                }
    },
            {
                breakpoint: 920,
                settings: {
                     arrows: false,
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
    },
            {
                breakpoint: 767.98,
                settings: {
                    arrows: false,
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
    }
  ]
    });
    
    

    /* -------------------------------------
	          Preloader				
	 	-------------------------------------- */
    $('.preloader').delay(2500).fadeOut(1000);

});


jQuery.extend(jQuery.validator.messages, {
    required: "Este campo es obligatorio.",
    remote: "Por favor, rellena este campo.",
    email: "Por favor, escribe una direcci\u00F3n de correo v\u00E1lida",
    url: "Por favor, escribe una URL v\u00E1lida.",
    date: "Por favor, escribe una fecha v\u00E1lida.",
    dateISO: "Por favor, escribe una fecha (ISO) v\u00E1lida.",
    number: "Por favor, escribe un número entero v\u00E1lido.",
    digits: "Por favor, escribe s\u00F3lo dí­gitos.",
    creditcard: "Por favor, escribe un número de tarjeta v\u00E1lido.",
    equalTo: "Por favor, escribe el mismo valor de nuevo.",
    accept: "Por favor, escribe un valor con una extensi\u00F3n aceptada.",
    maxlength: jQuery.validator.format("Por favor, no escribas m\u00E1s de {0} caracteres."),
    minlength: jQuery.validator.format("Por favor, no escribas menos de {0} caracteres."),
    rangelength: jQuery.validator.format("Por favor, escribe un valor entre {0} y {1} caracteres."),
    range: jQuery.validator.format("Por favor, escribe un valor entre {0} y {1}."),
    max: jQuery.validator.format("Por favor, escribe un valor menor o igual a {0}."),
    min: jQuery.validator.format("Por favor, escribe un valor mayor o igual a {0}.")
});