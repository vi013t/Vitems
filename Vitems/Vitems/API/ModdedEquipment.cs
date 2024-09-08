using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Vitems.Util;

namespace Vitems.API;

public abstract class ModdedEquipment : EquipmentDef
{
	protected abstract string Name { get; }
	protected abstract string ShortDescription { get; }
	protected abstract string LongDescription { get; }
	protected abstract string Lore { get; }
	protected bool IsLunar { get; } = false;

	protected abstract bool ActivateEquipment(EquipmentSlot slot);

	private string NameToken 
	{ 
		get
		{
			return this.Name.Replace("'", "").Replace(" ", "_").ToUpper();
		} 
	}

	protected ModdedEquipment()
	{
		// Item properties
		this.name = $"{this.NameToken}_NAME";
		this.nameToken = $"{this.NameToken}_NAME";
		this.pickupToken = $"{this.NameToken}_PICKUP";
		this.descriptionToken = $"{this.NameToken}_DESCRIPTION";
		this.loreToken = $"{this.NameToken}_LORE";
		this.pickupIconSprite = Main.MainAssets.LoadAsset<Sprite>($"{this.NameToken.ToLower()}.png");
		this.pickupModelPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mystery/PickupMystery.prefab").WaitForCompletion();
		this.isLunar = IsLunar;

		// Register the item
		ItemAPI.Add(new CustomEquipment(this, new ItemDisplayRuleDict(null)));

		// Language tokens
		LanguageAPI.Add($"{this.NameToken}_NAME", this.Name);
		LanguageAPI.Add($"{this.NameToken}_PICKUP", this.ShortDescription);
		LanguageAPI.Add($"{this.NameToken}_DESCRIPTION", this.LongDescription);
		LanguageAPI.Add($"{this.NameToken}_LORE", this.Lore);

		// Log initialization completion
		Log.Debug($"Initialized item: {this.Name}");

		// Event listeners
		On.RoR2.EquipmentSlot.PerformEquipmentAction += PerformEquipmentAction;
	}

	private bool PerformEquipmentAction(On.RoR2.EquipmentSlot.orig_PerformEquipmentAction orig, RoR2.EquipmentSlot self, EquipmentDef equipmentDef)
	{
		if (equipmentDef == this) return ActivateEquipment(self);
		return orig(self, equipmentDef);
	}
}
