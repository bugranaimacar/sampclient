<?php

include "salt.php";
$method = 'aes-256-cbc';

$key = substr(hash('sha256', $password, true), 0, 32);

$iv = chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0);

$cleolar = <<<EOT
18218 anti crasher 3dl
18138 compass.dll
51052 emergency.cs
44338 enhance.cs
17727 fovchanger.cs
18114 fpsunlock.cs
19288 head.cs
18 memfix.cs
17809 memory4gb.cs
22852 mabur.cs
17809 memory8gb.cs
17560 memory2048.cs
337 sampcs_helper.cs
17924 sensivityfix.cs
225 shotcamshake.cs
34124 shootcamerawork
19396 stablevehcam
17575 fpsdelimiter.cs
17593 fpsunlimiter.cs
18008 tree remove
sun.cs|23c19223|17662
anticrasher_03DL.cs|89ac3dbb|18218
compas.cs|47e1c9be|18138
emergency.cs|4d6aa7cd|51052
FPSUnlock.cs|87677c89|18114
memory2048.cs|efe0e745|17560
newOpcodes.cleo|a6ef96c2|161280
sampcs_helper.cs|1411348a|337
SensitivityFix.cs|facb92ae|17924
zonetexts.cs|5d258fde|18692
FileSystemOperations.cleo|8193b56a|65024
IniFiles.cleo|23c49ea2|81920
IntOperations.cleo|8831bc34|58368
newOpcodes.cleo|a6ef96c2|161280
sampcs_helper.cs|1411348a|337
MemFix.cs|4d137ef8|18
enhance.cs|61950cea|44338
rina_backfire.cs|b659c15f|18039
Backfire - ALS (Junior_Djjr).cs|3ced0065|29456
SightDistance_by_0x688.cleo|d440c550|16896
SkyBoX.cs|9bd61260|19004
SkyBoX2.cs|61fc7940|19006
SkyBoX3.cs|a24a02e9|19006
SkyBoX4.cs|4fae71ee|19008
Timecyc.cs|636960d8|17811
wase.cs|21c44533|1591
newOpcodess.cleo|52fb14df|75264
mixsets.cs|acfd5509|54780
SnowFX-Start.cs|3d9266f9|957
SnowFX-Frost.s|9e379cec|1167
SnowFX-Snow.s|f1e4382b|1135
snowfalling.cs|39cc1d70|20563
granadas.cs|306dbae2|24012
gtalexisvideos.cs|6f11384c|12960
CamHeli.cs|b9348f83|1997
Memory_4GB.cs|b889345e|17809
Memory_8GB.cs|db0298e1|17809
radio_off.cs|54ed5b98|17644
Stars.cs|6a71d061|18963
utzonetext.cs|d83ca62a|18692
HeliCamLock.cs|cbe8873f|17577
SAAS.cs|0e6480ce|24624
FOV_Changer.cs|5f07cb88|17727
remove3dgunflash.cs|de386d1b|16
StableVehCam.cs|f306ad8b|19396
newopcodes.cleo|5339ffb6|48128
wsiv_by_dk22pac.cs|8e88a849|20865
memory512.cs|b1c790eb|17560
HUD.cs|c55ec3fc|18847
HUD.cs|1b3c54c4|18891
CoronaOptimizations.cs|ad7c287b|17696
StreamIniExtender.cs|8576fe8f|2012
ENBZ.cs|87fbe047|17726
Sun.cs|8e01f93d|17567
Radarzoom.cs|92e606ea|17727
night.cs|601d8620|17581
No_more_haze.cs|38c8c73f|17558
shotcamshake.cs|8073dc18|225
exhausttweaker.cs|c3ae695b|28450
breakpads.cs|97b3c93e|18191
imvehlm.cs|b8262e40|29547
tail.cs|a3f97967|19935
head.cs|eb17ce20|19288
HUDFix.cs|bd48a59f|18323
remove_zeros.cs|bd48a59f|18323
Dashcam.cs|cedd798d|33579
gta_iv_lights.cs|9b88b262|21960
gta_iv_lights.cs|9197fe71|18355
ClipboardCommands.cleo|b1a11861|42496
Drift.cs|471909a0|134
beep.cs|d1ac7402|1074
/IniFiles.cleo|3b0dc319|105984
hud colors (junior_djjr).cs|cc667962|3707
neon.cs|42102ceb|19019
(arm).cs|afb817ff|31669
(bron).cs|084af994|23415
(heal).cs|de4df6ed|26057
cruise control.cs|4404a496|654
steering.CS|88069457|18046
Enhance_ParticleTXD__Junior_Djjr_.cs|933f9ec2|44458
Aim Sensibility (Junior_Djjr).cs|cef0ada2|23795
EOT;


$cleolar2 = base64_encode(openssl_encrypt($cleolar, $method, $key, OPENSSL_RAW_DATA, $iv));


echo $cleolar2;

?>