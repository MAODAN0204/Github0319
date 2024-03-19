$(function () {

  // Fetch all the forms we want to apply custom Bootstrap validation styles to

  $("#form").submit(function(){
	var form=$("#form");

	if (!form[0].checkValidity()) {
		Event.preventDefault()
		 Event.stopPropagation()
    }
	/*else{
		var username = $("#username").val();
		var password= $("#password").val();
		var nickname= $("#nickname").val();
		var tel= $("#telephone").val();
		var mobile= $("#mobile").val();
		var email= $("#email").val();
		$.ajax({
			type: "GET",
			url: "savedata.php",
			//crossDomain:true,
			dataType: "json",
			data:{User:username,Pswd:password,Nkname:nickname,Handphone:tel,Mobile:mobile,Email:email},
			success: function(data){
			if(data=="OK")
			{
				window.location.href ="save_complete.html";
				}
			},
			error: function(e){
				console.log(e);
			}
		});
		window.location.href ="save_complete.html";
	}*/

  });

  /*var forms = document.querySelectorAll('.needs-validation')
 
  // Loop over them and prevent submission
  Array.prototype.slice.call(forms)
      .forEach(function (form) {
      	form.addEventListener('submit', function (event) {
        	if (!form.checkValidity()) {
         		event.preventDefault()
          		event.stopPropagation()
	        }
		else
		{
			var username = $("#username").val();
        	var password= $("#password").val();
        	var nickname= $("#nickname").val();
        	var tel= $("#telephone").val();
        	var mobile= $("#mobile").val();
        	var email= $("#email").val();

			//check member
			
			$.ajax({
				type: "GET",
				url: "https://118.163.50.93/register/savedata.php?User=username&Pswd=password&Nkname=nickname&Handphone=tel&Mobile=mobile&Email=email",
				jsonp: 'callback',
				dataType: "jsonp",
				data:{User:username,Pswd:password,Nkname:nickname,Handphone:tel,Mobile:mobile,Email:email},
				success: function(data){
					alert(data);
				if(data=="OK")
				{
					window.location.href ="save_complete.html";
					}
				},
				error: function(e){
					console.log(e);
				}
			});
			/*$.ajax({
				type: "GET",
			url: "checkdata.php",
			//crossDomain:true,
			dataType: "jsonp",
			data:{User:username,email:email},
			success: function (response) {
				if(response =="0")
				{	
				//save data
				$.ajax({
					type: "GET",
					url: "savedata.php",
					//crossDomain:true,
					dataType: "jsonp",
					data:{User:username,Pswd:password,Nkname:nickname,Handphone:tel,Mobile:mobile,Email:email},
					success: function(data){
					if(data=="OK")
					{
						window.location.href ="save_complete.html";
						}
					},
					error: function(e){
						console.log(e);
					}
					});

				}else{	//�ϥΪ̦W+mail�w�s�b���i�s�W!
					alert("This Username and email has been registered!; \n Please use Forget password!");
				}
			},

			error: function (thrownError) {
				console.log(thrownError);
			}
			});*/
			/*window.location.href ="save_complete.html";
		}
        form.classList.add('was-validated')
      	}, false)
    })*/

	
});
