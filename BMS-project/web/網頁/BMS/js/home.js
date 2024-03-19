$(document).ready(function(){

    var today=new Date();
    $('#showdate').html(today.getFullYear()+"/"+(today.getMonth()+1)+"/"+today.getDate());

    $('#realtime').click(function(){
        window.location.href="real_data.php";
    });
    
    $('#recivedata').click(function(){
        window.location.href="analyze.php";
    });
    
    $('#devicelist').click(function(){
        window.location.href="devlist.php";
    });

    $('#userdata').click(function(){
        window.location.href="userdata.php";
    });

    $('#devicetype').click(function(){
        window.location.href="devicetype.php";
    });

    $('#getlince').click(function(){
        window.location.href="license.php";
    });

    $('#logout').click(function(){
        $.removeCookie('user');
        $.removeCookie('pswd');
        window.location.reload();
    })

});
