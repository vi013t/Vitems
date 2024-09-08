using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Vitems.API;

namespace Vitems.Items.Equipment;

internal class ExitSign : ModdedEquipment
{
	private static ExitSign itemInstance = null;

	protected override string Name => "Exit Sign";
	protected override string ShortDescription => "Teleport to the stage teleporter.";
	protected override string LongDescription => "Upon activation, teleport to the stage's teleporter. Cooldown for 60s.";
	protected override string Lore => "";

	private static Vector3 teleporterPosition;

	private static void OnTeleporterPlaced(On.RoR2.TeleporterInteraction.orig_Awake orig, TeleporterInteraction teleporterInteraction)
	{
		teleporterPosition = teleporterInteraction.gameObject.GetComponent<Transform>().position;
		orig(teleporterInteraction);
	}

	protected override bool ActivateEquipment(EquipmentSlot slot)
	{
		if (!slot.characterBody || !slot.characterBody.master) return false;

		slot.characterBody.GetComponent<CharacterMotor>().transform.position = teleporterPosition;
        slot.characterBody.master.GetBody().transform.position = teleporterPosition;

        return true;
	}
	
	public static ExitSign Instance()
    {
        if (itemInstance == null)
        {
            itemInstance = CreateInstance<ExitSign>();
            On.RoR2.TeleporterInteraction.Awake += OnTeleporterPlaced;
        }
        return itemInstance;
    }
}
