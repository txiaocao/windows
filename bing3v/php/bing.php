<?php
define("DS", DIRECTORY_SEPARATOR);

$url = "http://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&nc=1428896145111&pid=hp&video=1";
$json = file_get_contents($url);
$re = '/"\/\/(.*?).mp4"|"\/\/(.*?).jpg"|"\/\/(.*?).png"/';
$arr = array();
preg_match_all($re, $json, $arr);

foreach ($arr[0] as $key => $value) {
        $value = str_replace('"', "", $value);
        $value = "http:" .  $value;
        $filename = explode("/",$value);
        $filename = $filename[count($filename)-1];
        $path = getcwd().DS."data".DS.date("Y-m-d").".".$filename;
        if(!is_file($path)){ 
            $data = file_get_contents($value);
            file_put_contents($path, $data);
        }else{ 
            echo "exists is ".$filename.PHP_EOL;
        }
        
}
echo 'end'.PHP_EOL;
