<?php
	$macid = "";
	$macid2 = "";
	ignore_user_abort(true);
	set_time_limit(0);
if(isset($_GET['macid'])){
    $macid = $_GET['macid'];
}

include "salt.php";
$method = 'aes-256-cbc';

$key = substr(hash('sha256', $password, true), 0, 32);

$iv = chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0);


$macid2 = openssl_decrypt(base64_decode($macid), $method, $key, OPENSSL_RAW_DATA, $iv);

sleep(25);

    include "baglanti.php";
	$onaykapa = "UPDATE serials SET onaylandi='0' WHERE macid='$macid2'";

if($conn->query($onaykapa) === TRUE){
	$conn->close();
	exit();
}
?>