<!-- #INCLUDE FILE="domain.asp" -->

<% 
SearchWords=Request("SearchWords")
Set conn = Server.CreateObject("ADODB.Connection")
DBPath = Server.MapPath("repair.mdb")
conn.Open "driver={Microsoft Access Driver (*.mdb)};dbq=" & DBPath
Set rs = Server.CreateObject("ADODB.Recordset")
   If Len(SearchWords) > 0 Then
      SQLstr= "Select * From repair Where repair.WorkName='" & Request("WorkName") & "'"
      SQLstr = SQLstr & " and (repair.WorkSubject Like '%" & SearchWords & "%' or repair.WorkWords Like '%" & SearchWords & "%')"
   ElseIf Len(SearchWords) = 0 Then
      SQLstr= "Select * From repair Where WorkName='" & Request("WorkName") & "'"
   End If
SQLstr= SQLstr & " Order By CreateDate DESC"
rs.Open (SQLstr),conn,3
%>

<%
' ���ҭn�s��������
Page = cint(Request("Page"))
PageSize = cint(Request("PageSize"))

If Page = 0 Then
   Page = 1
End If

If PageSize = 0 Then
   rs.PageSize = 12         ' ]�w�C����� 12 ��
End If

If Not rs.eof Then          ' ����Ƥ~���� 
   rs.AbsolutePage = Page   ' �N��ƿ����� Page ��
End if
%>

<html>
<head>
<title>��µ�n�O����</title>
<meta http-equiv="Content-Type" content="text/html; charset=big5">
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

<body bgcolor="#F0FFE8" onClick="Expand()">
<div align="center"><img src="img/title.gif" WIDTH="209" HEIGHT="35"> 
  <hr>
<marquee  style="color: #FF00FF">�U��P��, ���չq�����@�t�ӨC�g�T�w��P���G�B���W�Ⱦn�պ���, �о��q��Ӯɬq�e����, �H�Q�ֳt�A��........</marquee>
<form action="default.asp" name="search" method="post" class="9-font">
    <a href="form.asp?Workname=<%=Request("WorkName")%>"><img src="img/button01.gif" border="0" WIDTH="14" HEIGHT="12">�ڭn�n�O[<%=request("workname")%>]</a><br>
    <font class="9-font">�j�M�r��G
     <input type="text" name="SearchWords" Size="15">
     <input type="hidden" name="WorkName" value=<%=Request("WorkName")%>>
     <input type="image" src="img/go.gif" align="absbottom" alt="�r���J�n�F�A�N�Ы��ڤ@�U�a�I" WIDTH="30" HEIGHT="30"></font></td></tr>
</form>
<%
   If Len(SearchWords) > 0 Then
      Response.Write "<font class=12-font><center>�j�M�s������<font color='#388038'><strong>" & SearchWords & "</strong></font>���T��</center></font>"
   End If
%>

<div class="9-font">
<font color="#3040FF">���:�@<font color="#FF0000">
<%=rs.recordcount%><font color="#3040FF">
��
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=1&amp">�Ĥ@��</a>]
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=Page-1%>&amp">�W�@��</a>]
<select name="jump" onChange="location.href=this.options[this.selectedIndex].value;" class="9-font">
 <option value selected>��ܭ���</option>
  <%FOR J=1 to rs.pagecount
        IF J <> page THEN
           IF J < 10  THEN 
              J = "0" & J
           END IF%>
 <option value="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=cint(J)%>&amp">�� <%=J%> ��
      <%END IF
    NEXT%>
</select>
<%If Page<>rs.PageCount Then%>
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=Page+1%>&amp">�U�@��</a>]
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=rs.pagecount%>&amp">�̫�@��</a>]
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
      <td width="38%" colspan="2"> 
        <div align="center"><font color="#FFFFFF">���D�D��</font></div>
      </td>
      <td width="10%"> 
        <div align="center"><font color="#FFFFFF">�ӽФH</font></div>
      </td>
      <td width="23%" colspan="3"> 
        <div align="center"><font color="#FFFFFF">�B�z����</font></div>
      </td>
      <td width="12%"> 
        <div align="center"><font color="#FFFFFF">�ӽФ��</font></div>
      </td>
      <td width="12%"> 
        <div align="center"><font color="#FFFFFF">�B�z���</font></div>
      </td>
    </tr>
