<% session("wolf")="repair" 
'�P�_�O�_�����a����
 if left(Request.ServerVariables("REMOTE_ADDR"),10)="163.15.62." or left(Request.ServerVariables("REMOTE_ADDR"),8)="192.168." then 
   check=true
 else
       Response.Redirect "default.asp?WorkName="&Request("WorkName")
 End If

%>
<HTML>
<HEAD>
<TITLE>��µ�n�O����</TITLE>
<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=big5"> 
<STYLE TYPE="text/css">
<!--
.9-font {  font-family: "Arial", "Helvetica", "sans-serif"; font-size: 9pt}
-->
</STYLE>
<Script Language="VBScript">
Function CheckData
 If Len(ThisForm.UserName.Value)=0 Or Len(ThisForm.UserEmail.Value)=0 Or Len(ThisForm.WorkSubject.Value)=0 Or Len(ThisForm.WorkWords.Value)=0 Then
  MsgBox "��줣�i�H���ť�",64,"�򥻿��~�I�I"
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
<FONT COLOR="#006600"><B>���s����[<%=Request("WorkName")%>]��µ�n�O����</B></FONT>
<HR>
<TABLE WIDTH="75%" BORDER="1" CELLPADDING="2" CELLSPACING="0" BORDERCOLORLIGHT="#808080" BORDERCOLORDARK="#FFFFFF" CLASS="9-font" BGCOLOR="#CCFFCC">
 <TR>
  <TD HEIGHT="24" WIDTH="100%" COLSPAN="3" BGCOLOR="#006600">
   <DIV ALIGN="CENTER"><FONT COLOR="#FFFF00"><B>�ӤH���</B></FONT></DIV>
  </TD>
 </TR>
 <TR>
  <TD HEIGHT="24" WIDTH="26%">
   <DIV ALIGN="RIGHT">�m�W</DIV>
  </TD>
  <TD HEIGHT="24" WIDTH="74%" COLSPAN="2">
   <INPUT TYPE="text" NAME="UserName" CLASS="9-font" SIZE="8">
  </TD>
 </TR>
 <TR>
  <TD HEIGHT="14" WIDTH="26%">
   <DIV ALIGN="RIGHT">�q�l�l��b��</DIV>
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
   <input type="checkbox" name=SendEmail value=1>�n���n�t�~�[�ιq�l�l��q���z��µ�����G�H
  </TD>
 </TR>
</TABLE>
<HR>
<TABLE WIDTH="75%" BORDER="1" CELLPADDING="2" CELLSPACING="0" BORDERCOLORLIGHT="#808080" BORDERCOLORDARK="#FFFFFF" BGCOLOR="#FFEAEA" CLASS="9-font">
 <TR>
  <TD COLSPAN="3" BGCOLOR="#9B0000">
   <DIV ALIGN="CENTER"><B><FONT COLOR="#FFFFFF">���D�y�z</FONT></B></DIV>
  </TD>
 </TR>
 <TR>
  <TD WIDTH="13%">
   <DIV ALIGN="RIGHT">�D��<BR><FONT COLOR="#FF0000">(�е����B��(�Z��)�γ]�ƽs��)</FONT></DIV>
  </TD>
  <TD WIDTH="87%" COLSPAN="2">
   <INPUT TYPE="text" NAME="WorkSubject" CLASS="9-font" SIZE="45">
   <BR>
   
  </TD>
 </TR>
 <TR>
  <TD WIDTH="13%">
   <DIV ALIGN="RIGHT">����<BR><FONT COLOR="#FF0000">(�иԭz�G�ٱ���)</FONT></DIV>
  </TD>
  <TD WIDTH="87%" COLSPAN="2">
   <TEXTAREA NAME="WorkWords" CLASS="9-font" COLS="48" ROWS="5"></TEXTAREA>
  </TD>
 </TR>
</TABLE>
<HR>
<input type=button Name=Button Value=�A�T�T�{�A�e�X onClick=CheckData class=9-font>
<input type=Reset Name=Button Value=�����b�A���g class=9-font>
</FORM>
</DIV>
</BODY>
</HTML>
