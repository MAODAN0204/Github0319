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
    <script src="js/analyze.js"></script>
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
            <div class="col-12 mt-5 mb-2"><h1 class="text-dark">資料匯出/查詢</h1></div>
        </div>
        <div class="jumbotron">
            <div class="row">
                <div class="col-md-6 col-12">
                    <div class="row mt-5 text-dark"> 
                        <div class="col-5 text-right"><h5>開始日期/時間</h5></div>
                        <div class="col-7">
                            <input id="txtStartDate" class=" form-control">
                        </div>
                    </div>
                    <div class="row mt-2 text-dark">    
                        <div class="col-5 text-right"><h5>結束日期/時間</h5></div>
                        <div class="col-7">
                            <input id="txtEndDate" class=" form-control">
                        </div>
                    </div>
                    <div class="row mt-2 text-dark">    
                        <div class="col-5 text-right"><h5>電池代號</h5></div>
                        <div class="col-7 ">
                            <select id="batteryno" name="btselect" class=" form-control">
                            <option value="">All</option>
                            <?php
                                include("../mysql_connect_wr.php");
                                $sql="select addr from esp.dev_data order by i_index asc"; 
                            
                                $result = $link->query($sql);
                                if(!empty($result) && $result->num_rows >0)
                                {
                                    while($row=$result->fetch_assoc())
                                    { 
                                        echo("<option value=".$row["addr"].">".$row["addr"]."</option>");
                                    }
                                }
                                mysqli_close($link);
                            ?>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-12">
                    <div class="row mt-3 mb-5">
                        <div class="col-12 text-danger "><p>1. 如果起始日期大於結束日期, 不運作;   <br>2. 下載完會有下載連結, 請自行點選。(按滑鼠右鍵另存連結檔)</p></div>
                        <div class="col-12 "><button id="expdata" class="btn btn-lg btn-dark">匯出文字檔</button></div>
                    </div>
                    <div class="row mt-3 mb-5">
                        <div class="col-12 text-danger "><p> 搜尋時間最長是300秒, 超過就是逾時, 不會有資料顯示.</p></div>
                        <div class="col-12 "><button id="searchdata" class="btn btn-lg btn-warning">查詢即時資料</button></div>
                    </div>
                </div>
            </div>
        </div>

        <div id="spinner" class="text-center">
                <div class="spinner-border text-warning"></div>
            </div>
        <div id="downarea" class="text-center">
        </div>
	    <div class="row" id="searcharea">
	    <div class="col-12 relative-overflow">
            <h2>即時分析資料</h2>
            <table class="table table-bordered table-striped">
	            <thead>
                  <tr>
                    <th>MAC</th>
                    <th>Date Time</th>
                    <th>Battery no.</th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th>Voltage</th>
                    <th>Current</th>
                    <th>Remaining Capacity</th>
                    <th>FullCharge Capacity</th>
                    <th>Cycle Count</th>
                    <th>State of Health</th>
                    <th>Relative State of Charge</th>
                    <th>BMS operation Status</th>
                    <th>BMS Safety Status</th>
                    <th>Led Error0</th>
                    <th>PT_CTL0-1</th>
                    <th>PT_CTL2</th>
                    <th>Voltage</th>
                    <th>Voltage</th>
                    <th>Voltage</th>
                    <th>Voltage</th>
                    <th>Cell Voltage1</th>
                    <th>Cell Voltage2</th>
                    <th>Cell Voltage3</th>
                    <th>Cell Voltage4</th>
                    <th>Cell Voltage5</th>
                    <th>Cell Voltage6</th>
                    <th>Cell Voltage7</th>
                    <th>Cell Voltage8</th>
                    <th>Cell Voltage9</th>
                    <th>Cell Voltage10</th>
                    <th>Cell Voltage11</th>
                    <th>Cell Voltage12</th>
                    <th>Cell Voltage13</th>
                    <th>Cell Voltage14</th>
                    <th>Cell Voltage15</th>
                    <th>Cell Voltage16</th>
                    <th>Temperature1</th>
                    <th>Temperature2</th>
                    <th>Temperature3</th>
                    <th>Temperature4</th>
                    <th>Temperature5</th>
                    <th>Temperature6</th>
                    <th>Temperature7</th>
                    <th>Temperature8</th>
                    <th>Temperature9</th>
                    <th>Temperature10</th>
                    <th>Temperature11</th>
                    <th>Temperature12</th>
                    <th>Temperature13</th>
                    <th>Temperature14</th>
                    <th>Temperature15</th>
                    <th>Temperature16</th>
                    <th>Temperature17</th>
                    <th>Temperature18</th>
                    <th>Temperature19</th>
                    <th>Temperature20</th>
                    <th>Temperature21</th>
                    <th>MaxError</th>
                    <th>PendingEDV</th>
                    <th>ExAveCellVoltage</th>
                    <th>BatteryStatus</th>
                    <th>Operation Status0</th>
                    <th>Operation Status1</th>
                    <th>Safety Status0</th>
                    <th>Safety Status1</th>
                    <th>Gauging Status</th>
                    <th></th>
                    <th></th>
                    <th>Serial Number_1</th>
                    <th>Serial Number_2</th>
                    <th>Serial Number_3</th>
                    <th>Manufacturer Name1</th>
                    <th>Manufacturer Name2</th>
                    <th>Manufacturer Name3</th>
                    <th>Manufacturer Date</th>
                    <th>Serial Number</th>
                    <th>Device Chemistry1</th>
                    <th>BMS_FW Ver1</th>
                    <th>BMS_FW Ver2</th>
                    <th>BMS_FW Date</th>
                  </tr>
	            </thead>
                <tbody id="tablebody">
                </tbody>
                </table>
	        </div>
	    </div>
    </div>
</body>