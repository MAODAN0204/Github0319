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
' 取所要瀏覽的頁次
Page = cint(Request("Page"))
PageSize = cint(Request("PageSize"))

If Page = 0 Then
   Page = 1
End If

If PageSize = 0 Then
   rs.PageSize = 12         ' ]定每頁顯示 12 筆
End If

If Not rs.eof Then          ' 有資料才執行 
   rs.AbsolutePage = Page   ' 將資料錄移至 Page 頁
End if
%>

<html>
<head>
<title>修繕登記中心</title>
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
<marquee  style="color: #FF00FF">各位同仁, 本校電腦維護廠商每週固定於星期二、五上午駐校維修, 請儘量於該時段前報修, 以利快速服務........</marquee>
<form action="default.asp" name="search" method="post" class="9-font">
    <a href="form.asp?Workname=<%=Request("WorkName")%>"><img src="img/button01.gif" border="0" WIDTH="14" HEIGHT="12">我要登記[<%=request("workname")%>]</a><br>
    <font class="9-font">搜尋字串：
     <input type="text" name="SearchWords" Size="15">
     <input type="hidden" name="WorkName" value=<%=Request("WorkName")%>>
     <input type="image" src="img/go.gif" align="absbottom" alt="字串輸入好了，就請按我一下吧！" WIDTH="30" HEIGHT="30"></font></td></tr>
</form>
<%
   If Len(SearchWords) > 0 Then
      Response.Write "<font class=12-font><center>搜尋瀏覽關於<font color='#388038'><strong>" & SearchWords & "</strong></font>的訊息</center></font>"
   End If
%>

<div class="9-font">
<font color="#3040FF">資料:共<font color="#FF0000">
<%=rs.recordcount%><font color="#3040FF">
筆
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=1&amp">第一頁</a>]
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=Page-1%>&amp">上一頁</a>]
<select name="jump" onChange="location.href=this.options[this.selectedIndex].value;" class="9-font">
 <option value selected>選擇頁數</option>
  <%FOR J=1 to rs.pagecount
        IF J <> page THEN
           IF J < 10  THEN 
              J = "0" & J
           END IF%>
 <option value="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=cint(J)%>&amp">第 <%=J%> 頁
      <%END IF
    NEXT%>
</select>
<%If Page<>rs.PageCount Then%>
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=Page+1%>&amp">下一頁</a>]
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=rs.pagecount%>&amp">最後一頁</a>]
<%End If%>
 頁數:第
<font color="#CC0000">
 <%=CINT(page)%>
</font>頁/共
<font color="#CC0000">
 <%=rs.PageCount%>
</font>頁
</div>

<hr width="100%">

  <table width="100%" border="1" cellspacing="0" cellpadding="2" bgcolor="#507838" bordercolorlight="#808080" bordercolordark="#FFFFFF" class="9-font">
    <tr> 
      <td width="38%" colspan="2"> 
        <div align="center"><font color="#FFFFFF">問題主旨</font></div>
      </td>
      <td width="10%"> 
        <div align="center"><font color="#FFFFFF">申請人</font></div>
      </td>
      <td width="23%" colspan="3"> 
        <div align="center"><font color="#FFFFFF">處理情形</font></div>
      </td>
      <td width="12%"> 
        <div align="center"><font color="#FFFFFF">申請日期</font></div>
      </td>
      <td width="12%"> 
        <div align="center"><font color="#FFFFFF">處理日期</font></div>
      </td>
    </tr>
<%For sh=1 to rs.PageSize%>
<%If rs.EOF Then Exit For%>
    <tr bgcolor="#C0E0B0"> 
      <td width="30%"> 
        <%=rs("WorkSubject")%>
      </td>
      <td width="8%" align="center"> 
        [<span style="cursor:hand; color:#FF0000" title="顯示/關閉問題說明" id="<%=sh%>">內容</span>]
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
       [<a href="replydetail.asp?WorkID=<%=rs("WorkID")%>&Workname=<%=rs("WorkName")%>">內容</a>]
      </td>
<%Else%>
      <td width="10%" align="center"><font color="#485498">尚未完成</td>
      <td width="10%" align="center"><font color="#485498">----------</font>
      </td>
      <td width="8%" align="center">
       [無]
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
           [&nbsp;<font color="#107818">說明</font>]<br><%=Replace(rs("WorkWords"),Chr(13),"<br>")%>
           <div align="right">
           [<a href="reply.asp?WorkID=<%=rs("WorkID")%>&Workname=<%=rs("WorkName")%>">修繕單位回應</a>]
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
<font color="#3040FF">資料:共<font color="#FF0000">
<%=rs.recordcount%><font color="#3040FF">
筆
[<a href="default.asp?Page=1&amp&Workname=<%=Request("WorkName")%>">第一頁</a>]
[<a href="default.asp?Page=<%=Page-1%>&amp&Workname=<%=Request("WorkName")%>">上一頁</a>]
<select name="jump" onChange="location.href=this.options[this.selectedIndex].value;" class="9-font">
 <option value selected>選擇頁數</option>
  <%FOR J=1 to rs.pagecount
        IF J <> page THEN
           IF J < 10  THEN 
              J = "0" & J
           END IF%>
 <option value="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=cint(J)%>&amp">第 <%=J%> 頁
      <%END IF
    NEXT%>
</select>
<%If Page<>rs.PageCount Then%>
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=Page+1%>&amp">下一頁</a>]
[<a href="default.asp?Workname=<%=Request("WorkName")%>&Page=<%=rs.pagecount%>&amp">最後一頁</a>]
<%End If%>
 頁數:第
<font color="#CC0000">
 <%=CINT(page)%>
</font>頁/共
<font color="#CC0000">
 <%=rs.PageCount%>
</font>頁
</div>
<br>
</font></div>
<div Align="Center" style="font-size:10pt">
程式修改及建置：中山高中 v1.2  <br> 
原始程式:萬芳國小<br>

</div>
</body>
</html>

<!-- #INCLUDE FILE="domain2.asp" -->