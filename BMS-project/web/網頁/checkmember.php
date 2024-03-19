<?php
include("mysql_connect_wr.php");

$name=$_POST["Uname"];
$ps=$_POST["Pswd"];

$sql="select id,username,nickname from member where Username='".$name."' and password='".$ps."'";
$data=[];

$result = $link->query($sql);
if($result->num_rows >0){
    while($row=$result->fetch_assoc())
    {
        $data[] = $row["id"];
        $data[] = $row["username"];
        $data[] = $row["nickname"];
    }
}
mysqli_close($link);
echo ($data);

?>