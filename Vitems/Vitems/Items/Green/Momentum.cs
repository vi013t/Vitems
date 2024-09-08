using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Vitems.API;
using Vitems.Util;
using static R2API.RecalculateStatsAPI;

namespace Vitems.Items.Green;

public class Momentum : ModdedItem
{
	private static Momentum itemInstance = null;
    private static BuffDef secondBuff = null;
    private static BuffDef frameBuff = null;

	protected override string Name => "Momentum";
	protected override Tier Tier => Tier.Green;
	protected override string ShortDescription => "Move faster the longer you sprint.";
	protected override string LongDescription => $"Every second of sprinting, gain 1 stack of the {Style.Utility("Momentum")} buff. Every stack of the {Style.Utility("Momentum")} buff increases your speed by {Style.Utility("5%")} {Style.Stack("(+1% per stack)")}. Upon stopping sprinting, lose all stacks of the {Style.Utility("Momentum")} buff.";
	protected override string Lore => "";

    private static void OnFixedUpdateHandleMomentumBuffs(On.RoR2.CharacterBody.orig_FixedUpdate update, CharacterBody character)
    {
        if (character.inventory)
        {

            // Add buffs
            var momentumCount = character.inventory.GetItemCount(itemInstance.itemIndex);
            if (momentumCount > 0) character.AddBuff(frameBuff);
            if (character.GetBuffCount(frameBuff) >= 60)
            {
                character.SetBuffCount(frameBuff.buffIndex, 0);
                character.AddBuff(secondBuff);
            }

            // Remove buffs when not spriting

            if (!character.isSprinting)
            {
                character.SetBuffCount(frameBuff.buffIndex, 0);
                character.SetBuffCount(secondBuff.buffIndex, 0);
            }
        }

        // Perform the original event
        update(character);
    }

    private static void ApplySpeedIncrease(CharacterBody character, StatHookEventArgs args)
    {
        if (!character.HasBuff(secondBuff)) return;

        var itemCount = character.inventory.GetItemCount(itemInstance.itemIndex);
        var buffCount = character.GetBuffCount(secondBuff);

        args.moveSpeedMultAdd += 0.05f * buffCount + 0.01f * itemCount;  
    }

    /// <summary>
    /// Returns the instance of the Momentum item. If the instance hasn't been created yet, it will be created. This is equivalent to referencing ModItems.momentum.
    /// Note that calling this, as well as referencing ModItems.momentum, cannot be done before Main.Awake().
    /// </summary>
    ///
    /// <returns>The instance of the Momentum item.</returns>
	public static Momentum Instance()
	{
        // If the item instance doesn't exist yet, create it, along with the buffs
		if (itemInstance == null)
		{ 
            // Create the item
            itemInstance = CreateInstance<Momentum>();

            // Register the frame buff
            frameBuff = CreateInstance<BuffDef>();
            frameBuff.buffColor = Color.cyan;
            frameBuff.canStack = true;
            frameBuff.isDebuff = false;
            frameBuff.name = "MomentumFrameBuff";
            frameBuff.iconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texMysteryIcon.png").WaitForCompletion();
            frameBuff.isHidden = true;
            ContentAddition.AddBuffDef(frameBuff);

            // Register the buff
            secondBuff = CreateInstance<BuffDef>();
            secondBuff.buffColor = Color.cyan;
            secondBuff.canStack = true;
            secondBuff.isDebuff = false;
            secondBuff.name = "MomentumBuff";
            secondBuff.iconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texMysteryIcon.png").WaitForCompletion();
            ContentAddition.AddBuffDef(secondBuff);

            // Add event listeners
            On.RoR2.CharacterBody.FixedUpdate += OnFixedUpdateHandleMomentumBuffs;
            RecalculateStatsAPI.GetStatCoefficients += ApplySpeedIncrease;
		}

        // Return the item instance
		return itemInstance;
	}
}
