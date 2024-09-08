using Vitems.API;
using Vitems.Util;

namespace Vitems.Items.White;

public class DopamineInjection : ModdedItem
{
	private static DopamineInjection itemInstance = null;

	protected override string Name => "Dopamine Injection";
	protected override Tier Tier => Tier.White;
	protected override string ShortDescription => "Spending gold heals you.";
	protected override string LongDescription => $"Spending gold heals you for {Style.Healing("20%")} {Style.Stack("(+5% per stack)")} of your max health.";
	protected override string Lore => "";

	public static DopamineInjection Instance()
	{
		if (itemInstance == null)
		{
			itemInstance = CreateInstance<DopamineInjection>();
		}
		return itemInstance;
	}
}
