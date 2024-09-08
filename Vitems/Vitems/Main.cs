using BepInEx;
using Vitems.Items;
using R2API;
using RoR2;
using System.Reflection;
using UnityEngine;
using Vitems.Util;
using Vitems.API;

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

		public void Awake()
		{
			Log.Init(Logger);
			VitemsAssets.LoadAssets();
			ModItems.RegisterItems();
		}

		private void Update()
		{
			Debugging.DropItemOnF1(ModItems.momentum);
		}
	}
}
