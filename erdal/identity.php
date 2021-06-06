<?php
	$clientname = "";
	$clientname2 = "";
	$windowshash = "";
	$windowshash = "";

ignore_user_abort(true);
set_time_limit(0);

if(isset($_GET['clientname'])){
    $clientname = $_GET['clientname'];
}

if(isset($_GET['windowshash'])){
    $windowshash = $_GET['windowshash'];
}

$referer = ""; 

if(isset($_SERVER['HTTP_REFERER'])){
    $referer = $_SERVER['HTTP_REFERER'];
}  
if(isset($_SERVER['HTTP_REFERER'])){
    $referer = $_SERVER['HTTP_REFERER'];
}
  
if ($referer == "Client_57684286")   
{
	
}
else
{
echo "Bu sayfaya erisim yasak!";
exit();
}


include "salt.php";
$method = 'aes-256-cbc';

$key = substr(hash('sha256', $password, true), 0, 32);

$iv = chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0);


$clientname2 = openssl_decrypt(base64_decode($clientname), $method, $key, OPENSSL_RAW_DATA, $iv);

$encrypted = base64_encode(openssl_encrypt("yes", $method, $key, OPENSSL_RAW_DATA, $iv));

$encrypted2 = base64_encode(openssl_encrypt("no", $method, $key, OPENSSL_RAW_DATA, $iv));

if(strlen($windowshash) != 45){
echo $encrypted;
	exit();
}

if(substr_count($windowshash, "j") != 3){
echo $encrypted;
	exit();
}

if(substr_count($windowshash, "k") != 6){
echo $encrypted;
	exit();
}

if(substr_count($windowshash, "2") != 1){
echo $encrypted;
	exit();
}

if(substr_count($windowshash, "t") != 9){
echo $encrypted;
	exit();
}

if(substr_count($windowshash, "z") != 4){
echo $encrypted;
	exit();
}

if(substr_count($windowshash, "*") != 5){
echo $encrypted;
	exit();
}

if(substr_count($windowshash, "/") != 2){
echo $encrypted;
	exit();
}

if(substr_count($windowshash, "!") != 8){
echo $encrypted;
	exit();
}

if(substr_count($windowshash, "5") != 7){
echo $encrypted;
	exit();
}

include "baglanti.php";
$query = "SELECT * FROM `serials` WHERE `clientname` = '$clientname2'";

$banquery = "SELECT * FROM `serials` WHERE `clientname` = '$clientname2' AND `banned` = '1'";

$query2 = "UPDATE `serials` SET `clientupdate`='1' WHERE `clientname` = '$clientname2'";
$query3 = "UPDATE `serials` SET `userhash`='$windowshash' WHERE `clientname` = '$clientname2'";

			
	$result = mysqli_query($conn, $query);
	$result2 = mysqli_query($conn, $banquery);

	if(mysqli_num_rows($result2) > 0){
		echo $encrypted;
		}
	else
	{
		echo $encrypted2;
	}
		
	if(mysqli_num_rows($result) > 0){
	if($conn->query($query2) === TRUE){
	}
	}
	
	if($conn->query($query3) === TRUE){
	}

?>
