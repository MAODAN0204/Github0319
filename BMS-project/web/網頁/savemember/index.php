<?php
include("..\mysql_connect_wr.php");

$id=$_GET["id"];
$Uname=$_GET["Uname"];
$Pswd=$_GET["Pswd"];
$nkname=$_GET["nkname"];
$tel=$_GET["tel"];
$mobile=$_GET["mobile"];
$email=$_GET["email"];


$sql="UPDATE esp.member SET Username='".$Uname."' , member.password='".$Pswd."',nickname='".$nkname."' , tel='".$tel."', mobile='".$mobile."',e_mail='".$email."' WHERE member_id='".$id."';";
$data="OK";

$result = $link->query($sql);
if(!$result){
	$data=$link->error;
}
mysqli_close($link);
echo json_encode($data);

if($data!="OK")
{
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
	    $mail->Subject = 'Reset your password';
	        $body='<div style="font-family:sans-serif;color:#636161;width:100%;padding-right:15px;padding-left:15px;margin-right:auto;margin-left:auto;"><h1 style="color:#073e59;">Personal Data has Changed Confirmation!</h1><hr style="margin-top:3em"><h4>Hi member,</h4><h4> Your personal data has Changed .</h4>';
	        $body=$body.'<h4 style="margin-top:1em;">';
	        $body=$body.'If you did not make this change, please Contact <a href="http://www.wyer.com.tw/en/" >US</a>.</h4><p style="padding-top:2em;">Sincerely,</p><h4>The WYER Team </h4>';
	    $mail->Body    = $body;
	    $mail->AltBody = 'This is the body in plain text for non-HTML mail clients';

	    if($mail->send())
	        echo("OK");
	}
	catch (Exception $e) {
	    echo ("Message could not be sent. Mailer Error: {$mail->ErrorInfo}");
	}
}

?>