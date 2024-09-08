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

### Getting the Project Locally

First, clone the project onto your local machine:

```bash
git clone https://github.com/vi013t/Vitems.git
```

### Building & Testing

Before making any changes, you should ensure that you can build and test the mod as-is. The mod should be able to be built and used directly after cloning, with zero changes or extra installations necessary.

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

Once the project is built, you can try it out by clicking "Start Modded" on the left hand side of R2ModMan in your Risk Of Rain 2 profile.

### Adding a New Item

To add a new item, you should do all of the following:

- Check the list of existing items and ensure your idea isn't already implemented as an existing item or extremely close to an existing item.
- Extend `Vitems.API.ModdedItem`. There are plenty of examples of how to implement this, see `/Vitems/Vitems/Items/White/RichGunpowder` for a simple example. Follow existing conventions as other items, such as the static `Instance()` method. Obviously, add functionality for your item.
- Create an instance of the item in `Vitems.Items.ModdedItems`. Follow the existing patterns.
- Create a model for the item under `/VitemsAssets/Assets/Models/Meshes`.
- Create a render of the model with the correct outline color (see `Reference` below), and place it in `/VitemsAssets/Assets/Textures/Icons/Item/`. The file name for the render *must* be `<item_name>.png`, where `<item_name>` is the name of your item in snake-case.
- Test your item. Ensure that the item has a properly formatted name, pickup text, description, and lore, as well as a working model and icon texture. You can call `Vitems.Util.DropItemOnF1(item.itemIndex)` in `Main.Update()` (see the commented-out line for an example); This will make it so that pressing `F1` in-game will drop your item onto the ground, allowing for easy testing with it. If you are going to make a pull request in attempt to get your changes merged into Vitems, please remove this before creating the pull request.
	- Try to be *as descriptive as possible* in your item description. Describe exactly how the item stacks, and where applicable, specifically state `base damage` or `total damage` to avoid confusion. Use the functions in `Vitems.Util.Style` to format your description; See any of the existing items for examples.
- Update `/README.md` with your item icon, name, description, and rarity.
- Create a pull request to merge in your changes. Note that pull requests are subject to requesting changes or denial due to code quality, item imbalance, poor assets, repeated or poor item functionality, and more. In your pull request, be thorough about your item &mdash; What does it do? How does it stack, and why? What rarity is it, and why? How do you feel it fits into Vitems? Are there any game-breaking possibilities that need to be addressed if a player obtains several stacks of your item? Does your item have a downside, despite being a non-Lunar item? The more thorough you are at addressing concerns ahead of time, the quicker the process will be and the more likely your pull request is to get merged. 

Also, bonus points if your item has a void variant. Ideally we'd like most or all items in the mod to have a void variant.

### Reference

[Item rarity outline compositor node reference for Blender](./docs/item_rarity_nodes.png)

- Change the color highlighted in the image to match your rarity:
	- White: `#FFFFFF`
	- Green: `#76c441`
	- Red: `#ef5943`
	- Lunar: `#46e5f8`

[Item modding tutorial](https://github.com/Phreelosu/AetheriumMod-SotS/blob/rewrite-master/Tutorials/Item%20Mod%20Creation.md)

[Simple item code example](https://github.com/vi013t/Vitems/blob/main/Vitems/Vitems/Items/White/RichGunpowder.cs)