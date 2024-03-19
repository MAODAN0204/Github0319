
<?php
include(mysql_connect_wr.php);

$sql="select * from wifi_test_db";
$result = $link->query($sql);
$data = array();

if($result){
    while($row=mysqli_fetch_arry($result)){
        $data[] += array('id'=>$row["Create_menber"],'id2'=>$row[""]);
    }
}
mysqli_close($link);

echo jason_encode($data);
?>