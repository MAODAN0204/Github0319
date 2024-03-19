<?php

    include("..\mysql_connect_wr.php");

    $user=$_GET["User"];
    $password=$_GET["Pswd"];
    $nickname=$_GET["Nkname"];
    $tel=$_GET["Handphone"];
    $mobile=$_GET["Mobile"];
    $email=$_GET["Email"];
    $id=getrand_id();

    $sql ="INSERT INTO esp.member (member_id,Username,password,nickname,tel,mobile,e_mail,create_time) VALUES ('".$id."','".$user."','".$password."','".$nickname."','".$tel."','".$mobile."','".$email."',CURRENT_TIMESTAMP)";
    $result = $link->query($sql);

    if(!empty($result) && $result===TRUE)
    {
 	echo json_encode("OK");
    }else{
	echo json_encode($result);
    }

    mysqli_close($link);

    function getrand_id(){
	$id_len = 10;	//字串長度
	$id = '';
	$word = 'abcdefghijkmnpqrstuvwxyz23456789';	//字典檔 你可以將 數字 0 1 及字母 O L 排除
	$len = strlen($word);	//取得字典檔長度

	for($i = 0; $i < $id_len; $i++){ 	//總共取 幾次
	    $id .= $word[rand() % $len];		//隨機取得一個字元
	}
	return $id;					//回傳亂數帳號
    }
?>

