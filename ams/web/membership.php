<?php

require_once 'savant/Savant3.php';
$tpl = new Savant3();

$content= array(
    array(
        'title' => 'How to Join',
        'text'  => '<img src="imgs/serve.jpg" id="imgServe" alt="UBC Badminton" />
                    <p>Drop by the UBC Badminton Club booth during Club Week to join! Club
                    Week typically occurs in <u>late September</u>.</p>
                    <p>Annual membership costs are listed below:</p>
                    <table border="1">
                        <tr>
                            <td width="150px">New Members:</td>
                            <td width="50px">$50</td>
                        </tr>
                        <tr>
                            <td>Returning Members:</td>
                            <td>$40</td>
                        </tr>
                        <tr>
                            <td>Non-AMS Members:</td>
                            <td>$60</td>
                        </tr>
                    </table>
                    <p>Due to limited gym space, we are only able to accomodate a certain number of members year. 
                    Membership will be given on a first come-first served basis.</p>'
    ),
    array(
        'title' => 'Membership Benefits',
        'text'  => '<p>The UBC Badminton Club offers its members a plethora of benefits including:</p>
                    <ul type="circle">
                        <li>food nights</li>
                        <li>all necessary equipment provided (i.e. racquets available; nylon shuttlecocks provided)</li>
                        <li>feather nights where we supply feather shuttlecocks for play</li>
                        <li>members-only tournament</li>
                        <li>discounted open tournament registration fees</li>
                        <li>recreational and competitive touraments for all levels of play</li>
                        <li>meet new people in a friendly, social atmosphere</li>
                        <li>organized queue board system allowing you to view other players\' levels</li>
                        <li>over 40 gym nights throughout the year</li>
                    </ul>'
    ),
    array(
        'title' => 'Drop-In',
        'text'  => '<p>Not a member? Drop-ins are welcome! Drop-in fees are as follows:</p>
                    <table border="1">
                        <tr>
                            <td width="150px">Monday/Tuesdays:</td>
                            <td width="50px">$5</td>
                        </tr>
                        <tr>
                            <td>Fridays:</td>
                            <td>$8</td>
                            </tr>
                        </table>

                        <p>Please note that the amount of drop-ins will be limited if the gym
                        night is deemed too full. Additionally, there are no drop-ins for the
                        first two weeks of the season.</p>'
    )
);

$tpl->content = $content;
$tpl->display('template.php');

?>
