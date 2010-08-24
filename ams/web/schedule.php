<?php

require_once 'savant/Savant3.php';
$tpl = new Savant3();

$content= array(
    array(
        'title' => 'Gym Schedule',
        'text'  => '<img src="imgs/osbourne.jpg" alt="Osbourne Gym" id="imgOsbourne" />

<script type="text/javascript">
    $(document).ready(function(){
        $("#open_location").click(function() { 
            $("#location").dialog({ 
                bgiframe: true,
                modal: true,
                height: 550,
                width: 500,
                buttons: {
                    Close: function() {
                        $(this).dialog("close");
                    }
                }
            });
            $("#location").dialog("open"); 
        });
    });
</script>

                    <div id="location" style="display:none;" title="Robert F. Osbourne Center (Gym A) - Location">
                        <p>6108 Thunderbird Boulevard<br/>
                           Vancouver, BC, Canada</p>
                        <center>
                        <iframe width="425" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="http://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q=robert+osbourne+gym&amp;sll=49.26011,-123.245994&amp;sspn=0.001057,0.002406&amp;ie=UTF8&amp;hq=robert+osbourne+gym&amp;hnear=&amp;ll=49.260199,-123.244858&amp;spn=0.006295,0.006295&amp;output=embed"></iframe><br /><small><a href="http://maps.google.com/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q=robert+osbourne+gym&amp;sll=49.26011,-123.245994&amp;sspn=0.001057,0.002406&amp;ie=UTF8&amp;hq=robert+osbourne+gym&amp;hnear=&amp;ll=49.260199,-123.244858&amp;spn=0.006295,0.006295" style="color:#0000FF;text-align:left">View Larger Map</a></small></center>
                    </div>

                    <p>Gym nights are held at the "Robert F. Osborne Center - Gym A"
                    <a id="open_location" href="#">6108 Thunderbird Boulevard</a>,
                    next to the Thunderbird Sports Center.</p>

                    <table border="1" cellpadding="5px">
                    <tr>
                        <td rowspan="2" valign="top" width="50px">Term 1</td>
                        <td width="50px">Tuesday</td>
                        <td>4:00 - 6:00 PM</td>
                    </tr>
                    <tr>
                        <td>Friday</td>
                        <td>6:30 - 11:00 PM</td>
                    </tr>
                    <tr>
                        <td rowspan="2" valign="top" width="50px">Term 2</td>
                        <td width="50px">Tuesday</td>
                        <td>5:30 - 7:30 PM</td>
                    </tr>
                    <tr>
                        <td>Friday</td>
                        <td>7:00 - 11:00 PM</td>
                    </tr>
                    </table>'
    ),
    array(
        'title' => 'Event Calendar',
        'text'  => '<iframe src="http://www.google.com/calendar/embed?showTitle=0&amp;height=450&amp;wkst=1&amp;bgcolor=%23FFFFFF&amp;src=ra82akobth6fn1qg7pbgnlj5vg%40group.calendar.google.com&amp;color=%235229A3&amp;ctz=America%2FVancouver" style=" border-width:0 " width="755" height="450" frameborder="0" scrolling="no"></iframe>'
    )
);

$tpl->content = $content;
$tpl->display('template.php');

?>
