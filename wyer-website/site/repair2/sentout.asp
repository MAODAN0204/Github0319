<% if Session("wolf")<>"repair" then Response.REDIRECT "default.asp" %>
<!--#include file="adovbs.inc" -->
<%
Set conn = Server.CreateObject("ADODB.Connection")
DBPath = Server.MapPath("repair.mdb")
conn.Open "driver={Microsoft Access Driver (*.mdb)};dbq=" & DBPath
%>
<%
Select Case Request("Command")
Case "deletework"
   If InStr(Request("pTag"),"leader") > 0 Then
      SQLstr="Delete From repair Where WorkID=" & Request("WorkID")
      conn.Execute(SQLstr)
      SQLstr="Delete From reply Where WorkID=" & Request("WorkID")
      conn.Execute(SQLstr)
      conn.Close
      Set conn=Nothing
   Else
      Response.Write "<HTML><HEAD><TITLE></TITLE></HEAD>"
      Response.Write "<BODY style='font-size:9pt'>"
      Response.Write "<HR>"
      Response.Write "<center>敬愛的同仁，<BR>"
      Response.Write "您沒有系統管理者的權限，所以不允許您刪除整篇申請。<BR>"
      Response.Write "如果真的有需要進入，請您與資訊小組聯繫。<BR><HR></center>"
      Response.Write "<CENTER>"
      Response.Write "<A HREF='repair.htm' class=9-font>返回修繕登記中心主畫面</A>"
      Response.Write "</CENTER>"
      Response.Write "</BODY>"
      Response.Write "</HTML>"
      Response.End
   End If
Case "deletereply"
   SQLstr="Select * From reply Where ReplyID=" & Request("ReplyID")
   Set rs=conn.Execute(SQLstr)
If Request("pFrom") = rs("WorkMaster") Then
   SQLstr="Delete From reply Where ReplyID=" & Request("ReplyID")
   conn.Execute(SQLstr)
   conn.Close
   Set conn=Nothing
Else
   Response.Write "<HTML><HEAD><TITLE></TITLE></HEAD>"
   Response.Write "<BODY style='font-size:9pt'>"
   Response.Write "<HR>"
   Response.Write "<center>敬愛的同仁，<BR>"
   Response.Write "這篇回應不是您張貼的，所以不允許您刪除。<BR>"
   Response.Write "如果真的有需要進入，請您與資訊小組聯繫。<BR><HR></center>"
   Response.Write "<CENTER>"
   Response.Write "<A HREF='repair.htm' class=9-font>返回修繕登記中心主畫面</A>"
   Response.Write "</CENTER>"
   Response.Write "</BODY>"
   Response.Write "</HTML>"
   Response.End
End If
Case "reply"
   WorkID=Request("WorkID")
   WorkName=Request("WorkName")
   WorkMaster=Request("WorkMaster")
   WorkStatus=Request("WorkStatus")
   WorkComment=Request("WorkComment")
   SendEmail=Request("SendEmail")

   Set AddRs=SERVER.CreateObject("ADODB.Recordset")
   AddRs.Open "reply", conn , adOpenDynamic, adLockPessimistic
   AddRs.AddNew
   AddRs("WorkID")=Request.Form("WorkID")
   AddRs("WorkMaster")=Request.Form("WorkMaster")
   AddRs("WorkStatus")=Request.Form("WorkStatus")
   AddRs("WorkComment")=Request.Form("WorkComment")
   AddRs.Update
   AddRs.Close
   Set AddRs=Nothing

   SQLstr="Select * From repair Where WorkID=" & WorkID
   Set rs=conn.Execute(SQLstr)

'電子郵件回覆開始
If SendEmail=1 Or rs("SendEmail")=1 Then
   AddWordsTitle="給" & rs("UserName") & "：" & chr(13)
   AddWordsTitle=AddWordsTitle & chr(13)

If SendEmail=1 And rs("SendEmail")=0 Then
   AddWordsTitle=AddWordsTitle & "雖然您曾經說過不要用電子郵件通知您" & chr(13)
   AddWordsTitle=AddWordsTitle & "但是回應的修繕人員堅持要用專函通知您" & chr(13)
ElseIf SendEmail=0 And rs("SendEmail")=1 Then
   AddWordsTitle=AddWordsTitle & "您曾經說過要用電子郵件通知您" & chr(13)
   AddWordsTitle=AddWordsTitle & "所以回應的修繕人員現在用專函通知您" & chr(13)
End If

   AddWordsEnd=chr(13) &"來自：" & WorkMaster & chr(13)
   AddWordsEnd=AddWordsEnd & "=================================================" & chr(13)
   AddWordsEnd=AddWordsEnd & "中山高中電腦中心-寄發時間："
   AddWordsEnd=AddWordsEnd & Year(Now())   & "年"
   AddWordsEnd=AddWordsEnd & Month(Now())  & "月"
   AddWordsEnd=AddWordsEnd & Day(Now())    & "日"
   AddWordsEnd=AddWordsEnd & Hour(Now())   & "點"
   AddWordsEnd=AddWordsEnd & Minute(Now()) & "分"
   AddWordsEnd=AddWordsEnd & Second(Now()) & "秒" & chr(13)
   AddWordsEnd=AddWordsEnd & "=================================================" & chr(13)

   Body="您的問題：" & chr(13)

   Body=Body & "■申報種類■" & chr(13) & rs("WorkName") & chr(13)
   Body=Body & "■問題主旨■" & chr(13) & rs("WorkSubject") & chr(13)
   Body=Body & "■問題內容■" & chr(13) & rs("WorkWords") & chr(13) & chr(13)
   Body=Body & WorkMaster & "的答覆：" & chr(13)
   Body=Body & "★處理狀況★" & chr(13) & WorkStatus & chr(13)
   Body=Body & "★處理說明★" & chr(13) & WorkComment & chr(13) & chr(13)
   Body=Body & "詳細情形敬請按下以下超連結：" & chr(13)
   Body=Body & "http://www.cshs.kh.edu.tw" & chr(13)
   Body=Body & "(請保持網路連線狀態)" & chr(13)

   Set Mail=Server.CreateObject("CDONTS.NewMail")
'以下請修改為貴校負責老師email位址
   Mail.From       = "Myname@Mydomain"
   Mail.To         = rs("UserEmail") & "@Mydomain"

   Mail.Subject    = "(校園修繕系統)自動回覆通知"
   Mail.Body       = AddWordsTitle & Body & AddWordsEnd
   Mail.Send
   Set Mail=Nothing
End If
'電子郵件回覆完成

   conn.Close
   Set conn=Nothing

Case Else
   Set AddRs=SERVER.CreateObject("ADODB.Recordset")
   AddRs.Open "repair", conn , adOpenDynamic, adLockPessimistic
   AddRs.AddNew
   AddRs("WorkName")=Request.Form("WorkName")
   AddRs("WorkSubject")=Request.Form("WorkSubject")
   AddRs("WorkWords")=Request.Form("WorkWords")
   AddRs("UserName")=Request.Form("UserName")
   AddRs("UserEmail")=Request.Form("UserEmail")
   AddRs("SendEmail")=Request.Form("SendEmail")
   AddRs.Update
   AddRs.Close
   Set AddRs=Nothing
   conn.Close
   Set conn=Nothing
End Select
Response.Redirect "default.asp?WorkName=" & Request("WorkName")
%>