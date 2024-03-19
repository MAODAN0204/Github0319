<?php
include_once "tadtools_header.php";
include_once "jquery.php";

class tinymap
{
    public $id;
    public $x;
    public $y;
    public $title;
    public $zoom;
    public $show_jquery;

    //�غc���
    public function tinymap($id, $x, $y, $title, $zoom = 15, $show_jquery = true)
    {
        $this->id          = $id;
        $this->x           = $x;
        $this->y           = $y;
        $this->zoom        = $zoom;
        $this->title       = $title;
        $this->show_jquery = $show_jquery;
    }

    //���͸��|�u��
    public function render()
    {
        $jquery = ($this->show_jquery) ? get_jquery() : "";

        $main = "
    $jquery
    <script type='text/javascript' src='http://maps.google.com/maps/api/js?sensor=false'></script>
    <script type='text/javascript' src='" . TADTOOLS_URL . "/tinymap/jquery.tinyMap-2.5.2.js'></script>

    <script type='text/javascript'>
     $(document).ready(function()
     {
       $('{$this->id}').tinyMap({
          center: {x: '{$this->x}', y: '{$this->y}'},
          zoom: {$this->zoom},
          marker: [
            {addr: '{$this->x}, {$this->y}', text: '{$this->title}'}
          ]
      });
     })
    </script>";

        return $main;
    }
}
