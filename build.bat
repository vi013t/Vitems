:: Disable command echoing
@echo off
for /f %%a in ('echo prompt $E^| cmd') do set "ESC=%%a"

echo %ESC%[0;32m Building Solution...%ESC%[0;0m
echo:

:: Build the Visual Studio project
dotnet build ./Vitems/RoR2Mods.sln

echo:
echo %ESC%[0;32mSolution build complete.%ESC%[0;0m
echo:

:: Build the Unity assets

:: Copy over the DLL to R2ModMan
echo | set /p dummyName=%ESC%[0;32mCopying%ESC%[0;0m mod to R2ModMan...
cp Vitems\Vitems\bin\Debug\netstandard2.1\Vitems.dll %appdata%\r2modmanPlus-local\RiskOfRain2\profiles\Default\BepInEx\plugins\Unknown-Vitems

echo %ESC%[0;32mDone%ESC%[0;0m

:: Finish Build
echo:
echo %ESC%[0;32mFinished %ESC%[0;0mBuilding
