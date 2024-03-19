$(document).ready(function(){

   $("#devdata").click(function(){

        var devno=$( "#devno" ).val();
        if($('#devno').val()=="" ){
            alert("請選擇MAC");
        }else{
        $('#spinner').show();
        $('#searcharea').hide();
        $.ajax({
            method: "GET",
            dataType:"json",
            url: "searchdata_list.php",
            data: { mac:devno },
            success:function(data){
                $('#spinner').hide();
                $('#downarea').hide();
                $('#searcharea').show();
            },
            complete:function(event,xhr,options){
                
                var now=new Date();
                var today=now.getFullYear().toString()+  (now.getMonth()+1).toString()  + now.getDate().toString() ;
                $('#spinner').hide();
                $('#searcharea').show();
                $("#tablebody").html(event.responseText);
            }
            });
        }

   });
});
