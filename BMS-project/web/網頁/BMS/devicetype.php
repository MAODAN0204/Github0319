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
    <script src="js/devtype.js"></script>
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
            if ($user=="")
            {
                echo("<h1 class='text-center mt-5'><a href='/BMS/'><u>請先登入</u><a/></h1>" );
            }else{
        ?>
        <div >
            <div class="alert alert-success">
                <strong class="text-danger">註 ! </strong> <strong>子型態-</strong> 只有多類別的資料列才需在子型態列出做判別, 例如 type 1有五項才要分開列，否則子型態不用輸入任何值.
            </div>
            <table class="table table-striped">
                <thead >
                    <tr class="bg-secondary text-white">
                        <th>修改</th>
                        <th>設備型態</th>
                        <th>子型態</th>
                        <th>型態名稱</th>
                        <th>分割欄位值</th>
                        <th>總長度</th>
                    </tr>
                </thead>
                <tbody>
                <?php
                include("../mysql_connect_wr.php");
                $sql="select dev_type,sub_type,tpe_name,spe_array,tol_length from esp.dev_type order by i_index asc"; 
            
                $result = $link->query($sql);
                if(!empty($result) && $result->num_rows >0)
                {
                    $data="";
                    while($row=$result->fetch_assoc())
                    { 
                        $data=$data."<tr class='text-center'><td><input type='radio' name='optradio' value='".$row["dev_type"]."^".$row["sub_type"]."'></label></td><td>".$row["dev_type"]."</td><td>".$row["sub_type"]."</td><td>".$row["tpe_name"]."</td><td class='text-left'>".$row["spe_array"]."</td><td>".$row["tol_length"]."</td></tr>";
                    }
                    echo($data);
                }
                mysqli_close($link);
                ?>
                </tbody>
            </table>
        </div>
        <div class="row" id="func">
            <div class="col-12">
                <button class="btn btn-outline-dark mr-4" id="modify">修改</button>
                <button class="btn btn-outline-danger" id="new">新增</button></div>
        </div>
        <div class="jumbotron" id="Add">
            <div class="row form-group">
                <div class="col-12 text-danger text-right"><h1>新增資料</h1></div>
                <div class="col-2 text-larger text-right pr-2">設備型態</div>
                <div class="col-4"><input type="text" class="form-control" id="dev_type"></div>
                <div class="col-2 text-larger text-right pr-2">子型態</div>
                <div class="col-4"><input type="text" class="form-control" id="sub_type"></div>
            </div>
            <div class="row form-group">
                <div class="col-2 text-larger text-right pr-2">型態名稱</div>
                <div class="col-4"><input type="text" class="form-control" id="tpe_name"></div>
                <div class="col-2 text-larger text-right pr-2">總長度</div>
                <div class="col-4"><input type="text" class="form-control" id="tol_length"></div>
            </div>
            <div class="row form-group">
                <div class="col-2 text-larger text-right pr-2">分割欄位值</div>
                <div class="col-10"><input type="text" class="form-control" id="spe_array"></div>
                <div class="col-12 text-right mt-2"><button class="btn btn-danger mr-3" id="add_save">儲存</button> <button class="btn btn-secondary cancel">取消</button></div>
            </div>
        </div>
        <div  class="jumbotron" id="modi">
            <div class="text-dark text-right"><h1>修改資料</h1></div>
            <div class="form-group">
                <label >分割欄位值:</label>
                <input type="text" class="form-control" id="spe_array_m">
            </div>
            <div class="form-group">
                <label >總長度:</label>
                <input type="text" class="form-control" id="tol_length_m">
                
            </div>
            <div class="form-group text-right">
                <button class="btn btn-dark mr-3" id="modi_save">儲存</button>
                <button class="btn btn-secondary cancel">取消</button>
            </div>
        </div>
        <?php }
        ?>
    </div>
</body>
</html>