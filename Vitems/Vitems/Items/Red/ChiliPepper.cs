using Vitems.API;
using Vitems.Util;

namespace Vitems.Items.Red;

public class ChiliPepper : ModdedItem
{
	private static ChiliPepper itemInstance = null;

	protected override string Name => "Chili Pepper";
	protected override Tier Tier => Tier.Red;
	protected override string ShortDescription => "Killing enemies surrounds you with a heat wave.";
	protected override string LongDescription => $"Killing enemies surrounds you with a {Style.Utility("heat wave")} that deals {Style.Damage("1200% base damage per second")} and {Style.Damage("burns enemies for 10s")}. The heat wave grows with every kill, increasing its radius by {Style.Utility("2m")}. Stacks up to {Style.Utility("18m")} {Style.Stack("(+12m per stack)")}.";
	protected override string Lore => "";

	public static ChiliPepper Instance()
	{
		if (itemInstance == null)
		{
			itemInstance = CreateInstance<ChiliPepper>();
		}
		return itemInstance;
	}
}
