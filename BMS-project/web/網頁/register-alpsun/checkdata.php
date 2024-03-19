<?php

    include("..\mysql_connect_wr.php");

    $id=$_GET["User"];
    $email=$_GET["email"];

    $sql ="select count(*) as count from esp.member where username='".$id."' and e_mail='".$email."'";
    $result = $link->query($sql);
    $data=0;

    if(!empty($result) && $result->num_rows > 0){
    	while($row=$result->fetch_assoc())
	{
	        $data =$row["count"];
	}
    }

    mysqli_close($link);
    echo json_encode($data);
?>

