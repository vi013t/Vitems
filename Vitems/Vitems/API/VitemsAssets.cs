using System.Reflection;
using UnityEngine;

namespace Vitems.API;

public class VitemsAssets
{
	private static AssetBundle assets;

	public static void LoadAssets()
	{
		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Vitems.vitems_assets"))
		{
			assets = AssetBundle.LoadFromStream(stream);
		}
	}

	public static Sprite Sprite(string name)
	{
		return assets.LoadAsset<Sprite>(name);
	}
}
