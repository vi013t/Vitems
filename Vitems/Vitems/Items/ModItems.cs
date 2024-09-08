using RoR2;
using System.Collections.Generic;

namespace Vitems.Items
{
	public class ModItems
	{
		/// <summary>
		/// The list of all custom modded items in the mod. Assuming RegisterItems() is coded properly, this is guaranteed to be in
		/// order of rarity, containing all white items, and then green, followed by red, void, lunar, equipment, and lunary equipment.
		/// Additionally, within each rarity, the items are listed alphabetically.
		/// </summary>
		public static List<ItemDef> allItems = new();

		// White
		public static ItemDef dopamineInjection;
		public static ItemDef richGunpowder;
		public static ItemDef soulExtractor;
		public static ItemDef weightedDie;

		// Green
		public static ItemDef luckyJuicer;
		public static ItemDef momentum;

		// Red
		public static ItemDef chiliPepper;
		public static ItemDef soulCleanser;

		// Void
		public static ItemDef disruptedCharger;

		// Lunar Equipment
		public static EquipmentDef nuclearBomb;
		public static EquipmentDef exitSign;

		/// <summary>
		/// Creates and registers the custom modded items.
		/// </summary>
		public static void RegisterItems()
		{
			dopamineInjection = White.DopamineInjection.Instance();
			richGunpowder = White.RichGunpowder.Instance();
			soulExtractor = White.SoulExtractor.Instance();
			weightedDie = White.WeightedDie.Instance();

			luckyJuicer = Green.LuckyJuicer.Instance();
			
			momentum = Green.Momentum.Instance();

			chiliPepper = Red.ChiliPepper.Instance();
			soulCleanser = Red.SoulCleanser.Instance();

			disruptedCharger = Void.DisruptedCharger.Instance();

			nuclearBomb = Equipment.NuclearBomb.Instance();
			exitSign = Equipment.ExitSign.Instance();
		}
	}
}
