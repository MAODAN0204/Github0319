﻿<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link rel="stylesheet" type="text/css" href="bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="css.css" />
</head>
<body>
    <div class="container">
        <div class="row mt-3">
            <div class="col-12 text-center pt-4"><img src="images\logo-right.png" class="width-80ps" /></div>
            <div class="col-12 pt-4 pb-4"><h2 class="text-center text-navy">Rnew Password</h2></div>
        </div>
        <?php
            $id=$_GET["id"];
            include("..\mysql_connect_wr.php");
	    $mail=$_GET["mail"];
                
        ?>
	<form class="g-3 needs-validation" name="renewpassord" method="POST" action="savedata.php" id="form">
        <div class="row pl-5 pr-5">
            <div class="col-12">New Passoword <span class="text-red text-bold">*</span></div>
            <div class="col-12"><div class="pb-3"><input type="text" name="password1" class="form-control" id="password1" placeholder="Minimum 6 characters" minlength="6" required />
		<div class="invalid-feedback">
      		Please check this field value.
		</div></div></div>
            <div class="col-12">Confirm Passoword <span class="text-red text-bold">*</span></div>
            <div class="col-12"><div class="pb-3"><input type="text" name="password2" class="form-control" id="password2" placeholder="Match original Password withc minimum 6 characters" minlength="6" required />
		<div class="invalid-feedback">
      		Please check this field value.
		</div></div></div>
	    <div class="col-12"><input type="hidden" name="mail" value=<?php echo($mail); ?> /></div>
        </div>

        <div class="row">
            <div class="col-12 pt-3 pb-5 text-center"><button id="save_btn" type="submit" class="btn btn-lg width-80ps btn-info btn-shadow"><img src="images\save.png" />  Save </button>

        </div>
	</form>	
    </div>
    <p style="margin-bottom:10em;">  </p>

    <script src="jquery-3.7.0.min.js"></script>
    <script src="bootstrap.min.js"></script>
    <script src="scriptjs.js"></script>
</body>
</html>