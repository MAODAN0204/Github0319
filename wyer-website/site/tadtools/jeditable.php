<?php
include_once "tadtools_header.php";
include_once "jquery.php";

class jeditable
{
    public $cols;
    public $show_jquery;

    //�غc���
    public function jeditable($show_jquery = true)
    {
        $this->show_jquery = $show_jquery;
    }

    //�]�w��r��� $submitdata="{'sn':$the_sn}
    public function setTextCol($selector, $file, $width = '100%', $height = '12px', $submitdata = "", $tooltip = "")
    {
        $submitdata_set = (empty($submitdata)) ? "" : "submitdata:$submitdata,";
        $this->cols[]   = "
        $('$selector').editable('$file', {
          type : 'text',
          indicator : 'Saving...',
          width: '$width',
          height: '$height',
          $submitdata_set
          onblur:'submit',
          event: 'click',
          style   : 'display: inline',
          placeholder : '{$tooltip}'
        });";
    }

    //�]�w�j�q��r��� $submitdata="{'sn':$the_sn}
    public function setTextAreaCol($selector, $file, $width = '100%', $height = '50px', $submitdata = "", $tooltip = "")
    {
        $submitdata_set = (empty($submitdata)) ? "" : "submitdata:$submitdata,";
        $this->cols[]   = "
        $('$selector').editable('$file', {
          type : 'textarea',
          indicator : 'Saving...',
          width: '$width',
          height: '$height',
          $submitdata_set
          onblur:'submit',
          event: 'click',
          style   : 'display: inline',
          placeholder : '{$tooltip}'
        });";
    }

    //�]�w�U����� $submitdata="{'sn':$the_sn},$data="{'�k��':'�k��' , '�k��':'�k��'}";
    public function setSelectCol($selector, $file, $data = '', $submitdata = "", $tooltip = "")
    {
        $submitdata_set = (empty($submitdata)) ? "" : "submitdata:$submitdata,";
        $this->cols[]   = "
        $('$selector').editable('$file', {
          type : 'select',
          indicator : 'Saving...',
          data   : \"{$data}\",
          $submitdata_set
          onblur:'submit',
          event: 'click',
          style   : 'display: inline',
          placeholder : '{$tooltip}'
        });";
    }

    //���͸��|�u��
    public function render()
    {
        global $xoTheme;

        if (is_array($this->cols)) {
            $all_col = implode("\n", $this->cols);
        }
        $jquery = ($this->show_jquery) ? get_jquery() : "";

        if ($xoTheme) {
            $xoTheme->addScript('modules/tadtools/jeditable/jquery.jeditable.mini.js');

            $xoTheme->addScript('', null, "
              (function(\$){
                \$(document).ready(function(){
                  {$all_col}
                });
              })(jQuery);
            ");
        } else {

            $main = "
            $jquery
            <script src='" . TADTOOLS_URL . "/jeditable/jquery.jeditable.mini.js' type='text/javascript' language='JavaScript'></script>
            <script type='text/javascript'>
             $(document).ready(function()
             {
               $all_col
             })
            </script>";
            return $main;
        }
    }
}

/*
include_once XOOPS_ROOT_PATH."/modules/tadtools/jeditable.php";
$file="save.php";
$jeditable = new jeditable();
$jeditable->setTextCol("#candidate_note",$file,'140px','12px',"{'vote_sn':$vote_sn,'candidate_id':'$candidate_id','op' : 'save'}","�s��Ƶ�");
$jeditable->setTextAreaCol("#id",$file,'140px','12px',"{'sn':$sn,'op' : 'save'}","�I���s��");
$jeditable->setSelectCol("#id",$file,"{'boy':'�k��' , 'girl':'�k��' , 'selected':'girl'}","{'sn' : $sn , 'op' : 'save'}","�I���s��");
$jeditable_set=$jeditable->render();

<?php
include "header.php";
$sql="update ".$xoopsDB->prefix("vote_candidate")." set `candidate_note`='{$_POST['value']}' where vote_sn='{$_POST['vote_sn']}' and candidate_id='{$_POST['candidate_id']}'";
$xoopsDB->queryF($sql);
echo $_POST['value'];
?>

 */
