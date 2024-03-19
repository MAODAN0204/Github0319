<?php
set_time_limit(0);

$Mac=$_GET["Mac"];
$battery=$_GET["battery"];
$i_type=$_GET["itype"];

include("..\mysql_connect_wr.php");

$s_field="";

$sql="select dev_mac,dat_time";

if ($i_type== "1"){
    $sql=$sql." from esp.rec_data where rec_data.dev_mac='".$Mac."' and rec_data.A001 In ('Q1','Q6','QFC','WC','RT') order by i_index desc LIMIT 5";
}else{
    $sql=$sql.",A001 as a001,A005 as a002,A006 as a003,A009 as a004,A010 as a005,A011 as a006,A013 as a007,A037 as a008,A038 as a009,A039 as a010,A040 as a011,A041 as a012,A042 as a013 from ";
    if(date('Y')=="2023")
	$sql=$sql."esp.rec_data";
    else
	$sql=$sql."a".date("Y").".rec_".$battery;
    $sql=$sql." where dev_mac='".$Mac."' and A001='".$battery."' order by i_index desc LIMIT 1";
}
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