using RoR2;
using UnityEngine;

namespace Vitems.Util;

public class Debugging
{
	public static void DropItemOnF1(ItemDef item)
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
			Log.Info($"Player pressed F1. Spawning custom item at coordinates {transform.position}");
			PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(item.itemIndex), transform.position, transform.forward * 20f);
		}
	}
}
