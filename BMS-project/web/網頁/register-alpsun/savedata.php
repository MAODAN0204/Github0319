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
        mysqli_close($link);
        require ('../PHPMailer/src/Exception.php');
        require ('../PHPMailer/src/PHPMailer.php');
        require ('../PHPMailer/src/SMTP.php');
        
        $mail = new PHPMailer\PHPMailer\PHPMailer();
        
        try {
            //Server settings
            
            $mail->IsSMTP();                                            //Send using SMTP
            $mail->CharSet="UTF-8";
            $mail->Host       = 'ms.mailcloud.com.tw';                  //Set the SMTP server to send through
            $mail->SMTPDebug =1;					//Enable verbose debug output
            $mail->SMTPAuth   = true;                                   //Enable SMTP authentication
            $mail->Username   = 'service.adm@neotec.com.tw';            //SMTP username
            $mail->Password   = 'he7KJR88LriFYGe';                      //SMTP password
            $mail->SMTPSecure = 'ssl';            			//Enable implicit TLS encryption
            $mail->Port       = 465;                                    //TCP port to connect to; use 587 if you have set `SMTPSecure = PHPMailer::ENCRYPTION_STARTTLS`
        
            //Recipients
            $mail->setFrom('noreply@wyer.com.tw', 'Mailer');
            $mail->addAddress($email, 'Joe User');     //Add a recipient
            //$mail->addAddress('ellen@example.com');               //Name is optional
            //$mail->addReplyTo('info@example.com', 'Information');
            //$mail->addCC('cc@example.com');
            //$mail->addBCC('bcc@example.com');
        
            //Attachments
            //$mail->addAttachment('/var/tmp/file.tar.gz');         //Add attachments
            //$mail->addAttachment('/tmp/image.jpg', 'new.jpg');    //Optional name
        
            //Content
            $mail->IsHTML(true);                                  //Set email format to HTML
            $mail->Subject = 'Register App Confirmatioin!';
                $body='<div style="font-family:sans-serif;color:#636161;width:100%;padding-right:15px;padding-left:15px;margin-right:auto;margin-left:auto;"><h1 style="color:#073e59;">Thank you for your registration.</h1><hr style="margin-top:3em"><h4>Hi member,</h4><h4>You have successfully registered for our APP.</h4>';
                $body=$body.'<h4 style="margin-top:1em;">We are happy to you join to us and using this APP.</h4><hr><p style="margin-top:5em"> If you have not done so , please contact <a href="http://www.wyer.com.tw/en/" >Us</a> , we will disable this account.</p><p style="padding-top:2em;">Sincerely,</p><h4>The WYER Team </h4>';
            $mail->Body    = $body;
            $mail->AltBody = 'This is the body in plain text for non-HTML mail clients';
        
            if($mail->send())
                echo("OK");
        }catch (Exception $e) {
            echo ("Message could not be sent. Mailer Error: {$mail->ErrorInfo}");
        }

    }else{
            header("Location: https://118.163.50.93/register-alpsun/save_complete.php?msg=email had been register");
    }

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

