<?php
include("..\mysql_connect_wr.php");

$Mac=$_GET["Mac"];
$memberID=$_GET["ID"];

$sql="UPDATE esp.user_dev SET leave_time=now() where dev_mac='".$Mac."' and member_id='".$memberID."'";

if(mysqli_query($link, $sql))
{
	echo json_encode("OK");
}
else
{
	echo json_encode("No");
}
//$result = $link->query($sql);
//echo($sql);

mysqli_close($link);

//echo json_encode("OK");
?>