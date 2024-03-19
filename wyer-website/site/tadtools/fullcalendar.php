<?php
include_once "tadtools_header.php";

class fullcalendar
{

    public $js_parameter   = array();
    public $json_parameter = array();
    public $quotation      = array();

    //建構函數
    public function fullcalendar()
    {

    }

    public function add_js_parameter($key = '', $val = '', $quotation = true)
    {
        $this->js_parameter[$key] = $val;
        $this->quotation[$key]    = $quotation;
    }

    public function add_json_parameter($key = '', $val = '')
    {
        $this->json_parameter[$key] = $val;
    }

    //產生月曆
    public function render($selector = '#calendar', $json_file = '')
    {
        global $xoTheme;

        $jquery = get_jquery();

        if ($xoTheme) {
            $xoTheme->addStylesheet('modules/tadtools/fullcalendar/fullcalendar.css');
            $xoTheme->addScript('modules/tadtools/fullcalendar/fullcalendar.js');
            $fullcalendar = '';
        } else {
            $fullcalendar = "
            $jquery
            <link rel='stylesheet' type='text/css' href='" . TADTOOLS_URL . "/fullcalendar/fullcalendar.css'>
            <script src='" . TADTOOLS_URL . "/fullcalendar/fullcalendar.js' type='text/javascript'></script>";
        }

        $js_parameter = "";
        if (!empty($this->js_parameter)) {
            foreach ($this->js_parameter as $key => $value) {
                $js_parameter .= $this->quotation[$key] ? "{$key}: '{$value}'," : "{$key}: {$value},";
            }
        }

        $get_event = "";
        if ($json_file) {
            $json_parameter = "start: start.getTime(), end: end.getTime(), ";
            if (!empty($this->json_parameter)) {
                foreach ($this->json_parameter as $key => $value) {
                    $json_parameter_arr[] = "{$key}: '{$value}'";
                }
                $json_parameter .= implode(',', $json_parameter_arr);
            }
            $get_event = "
            events: function(start, end, callback) {
                \$.getJSON('$json_file',
                {
                  {$json_parameter}
                },
                function(result) {
                  callback(result);
                });
              },
            ";
        }

        $fullcalendar .= "<script type='text/javascript'>
          \$(function() {
              \$('{$selector}').fullCalendar({
                buttonText:{
                  today:'" . TADTOOLS_CALENDAR_TODAY . "',
                  prev:'" . TADTOOLS_CALENDAR_PREV_MONTH . "',
                  next:'" . TADTOOLS_CALENDAR_NEXT_MONTH . "'
                },
                {$js_parameter}
                {$get_event}
                header: {
                  left: 'prev,next today',
                  center: 'title',
                  right: ''
                }
              })
          });
        </script>";
        return $fullcalendar;
    }
}

/*
if(!file_exists(XOOPS_ROOT_PATH."/modules/tadtools/fullcalendar.php")){
redirect_header("http://www.tad0616.net/modules/tad_uploader/index.php?of_cat_sn=50",3, _TAD_NEED_TADTOOLS);
}
include_once XOOPS_ROOT_PATH."/modules/tadtools/fullcalendar.php";
$fullcalendar=new fullcalendar();
$fullcalendar->add_js_parameter('year', 1973);
$fullcalendar->add_js_parameter('month', 6);
$fullcalendar->add_js_parameter('date', 16);
$fullcalendar->add_json_parameter('WebID', $WebID);
$fullcalendar->add_json_parameter('CateID', $CateID);
$fullcalendar_code=$fullcalendar->render('#calendar', 'get_event.php');
$xoopsTpl->assign('fullcalendar_code',$fullcalendar_code);
 */
