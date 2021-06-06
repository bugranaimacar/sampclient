<?php

include "salt.php";
$method = 'aes-256-cbc';

$key = substr(hash('sha256', $password, true), 0, 32);

$iv = chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0);

$cleolar = <<<EOT
92216 bass.dll
188416 eax.dll
36864 ogg.dll
1060864 vorbis.dll
53760 vorbisFile.dll
65536 vorbisHooked.dll
1466368 samp.dll
107584 bass.dll
3995136 audio.dll
225792 deletewhitecrosshair.dot
100864 gtainterface.dll
1498960 msvcr100.dll
752128 newton.dll
160256 plugin.dll
2759680 sampar.dll
661448 msvcp110.dll
421200 msvcp100.dll
455328 msvcp120.dll
773968 msvcr100.dll
1498960 msvcr100d.dll
970912 msvcr120.dll
1060864 vorbis..dll
EOT;


$cleolar2 = base64_encode(openssl_encrypt($cleolar, $method, $key, OPENSSL_RAW_DATA, $iv));


echo $cleolar2;

?>

