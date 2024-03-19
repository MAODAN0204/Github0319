<?php
include(mysql_connect_wr.php);

$name=$_GET("Uname");
$ps=$_GET("Pswd");

$sql="select id from member where Username='".$name."' and password='".$ps."'";
$data="";

$result = $link->query($sql);
if($result){
    while($row=mysqli_fetch_arry($result)){
        $data = $row["id"];
    }
}
mysqli_close($link);

echo jason_encode($data);
?>