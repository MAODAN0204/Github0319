$(document).ready(function(){

  $('input#txtStartDate,input#txtEndDate').datetimepicker({
    dateFormat: 'yy-mm-dd',
    timeFormat: 'HH',
    modal:true,
  });

  $("#expdata").click(function(){

    if($('#txtStartDate').val() >$('#txtEndDate').val() )
    {
        alert("起始日起不可大於結束日期");
    }
    else
    {
        var startDate=$('#txtStartDate').val();
        var endDate=$('#txtEndDate').val();
        var battery=$( "#batteryno" ).val();
        if($('#txtStartDate').val()=="" || $('#txtEndDate').val()=="")
        {
          alert("請選擇轉檔日期");
        }else if($('#batteryno').val()==""){
          alert("請選擇電池代號");
        }
        else{
          $('#spinner').show();
          $('#downarea').hide();
          $('#searcharea').hide();
          $.ajax({
              method: "POST",
              dataType:"json",
              url: "expdata.php",
              data: { startDT:startDate,endDT:endDate,battery:battery },
              complete:function(jqxHR){
                
                var now=new Date();
                var today=now.getFullYear().toString()+  ((now.getMonth()+1)<10?'0':'')+(now.getMonth()+1)  + (now.getDate()<10?'0':'') +now.getDate();
                $('#spinner').hide();
                $('#downarea').show();
                $('#searcharea').hide();
                $("#downarea").html("<h2><a href='/BMS/txt/"+battery+"-"+today+".txt' target='_block'>download</a></h2>");
              }
            });
        }
    }
   });
        
   $("#searchdata").click(function(){

    var startDate=$('#txtStartDate').val();
    var endDate=$('#txtEndDate').val();
    var battery=$( "#batteryno" ).val();
    if($('#txtStartDate').val() >$('#txtEndDate').val() )
    {
        alert("起始日起不可大於結束日期");
    }
    else
    {
        if($('#txtStartDate').val()=="" || $('#txtEndDate').val()=="")
        {
          alert("請選擇轉檔日期");
        }else if($('#batteryno').val()==""){
          alert("請選擇電池代號");
        }
        else{
      $('#spinner').show();
      $('#downarea').hide();
      $('#searcharea').hide();
      $.ajax({
          method: "GET",
          dataType:"json",
          url: "searchdata.php",
          data: { startDT:startDate,endDT:endDate,battery:battery },
          success:function(data){
            $('#spinner').hide();
            $('#downarea').hide();
            $('#searcharea').show();
          },
          complete:function(event,xhr,options){
            
            var now=new Date();
            var today=now.getFullYear().toString()+  (now.getMonth()+1).toString()  + now.getDate().toString() ;
            $('#spinner').hide();
            $('#downarea').hide();
            $('#searcharea').show();
            $("#tablebody").html(event.responseText);
          }
        });
    }
   }	
   });
});
