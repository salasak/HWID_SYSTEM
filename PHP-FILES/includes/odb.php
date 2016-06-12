<?php
define('DB_HOST', 'localhost');
define('DB_NAME', 'database');
define('DB_USERNAME', 'database');
define('DB_PASSWORD', 'database');
define('ERRMSG', 'ERROR: SOMETHING IS WRONG!');

try {
$odb = new PDO('mysql:host=' . DB_HOST . ';dbname=' . DB_NAME, DB_USERNAME, DB_PASSWORD);
}
catch( PDOException $Exception ) {
	die(ERRMSG);
}

?>
