<?php
include("..\mysql_connect_wr.php");


$name=$_GET["Uname"];
$ps=$_GET["Pswd"];

$sql="select member_id,username,nickname from esp.member where Username='".$name."' and password='".$ps."'";
$data=array();

$result = $link->query($sql);
if(!empty($result) && $result->num_rows > 0){
    while($row=$result->fetch_assoc())
    {
        $data = array( "id"=>$row["member_id"],"usname"=>$row["username"],"nkname"=>$row["nickname"]);
    }
}
mysqli_close($link);
echo json_encode($data);

?>