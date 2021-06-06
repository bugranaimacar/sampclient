<?php

include "salt.php";
$method = 'aes-256-cbc';

$key = substr(hash('sha256', $password, true), 0, 32);

$iv = chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0);

$cleolar = <<<EOT
Directx|3e6cf79b|763392
Dizz Nicca 2.1|9bca953c|735232
Freezin 0.306|cb451418|733696
Freezin 2.1|cb451418|733696
Marty McFly 203|dc249201|733184
Miz Nguyen|f3adc253|208896
MM ENBZ|4ab4aa34|92672
Random ENB|cebd2059|290816
V Graphic|84f6f6de|770560
ZiPPs|cebd2059|290816
MMGE 3|0113d847|734208
Ultra Graphics|4dbd3f2f|290816
Redux Graphics|833f7785|237568
GRAPHIC MOD V5.0 FOR GTA SAN ANDREAS|bbeb2ce2|87552
Malik's Low ENB|b89c434e|262144
SA DirectX 2.0|a96933d2|770560
enb-real-for-very-low-pc|cd77f7bb|290816
dizz-nicca-s-glohancement|2629ea84|734208
EOT;


$cleolar2 = base64_encode(openssl_encrypt($cleolar, $method, $key, OPENSSL_RAW_DATA, $iv));


echo $cleolar2;

?>


