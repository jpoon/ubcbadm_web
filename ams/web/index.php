<?php

require_once 'savant/Savant3.php';
$tpl = new Savant3();

$content= array(
    array(
        'title'  => 'Welcome',
        'text'   => '<p>If you\'ve just been introduced to the sport or you feel like
                your badminton racquet is an extension of your arm, come out, and join us
                at one of our many gyms nights for hours of badminton fun! Whether your school
                budget prevents you from purchasing a badminton racquet or your collection of
                badminton racquets cost more than your tuition, we have a place for you!</p>

                <img id="imgFloor" src="imgs/floor.jpg" alt="UBC Badminton Club" />
            
                <p>Players of all skill level are welcome to join so there is no need to fret
                about finding someone to play with or against.</p>

                <p>Check out our <a href="schedule.php">Gym Schedule</a> or
                <a href="membership.php">Membership</a> pages for more information on when to 
                play and how to join.</p>'
    )
);

$announcements= array(
    array(
        'title' => 'Term 2 Gym Times',
        'date' => 'Ongoing for 2nd Term 2010',
        'text' => 'For the updated term 2 gym times, please refer to this <a href="schedule.php">page</a>'
    ),
    array(
        'title' => 'Doubles League',
        'date' => 'Ongoing for 2nd Term 2010',
        'text' => 'What\'s it about? Click <a href="doublesleague.php">here</a> for more information!'
    ),
    array(
        'title' => 'Want to join the Exec Team for 2010-11?',
        'date' => '',
        'text' => 'Talk to one of the current executives for more details.'
    ),
);

$tpl->content = $content;
$tpl->announcements = $announcements;
$tpl->display('template.php');

?>
