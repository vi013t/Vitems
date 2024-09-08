using UnityEngine.AddressableAssets;
using RoR2;
using UnityEngine;
using R2API;
using Vitems.API;
using Vitems.Util;

namespace Vitems.Items.Void;

public class DisruptedCharger : ModdedItem
{
	private static DisruptedCharger itemInstance = null;
	private static BuffDef secondBuff = null;
	private static BuffDef frameBuff = null;

	protected override string Name => "Disrupted Charger";
	protected override Tier Tier => Tier.WhiteVoid;
	protected override string ShortDescription => $"Deal more damage the slower you fire. {Style.Void("Corrupts all Rich Gunpowders")}.";
	protected override string LongDescription => $"Every second, gain one stack of {Style.Utility("Charged")}. When attacking an enemy, deal {Style.Damage("100% total damage")} {Style.Stack("(+10% per Charged stack)")} and remove all stacks of {Style.Utility("Charged")}. {Style.Void("Corrupts all Rich Gunpowders")}.";
	protected override string Lore => "";

	private static void OnFixedUpdateHandleUpdatingChargedBuffs(On.RoR2.CharacterBody.orig_FixedUpdate update, CharacterBody character)
	{
		if (character.inventory)
		{
			var disruptedChargerCount = character.inventory.GetItemCount(itemInstance.itemIndex);
			if (disruptedChargerCount > 0) character.AddBuff(frameBuff);
			if (character.GetBuffCount(frameBuff) >= 60)
			{
				character.SetBuffCount(frameBuff.buffIndex, 0);
				character.AddBuff(secondBuff);
			}
		}

		update(character);
	}

	/// <summary>
	/// Called when something is damaged. In this method, we check if the thing hurt was damaged by a CharacterBody. If so, we
	/// check if they have any "charged" buffs. These buffs represent the number of seconds since the player last directly damaged an enemy.
	/// We then use the amount of stacks of that buff to apply a damage increase.
	/// </summary>
	///
	/// <param name="damageEvent">The original damage event</param>
	/// <param name="healthDamaged">The CharacterBody that was damaged</param>
	/// <param name="damageInfo">Information about the damage event</param>
	private static void OnHealthBarHurtHandleAttackerChargedBuffs(On.RoR2.HealthComponent.orig_TakeDamage damageEvent, HealthComponent healthDamaged, DamageInfo damageInfo)
	{
		if (damageInfo.attacker && damageInfo.attacker.GetComponent<CharacterBody>())
		{
			CharacterBody attackerBody = damageInfo.attacker.GetComponent<CharacterBody>();
			var buffCount = attackerBody.GetBuffCount(secondBuff);
			if (buffCount > 0)
			{
				damageInfo.damage *= 1 + buffCount * 0.1f;
				attackerBody.SetBuffCount(secondBuff.buffIndex, 0);
			}
		}

		damageEvent(healthDamaged, damageInfo);
	}

	/// <summary>
	/// Returns the instance of the DisruptedCharger item. If the item hasn't yet been registered, this will register the item before returning the
	/// registered instance. This is equivalent to the value of ModItems.DisruptedCharger.
	/// </summary>
	///
	/// <returns>The DisruptedCharger item instance</returns>
	public static DisruptedCharger Instance()
	{
		if (itemInstance == null)
		{
			// Register the item
			itemInstance = CreateInstance<DisruptedCharger>();

			// Register the frame buff
			frameBuff = CreateInstance<BuffDef>();
			frameBuff.buffColor = Color.cyan;
			frameBuff.canStack = true;
			frameBuff.isDebuff = false;
			frameBuff.name = "DisruptedChargerFrameBuff";
			frameBuff.iconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texMysteryIcon.png").WaitForCompletion();
			frameBuff.isHidden = true;
			ContentAddition.AddBuffDef(frameBuff);

			// Register the buff
			secondBuff = CreateInstance<BuffDef>();
			secondBuff.buffColor = Color.cyan;
			secondBuff.canStack = true;
			secondBuff.isDebuff = false;
			secondBuff.name = "DisruptedChargerBuff";
			secondBuff.iconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texMysteryIcon.png").WaitForCompletion();
			ContentAddition.AddBuffDef(secondBuff);

			// Add event listeners
			On.RoR2.CharacterBody.FixedUpdate += OnFixedUpdateHandleUpdatingChargedBuffs;
			On.RoR2.HealthComponent.TakeDamage += OnHealthBarHurtHandleAttackerChargedBuffs;
		}
		return itemInstance;
	}
}
