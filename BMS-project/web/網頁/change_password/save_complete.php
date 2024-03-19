<!DOCTYPE html>

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
        </div>
        <?php 
            $msg=$_GET["msg"];
            if($msg=="Register Completed")
            {
        ?>
        <div class="row pt-5">
            <div class="col-12 text-center"><img src="images\check_icon.png" class="width-50ps"></div>
        </div>

        <div class="row">
            <div class="col-12 pt-3 pb-5 text-center"><h3 class="text-navy">Save Completed!<br><br>Password has Changed.</h3>
            <?php
            }
            else{

            echo('<div class="row pt-5">
            <div class="col-12 pt-3 pb-5 text-center"><h3 class="text-navy">'.$msg.'</h3><h5 class="pt-5 text-dark">Please regrister again! <a href="https://118.163.50.93/register-alpsun/">go back </a></h5>');
		}
            ?>
	</div>
    </div>

    <script src="jquery-3.7.0.min.js"></script>
    <script src="bootstrap.min.js"></script>
    <script src="scriptjs.js"></script>
</body>
</html>