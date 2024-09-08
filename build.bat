:: Disable command echoing & create escape codes
@echo off
for /f %%a in ('echo prompt $E^| cmd') do set "ESC=%%a"

:: Log begin
echo:
echo %ESC%[0;32mBuilding%ESC%[0;0m Vitems.

:: Build the Unity assets
echo | set /p dummyName=%ESC%[0;32m    Building%ESC%[0;0m Unity AssetBundle...
"C:\Program Files\Unity\Hub\Editor\2023.2.9f1\Editor\Unity.exe" -batchmode -quit -projectPath "./VitemsAssets" -executeMethod BuildAssets.BuildAllAssetBundles
echo %ESC%[0;32m Done%ESC%[0;0m.

:: Build the Visual Studio project
echo | set /p dummyName=%ESC%[0;32m    Building%ESC%[0;0m solution...
dotnet build ./Vitems/RoR2Mods.sln 1> nul || exit /b
echo %ESC%[0;32m Done%ESC%[0;0m.

:: Copy over the DLL to R2ModMan
echo | set /p dummyName=%ESC%[0;32m    Copying%ESC%[0;0m mod to R2ModMan...
cp Vitems\Vitems\bin\Debug\netstandard2.1\Vitems.dll %appdata%\r2modmanPlus-local\RiskOfRain2\profiles\Default\BepInEx\plugins\Unknown-Vitems
echo %ESC%[0;32m Done%ESC%[0;0m.

:: Log end
echo %ESC%[0;32mFinished %ESC%[0;0mBuilding Vitems.
echo:

echo %ESC%[0;32mClick %ESC%[0;0m"Start Modded" from R2ModMan to try the mod and test changes.
