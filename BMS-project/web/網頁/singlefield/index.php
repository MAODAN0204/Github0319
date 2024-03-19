<?php
$Mac=$_GET["Mac"];
$battery=$_GET["battery"];
$i_kind=$_GET["rang"];
$i_type=$_GET["itype"];
$field=$_GET["field"];

include("..\mysql_connect_wr.php");

$s_field="";

$sql="";

switch ($i_kind)
{
	case "month":
	    $sql="select d_week as dat_time,";
	break;
	case "year":
	    $sql="select d_month as dat_time,";
	break;
}

/*
if($i_type=="1")
{
    $sql=$sql." avg(".$field.") as field,round(avg(A002),0)A002,round(avg(A003),0)A003,round(avg(A004),0)A004,round(avg(A005),2)A005,round(avg(A006),2)A006,round(avg(A037),2)A037,round(avg(A038),2)A038,round(avg(A039),2)A039,round(avg(A040),2)A040 from rec_data use index(dat_time) where rec_data.dev_mac='".$Mac."' ";
}else{
    $sql=$sql." avg(".$field.") as field,round(avg(A002),0)A002,round(avg(A003),0)A003,round(avg(A004),0)A004,round(avg(A005),2)A005,round(sum(A006),2)A006,round(avg(A037),2)A037,round(avg(A038),2)A038,round(avg(A039),2)A039,round(avg(A040),2)A040 from rec_data use index(dat_time) where rec_data.dev_mac='".$Mac."' and A001='".$battery."'";
}
*/
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
		$sql=$sql."select IFNULL(round(avg(A002),2),0) a001,IFNULL(round(avg(A005),2),0)a002,IFNULL(round(avg(A006),0),0)a003,IFNULL(round(avg(A009),0),0)a004,IFNULL(round(avg(A010),0),0)a005,";
		$sql=$sql."IFNULL(round(avg(A011),0),0)a006,IFNULL(round(avg(A013),0),0)a007,IFNULL(round(avg(A037),0),0)a008,IFNULL(round(avg(A038),0),0)a009,IFNULL(round(avg(A039),0),0)a010,IFNULL(round(avg(A040),0),0)a011";
		$sql=$sql.",IFNULL(round(avg(A041),0),0)a012,IFNULL(round(avg(A042),0),0)a013 from ".$tablestr." where dev_mac='".$Mac."' and A001='".$battery."' and h_today=date_format(now(),'%Y-%m-%d ".$i."') UNION all ";
	    }

	    $sql=$sql."select IFNULL(round(avg(A002),2),0) A001,IFNULL(round(avg(A005),2),0)a002,IFNULL(round(avg(A006),0),0)a003,IFNULL(round(avg(A009),0),0)a004,IFNULL(round(avg(A010),0),0)a005,";
	    $sql=$sql."IFNULL(round(avg(A011),0),0)a006,IFNULL(round(avg(A013),0),0)a007,IFNULL(round(avg(A037),0),0)a008,IFNULL(round(avg(A038),0),0)a009,IFNULL(round(avg(A039),0),0)a010,IFNULL(round(avg(A040),0),0)a011";
	    $sql=$sql.",IFNULL(round(avg(A041),0),0)a012,IFNULL(round(avg(A042),0),0)a013 from ".$tablestr." where dev_mac='".$Mac."' and A001='".$battery."' and h_today=date_format(now(),'%Y-%m-%d 23') ";
	    
	//a001->,a002->voltage,a003->current,a004->cyclecount,a005->stateof health,a006->relativestate of charge,a007->bms safetystatus, a008->temperature1,a009->temperature2, a010->temperature3, a011->temperature4

	break;
	case "week":
	    $sql="select A006 as A001,A007 as A002,A008 as A003,A009 as A004,A010 as A005,A011 as A006,A012 as A007,A013 as A008,A014 as A009,A015 as A010 from a".strval(date('Y'.strtotime('year'))).".day_".$battery;	
	    $sql=$sql." where yearweek(Dat_Time) = YEARWEEK(NOW())  ";
	break;
	case "month":
	    $sql=$sql."and d_month=date_format(now(),'%Y-%m') group by d_week ";
	break;
	case "year":
	    $sql=$sql."and d_year=date_format(now(),'%Y') group by d_month ";
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