<?php
include("..\mysql_connect_wr.php");


$Mac=$_GET["Mac"];

$sql="select i_type from esp.dev_data where dev_mac='".$Mac."' limit 1";
$data="";
$result = $link->query($sql);
if(!empty($result) && $result->num_rows >0)
{
	 while($row=$result->fetch_assoc())
    {
        $data=strval ($row["i_type"]);
    }
  
}
mysqli_close($link);
echo ($data);

?>