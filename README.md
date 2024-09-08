# Vitems

Vitems (Vi's Items) is a mod for Risk of Rain 2, originally created by Violet.

## Items

| Icon                                                                                      | Item                        | Description                                                             | Rarity |
|:------------------------------------------------------------------------------------------|-----------------------------|-------------------------------------------------------------------------|--------|
| ![rich gunpowder icon](/VitemsAssets/Assets/Textures/Icons/Item/rich_gunpowder.png)       | **Rich Gunpowder**          | Deal more damage.                                                       | White  |
| ![soul extractor icon](/VitemsAssets/Assets/Textures/Icons/Item/soul_extractor.png)       | **Soul Extractor**          | Killing enemies in the teleporter radius adds charge to the teleporter. | White  |
| ![momentum icon](/VitemsAssets/Assets/Textures/Icons/Item/momentum.png)                   | **Momentum**                | Move faster the longer you sprint without stopping.                     | Green  |
| ![disrupted charger icon](/VitemsAssets/Assets/Textures/Icons/Item/disrupted_charger.png) | **Disrupted Charger**       | Deal more damage the slower you fire.                                   | Void   |

## Contributing

### Building & Testing

To build the project, run `./build.bat`. Note that currently this has a few restrictions:

- Your Unity editor *must* be located at `C:\Program Files\Unity\Hub\Editor\2023.2.9f1\Editor`. If you have an installation of `Unity 2023.2.9f1`, this is the default location.
- Your profile name on R2ModMan must be "Default". Specifically, your BepInEx plugin directory must be `%appdata%\r2modmanPlus-local\RiskOfRain2\profiles\Default\BepInEx\plugins`. This is the default for R2ModMan/BepInEx.

Alternatively, you can build manually:

- Open the solution (`/Vitems/RoR2Mods.sln`) with Visual Studio 2022. 
	- Build it with `Build > Build Solution` on the top menu.
- Open the Unity asset project (`/VitemsAssets`) in Unity. 
	- Open the asset bundler by going to `Window > AssetBundle Browser`. Go to the `Build` tab, and browse the output path to `/Vitems/Vitems/`. It should be the same directory as `Vitems.csproj`.
- Ensure all checkboxes are unchecked, and click `Build`.
- Open Risk Of Rain 2 in R2ModMan.
	- Uninstall any existing installation of Vitems, by going to `Installed` on the left, scrolling for any Vitems installation, and if you find one, clicking it and clicking `Uninstall`.
	- Click `Settings` on the left, and then `Profile > Import local mod`. Navigate to the Vitems DLL, which will be in `/Vitems/Vitems/bin/Debug/netstandard2.1/Vitems.DLL`. Choose this file in the file picker window.

Once the project is build, you can try it out by clicking "Start Modded" on the left hand side of R2ModMan in your Risk Of Rain profile.