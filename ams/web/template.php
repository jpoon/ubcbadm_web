<?php
require_once ('include/html_head.php');
?>
<body>
<div id="container">
    <?php
    require_once ('include/header.php');
    ?>

    <div id="contentContainer">
        <?php
        require_once ('include/navigation.php');
    	?>	

        <div id="content">

            <?php if (isset($this->announcements)): ?>
            <div id="centercontent">
            <?php endif; ?>

                <?php if (is_array($this->content)): ?>
                    <?php foreach ($this->content as $key => $val): ?>
                        <h1><?php echo $val['title']; ?></h1>
                        <p><?php echo $val['text']; ?></p>
                    <?php endforeach; ?>
                <?php endif; ?>

            <?php if (isset($this->announcements)): ?>
            </div>
            <?php endif; ?>

            <?php if (isset($this->announcements)): ?>
            <script type="text/javascript">
                $(document).ready(function(){
                    $("#rightcontent").show("drop", { direction: "down" }, 1500);
                });
            </script>
            <div id="rightcontent">
            <?php if (is_array($this->announcements)): ?>
                    <?php foreach ($this->announcements as $key => $val): ?>
                        <h2><?php echo $val['title']; ?></h2>
                        <span class="date"><?php echo $val['date']; ?></span>
                        <?php echo $val['text']; ?>
                    <?php endforeach; ?>
                <?php endif; ?>
            </div>
            <?php endif; ?>

      </div>
      <div class="clear"></div>
    </div>
    <?php
    require_once ('include/footer.php');
    ?>

</div>
</body>
</html>