<%For sh=1 to rs.PageSize%>
<%If rs.EOF Then Exit For%>
    <tr bgcolor="#C0E0B0"> 
      <td width="30%"> 
        <%=rs("WorkSubject")%>
      </td>
      <td width="8%" align="center"> 
        [<span style="cursor:hand; color:#FF0000" title="���/�������D����" id="<%=sh%>">���e</span>]
      </td>
      <td width="10%" align="center"><font color="#485498"><a href="mailto:<%=rs("UserEmail")%>"><%=rs("UserName")%></a></td>

<%
Set rs1 = Server.CreateObject("ADODB.Recordset")
rs1.Open "Select * From reply Where WorkID=" & rs("WorkID") & " Order By WorkDate", conn, 3
%>
<%If Not rs1.EOF Then%>
<%rs1.MoveLast%>
      <td width="10%" align="center"><%=rs1("WorkStatus")%>
      </td>
      <td width="10%" align="center"><%=rs1("WorkMaster")%>
      </td>
      <td width="8%" align="center">
       [<a href="replydetail.asp?WorkID=<%=rs("WorkID")%>&Workname=<%=rs("WorkName")%>">���e</a>]
      </td>
<%Else%>
      <td width="10%" align="center"><font color="#485498">�|������</td>
      <td width="10%" align="center"><font color="#485498">----------</font>
      </td>
      <td width="8%" align="center">
       [�L]
      </td>
<%End If%>

      <td width="10%"><font color="#485498"><%=Year(rs("CreateDate")) & "/" & Month(rs("CreateDate")) & "/" & Day(rs("CreateDate"))%></td>

<%If Not rs1.EOF Then%>
<%rs1.MoveFirst%>
<%rs1.MoveLast%>
      <td width="10%"><font color="#485498"><%=Year(rs1("WorkDate")) & "/" & Month(rs1("WorkDate")) & "/" & Day(rs1("WorkDate"))%></td>
<%Else%>
      <td width="10%" align="center"><font color="#485498">----------</td>
<%End IF%>

<%rs1.Close%>
<%Set rs1=Nothing%>
    </tr>
       <tr>
        <td colspan="8">
         <div id="<%=sh%>child" style="display:none">
          <table bgcolor="#C8E0C8" border="1" cellspacing="0" cellpadding="2" bordercolorlight="#808080" bordercolordark="#FFFFFF" width="100%" class="9-font">
           <tr><td>
           [&nbsp;<font color="#107818">����</font>]<br><%=Replace(rs("WorkWords"),Chr(13),"<br>")%>
           <div align="right">
           [<a href="reply.asp?WorkID=<%=rs("WorkID")%>&Workname=<%=rs("WorkName")%>">��µ���^��</a>]
           </div>
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
[<a href="default.asp?Page=1&amp&Workname=<%=Request("WorkName")%>">�Ĥ@��</a>]
[<a href="default.asp?Page=<%=Page-1%>&amp&Workname=<%=Request("WorkName")%>">�W�@��</a>]
<select name="jump" onChange="location.href=this.options[this.selectedIndex].value;" class="9-font">
 <option value selected>��ܭ���</option>
  <%FOR J=1 to rs.pagecount
        IF J <> page THEN
           IF J < 10  THEN 
              J = "0" & J
           END IF%>
 <option value="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=cint(J)%>&amp">�� <%=J%> ��
      <%END IF
    NEXT%>
</select>
<%If Page<>rs.PageCount Then%>
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=Page+1%>&amp">�U�@��</a>]
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=rs.pagecount%>&amp">�̫�@��</a>]
<%End If%>
 ����:��
<font color="#CC0000">
 <%=CINT(page)%>
</font>��/�@
<font color="#CC0000">
 <%=rs.PageCount%>
</font>��
</div>
<br>
</font></div>
<div Align="Center" style="font-size:10pt">
�{���ק�Ϋظm�G���s���� v1.2  <br> 
��l�{��:�U�ڰ�p<br>

</div>
</body>
</html>

<!-- #INCLUDE FILE="domain2.asp" -->