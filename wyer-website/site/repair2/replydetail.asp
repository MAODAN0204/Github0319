<%WorkID=Request("WorkID")%>
<%Command=Request("Command")%>
<%
Set conn = Server.CreateObject("ADODB.Connection")
DBPath = Server.MapPath("repair.mdb")
conn.Open "driver={Microsoft Access Driver (*.mdb)};dbq=" & DBPath
Set rs = Server.CreateObject("ADODB.Recordset")
rs.Open "Select * From repair Where WorkID=" & Request("WorkID"), conn, 3
%>
<html>
<head>
<title>��µ�n�O����</title>
<meta HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=big5"> 
<style type="text/css">
<!--
.9-font {  font-size: 9pt; font-family:"Arial"}
a:link {  color: #3E3EB5; text-decoration: none}
a:visited {  color: #868ECA; text-decoration: none}
a:hover {  color: #D05146; text-decoration: underline}
a:active {  color: #FF0000; text-decoration: underline}
-->
</style>
<script Language="JavaScript">
<!--
function Expand() {
 if (""!=event.srcElement.id) {
    var ch=event.srcElement.id + "child" 
    var el=document.all[ch]
    if (null!=el) el.style.display = "none" == el.style.display ? "" : "none"
    event.returnValue=false
 }
}
-->
</script>
</head>

<body BGCOLOR="#E8FFEC" onClick="Expand()">
<div ALIGN="CENTER">
<font COLOR="#006600"><b>�ӽФ��e</b></font>
<%If Request("Command")="delete" Then%>
<br><font class="9-font">
<a href="sentout.asp?WorkID=<%=Request("WorkID")%>&Command=deletework&WorkName=<%=Request("WorkName")%>&pTag=<%=Request("pTag")%>&pFrom=<%=Request("pFrom")%>"><img src="img/button01.gif" border=0>�R���o�@��g��µ�n�O�H�Ψ�����^��</a></font>
<%End If%>
<hr>
<table WIDTH="75%" BORDER="1" CELLPADDING="2" CELLSPACING="0" BORDERCOLORLIGHT="#808080" BORDERCOLORDARK="#FFFFFF" CLASS="9-font" BGCOLOR="#CCFFCC">
 <tr>
  <td HEIGHT="24" WIDTH="100%" COLSPAN="3" BGCOLOR="#006600">
   <div ALIGN="CENTER"><font COLOR="#FFFF00"><b>�ӤH���</b></font></div>
  </td>
 </tr>
 <tr>
  <td HEIGHT="24" WIDTH="26%">
   <div ALIGN="RIGHT">�m�W</div>
  </td>
  <td HEIGHT="24" WIDTH="74%" COLSPAN="2">
   <%=rs("UserName")%>
  </td>
 </tr>
 <tr>
  <td HEIGHT="14" WIDTH="26%">
   <div ALIGN="RIGHT">�q�l�l��b��</div>
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
   <div ALIGN="CENTER"><b><font COLOR="#FFFFFF">���D�y�z</font></b></div>
  </td>
 </tr>
 <tr>
  <td WIDTH="13%">
   <div ALIGN="RIGHT">�D��</div>
  </td>
  <td WIDTH="87%" COLSPAN="2">
   <%=rs("WorkSubject")%>
  </td>
 </tr>
 <tr>
  <td WIDTH="13%">
   <div ALIGN="RIGHT">����</div>
  </td>
  <td WIDTH="87%" COLSPAN="2">
   <%=Replace(rs("WorkWords"),chr(13),"<br>")%>
  </td>
 </tr>
</table>
<hr>
<%
rs.CLose
Set rs=Nothing
%>




<%
Set rs = Server.CreateObject("ADODB.Recordset")
rs.Open "Select * From reply Where WorkID=" & Request("WorkID") & " Order By WorkDate", conn, 3
%>

<%
' ���ҭn�s��������
Page = cint(Request("Page"))
PageSize = cint(Request("PageSize"))

If Page = 0 Then
   Page = 1
End If

If PageSize = 0 Then
   rs.PageSize = 6         ' �]�w�C����� 6 ��
End If

If Not rs.eof Then          ' ����Ƥ~���� 
   rs.AbsolutePage = Page   ' �N��ƿ����� Page ��
End if
%>

<font COLOR="#0000FF"><b>�ԲӦ^�����{</b></font>
<hr>

<div class="9-font">
<font color="#3040FF">���:�@<font color="#FF0000">
<%=rs.recordcount%><font color="#3040FF">
��
[<a href="replydetail.asp?Page=1&amp;WorkID=<%=Request("WorkID")%>">�Ĥ@��</a>]
[<a href="replydetail.asp?Page=<%=Page-1%>&amp;WorkID=<%=Request("WorkID")%>">�W�@��</a>]
<select name="jump" onChange="location.href=this.options[this.selectedIndex].value;" class="9-font">
 <option value selected>��ܭ���</option>
  <%FOR J=1 to rs.pagecount
        IF J <> page THEN
           IF J < 10  THEN 
              J = "0" & J
           END IF%>
 <option value="replydetail.asp?Page=<%=cint(J)%>&amp;WorkID=<%=Request("WorkID")%>">�� <%=J%> ��
      <%END IF
    NEXT%>
</select>
<%If Page<>rs.PageCount Then%>
[<a href="replydetail.asp?Page=<%=Page+1%>&amp;WorkID=<%=Request("WorkID")%>">�U�@��</a>]
[<a href="replydetail.asp?Page=<%=rs.pagecount%>&amp;WorkID=<%=Request("WorkID")%>">�̫�@��</a>]
<%End If%>
 ����:��
<font color="#CC0000">
 <%=CINT(page)%>
</font>��/�@
<font color="#CC0000">
 <%=rs.PageCount%>
</font>��
</div>

<hr width="100%">

  <table width="100%" border="1" cellspacing="0" cellpadding="2" bgcolor="#507838" bordercolorlight="#808080" bordercolordark="#FFFFFF" class="9-font">
    <tr> 
      <td width="40%" colspan="2"> 
        <div align="center"><font color="#FFFFFF">�B�z����</font></div>
      </td>
      <td width="25%"> 
        <div align="center"><font color="#FFFFFF">�B�z�H��</font></div>
      </td>
      <td width="35%"> 
        <div align="center"><font color="#FFFFFF">�B�z���</font></div>
      </td>
    </tr>
<%For sh=1 to rs.PageSize%>
<%If rs.EOF Then Exit For%>
    <tr bgcolor="#C0E0B0"> 
      <td width="32%"> 
        <%=rs("WorkStatus")%>
      </td>
      <td width="8%" align="center"> 
        [<span style="cursor:hand; color=#FF0000" title="���/�����B�z����" id="<%=sh%>">���e</span>]
      </td>
      <td width="25%" align="center"><font color="#485498"><%=rs("WorkMaster")%></td>
      <td width="35%" align="center"><font color="#485498"><%=rs("WorkDate")%></td>
    </tr>
    <tr>
     <td colspan="4">
        <div id="<%=sh%>child" style="display:none">
         <table bgcolor="#C8E0C8" border="1" cellspacing="0" cellpadding="2" bordercolorlight="#808080" bordercolordark="#FFFFFF" width="100%" class="9-font">
          <tr><td>
          [&nbsp;<font color="#107818">����</font>]<br><%=Replace(rs("WorkComment"),Chr(13),"<br>")%>
<%If Request("Command")="delete" Then%>
          <div align="right">
          [<a href="sentout.asp?ReplyID=<%=rs("ReplyID")%>&amp;Command=deletereply&amp;pTag=<%=Request("pTag")%>&amp;pFrom=<%=Request("pFrom")%>">�u�R���o�g�^��</a>]
          </div>
<%End If%>
          </td></tr>
         </table>
        </div>
       </td>
      </tr>
<%rs.MoveNext%>
<%Next%>
  </table>

<hr width="95%">

<div class="9-font">
<font color="#3040FF">���:�@<font color="#FF0000">
<%=rs.recordcount%><font color="#3040FF">
��
[<a href="replydetail.asp?Page=1&amp;WorkID=<%=Request("WorkID")%>">�Ĥ@��</a>]
[<a href="replydetail.asp?Page=<%=Page-1%>&amp;WorkID=<%=Request("WorkID")%>">�W�@��</a>]
<select name="jump" onChange="location.href=this.options[this.selectedIndex].value;" class="9-font">
 <option value selected>��ܭ���</option>
  <%FOR J=1 to rs.pagecount
        IF J <> page THEN
           IF J < 10  THEN 
              J = "0" & J
           END IF%>
 <option value="replydetail.asp?Page=<%=cint(J)%>&amp;WorkID=<%=Request("WorkID")%>">�� <%=J%> ��
      <%END IF
    NEXT%>
</select>
<%If Page<>rs.PageCount Then%>
[<a href="replydetail.asp?Page=<%=Page+1%>&amp;WorkID=<%=Request("WorkID")%>">�U�@��</a>]
[<a href="replydetail.asp?Page=<%=rs.pagecount%>&amp;WorkID=<%=Request("WorkID")%>">�̫�@��</a>]
<%End If%>
 ����:��
<font color="#CC0000">
 <%=CINT(page)%>
</font>��/�@
<font color="#CC0000">
 <%=rs.PageCount%>
</font>��
</div>

</font>


</div>
</body>
</html>
