<html>
<head>
<title>Task</title>
</head>
<body>
<p>This text can understand only <i>TARDIS-browser</i>:</p><br>
<?php
if (isset($_SERVER['HTTP_USER_AGENT']))
{
        if ($_SERVER['HTTP_USER_AGENT'] == "TARDIS-browser")
        {
                echo "<b>CRG{smarter_than_the_owner}</b>";
        }
        else
        {
                echo "<b>/╟╥█1▼▓☼‰Q-╙¶€</b>";
        }
}
else
{
        echo "<b>/╟╥█1▼▓☼‰Q-╙¶€</b>";
}
?>
</body>
</html>

