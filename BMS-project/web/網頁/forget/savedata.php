<?php

    include("..\mysql_connect_wr.php");

    //$callback = isset($_GET['callback']) ? trim($_GET['callback']) : ''; //jsonp回调参数，必需
    $user=$_POST["username"];
    $password=$_POST["password"];
    $nickname=$_POST["nickname"];
    $tel=$_POST["telephone"];
    $mobile=$_POST["mobile"];
    $email=$_POST["email"];
    $id=getrand_id();

    //email不可重覆, 名字可以重覆
    $sql ="select count(*) as count from esp.member where e_mail='".$email."'";
    $result = $link->query($sql);
    $data=0;

    if(!empty($result) && $result->num_rows > 0){
    	while($row=$result->fetch_assoc())
	    {
	        $data =$row["count"];
	    }
    }

    if($data==0)
    {
        $sql ="INSERT INTO esp.member (member_id,Username,password,nickname,tel,mobile,e_mail,create_time) VALUES ('".$id."','".$user."','".$password."','".$nickname."','".$tel."','".$mobile."','".$email."',CURRENT_TIMESTAMP)";
        $result = $link->query($sql);

        if(!empty($result) && $result===TRUE)
        {
            header("Location: https://118.163.50.93/register-alpsun/save_complete.php?msg=Register Completed");
        }else{
            header("Location: https://118.163.50.93/register-alpsun/save_complete.php?msg=Save not successful.");
        }
    }
    else{
        header("Location: https://118.163.50.93/register-alpsun/save_complete.php?msg=email had been register");
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

