<?php

$devtype=@$_POST["dev"];
$subtype=@$_POST["sub"];    //可充空白
$tpename=@$_POST["tpe"];
$spearray=@$_POST["spe"];
$tollen=@$_POST["tol"];

include("../mysql_connect_wr.php");
if($tpename!="")    //new
{
    $sql="Insert into esp.dev_type (dev_type,sub_type,tpe_name,spe_array,tol_length) values (".$devtype.",'".$subtype."','".$tpename."','".$spearray."',".$tollen.");";
}else{              //modify
    $sql="UPDATE esp.dev_type SET spe_array='".$spearray."' , tol_length=".$tollen." where dev_type='".$devtype."' and sub_type='".$subtype."';";
}

set_time_limit(300);    //延長查詢時間300秒
$result = $link->query($sql);
if($result==false){
    echo($link->error);
}
mysqli_close($link);
?>