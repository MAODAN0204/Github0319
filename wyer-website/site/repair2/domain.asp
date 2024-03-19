<% '判斷使用者是否在允許的網域內, 可利用此段限制只服務校內
 if left(Request.ServerVariables("REMOTE_ADDR"),10)="163.15.62." or left(Request.ServerVariables("REMOTE_ADDR"),8)="192.168."  or left(Request.ServerVariables("REMOTE_ADDR"),9)="127.0.0.1" then 
'下列為永遠成立
'  if 1=1 then
   check=true
%>