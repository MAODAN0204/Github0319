<?php

$devno=@$_POST["no"];
$subtype=@$_POST["sub"];
include("../mysql_connect_wr.php");

$sql="select spe_array,tol_length from esp.dev_type where dev_type=".$devno." and sub_type='".$subtype."';"; 

set_time_limit(300);    //延長查詢時間300秒
$result = $link->query($sql);
$data="";
if(!empty($result) && $result->num_rows >0)
{
    while($row=$result->fetch_assoc())
    {
        $data=$row["spe_array"]."#".$row["tol_length"];
    }
}
else{
    $data=$data."NO data";
}
mysqli_close($link);
echo($data);
?>