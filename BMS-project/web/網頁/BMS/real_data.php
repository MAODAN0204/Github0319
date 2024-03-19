<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html lang="zh-tw" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>即時資料</title>
    <link type="text/css" rel="stylesheet" href="css/bootstrap.min.css">
    <link type="text/css" rel="stylesheet" href="css/sytle.css">

    <script src="js/jquery-3.7.0.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/realpage.js"></script>
</head>
<body>
    <div class="container">
        <H1 class="text-center pt-3 pb-3">即時資料</H1>
        
        <div class="row">
	    <div class="col-3"><span id="second" class="text-muted"></span></div>
	    <div class="col-5"><H6 class="text-center text-info pb-2">每<span class="text-danger"> 5 秒</span>刷新一次, 請注意接收時間(<span class="text-warning">黃色字體</span>), <br>如果沒有訊號, 資料時間將會停留在上次收到的那七筆.</H6></div>
	    <div class="col-4 text-right pb-2"><button id="login" class="btn btn-primary">首頁登入</button></div>
            <div class="col-12">
                <div class="board">
                    <?php 
                    include("mysql_connect_wr.php");
 		    $t_year=date("Y");
		    if($t_year=="2023")
                        $sql="select Dat_time,A001,A002,A003,B001,B002 from esp.org_data order by i_index desc limit 7"; 
		    else
                        $sql="select Dat_time,A001,A002,A003,B001,B002 from a".$t_year.".org_data order by i_index desc limit 7"; 
                     $result = $link->query($sql);
                    if(!empty($result) && $result->num_rows >0)
                    {
                        while($row=$result->fetch_assoc())
                        {
			
                            echo("<p><span class='text-warning'>".$row["Dat_time"]."</span> - ".$row["A001"]." - ".$row["A002"]." - ".$row["A003"]." - ".$row["B001"]."- <small class='text-light'>".$row["B002"]."</small></p>");
                        }
                    }
                    mysqli_close($link);
                    ?>
                    
                </div>
            </div>
        </div>
        <div class="row mt-5">
            <div class="col-12 text-center"><button id="reflash" class="btn btn-danger">更新</button></div>
        </div>
    </div>
    

</body>
</html>