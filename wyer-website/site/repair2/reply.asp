<!-- #include virtual="/pwd/pwd.asp" -->
<%
   session("wolf")="repair"
If InStr(pTag,"repair")=0 Then
   Response.Write "<HTML><HEAD><TITLE></TITLE></HEAD>"
   Response.Write "<BODY style='font-size:9pt'>"
   Response.Write "<HR>"
   Response.Write "<center>敬愛的同仁，<BR>"
   Response.Write "您不是修繕人員，所以不允許您進入。<BR>"
   Response.Write "如果真的有需要進入，請您與資訊小組聯繫。<BR><HR></center>"
   Response.Write "<CENTER>"
   Response.Write "<A HREF='repair.htm' class=9-font>返回修繕登記中心主畫面</A>"
   Response.Write "</CENTER>"
   Response.Write "</BODY>"
   Response.Write "</HTML>"
   Response.End
End If
%>

<%WorkID=Request("WorkID")%>
<%
Set conn = Server.CreateObject("ADODB.Connection")
DBPath = Server.MapPath("repair.mdb")
conn.Open "driver={Microsoft Access Driver (*.mdb)};dbq=" & DBPath
Set rs = Server.CreateObject("ADODB.Recordset")
rs.Open "Select * From repair Where WorkID=" & Request("WorkID"), conn, 3
%>
<html>
<head>
<title>修繕登記中心</title>
<meta HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=big5"> 
<style type="text/css">
<!--
.9-font {  font-size: 9pt; font-family:"Arial"}
a:link {  color: #3E3EB5; text-decoration: none}
a:visited {  color: #868ECA; text-decoration: none}
a:hover {  color: #D05146; text-decoration: underline}
a:active {  color: #FF0000; text-decoration: underline}
-->
</style></head>

<body BGCOLOR="#E8FFEC">
<div ALIGN="CENTER">
<font COLOR="#006600"><b>申請內容</b></font>
<hr>
<table WIDTH="75%" BORDER="1" CELLPADDING="2" CELLSPACING="0" BORDERCOLORLIGHT="#808080" BORDERCOLORDARK="#FFFFFF" CLASS="9-font" BGCOLOR="#CCFFCC">
 <tr>
  <td HEIGHT="24" WIDTH="100%" COLSPAN="3" BGCOLOR="#006600">
   <div ALIGN="CENTER"><font COLOR="#FFFF00"><b>個人資料</b></font></div>
  </td>
 </tr>
 <tr>
  <td HEIGHT="24" WIDTH="26%">
   <div ALIGN="RIGHT">姓名</div>
  </td>
  <td HEIGHT="24" WIDTH="74%" COLSPAN="2">
   <%=rs("UserName")%>
  </td>
 </tr>
 <tr>
  <td HEIGHT="14" WIDTH="26%">
   <div ALIGN="RIGHT">電子郵件帳號</div>
  </td>
  <td HEIGHT="14" WIDTH="74%" COLSPAN="2">
   <%=rs("UserEmail")%>
  </td>
 </tr>
</table>
<hr width="80%">

<table WIDTH="75%" BORDER="1" CELLPADDING="2" CELLSPACING="0" BORDERCOLORLIGHT="#808080" BORDERCOLORDARK="#FFFFFF" BGCOLOR="#FFEAEA" CLASS="9-font">
 <tr>
  <td COLSPAN="3" BGCOLOR="#9B0000">
   <div ALIGN="CENTER"><b><font COLOR="#FFFFFF">問題描述</font></b></div>
  </td>
 </tr>
 <tr>
  <td WIDTH="13%">
   <div ALIGN="RIGHT">主旨</div>
  </td>
  <td WIDTH="87%" COLSPAN="2">
   <%=rs("WorkSubject")%>
  </td>
 </tr>
 <tr>
  <td WIDTH="13%">
   <div ALIGN="RIGHT">說明</div>
  </td>
  <td WIDTH="87%" COLSPAN="2">
   <%=Replace(rs("WorkWords"),chr(13),"<br>")%>
  </td>
 </tr>
</table>
<hr>
<form ACTION="sentout.asp" METHOD="POST">
<font COLOR="#0000FF"><b>修繕人員回應</b></font>
<br>
<font class="9-font">
<a href="replydetail.asp?WorkID=<%=Request("WorkID")%>&Command=delete&WorkName=<%=rs("WorkName")%>&pTag=<%=pTag%>&pFrom=<%=pFrom%>"><img src="img/button01.gif" border=0>管理選單</a>
</font>
<hr>
<table WIDTH="75%" BORDER="1" CELLPADDING="2" CELLSPACING="0" BORDERCOLORLIGHT="#808080" BORDERCOLORDARK="#FFFFFF" BGCOLOR="#E0E0E0" CLASS="9-font">
 <tr BGCOLOR="#989898">
  <td COLSPAN="3">
   <div ALIGN="CENTER"><b><font COLOR="#FFFFFF">處理人員</font></b></div>
  </td>
 </tr>
 <tr>
  <td COLSPAN="3">
   單位/職稱：<input TYPE="hidden" NAME="WorkMaster" value="<%=pFrom%>"><%=pFrom%>
  </td>
 </tr>
</table>
<hr Width="80%">
<table WIDTH="75%" BORDER="1" CELLPADDING="2" CELLSPACING="0" BORDERCOLORLIGHT="#808080" BORDERCOLORDARK="#FFFFFF" BGCOLOR="#E0E0E0" CLASS="9-font">
 <tr BGCOLOR="#989898">
  <td COLSPAN="3">
   <div ALIGN="CENTER"><b><font COLOR="#FFFFFF">處理狀況</font></b></div>
  </td>
 </tr>
 <tr>
  <td COLSPAN="3">
   處理中<input TYPE="radio" NAME="WorkStatus" VALUE="處理中">
  </td>
 </tr>
 <tr>
  <td COLSPAN="3">
   處理完畢<input TYPE="radio" NAME="WorkStatus" VALUE="處理完畢" checked>
  </td>
 </tr>
 <tr>
  <td COLSPAN="3">
   須聯絡原廠<input TYPE="radio" NAME="WorkStatus" VALUE="須聯絡原廠">
  </td>
 </tr>
</table>
<hr Width="80%">
<table WIDTH="75%" BORDER="1" CELLPADDING="2" CELLSPACING="0" BORDERCOLORLIGHT="#808080" BORDERCOLORDARK="#FFFFFF" BGCOLOR="#D0C8A8" CLASS="9-font">
 <tr>
  <td COLSPAN="3" BGCOLOR="#B0A068">
   <div ALIGN="CENTER"><b><font COLOR="#FFFFFF">補充說明</font></b></div>
  </td>
 </tr>
 <tr>
  <td WIDTH="15%">
   <div ALIGN="RIGHT">說明</div>
  </td>
  <td WIDTH="85%" COLSPAN="2">
   <textarea NAME="WorkComment" CLASS="9-font" COLS="48" ROWS="5"></textarea>
  </td>
 </tr>
 <tr>
  <td WIDTH="15%">
   <div ALIGN="RIGHT">&nbsp;</div>
  </td>
  <td WIDTH="85%" COLSPAN="2">
   <input type="checkbox" name="SendEmail" value="1">要不要另加用電子郵件通知申請者？
  </td>
 </tr>
</table>
<hr Width="80%">
<input TYPE="hidden" NAME="Command" VALUE="reply">
<input TYPE="hidden" NAME="WorkID" VALUE="<%=Request("WorkID")%>">
<INPUT TYPE="hidden" NAME=WorkName VALUE=<%=rs("WorkName")%>>
<input TYPE="submit" VALUE="送出登記">
<input TYPE="reset" VALUE="擦掉重寫">
</form>
</div>
</body>
</html>