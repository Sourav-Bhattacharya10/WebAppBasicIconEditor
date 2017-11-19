$(document).ready(function () {
    if ($(this).attr('title') == "Home")
        $('#HomeLink').addClass('active');
    else
        $('#AboutLink').addClass('active');

    $('#HomeLink').click(function () {
        $('#AboutLink').removeClass('active');
        $('#HomeLink').addClass('active');
    });

    $('#AboutLink').click(function () {
        $('#HomeLink').removeClass('active');
        $('#AboutLink').addClass('active');
    });
});