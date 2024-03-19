$(document).ready(function(){
    var i=0;

    setTimeout(reload,5000); //指定5秒更新一次   
    function reload(){
	window.location.reload();
    }
    
    setInterval(countsecond, 1000);
    function countsecond() 
    {
	$('#second').html(i);
	i=i+1;
    }

     $('#reflash').click(function(){
	window.location.reload();
	});
    $('#login').click(function(){
	window.location.href="https://118.163.50.93/BMS/";
	});
});
