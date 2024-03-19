<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ERP2._Default" Debug="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Wyer ERP</title>
    <!-- For Resposive Device -->
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <!-- For Window Tab Color -->
    <!-- Chrome, Firefox OS and Opera -->
    <meta name="theme-color" content="#061948"/>
    <!-- Windows Phone -->
    <meta name="msapplication-navbutton-color" content="#061948"/>
    <!-- iOS Safari -->
    <meta name="apple-mobile-web-app-status-bar-style" content="#061948"/>
    <!-- Favicon -->
    <link rel="icon" type="image/png" sizes="56x56" href="images/fav-icon/icon.png"/>
    <!-- Main style sheet -->
    <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css"/>
    <link rel="stylesheet" type="text/css" href="css/css.css"/>  

</head>
<body>
    <div class="wall">
        <div class="box">
            <form id="form1" runat="server">  
                <div class="" runat="server" ID="login">
                    <div class="logo-sec"><img src="images/logo-shadow.png" /><span class="text-big">登入系統</span></div>
                    <div>
                        <div class="row">
                            <div class="col-3 pb-4">
                                <h2 id="" class="text-right">帳號</h2>
                            </div>
                            <div class="col-9">
                                <asp:TextBox runat="server" type="Text" ID="Username" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3 pb-4">
                                <h2 class="text-right">密碼</h2>
                            </div>
                            <div class="col-9">
                                <asp:TextBox runat="server" type="Password" ID="ps" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" />
                            
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 text-center"><asp:Button runat="server" id="SubmitBtn" type="button" class="btn btn-lg btn-warning" Text="登入" onclick="SubmitBtn_Click"/></div>
                            <div class="col-6 text-center"><asp:Button runat="server" id="ClearBtn" type="button" class="btn btn-lg btn-primary" Text="清除" onclick="ClearBtn_Click" /></div>
                        </div>
                        <div class="msginfo"><asp:Label runat="server" ID="Label1" Text=""></asp:Label></div>
                    </div>
                </div>
                
                <div class="" runat="server" ID="menulist">

                    <div class="row">
                        <div class="col-12 text-center"><img src="images\logo-shadow.png" /></div>
                        <div class="col-6 text-center pt-4"><asp:Button runat="server" id="ProSys" CssClass="btn btn-warning btn-lg" Text="產品序號取號系統" OnClick="ProClick"/></div>
                        <div class="col-6 text-center pt-4"><asp:Button runat="server" id="OthSys" class="btn btn-secondary btn-lg" Text="其他類序號取號系統" onclick="OthClick"/></div>
                    </div>
                </div>
            </form>
       </div>
   </div>
    <script src="js/jquery-3.7.0.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/Script.js"></script>
</body>
</html>
