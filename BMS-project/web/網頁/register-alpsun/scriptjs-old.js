// JavaScript source code

$(function () {

  // Fetch all the forms we want to apply custom Bootstrap validation styles to
  var forms = document.querySelectorAll('.needs-validation')

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
			url: "checkdata.php",
			dataType: "json",
			data:{User:username,email:email},
			success: function (response) {
				if(response =="0")
				{	
				//save data
				$.ajax({
					type: "GET",
					url: "savedata.php",
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

				}else{	//�ϥΪ̦W+mail�w�s�b���i�s�W!
					alert("This Username and email has been registered!; \n Please use Forget password!");
				}
			},

			error: function (thrownError) {
				console.log(thrownError);
			}
			});

		}
        //form.classList.add('was-validated')
      	//}, false
		});
    });

    $("#save_btn").click(function () {

        
    });
});
