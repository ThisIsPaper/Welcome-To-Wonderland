$(document).ready( function() {

    window.onscroll = function(e) {
        $scroll = $(window).scrollTop();
        $siteHeader = $('#siteHeader');

        if ( $scroll > 250 ) {
            $siteHeader.addClass('compressed');
        }
        else {
            $siteHeader.removeClass('compressed');
        }
    }

});