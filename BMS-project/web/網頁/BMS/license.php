<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html lang="zh-tw" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="icon" type="image/png" sizes="56x56" href="images/logo.ico"> 
    <title>Wyer</title>
    <link type="text/css" rel="stylesheet" href="css/bootstrap.min.css">
    <link type="text/css" rel="stylesheet" href="css/sytle.css">

    <script src="js/jquery-3.7.0.min.js"></script>
    <script src="js/jquery.cookie.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <script src="js/home.js"></script>
    <script src="js/devlist.js"></script>
</head>
<body>
<?php
    $user="";
        if (isset($_COOKIE["user"]))
        {
            $user=@$_COOKIE["user"];
            $psd=@$_COOKIE["pswd"];
        }
    
    ?>
    <div class="container">
        <div class="row">
            <div class="col-6"><img class="img52 mt-2" src="images/wyer_logo.png" /></div>
            <div class="col-6 text-right">Date：<span id="showdate">123</span><?php if($user!=""){ echo("<br> 登入者 : ".$user);echo("<button class='btn ml-2 text-danger border'>登出</button> "); }?></div>
        </div>
        <hr>
        <div class="row">
        <table class="menubtn">
                <tr>
                    <td><button id="realtime" class="btn btn-info btn-block">即時資料</button></td>
                    <td><button id="recivedata" class="btn btn-info btn-block">接收分析</button></td>
                    <td><button id="devicelist" class="btn btn-info btn-block">設備明細</button></td>
                    <td><button id="userdata" class="btn btn-primary btn-block">使用者資料</button></td>
                    <td><button id="devicetype" class="btn btn-primary btn-block">設備型態輸入</button></td>
                    <td><button id="getlince" class="btn btn-primary btn-block">權限資料</button></td>
                </tr>
            </table>
        </div>
        <hr>
        <!--以下開始--->
        <?php
            if (isset($_COOKIE["user"]))
            {
                $user=@$_COOKIE["user"];
                $psd=@$_COOKIE["pswd"];
            }
            else
            {
                echo("<h1 class='text-center mt-5'><a href='/BMS/'><u>請先登入</u><a/></h1>" );
            }
        ?>
    </div>
</body>
</html>