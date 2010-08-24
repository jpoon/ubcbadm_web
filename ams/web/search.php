<?php

require_once 'savant/Savant3.php';
$tpl = new Savant3();

$content= array(
    array(
        'title' => '',
        'text'  => '<div id="cse-search-results" style="overflow:hidden;"></div>
                    <script type="text/javascript">
                        var googleSearchIframeName = "cse-search-results";
                        var googleSearchFormName = "cse-search-box";
                        var googleSearchFrameWidth = 756;
                        var googleSearchDomain = "www.google.com";
                        var googleSearchPath = "/cse";
                    </script>
                    <script type="text/javascript" src="scripts/googleSearchResults.js"></script>'
    )
);

$tpl->content = $content;
$tpl->display('template.php');

?>
