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
      Response.Write "<center>�q�R���P���A<BR>"
      Response.Write "�z�S���t�κ޲z�̪��v���A�ҥH�����\�z�R����g�ӽСC<BR>"
      Response.Write "�p�G�u�����ݭn�i�J�A�бz�P��T�p���pô�C<BR><HR></center>"
      Response.Write "<CENTER>"
      Response.Write "<A HREF='repair.htm' class=9-font>��^��µ�n�O���ߥD�e��</A>"
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
   Response.Write "<center>�q�R���P���A<BR>"
   Response.Write "�o�g�^�����O�z�i�K���A�ҥH�����\�z�R���C<BR>"
   Response.Write "�p�G�u�����ݭn�i�J�A�бz�P��T�p���pô�C<BR><HR></center>"
   Response.Write "<CENTER>"
   Response.Write "<A HREF='repair.htm' class=9-font>��^��µ�n�O���ߥD�e��</A>"
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

'�q�l�l��^�ж}�l
If SendEmail=1 Or rs("SendEmail")=1 Then
   AddWordsTitle="��" & rs("UserName") & "�G" & chr(13)
   AddWordsTitle=AddWordsTitle & chr(13)

If SendEmail=1 And rs("SendEmail")=0 Then
   AddWordsTitle=AddWordsTitle & "���M�z���g���L���n�ιq�l�l��q���z" & chr(13)
   AddWordsTitle=AddWordsTitle & "���O�^������µ�H������n�αM��q���z" & chr(13)
ElseIf SendEmail=0 And rs("SendEmail")=1 Then
   AddWordsTitle=AddWordsTitle & "�z���g���L�n�ιq�l�l��q���z" & chr(13)
   AddWordsTitle=AddWordsTitle & "�ҥH�^������µ�H���{�b�αM��q���z" & chr(13)
End If

   AddWordsEnd=chr(13) &"�ӦۡG" & WorkMaster & chr(13)
   AddWordsEnd=AddWordsEnd & "=================================================" & chr(13)
   AddWordsEnd=AddWordsEnd & "���s�����q������-�H�o�ɶ��G"
   AddWordsEnd=AddWordsEnd & Year(Now())   & "�~"
   AddWordsEnd=AddWordsEnd & Month(Now())  & "��"
   AddWordsEnd=AddWordsEnd & Day(Now())    & "��"
   AddWordsEnd=AddWordsEnd & Hour(Now())   & "�I"
   AddWordsEnd=AddWordsEnd & Minute(Now()) & "��"
   AddWordsEnd=AddWordsEnd & Second(Now()) & "��" & chr(13)
   AddWordsEnd=AddWordsEnd & "=================================================" & chr(13)

   Body="�z�����D�G" & chr(13)

   Body=Body & "���ӳ�������" & chr(13) & rs("WorkName") & chr(13)
   Body=Body & "�����D�D����" & chr(13) & rs("WorkSubject") & chr(13)
   Body=Body & "�����D���e��" & chr(13) & rs("WorkWords") & chr(13) & chr(13)
   Body=Body & WorkMaster & "�����СG" & chr(13)
   Body=Body & "���B�z���p��" & chr(13) & WorkStatus & chr(13)
   Body=Body & "���B�z������" & chr(13) & WorkComment & chr(13) & chr(13)
   Body=Body & "�Բӱ��ηq�Ы��U�H�U�W�s���G" & chr(13)
   Body=Body & "http://www.cshs.kh.edu.tw" & chr(13)
   Body=Body & "(�ЫO�������s�u���A)" & chr(13)

   Set Mail=Server.CreateObject("CDONTS.NewMail")
'�H�U�Эקאּ�Q�խt�d�Ѯvemail��}
   Mail.From       = "Myname@Mydomain"
   Mail.To         = rs("UserEmail") & "@Mydomain"

   Mail.Subject    = "(�ն��µ�t��)�۰ʦ^�гq��"
   Mail.Body       = AddWordsTitle & Body & AddWordsEnd
   Mail.Send
   Set Mail=Nothing
End If
'�q�l�l��^�Ч���

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