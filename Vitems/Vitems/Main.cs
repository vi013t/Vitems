using BepInEx;
using Vitems.Items;
using R2API;
using RoR2;
using System.Reflection;
using UnityEngine;
using Vitems.Util;

namespace Vitems
{
	[BepInDependency(ItemAPI.PluginGUID)]
	[BepInDependency(LanguageAPI.PluginGUID)]
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	public class Main : BaseUnityPlugin
	{
		public const string PluginGUID = PluginAuthor + "." + PluginName;
		public const string PluginAuthor = "Violet";
		public const string PluginName = "Vitems";
		public const string PluginVersion = "1.0.0";

		public static AssetBundle MainAssets;

		public void Awake()
		{
			Log.Init(Logger);

			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Vitems.vitems_assets"))
			{
				MainAssets = AssetBundle.LoadFromStream(stream);
			}

			ModItems.RegisterItems();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.F2))
			{
				var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
				Log.Info($"Player pressed F2. Spawning all custom items at coordinates {transform.position}");
				foreach (var item in ModItems.allItems)
				{
					PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(item.itemIndex), transform.position, transform.forward * 20f);
				}
			}

			if (Input.GetKeyDown(KeyCode.F1))
			{
				var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
				Log.Info($"Player pressed F1. Spawning custom item at coordinates {transform.position}");
				PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(ModItems.soulExtractor.itemIndex), transform.position, transform.forward * 20f);
			}
		}
	}
}
