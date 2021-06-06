<?php
	$macid = "";
	$macid2 = "";
if(isset($_POST['macid'])){
    $macid = $_POST['macid'];
}

ignore_user_abort(true);
	set_time_limit(0);
	
$referer = ""; 
if(isset($_SERVER['HTTP_REFERER'])){
    $referer = $_SERVER['HTTP_REFERER'];
}  

if ($referer == "Client_57684286")   
{
}
else
{
echo "Buraya erisim yasak!";
exit();
}

include "salt.php";
$method = 'aes-256-cbc';

$key = substr(hash('sha256', $password, true), 0, 32);

$iv = chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0);


$macid2 = openssl_decrypt(base64_decode($macid), $method, $key, OPENSSL_RAW_DATA, $iv);


    include "baglanti.php";
	$onaykapa = "UPDATE serials SET onaylandi='0' WHERE macid='$macid2'";

if($conn->query($onaykapa) === TRUE){
	$conn->close();
}
?>