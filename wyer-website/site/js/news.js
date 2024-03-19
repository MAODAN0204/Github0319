// JavaScript source code

(function ($) {

    var url = $(location).attr('hash').replace('#','');
    
    if (url != '') {
        activaTab(url);
    }

    function activaTab(tab) {
        $('.nav-tabs a[href="#' + tab + '"]').tab('show');
    };
    
})(jQuery)