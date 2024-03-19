<?php

  include("..\mysql_connect_wr.php");

  $id=$_POST["email"];

$sql="select member_id from esp.member where e_mail='".$id."'";
$data="";
$result = $link->query($sql);
if(!empty($result) && $result->num_rows > 0){
    while($row=$result->fetch_assoc())
    {
        $data = $row["member_id"];
    }
}

if($data!="")	
{
  $member=base64_encode($data);
  $email=base64_encode($id);						//encode memberid to send mail
  //use PHPMailer\src\PHPMailer;
  //use PHPMailer\src\Exception;

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
    $mail->addAddress($id, 'Joe User');     //Add a recipient
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
        $body='<div style="font-family:sans-serif;color:#636161;width:100%;padding-right:15px;padding-left:15px;margin-right:auto;margin-left:auto;"><h1 style="color:#073e59;">Reset Your Password</h1><hr style="margin-top:3em"><h4>Hi member,</h4><h4>Please use the below link to reset your password .</h4>';
        $body=$body.'<p> <a href="https://118.163.50.93/change_password/?mail='.$email.'&id='.$member.'" > http://www.wyer.com.tw/change_password/</a></p><h4 style="margin-top:1em;">';
        $body=$body.'After renew your password , please using new password to login app.</h4><hr><p style="margin-top:5em">If you have problems resetting your password, please contact <a href="http://www.wyer.com.tw/en/" >wyer service</a></p><p style="padding-top:2em;">Sincerely,</p><h4>The WYER Team </h4>';
    $mail->Body    = $body;
    $mail->AltBody = 'This is the body in plain text for non-HTML mail clients';

    if($mail->send())
        header("Location: https://118.163.50.93/forget-alpsun/complete.php?msg=Send Completed");
	
    else 
	
        header("Location: https://118.163.50.93/forget-alpsun/complete.php?msg=No Send");
}
catch (Exception $e) {
    echo ("Message could not be sent. Mailer Error: {$mail->ErrorInfo}");
}
}else{
    header("Location: https://118.163.50.93/forget-alpsun/complete.php?msg=No this mail address");
}
?>

