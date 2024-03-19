<?php
$field=$_GET["field"];

include("..\mysql_connect_wr.php");

$sql="select sql_field from esp.dev_field where dev_field='".$field."'";


$data="";

$result = $link->query($sql);
if(!empty($result) && $result->num_rows >0)
{
    while($row=$result->fetch_assoc())
    {
        $data=$row["sql_field"];
    }
}

mysqli_close($link);
echo json_encode($data);

?>