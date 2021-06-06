<?php
    $username = "";
	$username2 = "";
	$macid = "";
	$macid2 = "";
	$cpuid = "";
	$cpuid2 = "";
	$hddserial = "";
	$hddserial2 = "";
	$ipadress = "";
	$ipadress2 = "";
	$osname = "";
	$osname2 = "";
	$clientname = "";
	$clientname2 = "";
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


	
if(isset($_POST['username'])){
    $username = $_POST['username'];
}

if(isset($_POST['macid'])){
    $macid = $_POST['macid'];
}

if(isset($_POST['osname'])){
    $osname = $_POST['osname'];
}

if(isset($_POST['cpuid'])){
    $cpuid = $_POST['cpuid'];
}

if(isset($_POST['ipadress'])){
    $ipadress = $_POST['ipadress'];
}

if(isset($_POST['hddserial'])){
    $hddserial = $_POST['hddserial'];
}

if(isset($_POST['clientname'])){
    $clientname = $_POST['clientname'];
}





include "salt.php";
$method = 'aes-256-cbc';

$key = substr(hash('sha256', $password, true), 0, 32);

$iv = chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0);


$username2 = openssl_decrypt(base64_decode($username), $method, $key, OPENSSL_RAW_DATA, $iv);
$ipadress2 = openssl_decrypt(base64_decode($ipadress), $method, $key, OPENSSL_RAW_DATA, $iv);
$osname2 = openssl_decrypt(base64_decode($osname), $method, $key, OPENSSL_RAW_DATA, $iv);
$macid2 = openssl_decrypt(base64_decode($macid), $method, $key, OPENSSL_RAW_DATA, $iv);
$cpuid2 = openssl_decrypt(base64_decode($cpuid), $method, $key, OPENSSL_RAW_DATA, $iv);
$hddserial2 = openssl_decrypt(base64_decode($hddserial), $method, $key, OPENSSL_RAW_DATA, $iv);
$clientname2 = openssl_decrypt(base64_decode($clientname), $method, $key, OPENSSL_RAW_DATA, $iv);



	
    include "baglanti.php";
	$sql = "INSERT INTO serials(username, clientname, onaylandi, ipadress, osname, macid, cpuid, hddserial) VALUES ('$username2', '$clientname2', '1', '$ipadress2', '$osname2', '$macid2', '$cpuid2', '$hddserial2')";
	$query = "SELECT * FROM `serials` WHERE `macid` = '$macid2'";		
	$result = mysqli_query($conn, $query);
	
	
	if(mysqli_num_rows($result) > 0){

    $query2 = "UPDATE `serials` SET `username`='$username2',
`clientname`='$clientname2', `ipadress`='$ipadress2', `osname`='$osname2', `onaylandi`='1' WHERE `macid` = '$macid2'";
	if($conn->query($query2) === TRUE){
		header("Location: http://localhost/erdal/naimcibaba.php?macid=$macid");
	}
}
else{
    if($conn->query($sql) === TRUE){
		header("Location: http://localhost/erdal/naimcibaba.php?macid=$macid");
	}
	

}


?>