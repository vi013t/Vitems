using RoR2;
using System;
using Vitems.API;
using Vitems.Util;

namespace Vitems.Items.White;

public class RichGunpowder : ModdedItem
{
	private static RichGunpowder itemInstance = null;

	protected override string Name => "Rich Gunpowder";
	protected override Tier Tier => Tier.White;
	protected override string ShortDescription => "Deal more damage.";
	protected override string LongDescription => $"Attacks deal {Style.Damage("100%")} {Style.Stack("(+7% per stack)")} {Style.Damage("total damage")}";
	protected override string Lore => string.Join(Environment.NewLine + Environment.NewLine,
		"\"We gotta pack sum' more punch into these suckers. Bastards ain't droppin' like they use' to.\"",
		"\"What in tarnation am I s'pposed to do 'bout it? Ain't my fault your aim is wors' than my Nana's gin.\"",
		"\"You S'PPOSED to make better damn guns for us to scrap the bastards with.\"",
		"\"Well why don't you take that there stick outta your ass and show it to the Sheriff? I can't make nothin' without his equip'men'.\"",
		"\"Quit your whinin' or your guts'll be the next ones coverin' my bullets. Quality control says your powder might as well be made o' piss and salt.\"",
		"\"Quality control ain't know their head from their ass. Those fuckers are more descpicable than the damn demons you're mowin' down.\"",
		"\"I don' give a rats ass what you think of them. Make better powder or your ass'll be on my wall.\""
	);

	private static void OnEnemyHit(On.RoR2.HealthComponent.orig_TakeDamage damageEvent, HealthComponent enemy, DamageInfo damageInfo)
	{
		if (damageInfo.attacker && damageInfo.attacker.GetComponent<CharacterBody>())
		{
			var character = damageInfo.attacker.GetComponent<CharacterBody>();
			if (character.inventory)
			{
				var gunpowderCount = character.inventory.GetItemCount(itemInstance);
				if (gunpowderCount > 0)
				{
					damageInfo.damage *= 1 + 0.07f * gunpowderCount;
				}
			}
		}

		damageEvent(enemy, damageInfo);
	}

	public static RichGunpowder Instance()
	{
		if (itemInstance == null)
		{
			itemInstance = CreateInstance<RichGunpowder>();
			On.RoR2.HealthComponent.TakeDamage += OnEnemyHit;
		}
		return itemInstance;
	}
}
