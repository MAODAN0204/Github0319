$(document).ready(function(){

   $("#modify").click(function(){

        var devno=$("input[name='optradio']:checked") .val();
        if(devno==undefined ){
            alert("請選擇修改項目");
        }else{
            $('#modi').show();
            $('#Add').hide();
            $('#func').hide();
            var str=devno.split("^");
            $.ajax({
                method: "POST",
                dataType:"json",
                url: "devtypefunction.php",
                data: { no:str[0],sub:str[1]},
                complete:function(event,xhr,options){
                    var str=event.responseText.split("#");
                    if(str.length>1)
                    {
                        $('#spe_array_m').val(str[0]);
                        $('#tol_length_m').val(str[1]);
                    }else{
                        alert("沒資料");
                    }

                }
            });
        }
   });

   $("#new").click(function(){
        $('#Add').show();
        $('#modi').hide();
        $('#func').hide();
   });

   $(".cancel").click(function(){
        $('#modi').hide();
        $('#Add').hide();
        $('#func').show();
   });

   $("#add_save").click(function(){
        var dev_type=$('#dev_type').val();
        var sub_type=$('#sub_type').val();
        var tpe_name=$('#tpe_name').val();
        var tol_length=$('#tol_length').val();
        var spe_array=$('#spe_array').val();
        if(dev_type=="" || tpe_name=="" || tol_length=="" || spe_array=="" )
        {
            alert("欄位不可空白");
        }else{
            $.ajax({
                method: "POST",
                dataType:"json",
                url: "devtypesave.php",
                data: { spe:spe_array,tol:tol_length,dev:dev_type,sub:sub_type,tpe:tpe_name },
                complete:function(event,xhr,options){
                    window.location.reload();
                }
            });
        }
        window.location.reload();
    });

    $("#modi_save").click(function(){
        var spe_array=$('#spe_array_m').val();
        var tol_length=$('#tol_length_m').val();
        var devno=$("input[name='optradio']:checked").val();
        if(spe_array=="" || tol_length=="")
        {
            alert("欄位不可空白");
        }else{
            var str=devno.split("^");
            $.ajax({
                method: "POST",
                dataType:"json",
                url: "devtypesave.php",
                data: { spe:spe_array,tol:tol_length,dev:str[0],sub:str[1] },
                complete:function(event,xhr,options){
                    window.location.reload();
                }
            });
        }
    });

});
