<?php
include("..\mysql_connect_wr.php");
$ID=$_GET["ID"];
$Battery=$_GET["battery"];

$sql="select user_dev.dev_mac,dev_name,dat_time,field,s_value from esp.user_dev,esp.alert_msg where user_dev.dev_mac=alert_msg.dev_mac and user_dev.dev_mac='".$ID."' and temp01='".$Battery."'and leave_time is null order by i_index desc";

$data=array();

$result = $link->query($sql);
if(!empty($result) && $result->num_rows >0)
{
    while($row=$result->fetch_assoc())
    {
        array_push($data,$row);
    }
    
}

// update alert count
$sql="UPDATE alert_msg SET int_status=1 where dev_mac='".$ID."' and temp01='".$Battery."' ";
$result = $link->query($sql);
//

mysqli_close($link);
echo json_encode($data);
?>