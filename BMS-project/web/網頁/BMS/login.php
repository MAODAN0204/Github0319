<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
</head>
<body>
    <?php
    
    ?>
    <div class="row">
                <div class="col-12 mt-5 mb-3">
                    <h1 class="text-center">Login</h1></div>
            </div>
            <form action="login.php" method="post">
                <div class="row mt-4">
                    <div class="col-md-4 col-2"></div>
                    <div class="col-md-4 col-5"><h3>帳號</h3></div>
                    <div class="col-md-4 col-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-2"></div>
                    <div class="col-md-4 col-8 "><input class="form-control" type="text" id="username" placeholder="輸入帳號"></div>
                    <div class="col-md-4 col-2"></div>
                </div>
                <div class="row mt-4">
                    <div class="col-md-4 col-2"></div>
                    <div class="col-md-4 col-8"><h3>密碼</h3></div>
                    <div class="col-md-4 col-2"></div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-2"></div>
                    <div class="col-md-4 col-8"><input class="form-control" type="password" id="psd" placeholder="輸入密碼"></div>
                    <div class="col-md-4 col-2"></div>
                </div>

                <div class="row mt-5 mb-2">
                    <div class="col-6 text-right pr-3"><button type="reset" class="btn btn-lg btn-dark">清除</button></div>
                    <div class="col-6 text-left pl-5"><button type="submit" class="btn btn-lg btn-secondary">登入</button></div>
                </div>
		<div class="row mt-3 mb-5">
		    <div class="col-12"><h5 class="text-info">請使用登入網域(電腦)的使用者帳號及密碼登入</h5></div>
		</div>
            </form>
</body>