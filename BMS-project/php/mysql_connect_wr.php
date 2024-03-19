<?php

$link = @mysqli_connect('localhost','root','16450558','test_data');

if(!$link){
    echo "connect falited!";
    exit();
}else{
    echo "connected!";
}
?>