using Vitems.API;
using Vitems.Util;

namespace Vitems.Items.Green;

public class LuckyJuicer : ModdedItem
{
	private static LuckyJuicer itemInstance = null;

	protected override string Name => "Lucky Juicer";
	protected override Tier Tier => Tier.Green;
	protected override string ShortDescription => "Yield an additional item from Shrines of Chance";
	protected override string LongDescription => $"Shrines of Chance can yield an additional {Style.Utility("1")} {Style.Stack("(+1 per stack)")} items.";
	protected override string Lore => "";

	public static LuckyJuicer Instance()
	{
		if (itemInstance == null)
		{
			itemInstance = CreateInstance<LuckyJuicer>();
		}
		return itemInstance;
	}
}
