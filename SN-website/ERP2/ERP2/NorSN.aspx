<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NorSN.aspx.cs" Inherits="ERP2.NorSN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Wyer ERP</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
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
    <div class="asideMenu">
      <button class="btn menu">❯</button>
      <div class="menutitle">Menu</div>
      <div class="list">
        <ul class="optionTitle">
          <li id="proSN">產品序號</li>
          <li id="othSN">其他序號</li>
        </ul>
      </div>
    </div>
   <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbr-dark bg-deepblue justify-content-end">
            <div class="container-fluid">
                <a class="navbar-brand" ><img src="images/wyer-right-w.png" id="logo"/></a>
            <div class=""><span class="nav-txt">序號取號系統</span></div>
            <ul class="navbar-nav navbar-right text-white">
                <li class="pr-3"><img src="images/person.png" /><asp:Label runat="server" ID="LoginNM" Text="NO Man"></asp:Label></li>
                <li class=""><asp:ImageButton ID="Logout1" runat="server" OnClick="Logout_Click" ImageUrl="images/arrow-32.png" title="LogOut"  /></li>
            </ul>
            </div>
        </nav>
        <div class="container-fluid">
            <div class="row section">
                <div class="col-6">
                    <!--step 1-->
                    <div class="row step">
                        <div class="col-2 step-left1"><h1>1  </h1></div>
                        <div class="col-3 step-name1"><span>年份</span></div>
                        <div class="col-5 step-content">
                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label> <b class="text-primary">➜</b> <asp:Label ID="YearLabel" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-2 step-right">
                            <img src="images/question.png" alt="" title="取西元年最後二碼.&#10;如：2022 => 22"/>
                            
                        </div>                    
                    </div>
                    <!--step 2-->
                    <div class="row step">
                        <div class="col-2 step-left1"><h1>2</h1></div>
                        <div class="col-3 step-name1"><span>月份</span></div>
                        <div class="col-5 step-content">
                            <asp:Label ID="Label5" runat="server" Text=""></asp:Label> <b class="text-primary">➜</b> <asp:Label ID="MonLabel1" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-2 step-right">
                            <img src="images/question.png" alt="" title="取月份一碼.&#10;如：1 => 1&#10;如：2 => 2&#10;如：3 => 3&#10;如：4 => 4&#10;如：5 => 5&#10;如：6 => 6&#10;如：7 => 7&#10;如：8 => 8&#10;如：9 => 9&#10;如：10 => A&#10;如：11 => B&#10;如：12 => C"/>
                           
                        </div>
                    </div>
                    <!--step 3-->
                    <div class="row step">
                        <div class="col-2 step-left1"><h1>3</h1></div>
                        <div class="col-3 step-name1"><span>流水號</span></div>
                        <div class="col-5 step-content">
                            <asp:TextBox ID="SNtxt" runat="server"  Text="" CssClass="text-right width-14w" AutoPostBack="true" placeholder="請輸入欲取得組數"  OnTextChanged="PUTSN" ></asp:TextBox>   組
                        </div>
                        <div class="col-2 step-right">
                            <img src="images/question.png" alt="" title="請輸入要取得序號組數，要按 Enter系統才會自動計算"/>
                        </div>
                    </div>
                    <!--step 4-->
                    <div class="row step">
                        <div class="col-2 step-left1"><h1>4</h1></div>
                        <div class="col-3 step-name1"><span>備註</span></div>
                        <div class="col-5 step-content">
                            <asp:TextBox ID="TextBox1" runat="server"  Text="" CssClass="h5 width-14w "  placeholder="" TextMode="MultiLine" Rows="4" ></asp:TextBox>   
                        </div>
                        <div class="col-2 step-right">
                            <img src="images/question.png" alt="" title="備註欄位可寫可不寫，字限100以內"/>
                        </div>
                    </div>
                     <div class="col-12 result">
                取得之序號為：<asp:textbox ID="FirstSN" disabled="true" runat="server" Text=""></asp:textbox> 至 <asp:textbox ID="EndSN" disabled="true" runat="server" Text="">  </asp:textbox><botton class="btn btn-danger" id="checkbtn">新增</botton>
                    </div>
                </div>
                <div class="col-6 ">
                    <div class="col-12" id="RecentlyData" runat="server">
                        <h2 id="h2title" runat="server" class="title" >最近10筆存取記錄</h2>
                        <asp:Label ID="lastSN" runat="server" CssClass="text-danger mb-3" ></asp:Label>
                        <div class="border-box scroll-auto">
                            <table id="get-data" class="table">
                                <thead runat="server" id="tablethead">
                                <tr class="table-danger" >
                                    <th scope="col">#</th>
                                    <th scope="col">起始序號</th>
                                    <th scope="col">結束序號</th>
                                    <th scope="col">組數</th>
                                    <th scope="col">日期</th>
                                    <th scope="col">人員</th>
                                 </tr>
                              </thead>
                              <tbody ID="tablebody" runat="server">
                              </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="col-12" id="SearchData" runat="server"> <!--Data Search area-->
                        <div id="Sn_search" runat="server">
                            <h2 class="text-center">序號資料查詢</h2>
                            <div class="row pt-3">
                                <div class="col-3 text-right"><span>年份：</span></div>
                                <div class="col-4"><asp:DropDownList ID="Year_start" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-1 text-center"><span>至</span></div>
                                <div class="col-4"><asp:DropDownList ID="Year_end" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row pt-3">
                                <div class="col-3 text-right"><span>月份：</span></div>
                                <div class="col-4"><asp:DropDownList ID="Mon_start" runat="server" class="form-control">
                                    <asp:ListItem value="0">全部...</asp:ListItem>
                                    <asp:ListItem value="1">一月</asp:ListItem>
                                    <asp:ListItem value="2">二月</asp:ListItem>
                                    <asp:ListItem value="3">三月</asp:ListItem>
                                    <asp:ListItem value="4">四月</asp:ListItem>
                                    <asp:ListItem value="5">五月</asp:ListItem>
                                    <asp:ListItem value="6">六月</asp:ListItem>
                                    <asp:ListItem value="7">七月</asp:ListItem>
                                    <asp:ListItem value="8">八月</asp:ListItem>
                                    <asp:ListItem value="9">九月</asp:ListItem>
                                    <asp:ListItem value="A">十月</asp:ListItem>
                                    <asp:ListItem value="B">十一月</asp:ListItem>
                                    <asp:ListItem value="C">十二月</asp:ListItem>
                                                   </asp:DropDownList></div>
                                <div class="col-1 text-center"><span>至</span></div>
                                <div class="col-4"><asp:DropDownList ID="Mon_end" runat="server" class="form-control">
                                    <asp:ListItem value="0">全部...</asp:ListItem>
                                    <asp:ListItem value="1">一月</asp:ListItem>
                                    <asp:ListItem value="2">二月</asp:ListItem>
                                    <asp:ListItem value="3">三月</asp:ListItem>
                                    <asp:ListItem value="4">四月</asp:ListItem>
                                    <asp:ListItem value="5">五月</asp:ListItem>
                                    <asp:ListItem value="6">六月</asp:ListItem>
                                    <asp:ListItem value="7">七月</asp:ListItem>
                                    <asp:ListItem value="8">八月</asp:ListItem>
                                    <asp:ListItem value="9">九月</asp:ListItem>
                                    <asp:ListItem value="A">十月</asp:ListItem>
                                    <asp:ListItem value="B">十一月</asp:ListItem>
                                    <asp:ListItem value="C">十二月</asp:ListItem>
                                </asp:DropDownList></div>
                            </div>
                            <div class="row pt-3">
                                <div class="col-3 text-right">建立人員：</div>
                                <div class="col-6"><asp:DropDownList ID="member" runat="server" class="form-control">
                                                   </asp:DropDownList></div>
                                <div class="col-3 text-center"><asp:Button id="Search" runat="server" Text="查詢" CssClass="btn btn-primary" onClick="Search_click"/></div>
                                <div class="col-12"><hr /></div>
                            </div>
                            </div>
                        <div class="row pt3" runat="server" id="psmodisection">
                            <div class="col-12 row">
                                <div class="col-12"><h2 class="text-center font-weight-bolder pt-2">修改資料</h2></div>
                            </div>
                            <div class="row">
                                <dir class="col-6">起始序號：<input type="text" id="Label1" class="text-left" disabled/></dir>
                                <dir class="col-6">結束序號：<input type="text" id="Label2" class="text-left" disabled/></dir>
                            </div>
                            <div class="row">
                                <dir class="col-6">日期：<input type="text" id="Label4" class="text-left" disabled /></dir>
                                <dir class="col-6">建立人員：<input type="text" id="Label6" class="text-left" disabled /><input id="hideindex" type="hidden" /></dir>
                            </div>
                            <div class="row">
                                <dir class="col-12">備註：<textarea type="text" id="textbox2" class="form-control text-primary" aria-multiline="true"  rows="3" title="字數100字"/></textarea>
                                    <p class="text-right text-danger h6">字數：<span id="wordcounter">250</span></p>
                                </dir>
                            </div>
                            <div class="row">
                                <dir class="col-6 text-center" ><button id="ModiOK" class="btn btn-danger" />Save</button></dir>
                                <dir class="col-6 text-center"><button  id="ModiNO" class="btn btn-secondary" />Cancel</button></dir>
                            </div>
                            <div class="row">
                                <div class="col-12"><hr /></div>
                            </div>
                        </div>
                        <div class="row"> <!--Search out Data-->
                            <div class="col-12">
                                <div class="border-box scroll-auto">
                                    <table id="Search-data" class="table">
                                        <thead runat="server" id="Thead1">
                                        <tr class="table-danger" >
                                            <th scope="col" class="text-center">備註</th>
                                            <th scope="col">起始序號</th>
                                            <th scope="col">起始序號</th>
                                            <th scope="col">組數</th>
                                            <th scope="col">日期</th>
                                            <th scope="col">人員</th>
                                         </tr>
                                      </thead>
                                      <tbody ID="Tbody2" runat="server">
                                      </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row pt-3">
                        <div class="col-6 text-center"><asp:Button id="BTNALLLASTSN" class="btn btn-info" OnCommand="Get_SQLDATA"  CommandName="Click1" CommandArgument="所有序號查詢/備註修改" runat="server" text="所有序號查詢/備註修改"/></div>
                        <div class="col-6 text-center"><asp:Button id="BTN10SN" s class="btn  btn-info" OnCommand="Get_SQLDATA"  CommandName="Click2" CommandArgument="最近10筆存取記錄" runat="server" text="最近10筆存取記錄"/></div>
                    </div>
                </div>
            </div>
        </div>
       <div id="msgbox" runat="server">
           <div class="ErrBox">
               <div><h6 class="pl-2 pr-2 pt-2 font-weight-bolder text-primary">提示視窗</h6></div>
               <hr />
               <div><h4 class="pl-3 pr-3 pt-3">請確認資料是否正確! </h4></div>
               <hr />
               <div class="row pt-2 pb-2">
                   <div class="col-12 text-center"><asp:Button ID="Button3" runat="server" Text="確定" class="btn ml-2 btn-lg btn-danger" onclick="Modibtn_Click"/></div>
               </div>
           </div>
           <div class="QuestBox">
               <div><h6 class="pl-3 pr-3 pt-3 font-weight-bolder text-primary">提示視窗</h6></div>
               <hr />
               <div><h4 class="pl-3 pr-3 pt-3">請確認資料是否正確!<br /> 是否新增此筆資料?</h4></div>
               <hr />
               <div class="row pt-2 pb-2">
                   <div class="col-6 text-center"><asp:Button ID="Button1" runat="server" Text="確定" class="btn ml-2 btn-lg btn-danger" onclick="OKbtn_Click"/></div>
                   <div class="col-6 text-center"><asp:Button ID="Button2" runat="server" Text="取消" class="btn ml-2 btn-lg btn-info" onclick="Modibtn_Click"/></div>
               </div>
           </div>
       </div>
     </form>
    <script src="js/jquery-3.7.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.6/umd/popper.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script charset="big5" src="js/Script.js"></script>
</body>
</html>
