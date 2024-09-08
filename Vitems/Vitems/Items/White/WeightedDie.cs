using Vitems.API;
using Vitems.Util;

namespace Vitems.Items.White;

public class WeightedDie : ModdedItem
{
	private static WeightedDie itemInstance = null;

	protected override string Name => "Weighted Die";
	protected override Tier Tier => Tier.Green;
	protected override string ShortDescription => "Improved odds of getting an item from a Shrine of Chance.";
	protected override string LongDescription => $"Improve the chance of getting an item from a Shrine of Chance by {Style.Utility("10%")} {Style.Stack("(+10% per stack)")}.";
	protected override string Lore => "";

	public static WeightedDie Instance()
	{
		if (itemInstance == null)
		{
			itemInstance = CreateInstance<WeightedDie>();
		}
		return itemInstance;
	}
}
