$(document).ready( function() {

  window.onscroll = function(e) {
    $scroll = $(window).scrollTop();
    $siteHeader = $('#siteHeader');

    if ( $scroll > 100 ) {
      $siteHeader.addClass('compressed');
    }
    else {
      $siteHeader.removeClass('compressed');
    }
  }

});