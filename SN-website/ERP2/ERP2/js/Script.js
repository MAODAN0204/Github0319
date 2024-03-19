// JavaScript source code

(function ($) {

    w_h_resize();

    $(window).resize(function () {
        w_h_resize();
    });

    function w_h_resize() {
        var height_i = $(window).height();
        $('.wall').css('height', height_i);
    }

    $(".menu").click(function () {
        $(".asideMenu").toggleClass("active");
        if ($(this).css("transform") == 'none') {
            $(this).css({
                "transform": "rotate(180deg)",
                "border-radius": "6px 0px 0px 6px"
            });
        } else {
            $(this).css({
                "transform": "",
                "border-radius": "0px 6px 6px 0px"
            });
        }
    });

    $('[data-toggle="tooltip"]').tooltip();

    $("#proSN").click(function () {
        window.location.href = "GetSN.aspx";
    });

    //===========keydown keypress count again=============

    $("#textbox2").on('keydown keyup keypress change', function () {

        var countMax = 250;
        var thisValueLength = $(this).val().length;
        var countDown = countMax - thisValueLength;
        $('#wordcounter').text(countDown);
    });
    

    $("#othSN").click(function () {
        window.location.href = "NorSN.aspx";
    });

    $("#checkbtn").click(function () {
        
        if ($("#FirstSN").val() != "" && $("#FirstSN").val() != "") {
            $("#msgbox").show();
            $(".QuestBox").show();
        } else {
            $("#msgbox").show();
            $(".ErrBox").show();
        }
    });

    $(".form-radio").click(function (e) {

        var id = e.target.id.replace("bt","");

        $(".form-radio").removeClass('btn-danger').addClass("btn-secondary");
        //e.target.parentElement.parentNode.parentNode.childNode.style.backgroundColor = "#ffffff";
        //remove all button background color change to default color

        //start change bg color
        $(e.target).removeClass('btn-dark').addClass("btn-danger");
        //change target button background

        //e.target.parentElement.parentNode.style.backgroundColor = "#fdfbf1";
        //change tr bg color

        //count ps field words and put to SPAN 
        
        $.ajax({
            type: "POST",
            url: "./NorSN.aspx/GetModiData",
            data: JSON.stringify({ 'id': id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                GiveData(data.d);
            },
            error: function (e, s, xhr) {
                console.log(e.responseText);
            }
        });

    });

    $("#ModiOK").click(function() {
        //確定修改要儲 存
        /*傳給ajax*/
        //var id = $("input[name=GroupRadio]:checked").val();
        var id = $("#hideindex").val();
        var data = $("#textbox2").val();

        $.ajax({
            type: "POST",
            url: "./NorSN.aspx/SavePS",
            data: JSON.stringify({ 'id': id,'data': data}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $("#Tbody2 tr").remove();  //clear table body data
                window.location.reload();
            },
            error: function (e, s, xhr) {
                console.log(e.responseText);
            }
        });
        
    });

    $("#ModiNO").click(function () {
        //取消修改

        $("#psmodisection").hide();
        $("#Sn_search").show();
        $('#Search-data').find('tbody').html('');
        $('#Search-data').find('tbody').remove();
    });

    function clearTb() {
        $("#Search-data>tbody").empty();
        return 0;
    }
    

    function GiveData(str) {
        //push data to modify input

        var s_str = str.split("/");
        $("#Label1").val(s_str[0]);
        $("#Label2").val(s_str[1]);
        $("#Label4").val(s_str[3]);
        $("#hideindex").val(s_str[2]);
        $("#Label6").val(s_str[4]);
        $("#textbox2").val(s_str[5]);

        $("#psmodisection").show();
        $("#Sn_search").hide();

        //count words
        var countMax = 250;
        var thisValueLength = $("#textbox2").val().length;
        var countDown = countMax - thisValueLength;
        $('#wordcounter').text(countDown);

        return "1";
    }
}) (jQuery);