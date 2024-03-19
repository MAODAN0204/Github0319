<?php

    include("..\mysql_connect_wr.php");

    $password=$_POST["password1"];
    $id=$_POST["mail"];
    $email=base64_decode($id);

    //email不可重覆, 名字可以重覆
    $sql ="UPDATE esp.member SET member.password='".$password."' where e_mail='".$email."';";
echo("--");
    $result = $link->query($sql);
    if (!$result) die($link->error);

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
        $mail->Subject = 'Password reset confirmation';
            $body='<div style="font-family:sans-serif;color:#636161;width:100%;padding-right:15px;padding-left:15px;margin-right:auto;margin-left:auto;">';
            $body=$body.'<h1 style="color:#073e59;">Password reset confirmation</h1><hr><h4 style="margin-top:3em">Hi member,</h4><h4>The account password has been changed.</h4><h4> If you access to your account did not make this change, please Contact <a href="">Us</a>.</h4>';
            $body=$body.'<h5 style="margin-top:3em;">Sincerely,</h5><h4>The WYER Team </h4><hr></div>';
        $mail->Body    = $body;
        $mail->AltBody = 'This is the body in plain text for non-HTML mail clients';
    	if($mail->send())
            header("Location: https://118.163.50.93/change_password/save_complete.html");
    }
    catch (Exception $e) {
        echo ("Message could not be sent. Mailer Error: {$mail->ErrorInfo}");
    }
?>

