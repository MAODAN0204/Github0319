<?php
include("..\mysql_connect_wr.php");

$id=$_GET["id"];


$sql="select * from esp.member where member_id='".$id."';";
$data=array();

$result = $link->query($sql);
if(!empty($result) && $result->num_rows > 0){
    while($row=$result->fetch_assoc())
    {
        $data = array( "id"=>$row["member_id"],"usname"=>$row["Username"],"pswd"=>$row["password"],"nkname"=>$row["nickname"],"tel"=>$row["tel"],"mobile"=>$row["mobile"],"e_mail"=>$row["e_mail"]);
    }
}

mysqli_close($link);
echo json_encode($data);

?>