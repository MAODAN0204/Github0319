<?php
include("..\mysql_connect_wr.php");


$dev=strtoupper($_GET["dev"]);
//�ˬd�O�_���oMAC
$sql="select count(*) from esp.dev_data where dev_mac='".$dev."';";
$data="0";

$result = $link->query($sql);
if(!empty($result) && $result->num_rows > 0){
    while($row=$result->fetch_row())
    {
        $data = "1";
    }
}

//�ˬdMAC���S���Q�O�H���U�L
$sql="select count(*) from esp.user_dev where dev_mac='".$dev."' and leave_time is null;";

$result = $link->query($sql);
if(!empty($result) && $result->num_rows > 0){
    while($row=$result->fetch_row())
    {
        $data = $data."1";
    }
}

//$data�Y��1��->0 ��� MAC���s�b�A�]�S�H���U, �Y��2��, �h��1X, 10��ܨS���U, 11�H�W��ܦ����U
mysqli_close($link);
echo json_encode($data);

?>