$(function () {

  // Fetch all the forms we want to apply custom Bootstrap validation styles to

  $("#form").submit(function(){
	var form=$("#form");
	if ( document.getElementById("password1").value != document.getElementById("password2").value ) {
            alert("Password mismatch");
            event.preventDefault();
            event.stopPropagation();
        }
	if (!form[0].checkValidity()) {
		Event.preventDefault()
		 Event.stopPropagation()
         }

  });

  

	
});
