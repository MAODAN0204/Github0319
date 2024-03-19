<?php
include("..\mysql_connect_wr.php");


$memberID=$_GET["member"];
$dev_mac=$_GET["dev_mac"];
$dev_name=$_GET["dev_name"];
$remark=$_GET["remark"];

$sql="INSERT INTO esp.user_dev ( member_id,dev_mac,dev_name,remark,create_time) VALUES ('".$memberID."','".$dev_mac."','".$dev_name."','".$remark."',now())";
$data="False";

$result = $link->query($sql);
if(!empty($result) && $result===TRUE)
{
    echo json_encode("OK");
}else{
    echo json_encode($result);
}
mysqli_close($link);

?>