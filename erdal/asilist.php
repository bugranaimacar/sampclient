<?php

include "salt.php";
$method = 'aes-256-cbc';

$key = substr(hash('sha256', $password, true), 0, 32);

$iv = chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0);

$cleolar = <<<EOT
264704
32256 mobilehud.asi
700928 modloader.asi
48128 mausefix.asi
889856 samp.asi
4041752 audio.asi
33280 multi-sampling fix.asi
161280 newopcodes.cleo
4041752 audio.asi
922624 samp.asi
41984 normalmapfix.asi
922624 samp.asi
41984 outfitfix.asi
63488 radarrect.asi
176640 sampgraphicrestore.asi
77824 shellfix.asi
41984 streammemfix.asi
27648 streammemfix1.0.asi
27648 streammemfix2.2.asi
65536 anticrashe.asi
111104 colormod.asi
243200 crashes.asi
7168 fix.block_roads
343552 mobilehud.asi
704000 modloader.asi
83456 normalmap.asi
69632 noshadows.asi
65024 sa_widescreen.asi
7168 sa_displayresolouiton.asi
27648 streammemfix.asi
cd7e08ba|700928|modloader
5db5689f|644096|modloader
55cbf4bd|306688|modloader
1a11907b|643584|modloader
d7d0aed8|704000|modloader
3690d889|63488|radarrect
766519b7|67584|sa_draw_distance_changer
b7feb071|67072|sa_draw_distance_changer
14521a29|67072|sa_draw_distance_changer
f2cc6cc0|65024|sa_widescreenfix_lite.asi
d64bca53|64512|sa_widescreenfix_lite.asi
5b2cfa47|330240|GTASA.WidescreenFix.asi
931df16b|100352|GTASA_widescreen_fix.asi
f54518c7|63488|gta3vcsa_resolution_override.asi
2888d0ae|325120|GTASA.WidescreenFix.asi
ee1bf059|329728|GTASA.WidescreenFix.asi
cd6babfe|330240|GTASA.WidescreenFix.asi
41ead3ba|264704|CLEO 4
223db3f2|149504|CLEO 4
d1c3e498|264704|CLEO 4
f2c85fce|333824|CLEO 4
e7c84074|53760|REFFIX
fe53548a|305664|bullethole
33ca7388|305664|bullethole
23d2bc5a|110592|colormod
8a25d24c|111104|colormod
697f5931|44544|crashes
c0677ddb|243200|crashes
3277ff1b|285696|crashes
b8fb3403|58368|GTA_IV_HUD
c7cf4370|425984|gtaiv-hud.asi 
835aa598|104448|Hooks
32fe75f7|118272|Hooks
517ced4e|12288|Hooks
087713c1|10240|Hooks
2b0d2118|13312|IndieVehSmoke
2837b624|6656|samp_grass
f73f1415|52224|shadows.asi
9586c69f|27648|StreamMemFix
3edc9540|27648|streammemfix2.2_test1.asi
3bdfb948|27648|streammemfix2.2_test2.asi
215fcbd1|247296|ImVehFt.asi
be4f8f6c|41984|NormalMapFix.asi
1286a54f|41984|OutFitFix.asi
a43d7954|77824|ShellFix.asi
11aeb1af|41984|StreamMemFix.asi
63e799cb|87552|StreamMemoryFixGTASA1_01_EURO_NOCD.asi
a1adb9f6|32768|flickr.asi
14c502fe|32256|mobileHud_byDK22Pac.asi
9c8ada17|343552|MobileHud.asi
2535f367|291840|skygfx.asi
faf7fd7a|292864|skygfx.asi v4.0
c06603d1|243200|skygfx.asi v3.6
6e1c86aa|245248|skygfx.asi v3.7
afd11c8a|13312|skygrad.asi
caa0c2e8|53248|reffix.asi
ce30bb4a|43520|SAMPGraphicRestore.asi
5702548e|127488|SAMPGraphicRestore.asi
39f6c678|176640|SAMPGraphicRestore.asi
2a6b962e|72192|SAMPGraphicRestore.asi
8ec88c55|72192|SAMPGraphicRestore.asi
c7562c33|176640|SAMPGraphicRestore.asi
0fc2f011|51712|shadows.asi
2cd283d2|32768|shadows.asi
48e19cbc|51200|shadows.asi
30d141de|279552|SilentPatchSA.asi
bfc030bb|226816|SilentPatchSA.asi
da410001|253952|SilentPatchSA.asi
730c4e70|271360|SilentPatchSA.asi
33365cc1|270848|SilentPatchSA.asi
c774bc86|270848|SilentPatchSA.asi
ae43955b|186368|SilentPatchSA.asi
4e37eeaa|173056|SilentPatchSA.asi
a98c9864|137216|SilentPatchSA.asi
2c69fa9b|3995136|audio.asi
e9cc5a0c|3997696|audio.asi
4ed3bb8c|3981824|audio.asi
391cd6ca|3987456|audio.asi
4417b46c|3990016|audio.asi
2475fd1a|4004352|audio.asi
0f4be189|4039704|audio.asi
df7154f1|865280|samp.asi SAMP ADDON
ce96b96d|859648|samp.asi SAMP ADDON
a2be9f27|889856|samp.asi SAMP ADDON
1b3ea064|888832|samp.asi SAMP ADDON
8de1d9b5|865280|samp.asi SAMP ADDON
f5ef753f|860672|samp.asi SAMP ADDON
9fdc036f|911360|samp.asi SAMP ADDON
cd516072|52224|InterfaceEditor.asi 
c89998d2|99840|weaponlimit.asi
5786ce94|69632|NoShadows.asi
9bd7d275|69120|exdisp.asi
7eb46764|13824|imfx.asi
48aec247|89088|imfx.asi
2ce38315|156672|imfx.asi
ebba2790|441344|imfx.asi
b989d938|670720|imfx.asi
0d5d3207|659456|imfx.asi
a27c863d|97280|imfx.asi
26ef0621|72192|lensflare.asi
d4e381c9|111104|enbshader
4bc3c010|100864|WaterFix
33dce081|7168|sadisplayresolutions.asi
d7b66c49|83456|normalmap.asi
0db697c7|70144|IDE_DD_Tweaker.asi
ee5b2573|52736|IDE_DD_tweaker.asi
34d2807b|68608|IDE_DD_Tweaker.asi
f789a008|97280|ImgLimitAdjuster.asi
cadf6612|65536|limit_adjuster_gta3vcsa.asi
c651ae34|403456|limit_adjuster_gta3vcsa.asi
8ea26775|366080|limit_adjuster_gta3vcsa.asi
1b230f31|103936|2dfx.asi
a463f3c3|107520|2dfx.asi
bcf34204|27648|GFX Hack
04c63b94|356352|shell.asi
0cd5df20|75264|samp-discord-plugin.asi
0a6b2335|74752|samp-discord-plugin.asi LAST VERSION
2c0b9b49|158720|sensfix.asi
91b04994|388608|III.VC.SA.LimitAdjuster.asi
4f369a3f|389632|III.VC.SA.LimitAdjuster.asi
35604e87|375808|III.VC.SA.LimitAdjuster.asi
2bce34f6|361984|SALodLights.asi
8ac7585e|177664|SALodLights.asi
160ecaed|361984|SALodLights.asi
5112a717|212480|SALodLights.asi
d75fc75f|354816|V_HUD_by_DK22Pac.asi
a424ae5b|179712|V_HUD_by_DK22Pac.asi
915300d4|169984|imCamAim.asi
1f6956e5|241664|GInputSA.asi
8293a7f1|155136|SAMP-GPS.asi
656d13bd|155136|SAMP-GPS.asi
38b27cbb|180224|SAMP-GPS.asi
315d0510|48128|mousefix.asi
528d7f66|5632|fix.black_roads.asi
95aa049a|7168|fix.black_roads.asi 2
88317735|181248|III.VC.SA.TransparentMenu.asi
384c968a|27648|anticrash.asi
7ff8e0c2|56320|Optimiser.asi
ff96248c|77824|ps2grass.asi
72981b2c|102912|ps2refl.asi
8709b7b1|418304|anims.asi
81e922b3|8192|Hud_Zero_Fix.asi
07aff3db|1103360|modupdater.asi
b48b24cc|238080|ClassicHud.asi
797f4a95|116736|dynsfx.asi
004a459b|33280|Multi-sampling fix.asi
d81b3229|64000|VehicleAudioHook.asi
76469b33|8704|vehlightsfix.asi
9437db1e|48640|wshps.asi
7a52c2d5|130048|wshps.asi
30622a13|272384|gsx.asi
329e158f|488960|VehFuncs.asi
a70794ab|438784|VehFuncs.asi
86719030|53760|bikearmfix.asi
10fc6eff|11264|rundll32exefix.asi
a77e92bc|52736|MotionBlur.asi
67dd7a57|172032|SA_GPS.asi
9fc62843|790016|gtaRenderHook.asi
00910139|54784|Searchlights.asi
44d78c02|110592|dof.asi
5d63bf38|443904|HudColorsVC.asi
a6322253|52736|sunshine.asi
e1a439c5|36864|sa_lighting.asi
6fc450c7|225792|DeleteWhiteCrosshairDot.asi
3a1ef6d8|76800|FramerateVigilante.SA.asi
64fb77cf|323584|gsx.asi
b8ed376a|289280|IndieVehicles.asi
ccf1ad47|478208|MixSets.asi
f231fbea|15360|sampfxtfix.asi
a6bee489|145920|VMPEditor.asi
56660870|86016|timecycle24.asi
ac7f2e55|526336|MixSets.asi
f4bfd5dd|230912|ImVehFtFix.asi
3c5fc248|459776|MobileHud.asi
90ea9e7f|255488|FramerateVigilante.SA.asi
4040728 audio.asi
928768 samp.asi
cfa74ad5|453120|VehFuncs.asi
889856 samp.asi
EOT;


$cleolar2 = base64_encode(openssl_encrypt($cleolar, $method, $key, OPENSSL_RAW_DATA, $iv));


echo $cleolar2;

?>


