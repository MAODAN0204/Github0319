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
	$id_len = 10;	//�r�����
	$id = '';
	$word = 'abcdefghijkmnpqrstuvwxyz23456789';	//�r���� �A�i�H�N �Ʀr 0 1 �Φr�� O L �ư�
	$len = strlen($word);	//���o�r���ɪ���

	for($i = 0; $i < $id_len; $i++){ 	//�`�@�� �X��
	    $id .= $word[rand() % $len];		//�H�����o�@�Ӧr��
	}
	return $id;					//�^�ǶüƱb��
    }
?>

