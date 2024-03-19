// JavaScript source code
(function ($) {
    /* project1 */
    $('#kind1-det,#kind2-det,#kind3-det,.blog-inner-page').css('display', 'none');

    $('#kind-1').click(function () {
        $('#kind1-det,.blog-inner-page').css('display', 'block');
        $('.our-case').css('display', 'none');
    });

    $('#kind-2').click(function () {
        $('#kind2-det,.blog-inner-page').css('display', 'block');
        $('.our-case').css('display', 'none');
    });

    $('#kind-3').click(function () {
        $('#kind3-det,.blog-inner-page').css('display', 'block');
        $('.our-case').css('display', 'none');
    });

    $('.go-pre').click(function () {
        $('#kind1-det,#kind2-det,#kind3-det,.blog-inner-page').css('display', 'none');
        $('.our-case').css('display', 'block');
    });
    /* project2 */
    /* project3 */
})(jQuery)
