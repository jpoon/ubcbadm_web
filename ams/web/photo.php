<?php

require_once 'savant/Savant3.php';
$tpl = new Savant3();

$content= array(
    array(
        'title' => 'Photo Gallery',
        'text'  => 'Badminton Club boasts a year full of events and activities.
                Browse our photo gallery for a glimpse into the club and view the
                variety of our events that we offer.</p>

                <iframe align="center" src="http://www.flickr.com/slideShow/index.gne?user_id=45726881@N03" width="700" height="650" frameBorder="1" scrolling="no"></iframe>'
    )
);

$tpl->content = $content;
$tpl->display('template.php');

?>
