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
    <script src="js/bootstrap.min.js"></script>

    <script src="js/home.js"></script>
    <script src="js/jquery.cookie.js"></script>
    <script src="js/userdata.js"></script>
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
            <div class="col-6 text-right">Date：<span id="showdate">123</span><?php if($user!=""){ echo("<br> 登入者 : ".$user);echo("<button id='logout' class='btn ml-2 text-danger border'>登出</button> "); }?></div>
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
            if($user=="")
            {
                echo("<h1 class='text-center mt-5'><a href='/BMS/'><u>請先登入</u><a/></h1>" );
            }else{
        ?>
        <div class="row ">
            <div class="col-12 mt-5 mb-2"><h1 class="text-dark">使用者查詢</h1></div>
        </div>
        <div class="jumbotron">
            <div class="row">
                <div class="col-12"><h6 class="text-primary text-center">選擇一項資料輸入查詢 (<mark>會員姓名</mark>查詢可能會有多筆資料, 請注意), 如果輸入多項條件, 以最上方條件為主要查詢條件。</h6></div>
                <div class="col-12">
                    <div class="row mt-2 text-dark"> 
                        <div class="col-4 text-right"><h5>會員編號</h5></div>
                        <div class="col-5">
                            <select id="memno" class=" form-control">
                                <option value=""></option>
                            <?php
                                include("../mysql_connect_wr.php");
                                $sql="select member_id from esp.member order by id asc"; 
                            
                                $result = $link->query($sql);
                                if(!empty($result) && $result->num_rows >0)
                                {
                                    while($row=$result->fetch_assoc())
                                    { 
                                        echo("<option value=".$row["member_id"].">".$row["member_id"]."</option>");
                                    }
                                }
                                mysqli_close($link);
                            ?>
                            </select>
                        </div>
                        <div class="col-3"></div>
                    </div>
                </div>
                <div class="col-12">
                    <div class="row mt-2 text-dark"> 
                        <div class="col-4 text-right"><h5>會員姓名</h5></div>
                        <div class="col-5"><input id="memname" type="text" class="form-control" placeholder="請輸入會員姓名"/></div>
                        <div class="col-3"></div>
                    </div>
                </div>
                <div class="col-12">
                    <div class="row mt-2 text-dark"> 
                        <div class="col-4 text-right"><h5>會員mail</h5></div>
                        <div class="col-5"><input id="email" type="email" class="form-control" placeholder="請輸入 email "/></div>
                        <div class="col-3"></div>
                    </div>
                </div>
                <div class="col-12">
                    <div class="row mt-5 mb-3">
                        <div class="col-9 text-right"><button id="memata" class="btn btn-lg btn-dark">查詢會員資料</button></div>
                        <div class="col-3 " ><button id="devdata" class="btn btn-lg btn-success">查詢設備資料</button></div>
                    </div>
                </div>
            </div>
        </div>
        <div id="spinner" class="text-center">
            <div class="spinner-border text-warning"></div>
        </div>
        <div class="row" id="searcharea">
            <div class="col-12 relative-overflow">
                <h2>查詢資料</h2>
                <table class="table table-bordered table-striped">
                    <thead id="tablehead">
                    </thead>
                    <tbody id="tablebody">
                    </tbody>
                    </table>
                </div>
            </div>
        </div>
        <?php
            }
            ?>
    </div>
</body>
</html>