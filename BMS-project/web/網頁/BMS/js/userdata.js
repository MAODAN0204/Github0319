$(document).ready(function(){
    
    $("#memata,#devdata").click(function(){
        var memno=$( "#memno" ).val();
        var memname=$( "#memname" ).val();
        var email=$( "#email" ).val();
        var kind="";
        var item="";
        var head="";
        if(this.id=="devdata"){
           kind="dev";
           head="<tr><th>會員編號</th><th>會員姓名</th><th>設備 MAC</th><th>設備名稱</th><th>備註</th><th>建立時間</th><th>刪除時間</th></tr>";
        }else{
           kind="member";
           head="<tr><th>會員編號</th><th>會員姓名</th><th>匿名名稱</th><th>電話號碼</th><th>行動號碼</th><th>e_mail</th><th>註冊時間</th></tr>";
        }
        if( memno=="" && memname=="" && email=="" ){
            alert("請選擇一個條件查詢資料");
        }else{
            $('#spinner').show();
            $('#searcharea').hide();
            var condition=""
            if(memno!=""){
                condition=memno;
                item="1";
            }else if(memname!=""){
                condition=memname;
                item="2";
            }else{
                condition=email;
                item="3";
            }
            $.ajax({
                method: "GET",
                dataType:"json",
                url: "memberdata.php",
                data: { data:condition,tb:kind,item:item },
                complete:function(event,xhr,options){
                    
                    var now=new Date();
                    var today=now.getFullYear().toString()+  (now.getMonth()+1).toString()  + now.getDate().toString() ;
                    $('#spinner').hide();
                    $('#searcharea').show();
                    $("#tablehead").html(head);
                    $("#tablebody").html(event.responseText);
                }
            });
        }
   });
});
