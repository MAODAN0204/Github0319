<?php
include("..\mysql_connect_wr.php");


$memberID=$_GET["ID"];

$sql="select user_dev.dev_mac,dev_name,remark from esp.user_dev where member_id='".$memberID."' and leave_time is null";

$data=array();

$result = $link->query($sql);
if(!empty($result) && $result->num_rows >0)
{
    while($row=$result->fetch_assoc())
    {
        array_push($data,$row);
    }
}
mysqli_close($link);
echo json_encode($data);
?>