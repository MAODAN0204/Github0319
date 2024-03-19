<?php
include("..\mysql_connect_wr.php");


$dev=strtoupper($_GET["dev"]);
//檢查是否有這MAC
$sql="select count(*) from esp.dev_data where dev_mac='".$dev."';";
$data="0";

$result = $link->query($sql);
if(!empty($result) && $result->num_rows > 0){
    while($row=$result->fetch_row())
    {
        $data = "1";
    }
}

//檢查MAC有沒有被別人註冊過
$sql="select count(*) from esp.user_dev where dev_mac='".$dev."' and leave_time is null;";

$result = $link->query($sql);
if(!empty($result) && $result->num_rows > 0){
    while($row=$result->fetch_row())
    {
        $data = $data."1";
    }
}

//$data若為1位->0 表示 MAC不存在，也沒人註冊, 若為2位, 則有1X, 10表示沒註冊, 11以上表示有註冊
mysqli_close($link);
echo json_encode($data);

?>