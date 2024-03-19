<?php

  $link = @mysqli_connect('192.168.1.248','wyerapp','Dscth@2107');
  //$link =mysql_connect('192.168.1.248','wyerapp','Dscth@2107','esp');

  if(!$link){
    echo "connect falited!";
    exit();
  }else{
    //echo "connected!";

  }
?>