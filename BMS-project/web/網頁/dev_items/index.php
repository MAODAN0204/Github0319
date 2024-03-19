<?php
include("..\mysql_connect_wr.php");


$memberID=$_GET["ID"];

$sql="select user_dev.dev_mac,dev_name,addr,i_type,remark from esp.user_dev,(select dev_mac,addr,i_type from esp.dev_data)a1";
$sql=$sql." where user_dev.dev_mac=a1.dev_mac and member_id='".$memberID."' and leave_time is null group by user_dev.dev_mac,dev_name,addr,i_type,remark;";

$data=array();
//echo($sql);
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