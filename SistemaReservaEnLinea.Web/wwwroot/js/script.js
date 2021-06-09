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

    //==================Animated Scroll Start==================

    var html_body = $('html, body');
    $('nav a').on('click', function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            if (target.length) {
                html_body.animate({
                    scrollTop: target.offset().top - 80
                },900);
                return false;
            }
        }
    });
    
    $(document).ready(function () {
        $('body').scrollspy({
            target: ".nav_area",
            offset: 50
        });
        $("#navbar-example a").on('click', function (event) {
            if (this.hash !== "") {
                event.preventDefault();
                var hash = this.hash;
                $('html, body').animate({
                    scrollTop: $(hash).offset().top
                }, 1000, function () {
                    window.location.hash = hash;
                });
            }
        });

    });


    // Closes responsive menu when a scroll link is clicked
    $('.nav-link').on('click', function () {
        $('.navbar-collapse').collapse('hide');
    });

    //==================Animated Scroll End==================


    //BACK TO TO BUTTON JAVA SCRIPT PART START

    var bc2top = $('#back-top-btn');
    bc2top.on('click', function () {
        html_body.animate({
            scrollTop: 0
        }, 100);
    });

    //BACK TO TO BUTTON JAVA SCRIPT PART END

/* -------------------------------------
	          Preloader				
	 	-------------------------------------- */
    $('#preloader').delay(2500).fadeOut(1000);
  /* -------------------------------------
	         datepicker js				
	 	-------------------------------------- */
   $('#datepicker').datepicker({
    format: 'mm/dd/yyyy',
    startDate: '-3d'
});
     $('#datepicker1').datepicker({
    format: 'mm/dd/yyyy',
    startDate: '-3d'
});
    $('#datepicker2').datepicker({
    format: 'mm/dd/yyyy',
    startDate: '-3d'
});
     $('#datepicker3').datepicker({
    format: 'mm/dd/yyyy',
    startDate: '-3d'
});
    $('#datepicker4').datepicker({
    format: 'mm/dd/yyyy',
    startDate: '-3d'
});
     $('#datepicker5').datepicker({
    format: 'mm/dd/yyyy',
    startDate: '-3d'
});
   $('#datepicker6').datepicker({
    format: 'mm/dd/yyyy',
    startDate: '-3d'
});
     $('#datepicker7').datepicker({
    format: 'mm/dd/yyyy',
    startDate: '-3d'
});

    //COUNTER PART JAVA SCRIPT PART END

 /* -------------------------------------
	       artical-slick js				
	 	-------------------------------------- */
    $('.artical-slick').slick({
        autoplay: false,
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
                breakpoint: 910,
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

    //FEEDBACK PART JAVA SCRIPT PART START

    $('.testimonial-cont').slick({
        autoplay: true,
        arrows: false,
        dots: false,
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
                breakpoint: 910,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
    },
            {
                breakpoint: 767.98,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
    }
  ]
    });

/* -------------------------------------
	       Tour Packages-slick js				
	 	-------------------------------------- */
    $('.tour-cont').slick({
        autoplay: false,
        arrows: false,
        dots: false,
        arrows: false,
        infinite: true,
        slidesToShow: 3,
        autoplaySpeed: 4000,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1

                }
    },
            {
                breakpoint: 920,
                settings: {
                     autoplay: true,
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
    },
            {
                breakpoint: 767.98,
                settings: {
                    autoplay: true,
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
    }
  ]
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


    //PROTFOLIO PART JAVA SCRIPT PART START

    var mixer = mixitup('.mix-port');

    //PROTFOLIO PART JAVA SCRIPT PART END


    //PRELOADER PART JAVA SCRIPT PART START

    window.addEventListener("load", function () {
        var miron = document.querySelector(".preloader");
        miron.classList.add("load_finish");
    });


   


    //PRELOADER PART JAVA SCRIPT PART END


});