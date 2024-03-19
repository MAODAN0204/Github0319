<?php
    //error_reporting (E_ALL ^ E_NOTICE);
    
    $to ="sales@wyer.com.tw";
    $from = $_POST['email'];
    $name = $_POST['username'];
    $headers = 'From: Your name ' .$from."\r\n";
    $headers .= 'Content-type: text/html; charset=UTF-8\r\n'."Content-Transfer-Encoding:8bit\n\r"; 
   
    $subject ="=?BIG5?B?".base64_encode("Wyer 網站客戶留言")."?=";

    $phone    = $_POST['phone'];
    $product  = $_POST['product'];
    $message  = $_POST['message'];
    
    $str="<div style='width:100%;text-align:center;'><table border=1 style='margin:auto;border-collapse:collapse;font-family:Microsoft JhengHei;font-size:16px'><tr><th colspan='4' style='font-size:1.5em;font-weight:bolder;text-align:center;padding:30px;'>".iconv("big5","UTF-8","禹漢網站客戶留言")."</th></tr><tr><td style='color:white;font-weight:bolder;background:#000;padding:20px 3px;'>".iconv("big5","UTF-8","客戶姓名")."</td><td>".$name."</td><td style='color:white;font-weight:bolder;background:#000;padding:20px 30px;'>E-Mail</td><td>".$from."</td></tr>";
    $str=$str."<tr><td style='color:white;background:#000;padding:20px 30px;'>".iconv("big5","UTF-8","聯絡電話")."</td><td>".$phone."</td><td style='color:white;font-weight:bolder;background:#000;padding:20px 30px;'>".iconv("big5","UTF-8","諮詢產品")."</td><td>".$product."</td></tr><tr><td colspan='4' style='padding:20px 30px;text-align:left;min-height:300px;vertical-align:top;'>".$message."</td></tr></table><p style='text-align:center'>".iconv("big5","UTF-8","資料來自：http://www.wyer.com.tw/contact.html")."</p></div>";
    $body = $str; 
    
    $send = mail($to, $subject, $body, $headers);
    echo 1;

    
?>