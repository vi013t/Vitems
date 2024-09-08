using UnityEngine.Networking;
using UnityEngine;
using Vitems.API;
using Vitems.Util;
using RoR2;
using R2API;

namespace Vitems.Items.White;

public class SoulExtractor : ModdedItem
{
	private static SoulExtractor itemInstance = null;

	/// <summary>
	/// The soul extractor buff. This is a buff placed on <i>enemies</i> while their last damage came from someone with a soul extractor.
	/// When they die, if they have the buff, and they're inside the teleporter radius during a teleporter event, it adds charge to
	/// the teleporter.
	/// </summary>
	private static BuffDef buff = null;
	private static HoldoutZoneController zoneController;

	protected override string Name => "Soul Extractor";
	protected override Tier Tier => Tier.White;
	protected override string ShortDescription => "Killing enemies adds charge to the teleporter.";
	protected override string LongDescription => $"Killing an enemy in the teleporter's radius adds {Style.Utility("1%")} {Style.Stack("(+1% per stack)")} to the teleporter's charge during teleporter events.";
	protected override string Lore => string.Join(System.Environment.NewLine + System.Environment.NewLine,
		"\"They say that in ancient times, monster souls were used as fuel to charge weapons and machinery.\"",
		"\"Really mommy? Is it true is it true?\"",
		"\"Yes sweetheart. A soul is the most powerful energy source in the world.\"",
		"\"Wow, really mommy!? In the whooooole world?\"",
		"\"Yes, muffin. And every one of us have been blessed with one. Nurture it, and it will nurture you.\""
	);

	/// <summary>
	/// Called every update frame of the teleporter. Each frame, we assign our stored zoneController object to the teleporter's controller, which
	/// handles things like the teleporter's charge. The zoneController is then used later in OnEnemyDeathCheckForBuffToAddTeleporterCharge().
	/// </summary>
	///
	/// <param name="updateEvent">The original update event</param>
	/// <param name="teleporterInteraction">The teleporter interaction object of the event</param>
	private static void OnTeleporterFixedUpdateAssignZoneController(On.RoR2.TeleporterInteraction.orig_FixedUpdate updateEvent, TeleporterInteraction teleporterInteraction)
	{
		zoneController = teleporterInteraction.holdoutZoneController;
		updateEvent(teleporterInteraction);
	}	
	
	/// <summary>
	/// Called when something with health dies. If the "something" has a body, and the body has the Soul Extractor buff, then
	/// we add charge to the teleporter based on the stacks of the buff it has, which is determined by the item count of the attacker in
	/// OnEnemyHitCheckToAddBuff().
	/// </summary>
	///
	/// <param name="deathEvent">The original death event</param>
	/// <param name="body">The character body that died</param>
	private static void OnEnemyDeathCheckForBuffToAddTeleporterCharge(On.RoR2.CharacterBody.orig_OnDeathStart deathEvent, CharacterBody body)
	{
		if (body && zoneController.charge > 0 && zoneController.IsBodyInChargingRadius(body))
		{
			zoneController.charge = Mathf.Clamp01(zoneController.charge + 0.01f * body.GetBuffCount(buff));
		}

		deathEvent(body);
	}
	
	/// <summary>
	/// Called when something with health takes damage. If it took damage from something with the Soul Extractor item, we apply the Soul Extractor
	/// buff, marking that this enemy, if killed by this damage within radius of the teleporter, should add charge to the teleporter. If the
	/// attacker does not have the Soul Extractor item, we remove all instances of the soul extractor buff, since only a killing shot from the
	/// player with the item should count towards charging the teleporter.
	/// </summary>
	/// 
	/// <param name="damageEvent">The original damage event</param>
	/// <param name="enemy">The enemy that was hit</param>
	/// <param name="damageInfo">Information about the damage event</param>
	private static void OnEnemyHitCheckToAddBuff(On.RoR2.HealthComponent.orig_TakeDamage damageEvent, HealthComponent enemy, DamageInfo damageInfo)
	{
		if (damageInfo.attacker && damageInfo.attacker.GetComponent<CharacterBody>())
		{
			var character = damageInfo.attacker.GetComponent<CharacterBody>();
			if (character.inventory)
			{
				var soulExtractorCount = character.inventory.GetItemCount(itemInstance);
				enemy.GetComponent<CharacterBody>()?.SetBuffCount(buff.buffIndex, soulExtractorCount);
			}
		}

		damageEvent(enemy, damageInfo);
	}

	public static SoulExtractor Instance()
	{
		// Create the item instance if it doesn't exist yet
		if (itemInstance == null)
		{
			// Create the item
			itemInstance = CreateInstance<SoulExtractor>();

			// Register the buff
			buff = CreateInstance<BuffDef>();
			buff.buffColor = Color.cyan;
			buff.canStack = true;
			buff.isDebuff = false;
			buff.name = "SoulExtractorBuff";
			buff.isHidden = true;
			ContentAddition.AddBuffDef(buff);

			// Add event listeners
			//On.RoR2.HealthComponent.Die += OnEnemyDeathCheckForBuffToAddTeleporterCharge;
			On.RoR2.HealthComponent.TakeDamage += OnEnemyHitCheckToAddBuff;
			On.RoR2.CharacterBody.OnDeathStart += OnEnemyDeathCheckForBuffToAddTeleporterCharge;
			On.RoR2.TeleporterInteraction.FixedUpdate += OnTeleporterFixedUpdateAssignZoneController;
		}

		// Return the item instance
		return itemInstance;
	}
}

