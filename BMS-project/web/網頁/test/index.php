<?php
//week
$day_of_the_week = date('w', strtotime(date('Y-m-d')))-1;
echo(date('Y-m-d'));
$week_fday = date("Y-m-d", strtotime(date('Y-m-d')." -".$day_of_the_week." days"));
//該週的最後一天
$week_lday = date("Y-m-d", strtotime("$week_fday +6 days"));

//$week_start_day = date('Y-m-d', strtotime((0-$day_of_the_week)."day",strtotime(date('Y-m-d'))));
echo($week_lday);

//$month=$_GET['m'];
//每天by月

/*echo date('Y-m-01', strtotime('-1 month'));
   echo "<br/>";
   echo date('Y-m-t', strtotime('-1 month'));
   echo "<br/>";*/

//$first_day=date('Y-m-01', strtotime('month'));date("Y-m-d")
//$first_day=date('Y-m-01', strtotime(date("Y-m-d")));
//$Last_day=date('Y-m-t', strtotime(date("Y-m-d")));
/*
$first_month=date('Y-01-d', strtotime(date("Y-m-d")));
$Last_month=date('Y-12', strtotime(date("Y-m-d")));
//echo($first_month);
for($s=0;$s<31;$s++)
{
//	if($rday==$Last_day)
//	    break;
	$rday=date('Y-m',strtotime($s.' month',strtotime($first_month)));
	echo($rday);	
	echo "<br/>";
}*/
//echo(strval(date('Y'.strtotime('year'))));


/*for($m=0;$m<7;$m++)
{

	$countday=date('Y-m-d',strtotime($m.'day',strtotime($week_start_day)));
	$sql=$sql." select IFNULL(round(avg(".$field."),2),0) field,IFNULL(round(avg(A002),2),0)a002,IFNULL(round(avg(A003),0),0)a003,IFNULL(round(avg(A004),0),0)a004,IFNULL(round(avg(A005),0),0)a005,";
	$sql=$sql."IFNULL(round(avg(A006),0),0)a006,IFNULL(round(avg(A037),0),0)a037,IFNULL(round(avg(A038),0),0)a038,IFNULL(round(avg(A039),0),0)a039,IFNULL(round(avg(A040),0),0)a040";
	$sql=$sql." from esp.rec_data where dev_mac='".$Mac."' and A001='".$battery."' and h_today like '".$countday."%' UNION all";

}
$countday=date('Y-m-d',strtotime('7day',strtotime($week_start_day)));
	$sql=$sql." select IFNULL(round(avg(".$field."),2),0) field,IFNULL(round(avg(A002),2),0)a002,IFNULL(round(avg(A003),0),0)a003,IFNULL(round(avg(A004),0),0)a004,IFNULL(round(avg(A005),0),0)a005,";
	$sql=$sql."IFNULL(round(avg(A006),0),0)a006,IFNULL(round(avg(A037),0),0)a037,IFNULL(round(avg(A038),0),0)a038,IFNULL(round(avg(A039),0),0)a039,IFNULL(round(avg(A040),0),0)a040";
	$sql=$sql." from esp.rec_data where dev_mac='".$Mac."' and A001='".$battery."' and h_today like '".$countday."%' ";
echo($sql);*/
/*
$sql="";
/*
for($i=0;$i<23;$i++)
{
	$sql=$sql."select IFNULL(round(avg(".$field."),2),0) field,IFNULL(round(avg(A002),2),0)a002,IFNULL(round(avg(A003),0),0)a003,IFNULL(round(avg(A004),0),0)a004,IFNULL(round(avg(A005),0),0)a005,";
	$sql=$sql."IFNULL(round(avg(A006),0),0)a006,IFNULL(round(avg(A037),0),0)a037,IFNULL(round(avg(A038),0),0)a038,IFNULL(round(avg(A039),0),0)a039,IFNULL(round(avg(A040),0),0)a040";
	$sql=$sql." from esp.rec_data where dev_mac='".$Mac."' and A001='".$battery."' and h_today=date_format(now(),'%Y-%m-%d ".$i."') UNION all ";
}

$sql=$sql."select IFNULL(round(avg(".$field."),2),0) field,IFNULL(round(avg(A002),2),0)a002,IFNULL(round(avg(A003),0),0)a003,IFNULL(round(avg(A004),0),0)a004,IFNULL(round(avg(A005),0),0)a005,";
	$sql=$sql."IFNULL(round(avg(A006),0),0)a006,IFNULL(round(avg(A037),0),0)a037,IFNULL(round(avg(A038),0),0)a038,IFNULL(round(avg(A039),0),0)a039,IFNULL(round(avg(A040),0),0)a040";
	$sql=$sql." from esp.rec_data where dev_mac='".$Mac."' and A001='".$battery."' and h_today=date_format(now(),'%Y-%m-%d 23') ";

echo ($sql);
*/
?>