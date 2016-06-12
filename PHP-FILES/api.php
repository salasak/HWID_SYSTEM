<?php
include("includes/odb.php");
$type = $_GET['type'];
if (empty($type)) {
  header('Location: https://google.com');
}
if (htmlspecialchars($type) == 'login') {
  $username = $_GET['username'];
  $password = $_GET['password'];
  $hwid = $_GET['hwid'];

  $SQLSelect = $odb -> prepare("SELECT * FROM `members` WHERE `username` = :username");
  $SQLSelect -> execute(array(':username' => $username));
  while ($show = $SQLSelect -> fetch(PDO::FETCH_ASSOC))
  {
  $passwordHash = $show['password'];
  $dbHwid = $show['hwid'];
  }

  if (empty($username) || empty($password) || empty($hwid))
  {
  die('Please fill in all fields. 0x02');
  }

  $SQLCheckLogin = $odb -> prepare("SELECT COUNT(*) FROM `members` WHERE `username` = :username");
  $passwordVerified = password_verify($password, $passwordHash);
  $SQLCheckLogin -> execute(array(':username' => $username));
  $countLogin = $SQLCheckLogin -> fetchColumn(0);
  if (!password_verify($password, $passwordHash)) {
    die('Username or password are invalid. 0x03');
  }

  $SQL = $odb -> prepare("SELECT `status` FROM `members` WHERE `username` = :username");
  $SQL -> execute(array(':username' => $username));
  $status = $SQL -> fetchColumn(0);
  if ($status == 1)
  {
    die('You are banned. Reason: 0x04');
  }

  if ($hwid != $dbHwid)
  {
  die('HWID do not match 0x01');
  }

  die('Successful login 0x05');

}

if (htmlspecialchars($type) == 'register') {
  $username = $_GET['username'];
  $password = $_GET['password'];
  $rpassword = $_GET['rpassword'];
  $email = $_GET['email'];
  $hwid = $_GET['hwid'];

  if (empty($username) || empty($password) || empty($rpassword) || empty($email) || empty($hwid))
  {
  die('Please fill in all the fields. 0x1');
  }

  if (!ctype_alnum($username) || strlen($username) < 4 || strlen($username) > 15)
  {
  die('Username must be alphanumberic and 4-15 characters in length. 0x3');
  }

  $SQL = $odb -> prepare("SELECT COUNT(*) FROM `members` WHERE `username` = :username");
  $SQL -> execute(array(':username' => $username));
  $countUser = $SQL -> fetchColumn(0);
  if ($countUser > 0)
  {
  die('Username is already in use. 0x2');
  }

  if (!filter_var($email, FILTER_VALIDATE_EMAIL))
  {
  die('Email is not a valid email address. 0x4');
  }

  if ($password != $rpassword)
  {
  die('Passwords do not match. 0x5');
  }

  $insertUser = $odb -> prepare("INSERT INTO `members` VALUES(NULL, :username, :password, :hwid, :email, :ip, 0, 0)");
  $insertUser -> execute(array(
  ':username' => htmlspecialchars($username),
  ':password' => password_hash($password, PASSWORD_BCRYPT),
  ':hwid' => htmlspecialchars($hwid),
  ':email' => htmlspecialchars($email),
  ':ip' => htmlspecialchars($_SERVER['REMOTE_ADDR'])
  ));
  die('User registered. 0x6');
}

?>
