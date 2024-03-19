<?php

$startDate=$_POST["startDT"];
$endDate=$_POST["endDT"];
$btno=$_POST["battery"];
$filename=$btno."-".date("Ymd").'.txt';
$t_year=date("Y");

$DOCUMENT_ROOT = $_SERVER [ 'DOCUMENT_ROOT' ];
$fp=fopen($DOCUMENT_ROOT."/BMS/txt/".$filename,"w");

$data="MAC,Date Time,Battery no.,,,,Voltage,Current,Remaining Capacity,FullCharge Capacity,Cycle Count,".
"State of Health,Relative State of Charge,BMS operation Status,BMS Safety Status,Led Error0,PT_CTL0-1,PT_CTL2,Reserve,Reserve,Reserve,".
"Reserve,Cell Voltage1,Cell Voltage2,Cell Voltage3,Cell Voltage4,Cell Voltage5,Cell Voltage6,Cell Voltage7,".
"Cell Voltage8,Cell Voltage9,Cell Voltage10,Cell Voltage11,Cell Voltage12,Cell Voltage13,Cell Voltage14,Cell Voltage15,".
"Cell Voltage16,Temperature1,Temperature2,Temperature3,Temperature4,Temperature5,Temperature6,Temperature7,".
"Temperature8,Temperature9,Temperature10,Temperature11,Temperature12,Temperature13,Temperature14,Temperature15,".
"Temperature16,Temperature17,Temperature18,Temperature19,Temperature20,Temperature21,MaxError,PendingEDV,".
"ExAveCellVoltage,BatteryStatus,Operation Status0,Operation Status1,Safety Status0,Safety Status1,Gauging Status,Reserve,Reserve,Serial Number_1,".
"Serial Number_2,Serial Number_3,Manufacturer Name1,Manufacturer Name2,Manufacturer Name3,Manufacturer Date,Serial Number,Device Chemistry1,".
"BMS_FW Ver1,BMS_FW Ver2,BMS_FW Date\r\n";

fwrite($fp,$data);

include("../mysql_connect_wr.php");

if($t_year=="2023"){
	$sql="select * from esp.rec_data";
}else{
	$sql="select * from a".$t_year.".rec_".$btno;
}
if($btno==""){
	$sql=$sql." where DATE_FORMAT(dat_time,'%Y-%m-%d %H')>='".$startDate."' and DATE_FORMAT(dat_time,'%Y-%m-%d %H') <='".$endDate."' order by i_index asc"; 
}else{
	$sql=$sql." where DATE_FORMAT(dat_time,'%Y-%m-%d %H')>='".$startDate."' and DATE_FORMAT(dat_time,'%Y-%m-%d %H') <='".$endDate."' and A001='".$btno."' order by i_index asc"; 
}
set_time_limit(300);    //延長查詢時間300秒
$result = $link->query($sql);

if(!empty($result) && $result->num_rows >0)
{
    
    while($row=$result->fetch_assoc())
    {
        $data=$row["dev_mac"].",".$row["DAT_TIME"].",".$row["A001"].",".$row["A002"].",".$row["A003"].",".$row["A004"].",".
        $row["A005"].",".$row["A006"].",".$row["A007"].",".$row["A008"].",".$row["A009"].",".$row["A010"].",".
        $row["A011"].",".$row["A012"].",".$row["A013"].",".$row["A014"].",".$row["A015"].",".$row["A016"].",".$row["A017"].",".$row["A018"].",".$row["A019"].",".$row["A020"].",".
        $row["A021"].",".$row["A022"].",".$row["A023"].",".$row["A024"].",".$row["A025"].",".$row["A026"].",".$row["A027"].",".$row["A028"].",".$row["A029"].",".$row["A030"].",".
        $row["A031"].",".$row["A032"].",".$row["A033"].",".$row["A034"].",".$row["A035"].",".$row["A036"].",".$row["A037"].",".$row["A038"].",".$row["A039"].",".$row["A040"].",".
        $row["A041"].",".$row["A042"].",".$row["A043"].",".$row["A044"].",".$row["A045"].",".$row["A046"].",".$row["A047"].",".$row["A048"].",".$row["A049"].",".$row["A050"].",".
        $row["A051"].",".$row["A052"].",".$row["A053"].",".$row["A054"].",".$row["A055"].",".$row["A056"].",".$row["A057"].",".$row["A058"].",".$row["A059"].",".$row["A060"].",".
        $row["A061"].",".$row["A062"].",".$row["A063"].",".$row["A064"].",".$row["A065"].",".$row["A066"].",".$row["A067"].",".$row["A068"].",".$row["A069"].",".$row["A070"].",".
        $row["A071"].",".$row["A072"].",".$row["A073"].",".$row["A074"].",".$row["A075"].",".$row["A076"].",".$row["A077"].",".$row["A078"].",".$row["A079"].",".$row["A080"]."\r\n";
        fwrite($fp,$data);
    }
}
else{
echo("no data");
}

fclose($fp);
mysqli_close($link);
echo(json_decode($DOCUMENT_ROOT."/BMS/txt/".$filename));
?>