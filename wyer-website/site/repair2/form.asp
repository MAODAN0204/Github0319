<% session("wolf")="repair" 
'判斷是否為本地網域
 if left(Request.ServerVariables("REMOTE_ADDR"),10)="163.15.62." or left(Request.ServerVariables("REMOTE_ADDR"),8)="192.168." then 
   check=true
 else
       Response.Redirect "default.asp?WorkName="&Request("WorkName")
 End If

%>
<HTML>
<HEAD>
<TITLE>修繕登記中心</TITLE>
<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=big5"> 
<STYLE TYPE="text/css">
<!--
.9-font {  font-family: "Arial", "Helvetica", "sans-serif"; font-size: 9pt}
-->
</STYLE>
<Script Language="VBScript">
Function CheckData
 If Len(ThisForm.UserName.Value)=0 Or Len(ThisForm.UserEmail.Value)=0 Or Len(ThisForm.WorkSubject.Value)=0 Or Len(ThisForm.WorkWords.Value)=0 Then
  MsgBox "欄位不可以為空白",64,"基本錯誤！！"
  Exit Function
 End If
ThisForm.Submit
End Function
</Script>
</HEAD>

<BODY BGCOLOR="#E8FFEC">
<DIV ALIGN="CENTER">
<FORM ACTION="sentout.asp" Name="ThisForm" METHOD="POST">
<input type="hidden" name="WorkName" value=<%=Request("WorkName")%>>
<FONT COLOR="#006600"><B>中山高中[<%=Request("WorkName")%>]修繕登記中心</B></FONT>
<HR>
<TABLE WIDTH="75%" BORDER="1" CELLPADDING="2" CELLSPACING="0" BORDERCOLORLIGHT="#808080" BORDERCOLORDARK="#FFFFFF" CLASS="9-font" BGCOLOR="#CCFFCC">
 <TR>
  <TD HEIGHT="24" WIDTH="100%" COLSPAN="3" BGCOLOR="#006600">
   <DIV ALIGN="CENTER"><FONT COLOR="#FFFF00"><B>個人資料</B></FONT></DIV>
  </TD>
 </TR>
 <TR>
  <TD HEIGHT="24" WIDTH="26%">
   <DIV ALIGN="RIGHT">姓名</DIV>
  </TD>
  <TD HEIGHT="24" WIDTH="74%" COLSPAN="2">
   <INPUT TYPE="text" NAME="UserName" CLASS="9-font" SIZE="8">
  </TD>
 </TR>
 <TR>
  <TD HEIGHT="14" WIDTH="26%">
   <DIV ALIGN="RIGHT">電子郵件帳號</DIV>
  </TD>
  <TD HEIGHT="14" WIDTH="74%" COLSPAN="2">
   <INPUT TYPE="text" NAME="UserEmail" Value="@Mydomain" CLASS="9-font" SIZE="30">
  </TD>
 </TR>
 <TR>
  <TD WIDTH="26%">
   <DIV ALIGN="RIGHT">&nbsp;</DIV>
  </TD>
  <TD WIDTH="74%" COLSPAN="2">
   <input type="checkbox" name=SendEmail value=1>要不要另外加用電子郵件通知您修繕的結果？
  </TD>
 </TR>
</TABLE>
<HR>
<TABLE WIDTH="75%" BORDER="1" CELLPADDING="2" CELLSPACING="0" BORDERCOLORLIGHT="#808080" BORDERCOLORDARK="#FFFFFF" BGCOLOR="#FFEAEA" CLASS="9-font">
 <TR>
  <TD COLSPAN="3" BGCOLOR="#9B0000">
   <DIV ALIGN="CENTER"><B><FONT COLOR="#FFFFFF">問題描述</FONT></B></DIV>
  </TD>
 </TR>
 <TR>
  <TD WIDTH="13%">
   <DIV ALIGN="RIGHT">主旨<BR><FONT COLOR="#FF0000">(請註明處室(班級)及設備編號)</FONT></DIV>
  </TD>
  <TD WIDTH="87%" COLSPAN="2">
   <INPUT TYPE="text" NAME="WorkSubject" CLASS="9-font" SIZE="45">
   <BR>
   
  </TD>
 </TR>
 <TR>
  <TD WIDTH="13%">
   <DIV ALIGN="RIGHT">說明<BR><FONT COLOR="#FF0000">(請詳述故障情形)</FONT></DIV>
  </TD>
  <TD WIDTH="87%" COLSPAN="2">
   <TEXTAREA NAME="WorkWords" CLASS="9-font" COLS="48" ROWS="5"></TEXTAREA>
  </TD>
 </TR>
</TABLE>
<HR>
<input type=button Name=Button Value=再三確認，送出 onClick=CheckData class=9-font>
<input type=Reset Name=Button Value=擦乾淨，重寫 class=9-font>
</FORM>
</DIV>
</BODY>
</HTML>
