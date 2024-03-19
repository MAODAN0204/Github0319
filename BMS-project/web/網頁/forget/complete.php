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
        <div class="row">
            <div class="col-12 text-center pt-4"><img src="images\logo-right.png" class="img-80ps" /></div>
            <div class="col-12 pt-4 pb-5"><h2 class="text-center text-navy">Forget<br>Password</h2></div>
        </div>

        <?php
            
            $msg=$_GET["msg"];

            if( $msg=="No this mail address" )
            {
                echo("<h2 style='text-align:center;color:red;'>".$msg."</h2><h5 style='padding-top:2em;text-align:center;'>please try again . <a href='https://118.163.50.93/forget-alpsun/' >go back </a> </h5>");
            }
            else
            {
                echo("<h2 style='text-align:center'>".$msg."</h2><h6 style='text-align:center;padding-top:2em;'>Please go to your email to receive the password change mail.</h6");
            }
            
        ?>
    </div>

    <script src="jquery-3.7.0.min.js"></script>
    <script src="bootstrap.min.js"></script>
    <script src="scriptjs.js"></script>
</body>
</html>