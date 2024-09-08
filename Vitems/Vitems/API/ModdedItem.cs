using UnityEngine.AddressableAssets;
using RoR2;
using UnityEngine;
using R2API;
using Vitems.Util;
using System;
using Vitems.Items;

namespace Vitems.API;

public enum Tier
{
	White,
	Green,
	Red,
	Yellow,
	Lunar,
	WhiteVoid,
	GreenVoid,
	RedVoid,
}

public abstract class ModdedItem : ItemDef
{
	protected abstract string Name { get; }
	protected abstract string ShortDescription { get; }
	protected abstract string LongDescription { get; }
	protected abstract string Lore { get; }
	protected abstract Tier Tier { get; }

	private string NameToken
	{
		get
		{
			return this.Name.Replace("'", "").Replace(" ", "_").ToUpper();
		}
	}

	protected ModdedItem()
	{
		// Item properties
		this.name = $"{this.NameToken}_NAME";
		this.nameToken = $"{this.NameToken}_NAME";
		this.pickupToken = $"{this.NameToken}_PICKUP";
		this.descriptionToken = $"{this.NameToken}_DESCRIPTION";
		this.loreToken = $"{this.NameToken}_LORE";
		this._itemTierDef = Addressables.LoadAssetAsync<ItemTierDef>($"RoR2/Base/Common/{this.TierAsset}.asset").WaitForCompletion();
		this.pickupIconSprite = VitemsAssets.Sprite($"{this.NameToken.ToLower()}.png");
		this.pickupModelPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mystery/PickupMystery.prefab").WaitForCompletion();
		this.canRemove = true;
		this.hidden = false;

		// Register the item
		ItemAPI.Add(new CustomItem(this, new ItemDisplayRuleDict(null)));

		// Language tokens
		LanguageAPI.Add($"{this.NameToken}_NAME", this.Name);
		LanguageAPI.Add($"{this.NameToken}_PICKUP", this.ShortDescription);
		LanguageAPI.Add($"{this.NameToken}_DESCRIPTION", this.LongDescription);
		LanguageAPI.Add($"{this.NameToken}_LORE", this.Lore);

		// Log initialization completion
		Log.Debug($"Initialized item: {this.Name}");
		ModItems.allItems.Add(this);
	}

	private string TierAsset
	{
		get
		{
			return this.Tier switch
			{
				Tier.White => "Tier1Def",
				Tier.Green => "Tier2Def",
				Tier.Red => "Tier3Def",
				Tier.WhiteVoid => "VoidTier1Def",
				Tier.GreenVoid => "VoidTier2Def",
				Tier.RedVoid => "VoidTier3Def",
				Tier.Lunar => "LunarTierDef",
				Tier.Yellow => "BossTierDef",
				_ => throw new ArgumentOutOfRangeException(nameof(this.Tier), $"Invalid tier: {this.Tier}")
			};
		}
	}
}
