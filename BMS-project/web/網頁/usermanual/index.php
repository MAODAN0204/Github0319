<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>User Manual</title>
    <link rel="stylesheet" type="text/css" href="./slick/slick.css">
    <link rel="stylesheet" type="text/css" href="./slick/slick-theme.css">
    <link rel="stylesheet" type="text/css" href="style.css" >
</head>
<body>
    <div id="page">
        <div class="row">
            <div class="column small-12 small-centered">
                <div class="slider slider-single">
                    <?php
                    for($i=1;$i<22;$i++)
                    { 
                        echo("<div><h3><img src='images/usermanual-tw-".$i.".jpg'></h3></div> ");
                    } ?>
                </div>
                <div class="slider slider-nav">
                    <?php
                    for($i=1;$i<22;$i++)
                    { 
                        echo("<div><h3><span><img src='images/usermanual-tw-".$i.".jpg'></span></h3></div> ");
                    } ?>
                </div>
            </div>
        </div>
    </div>
    <script src="https://code.jquery.com/jquery-2.2.0.min.js" type="text/javascript"></script>
    <script src="./slick/slick.js" type="text/javascript" charset="utf-8"></script>
    <script src="model.js" type="text/javascript" charset="utf-8"></script>
</body>
</html>