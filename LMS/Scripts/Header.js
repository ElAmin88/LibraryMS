﻿
$(document).on('scroll', function () {

    if ($(this).scrollTop() > 1) {
        $('header').addClass('stivky');
    }

    else {
        $('header').removeClass('stivky');
    }
});

/*top botton Scroll to top animatioon change JQuery code below*/

$(document).on('click', '#scrollToTop', function () {
    $('html,body').animate({ scrollTop: 0 }, 500);
    return false;
});

/*top botton show/hide animatioon change JQuery code below*/

$(document).scroll(function (e) {
    var scrollpos = $(this).scrollTop();

    if (scrollpos < 500) {
        $('#scrollToTop').addClass('hide');
    }
    else {
        $('#scrollToTop').removeClass('hide');

    }


});