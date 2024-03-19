<?php
set_time_limit(0);

$Mac=$_GET["Mac"];
$battery=$_GET["battery"];
$i_kind=$_GET["itype"];

include("..\mysql_connect_wr.php");

$s_field="";
$sql="";
switch ($i_kind)
{
	case "today":
	    $tablestr="";
	    if(date('Y')=="2023")
		$tablestr="esp.rec_data";
	    else
		$tablestr="a".date('Y').".rec_".$battery;
	    for($i=0;$i<23;$i++)
	    {
		$sql=$sql."select IFNULL(round(avg(A002),2),0) a001,IFNULL(round(avg(A005),2),0)a002,IFNULL(round(avg(A006),0),0)a003,IFNULL(round(avg(A011),0),0)a004,IFNULL(round(avg(A037),0),0)a005,";
		$sql=$sql."IFNULL(round(avg(A038),0),0)a006,IFNULL(round(avg(A039),0),0)a007,IFNULL(round(avg(A040),0),0)a008,IFNULL(round(avg(A041),0),0)a009,IFNULL(round(avg(A042),0),0)a010";
		$sql=$sql." from ".$tablestr." where dev_mac='".$Mac."' and A001='".$battery."' and h_today=date_format(now(),'%Y-%m-%d ".$i."') UNION all ";
	    }

	    $sql=$sql."select IFNULL(round(avg(A002),2),0) A001,IFNULL(round(avg(A005),2),0)a002,IFNULL(round(avg(A006),0),0)a003,IFNULL(round(avg(A011),0),0)a004,IFNULL(round(avg(A037),0),0)a005,";
		$sql=$sql."IFNULL(round(avg(A038),0),0)a006,IFNULL(round(avg(A039),0),0)a007,IFNULL(round(avg(A040),0),0)a008,IFNULL(round(avg(A041),0),0)a009,IFNULL(round(avg(A042),0),0)a010";
	    $sql=$sql." from ".$tablestr." where dev_mac='".$Mac."' and A001='".$battery."' and h_today=date_format(now(),'%Y-%m-%d 23') ";

	//a001->,a002->voltage,a003->current,a004->RelativeStateOfCharge(Battery Capacity),a005->temperature1,a006->temperature2, a007->temperature3, a008->temperature4, a009->temperature5, a010->temperature6

	break;
	case "week":
	    $sql="";
	    $day_of_the_week = date('w', strtotime(date('Y-m-d')));
	    $week_fday = date("Y-m-d", strtotime(date('Y-m-d')." -".$day_of_the_week." days"));

	    for($i=0;$i<6;$i++)
	    {
	    	$sql=$sql." select IFNULL(avg(A001),0) as A001,IFNULL(avg(A004),0) as A002,IFNULL(avg(A005),0) as A003,IFNULL(avg(A008),0) as A004,IFNULL(avg(A010),0) as A005,IFNULL(avg(A011),0) as A006,";
		$sql=$sql." IFNULL(avg(A012),0) as A007,IFNULL(avg(A013),0) as A008,IFNULL(avg(A014),0) as A009,IFNULL(avg(A015),0) as A010 from a".date('Y').".day_".$battery;	
	    	$sql=$sql." where Dat_time = '".date("Y-m-d", strtotime("$week_fday +".$i." days"))."'  UNION all " ;
	    }
	    $sql=$sql." select IFNULL(avg(A001),0) as A001,IFNULL(avg(A004),0) as A002,IFNULL(avg(A005),0) as A003,IFNULL(avg(A008),0) as A004,IFNULL(avg(A010),0) as A005,IFNULL(avg(A011),0) as A006,";
	    $sql=$sql." IFNULL(avg(A012),0) as A007,IFNULL(avg(A013),0) as A008,IFNULL(avg(A014),0) as A009,IFNULL(avg(A015),0) as A010 from a".date('Y').".day_".$battery;
	    $sql=$sql." where Dat_time = '".date("Y-m-d", strtotime("$week_fday +6 days"))."'" ;
		//A002->,A004->voltage,A005->current,A008->RelativeStateOfCharge(Battery Capacity),A010->temp1,A011->temp2,A012->temp3,A013->temp4,A014->temp5,A015->temp6
	break;
	case "month":
	    $sql="";
	    $first_day=date('Y-m-01', strtotime(date("Y-m-d")));
	    $Last_day=date('Y-m-t', strtotime(date("Y-m-d")));
	    
	    for($i=0;$i<31;$i++)
	    {
		$checkdate=date('Y-m-d',strtotime($i.'days',strtotime($first_day)));
		if($checkdate==$Last_day)
		    break;
	        $sql=$sql."select IFNULL(round(avg(A002),2),0) as A001,IFNULL(round(avg(A004),2),0) as A002,IFNULL(round(avg(A005),2),0) as A003,IFNULL(round(avg(A008),2),0) as A004,IFNULL(round(avg(A010),2),0) as A005,";
		$sql=$sql." IFNULL(round(avg(A011),2),0) as A006,IFNULL(round(avg(A012),2),0) as A007,IFNULL(round(avg(A013),2),0) as A008,IFNULL(round(avg(A014),2),0) as A009,IFNULL(round(avg(A015),2),0) as A010";
		$sql=$sql." from a".date('Y').".day_".$battery." where (Dat_Time) = '".$checkdate."' UNION all ";
	    }
	    $sql=$sql."select IFNULL(round(avg(A002),2),0) as A001,IFNULL(round(avg(A004),2),0) as A002,IFNULL(round(avg(A005),2),0) as A003,IFNULL(round(avg(A008),2),0) as A004,IFNULL(round(avg(A010),2),0) as A005,";
	    $sql=$sql." IFNULL(round(avg(A011),2),0) as A006,IFNULL(round(avg(A012),2),0) as A007,IFNULL(round(avg(A013),2),0) as A008,IFNULL(round(avg(A014),2),0) as A009,IFNULL(round(avg(A015),2),0) as A010";
	    $sql=$sql." from a".date('Y').".day_".$battery." where (Dat_Time) = '".$Last_day."'";
	    
	break;
	case "year":
	    $sql="";
	    $first_month=date('Y-01-d', strtotime(date("Y-m-d")));
	    $Last_month=date('Y-12', strtotime(date("Y-m-d")));
	    
	    for($i=0;$i<11;$i++)
	    {
		$checkdate=date('Y-m',strtotime($i.' month',strtotime($first_month)));
	        $sql=$sql."select IFNULL(round(avg(A002),2),0) as A001,IFNULL(round(avg(A004),2),0) as A002,IFNULL(round(avg(A005),2),0) as A003,IFNULL(round(avg(A008),2),0) as A004,IFNULL(round(avg(A010),2),0) as A005,";
		$sql=$sql." IFNULL(round(avg(A011),2),0) as A006,IFNULL(round(avg(A012),2),0) as A007,IFNULL(round(avg(A013),2),0) as A008,IFNULL(round(avg(A014),2),0) as A009,IFNULL(round(avg(A015),2),0) as A010";
		$sql=$sql." from a".date('Y').".day_".$battery." where (Dat_Time) like '".$checkdate."%' UNION all ";
	    }
	    $sql=$sql."select IFNULL(round(avg(A002),2),0) as A001,IFNULL(round(avg(A004),2),0) as A002,IFNULL(round(avg(A005),2),0) as A003,IFNULL(round(avg(A008),2),0) as A004,IFNULL(round(avg(A010),2),0) as A005,";
	    $sql=$sql." IFNULL(round(avg(A011),2),0) as A006,IFNULL(round(avg(A012),2),0) as A007,IFNULL(round(avg(A013),2),0) as A008,IFNULL(round(avg(A014),2),0) as A009,IFNULL(round(avg(A015),2),0) as A010";
	    $sql=$sql." from a".date('Y').".day_".$battery." where (Dat_Time) like '".$Last_month."%'";

	break;
	default:
	    $sql="";
	    $tablestr="";
	    if(date('Y')=="2023")
		$tablestr="esp.rec_data";
	    else
		$tablestr="a".date('Y').".rec_".$battery;
	    for($i=0;$i<23;$i++)
	    {
		$sql=$sql."select IFNULL(round(avg(A002),2),0) a001,IFNULL(round(avg(A005),2),0)a002,IFNULL(round(avg(A006),0),0)a003,IFNULL(round(avg(A009),0),0)a004,IFNULL(round(avg(A037),0),0)a005,";
		$sql=$sql."IFNULL(round(avg(A038),0),0)a006,IFNULL(round(avg(A039),0),0)a007,IFNULL(round(avg(A040),0),0)a008,IFNULL(round(avg(A041),0),0)a009,IFNULL(round(avg(A042),0),0)a010";
		$sql=$sql." from ".$tablestr." where dev_mac='".$Mac."' and A001='".$battery."' and h_today=date_format('".$i_kind."','%Y-%m-%d ".$i."') UNION all ";
	    }

	    $sql=$sql."select IFNULL(round(avg(A002),2),0) A001,IFNULL(round(avg(A005),2),0)a002,IFNULL(round(avg(A006),0),0)a003,IFNULL(round(avg(A009),0),0)a004,IFNULL(round(avg(A037),0),0)a005,";
		$sql=$sql."IFNULL(round(avg(A038),0),0)a006,IFNULL(round(avg(A039),0),0)a007,IFNULL(round(avg(A040),0),0)a008,IFNULL(round(avg(A041),0),0)a009,IFNULL(round(avg(A042),0),0)a010";
	    $sql=$sql." from ".$tablestr." where dev_mac='".$Mac."' and A001='".$battery."' and h_today=date_format('".$i_kind."','%Y-%m-%d 23') ";
  	    break;
}

$data=array();

//echo($sql);

$result = $link->query($sql);
if(!empty($result) && $result->num_rows >0)
{
    while($row=$result->fetch_assoc())
    {
        array_push($data,$row);
    }
}
mysqli_close($link);
echo json_encode($data);

?>