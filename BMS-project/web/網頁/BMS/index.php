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
</head>
<body>
    <?php
    ini_set("display_errors", 0);
    $check=false;  //檢查是否重登
    if (isset($_COOKIE["user"]))
    {
        $user=@$_COOKIE["user"];
        $psd=@$_COOKIE["pswd"];
    }
    else
    {
        $user=@$_POST["username"];
        $psd=@$_POST["psd"];
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
            
            if($user != "")  //如果有值的話才做
            {
                $ldaphost="neotec.nts";
                $ldapconn=ldap_connect($ldaphost);
                if(!$ldapconn)
                    echo "Connect Failure";
                //For simplification,you can wirte $ldapconn = ldap_connect($ldaphost)or die("Could not connect to ".$ldaphost);
                
                //rdn:relative distinguished name
                
                $ldaprdn=$user."@".$ldaphost;
                $ldappass=$psd;
                
                ldap_set_option($ldapconn, LDAP_OPT_PROTOCOL_VERSION, 3);
                ldap_set_option($ldapconn, LDAP_OPT_REFERRALS, 0);
                
                //Reference：http://php.net/manual/en/function.ldap-bind.php
                if ($ldapconn) {
                    // binding to ldap server
                    $ldapbind = ldap_bind($ldapconn, $ldaprdn, $ldappass);
                    // verify binding
                    if ($ldapbind) {
                        echo "<H1>登入成功!</H1>";
                        setcookie("user",$user);    //設定帳號
                        setcookie("pswd",$psd);     //設定密碼
                        $check=true;
                    } else {
                        echo "<H4 class='text-center text-warning'>登入失敗, 請重新登入!</H4>";
                    }
                }
                ldap_close($ldapconn);
            }    
            if($check==false)
            {
        ?>
            <div class="row">
                <div class="col-12 mt-5 mb-3">
                    <h1 class="text-center display-1">Login</h1></div>
            </div>
            <form action="index.php" method="post">
                <div class="row mt-4">
                    <div class="col-md-4 col-2"></div>
                    <div class="col-md-4 col-5"><h3>帳號</h3></div>
                    <div class="col-md-4 col-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-2"></div>
                    <div class="col-md-4 col-8 "><input class="form-control" type="text" name="username" id="username" placeholder="輸入帳號"></div>
                    <div class="col-md-4 col-2"></div>
                </div>
                <div class="row mt-4">
                    <div class="col-md-4 col-2"></div>
                    <div class="col-md-4 col-8"><h3>密碼</h3></div>
                    <div class="col-md-4 col-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-2"></div>
                    <div class="col-md-4 col-8"><input class="form-control" type="password" name="psd" id="psd" placeholder="輸入密碼"></div>
                    <div class="col-md-4 col-2"></div>
                </div>

                <div class="row mt-5 mb-5">
                    <div class="col-6 text-right pr-3"><button type="reset" class="btn btn-lg btn-secondary">清除</button></div>
                    <div class="col-6 text-left pl-5"><button type="submit" class="btn btn-lg btn-dark">登入</button></div>
                </div>
		<div class="row mt-3 mb-5">
		    <div class="col-12"><h6 class="text-info text-center">請使用網域(電腦)的使用者帳號及密碼登入 。(不需輸入網域名稱)</h6></div>
		</div>
            </form>
            <?php
            }
            ?>
    
        </div>
        <div class="mb-5"></div>

</body>
</html>