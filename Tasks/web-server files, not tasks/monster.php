<?php
	setcookie("Has a cookie?", "dHJ1ZQ==", time() + 12*60*60); //day
?>
<!DOCTYPE html>
<html>
<head>
	<title>Monster</title>
</head>
<body>
<style>
	img
	{
		margin-left: 35%;
		margin-top: 10%;
	}
</style>
<?php
if ($_COOKIE['Has a cookie?'] == "dHJ1ZQ==")
{
	echo "YOU CAN'T STOP US! GIVE US RAISINS AND FLOUR AND EVERITHING WILL BE GOOD!!!"
	echo "<img src=\"../images/cookie-monster.jpg\" alt=\"monster\">";
	
}
else if ($_COOKIE['Has a cookie?'] == "ZmFsc2U=")
{
	echo "<p>OKAY, WE ARE GOING HOME! BUT CRG{turn_it_back_pls}</p>";
	echo "<src=\"../images/sad-monster.jpg\" alt=\"monster\">";
}
?>
</body>
</html>
