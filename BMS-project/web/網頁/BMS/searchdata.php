<?php

$startDate=@$_GET["startDT"];
$endDate=@$_GET["endDT"];
$btno=@$_GET["battery"];
$t_year=date("Y");

include("../mysql_connect_wr.php");
if($t_year=="2023")
	$sql="select * from esp.rec_data";
else
	$sql="select * from a".$t_year.".rec_".$btno;

if($btno!=""&&$startDate!="")
	$sql=$sql." where DATE_FORMAT(dat_time,'%Y-%m-%d %H')>='".$startDate."' and DATE_FORMAT(dat_time,'%Y-%m-%d %H') <='".$endDate."' and A001='".$btno."' order by i_index asc limit 10"; 
elseif($startDate!=""&&$btno=="")
	$sql=$sql." where DATE_FORMAT(dat_time,'%Y-%m-%d %H')>='".$startDate."' and DATE_FORMAT(dat_time,'%Y-%m-%d %H') <='".$endDate."'  order by i_index asc limit 10"; 
elseif($startDate==""&&$btno!="")
    $sql=$sql." where A001='".$btno."'  order by i_index asc limit 10"; 
else
    $sql=$sql." order by i_index asc limit 10"; 

//echo($sql);
set_time_limit(300);    //延長查詢時間300秒
$result = $link->query($sql);
$data="";
if(!empty($result) && $result->num_rows >0)
{
    while($row=$result->fetch_assoc())
    {
        $data=$data."<tr><td>".$row["dev_mac"]."</td><td>".$row["DAT_TIME"]."</td><td>".$row["A001"]."</td><td>".$row["A002"]."</td><td>".$row["A003"]."</td><td>".$row["A004"]."</td>";
        $data=$data."<td>".$row["A005"]."</td><td>".$row["A006"]."</td><td>".$row["A007"]."</td><td>".$row["A008"]."</td><td>".$row["A009"]."</td><td>".$row["A010"]."</td>";
        $data=$data."<td>".$row["A011"]."</td><td>".$row["A012"]."</td><td>".$row["A013"]."</td><td>".$row["A014"]."</td><td>".$row["A015"]."</td><td>".$row["A016"]."</td><td>".$row["A017"]."</td><td>".$row["A018"]."</td><td>".$row["A019"]."</td><td>".$row["A020"]."</td>";
        $data=$data."<td>".$row["A021"]."</td><td>".$row["A022"]."</td><td>".$row["A023"]."</td><td>".$row["A024"]."</td><td>".$row["A025"]."</td><td>".$row["A026"]."</td><td>".$row["A027"]."</td><td>".$row["A028"]."</td><td>".$row["A029"]."</td><td>".$row["A030"]."</td>";
        $data=$data."<td>".$row["A031"]."</td><td>".$row["A032"]."</td><td>".$row["A033"]."</td><td>".$row["A034"]."</td><td>".$row["A035"]."</td><td>".$row["A036"]."</td><td>".$row["A057"]."</td><td>".$row["A038"]."</td><td>".$row["A039"]."</td><td>".$row["A040"]."</td>";
        $data=$data."<td>".$row["A041"]."</td><td>".$row["A042"]."</td><td>".$row["A043"]."</td><td>".$row["A044"]."</td><td>".$row["A045"]."</td><td>".$row["A046"]."</td><td>".$row["A047"]."</td><td>".$row["A048"]."</td><td>".$row["A049"]."</td><td>".$row["A050"]."</td>";
        $data=$data."<td>".$row["A051"]."</td><td>".$row["A052"]."</td><td>".$row["A053"]."</td><td>".$row["A054"]."</td><td>".$row["A055"]."</td><td>".$row["A056"]."</td><td>".$row["A097"]."</td><td>".$row["A058"]."</td><td>".$row["A059"]."</td><td>".$row["A060"]."</td>";
        $data=$data."<td>".$row["A061"]."</td><td>".$row["A062"]."</td><td>".$row["A063"]."</td><td>".$row["A064"]."</td><td>".$row["A065"]."</td><td>".$row["A066"]."</td><td>".$row["A087"]."</td><td>".$row["A068"]."</td><td>".$row["A069"]."</td><td>".$row["A070"]."</td>";
        $data=$data."<td>".$row["A071"]."</td><td>".$row["A072"]."</td><td>".$row["A073"]."</td><td>".$row["A074"]."</td><td>".$row["A075"]."</td><td>".$row["A076"]."</td><td>".$row["A077"]."</td><td>".$row["A078"]."</td><td>".$row["A079"]."</td><td>".$row["A080"]."</td></tr>";
    }
}
else{
    $data=$data."<tr><td>NO data</td></tr>";
}
mysqli_close($link);
echo($data);
?>