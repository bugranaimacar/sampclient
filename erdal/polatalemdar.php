<?php
	$macid = "";
	$cpuid = "";
	$hddserial = "";
	$macid2 = "";
	$cpuid2 = "";
	$hddserial2 = "";
if(isset($_GET['username'])){
    $username = $_GET['username'];
}

if(isset($_GET['macid'])){
    $macid = $_GET['macid'];
}

if(isset($_GET['cpuid'])){
    $cpuid = $_GET['cpuid'];
}

if(isset($_GET['hddserial'])){
    $hddserial = $_GET['hddserial'];
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
echo "Buraya erisim yasak!";
exit();
}


include "salt.php";
$method = 'aes-256-cbc';

$key = substr(hash('sha256', $password, true), 0, 32);

$iv = chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0);





	$macid2 = openssl_decrypt(base64_decode($macid), $method, $key, OPENSSL_RAW_DATA, $iv);
	$cpuid2 = openssl_decrypt(base64_decode($cpuid), $method, $key, OPENSSL_RAW_DATA, $iv);
	$hddserial2 = openssl_decrypt(base64_decode($hddserial), $method, $key, OPENSSL_RAW_DATA, $iv);
	
	
	
	include "baglanti.php";
	$query = "SELECT * FROM `serials` WHERE `macid` = '$macid2' AND `banned` = '1'";		
	$result = mysqli_query($conn, $query);
	$query2 = "SELECT * FROM `serials` WHERE `cpuid` = '$cpuid2' AND `banned` = '1'";		
	$result2 = mysqli_query($conn, $query2);
	$query3 = "SELECT * FROM `serials` WHERE `hddserial` = '$hddserial2' AND `banned` = '1'";		
	$result3 = mysqli_query($conn, $query3);

$encrypted = base64_encode(openssl_encrypt("yes", $method, $key, OPENSSL_RAW_DATA, $iv));


$encrypted2 = base64_encode(openssl_encrypt("no", $method, $key, OPENSSL_RAW_DATA, $iv));

    if($macid2 = "")
	{
		echo $encrypted2;
		exit();
	}

	if(mysqli_num_rows($result) > 0){
    echo $encrypted;
	}
	else
	{
		echo $encrypted2;
	}
	exit();
	
	if(mysqli_num_rows($result3) > 0){
    echo $encrypted;
	}
	else
		{
		echo $encrypted2;
	}
	exit();
	
	


?>