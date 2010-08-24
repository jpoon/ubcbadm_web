<?php

require_once 'savant/Savant3.php';
$tpl = new Savant3();

$content= array(
    array(
        'title' => 'Doubles League',
        'text'  => '<p>The Us year your super duper awesome UBC Badminton Club executive team 
                    come up with something new and exciting for you! An email was sent out to 
                    all members about this new system, but if you didn\'t get it and/or didn\'t 
                    read it don\'t worry -- Here\'s the information again!
                    </p>
            
                    <p>There are two phases for this system and they are as follows:</p>

                    <p><strong>Phase #1</strong></p>
                    <p>We will be ranking the teams according to their skill level. This system 
                    will be completely fair. The fairness is ensured because there will be no 
                    personal opinion involved. This ranking will be determined by the team\'s 
                    record during this period. There is no elimination during this period.</p>

                    <p><strong>Phase #2</strong></p>
                    <p>The teams will be seeded in the orders of their ranking and a draw system
                    will take place to determine the over all winner.</p>
                    <p><strong>I like the idea! How do I sign up?</strong></p>
                    <p>Simply send an email to ubc.badm@gmail.com with the information below. Only 
                    one person from the pair of you have to email us!</p>
                    <p>Your Full Name<br>
                    Your Partner\'s Full Name<br>
                    Are you playing....Men\'s Doubles/Women\'s Doubles/Mixed Doubles?</p>
                    <p><strong>More questions?</strong></p>
                    <p>Come talk to us during any gym nights! We\'d love to answer any queries you 
                    may have. Or you could contact us through the email address on the bottom right 
                    corner of this page.</p>'
    )
);

$tpl->content = $content;
$tpl->display('template.php');

?>
