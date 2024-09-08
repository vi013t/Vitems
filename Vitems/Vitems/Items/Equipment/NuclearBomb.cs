using UnityEngine.AddressableAssets;
using RoR2;
using UnityEngine;
using R2API;

namespace Vitems.Items.Equipment
{
    public class NuclearBomb : EquipmentDef
    {
        private static NuclearBomb itemInstance = null;

        private NuclearBomb()
        {
            name = "NUCLEAR_BOMB_NAME";
            nameToken = "NUCLEAR_BOMB_NAME";
            pickupToken = "NUCLEAR_BOMB_PICKUP";
            descriptionToken = "NUCLEAR_BOMB_DESCRIPTION";
            loreToken = "NUCLEAR_BOMB_LORE";
            isLunar = true;
            pickupIconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Common/MiscIcons/texMysteryIcon.png").WaitForCompletion();
            pickupModelPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mystery/PickupMystery.prefab").WaitForCompletion();
        }

        public static NuclearBomb Instance()
        {
            if (itemInstance == null)
            {
                // Register the item
                itemInstance = CreateInstance<NuclearBomb>();
                ItemAPI.Add(new CustomEquipment(itemInstance, new ItemDisplayRuleDict(null)));

                // Language tokens
                LanguageAPI.Add("NUCLEAR_BOMB_NAME", "Nuclear Bomb");
                LanguageAPI.Add("NUCLEAR_BOMB_PICKUP", "Call down a massive nuclear bomb, dealing immense damage and leaving a permanent radiation zone that damages allies.");
                LanguageAPI.Add("NUCLEAR_BOMB_DESCRIPTION", "On activation, call down a nuclear bomb dealing <style=cIsDamage>25,000%</style> base damage to all enemies in a <style=cIsUtility>30m</style> range, leaving behind a permanent radioactive zone that deals <style=cIsDamage>10%</style> max health in damage every second to all allies that enter it.");
            }
            return itemInstance;
        }
    }
}
