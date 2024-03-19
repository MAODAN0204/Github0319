<?php

$macno=@$_GET["mac"];

include("../mysql_connect_wr.php");

$sql="select * from esp.dev_data where dev_mac='".$macno."' "; 

set_time_limit(300);    //延長查詢時間300秒
$result = $link->query($sql);
$data="";
if(!empty($result) && $result->num_rows >0)
{
    while($row=$result->fetch_assoc())
    {
        $data=$data."<tr><td>".$row["dev_mac"]."</td><td>".$row["i_type"]."</td><td>".$row["addr"]."</td>";
        $data=$data."<td>".$row["temp01"]."</td><td>".$row["temp02"]."</td><td>".$row["temp03"]."</td>";
        $data=$data."<td>".$row["temp04"]."</td><td>".$row["temp05"]."</td><td>".$row["temp06"]."</td>";
        $data=$data."<td>".$row["temp07"]."</td><td>".$row["temp08"]."</td><td>".$row["temp09"]."</td><td>".$row["temp10"]."</td></tr>";
    }
}
else{
    $data=$data."<tr><td>NO data</td></tr>";
}
mysqli_close($link);
echo($data);
?>