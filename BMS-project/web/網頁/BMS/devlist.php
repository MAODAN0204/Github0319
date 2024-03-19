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
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.1/themes/ui-lightness/jquery-ui.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-ui-timepicker-addon/1.6.3/jquery-ui-timepicker-addon.min.css" />
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-ui-timepicker-addon/1.6.3/jquery-ui-timepicker-addon.min.js"></script>

    <script src="js/home.js"></script>
    <script src="js/devlist.js"></script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-6"><img class="img52" src="images/wyer_logo.png" /></div>
            <div class="col-6 text-right">Date：<span id="showdate">123</span></div>
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
        <div class="row ">
            <div class="col-12 mt-5 mb-2"><h1 class="text-dark">設備查詢</h1></div>
        </div>
        <div class="jumbotron">
            <div class="row">
                <div class="col-12">
                    <div class="row mt-5 text-dark"> 
                        <div class="col-4 text-right"><h5>MAC No.</h5></div>
                        <div class="col-5">
                            <select id="devno" name="btselect" class=" form-control">
                            <?php
                                include("../mysql_connect_wr.php");
                                $sql="select dev_mac from esp.dev_data group by dev_mac"; 
                            
                                $result = $link->query($sql);
                                if(!empty($result) && $result->num_rows >0)
                                {
                                    while($row=$result->fetch_assoc())
                                    { 
                                        echo("<option value=".$row["dev_mac"].">".$row["dev_mac"]."</option>");
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
                    <div class="row mt-5 mb-3">
                        <div class="col-9 text-right"><button id="devdata" class="btn btn-lg btn-dark">查詢</button></div>
                        <div class="col-3"></div>
                    </div>
                </div>
                <div class="col-12"><h6 class="text-primary text-center">如果要看更詳細的資料, 請到接收分析中查詢電池代號資料</h6></div>
            </div>
        </div>

        <div id="spinner" class="text-center">
            <div class="spinner-border text-warning"></div>
        </div>
	    <div class="row" id="searcharea">
	    <div class="col-12 relative-overflow">
            <h2>設備資料</h2>
            <table class="table table-bordered table-striped">
	            <thead>
                  <tr>
                    <th>MAC</th>
                    <th>型態</th>
                    <th>Battery no.</th>
                    <th>Serial Number</th>
                    <th>Manufacturer Name</th>
                    <th>Manufacturer Date</th>
                    <th>Serial Number</th>
                    <th>備用01</th>
                    <th>備用02</th>
                    <th>備用03</th>
                    <th>備用04</th>
                    <th>備用05</th>
                  </tr>
	            </thead>
                <tbody id="tablebody">
                </tbody>
                </table>
	        </div>
	    </div>
    </div>
</body>