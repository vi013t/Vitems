using Vitems.API;

namespace Vitems.Items.Red;

public class SoulCleanser : ModdedItem
{
	private static SoulCleanser itemInstance = null;

	protected override string Name => "Soul Cleanser";
	protected override Tier Tier => Tier.Red;
	protected override string ShortDescription => "Chance on hit to convert an elite monster into a normal one.";
	protected override string LongDescription => "<style=cIsUtility>15% chance</style> <style=cStack>(+5% per stack)</style> on hit to convert an elite monster into a normal one.";
	protected override string Lore => "\"Emerge from the darkness, blacker than darkness. Purify that which is impure.\"";

	public static SoulCleanser Instance()
	{
		if (itemInstance == null)
		{
			itemInstance = CreateInstance<SoulCleanser>();
		}
		return itemInstance;
	}
}
