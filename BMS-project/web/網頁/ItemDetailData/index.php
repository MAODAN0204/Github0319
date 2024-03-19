<?php
$Mac=$_GET["Mac"];
$i_type=$_GET["itype"];
//$d_date=$_GET["dDate"];

include("..\mysql_connect_wr.php");


$sql="select dev_mac,i_type,addr,temp01,temp02,temp03,temp04,temp05,temp06,temp07,temp08,temp09,temp10 from esp.dev_data where dev_mac='".$Mac."' group by dev_mac,i_type,addr,temp01,temp02,temp03,temp04,temp05,temp06,temp07,temp08,temp09,temp10;";
//
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