<?php

require_once 'savant/Savant3.php';
$tpl = new Savant3();

$content= array(
    array(
        'title' => 'About Us',
        'text'  => '<p>The University of British Columbia (UBC) Badminton Club is a registered 
                    AMS club at the University of British Columbia in Vancouver, British 
                    Columbia, Canada. The club is wholly administered and organized by a group
                    of passionate badminton players who are commited to promoting and
                    encouraging the sport of badminton.</p>'
    ),
    array(
        'title' => 'Executives',
        'text'  => '<img src="imgs/execs.jpg" id="img07Execs" alt="2007 Execs" />

                    <script type="text/javascript">
                      $(document).ready(function(){
                        $("#accordion").accordion({
                            autoHeight:false,
                        });
                      });
                    </script>

                    <div id="accordion" style="width:300px;">
                        <p><a href="#">President</a></p>
                        <div>
                            <ul><li>Jason Poon</li></ul>
                        </div>
                        <p><a href="#">Vice-President</a></p>
                        <div>
                            <ul><li>Bernard Cheung</li></ul></div>
                        <p><a href="#">Finance</a></p>
                        <div>
                            <ul><li>Nina Kao</li></ul>
                        </div>
                        <p><a href="#">Membership</a></p>
                        <div>
                            <ul><li>Patrick Shyong</li></ul>
                        </div>
                        <p><a href="#">Public Relations</a></p>
                        <div>
                            <ul><li>Andrew Li</li>
                                <li>Kiki Chan</li>
                                <li>Stephanie Lee</li></ul>
                        </div>
                        <p><a href="#">Special Events</a></p>
                        <div>
                            <ul><li>Andy Chow</li>
                                <li>Peter Liang</li>
                                <li>Zonghe Chua</li></ul>
                        </div>
                        <p><a href="#">Operations</a></p>
                        <div>
                            <ul><li>Alvin Tse</li>
                                <li>Justin Lew</li>
                                <li>Michelle Tran</li></ul>
                        </div>
                    </div>'
    )
);

$tpl->content = $content;
$tpl->display('template.php');

?>
