<?php
include("..\mysql_connect_wr.php");

$MAC=$_GET["MAC"];
$ID=$_GET["ID"];


$sql="select count(*) i_count from esp.alert_msg where dev_mac='".$MAC."' and temp01='".$ID."' and int_status=0";

$data=0;

$result = $link->query($sql);
if(!empty($result) && $result->num_rows >0)
{
    $i=0;
    while($row=$result->fetch_assoc())
    {
        $data= $row["i_count"];
        $i++;
    }
    
}
mysqli_close($link);
echo json_encode($data);
?>