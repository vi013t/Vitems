using UnityEditor;

public class BuildAssets
{
	[MenuItem("Assets/Build AssetBundles")]
	public static void BuildAllAssetBundles()
	{
		BuildPipeline.BuildAssetBundles("../Vitems/Vitems", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
	}
}