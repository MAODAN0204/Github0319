<?php

$condition=@$_GET["data"];
$type=@$_GET["item"];
$kind=@$_GET["tb"];
$sql="";
include("../mysql_connect_wr.php");
if($kind=="dev"){
    $sql="select user_dev.member_id,username,dev_mac,dev_name,remark,user_dev.create_time,leave_time from esp.user_dev,esp.member where user_dev.member_id=member.member_id  ";
    switch ($type)
    {
        case "1":
            $sql=$sql." and user_dev.member_id='".$condition."' "; 
            break;
        case "2":
            $sql=$sql." and username='".$condition."' "; 
            break;
        default :
            $sql=$sql." and e_mail='".$condition."' ";
            break;
    }
    
}else{
    $sql="select member_id,username,nickname,tel,mobile,e_mail,create_time,text01 from esp.member ";
    switch ($type)
    {
        case "1":
            $sql=$sql." where member_id='".$condition."' "; 
            break;
        case "2":
            $sql=$sql." where username='".$condition."' "; 
            break;
        default :
            $sql=$sql."where e_mail='".$condition."' "; 
            break;
    }
}

set_time_limit(300);    //延長查詢時間300秒
$result = $link->query($sql);
$data="";
if(!empty($result) && $result->num_rows >0)
{
    while($row=$result->fetch_row())
    {
        $data=$data."<tr><td>".$row[0]."</td><td>".$row[1]."</td><td>".$row[2]."</td>";
        $data=$data."<td>".$row[3]."</td><td>".$row[4]."</td><td>".$row[5]."</td>";
        $data=$data."<td>".$row[6]."</td></tr>";
    }
}
else{
    $data=$data."<tr><td>NO data</td></tr>";
}
mysqli_close($link);
echo($data);
?>