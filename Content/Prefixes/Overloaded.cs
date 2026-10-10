/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2026 LukasV-Coding

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria;
using Terraria.GameContent.Prefixes;

namespace WeDoALittleTrolling.Content.Prefixes
{
    public class Overloaded : ModPrefix
    {

        public static readonly PrefixCategory[] PrefixCategories =
        {
            PrefixCategory.Melee,
            PrefixCategory.Ranged,
            PrefixCategory.Magic,
            PrefixCategory.Summon,
            PrefixCategory.AnyWeapon,
            PrefixCategory.Accessory,
            PrefixCategory.Custom,
        };

        // TAPI_1.4.5.8: Hacky Workaround over wrong best reforge detection, disabled at the moment.
        /*
        public static void RegisterHooks()
        {
            On_Item.GetRollablePrefixes += On_Item_GetRollablePrefixes;
            On_Item.TryGetPrefixStatMultipliersForItem += On_Item_TryGetPrefixStatMultipliersForItem;
        }

        public static void UnregisterHooks()
        {
            On_Item.GetRollablePrefixes -= On_Item_GetRollablePrefixes;
            On_Item.TryGetPrefixStatMultipliersForItem -= On_Item_TryGetPrefixStatMultipliersForItem;
        }

        public static int[] On_Item_GetRollablePrefixes(Terraria.On_Item.orig_GetRollablePrefixes orig, Terraria.Item self)
        {
            int[] ret = orig.Invoke(self);
            if (ret != null && self != null)
            {
                foreach (PrefixCategory ctg in PrefixCategories)
                {
                    foreach (ModPrefix mPrefix in PrefixLoader.GetPrefixesInCategory(ctg))
                    {
                        bool alreadyInjected = (self.GetPrefixCategories().Contains(mPrefix.Category) && ret.Contains(mPrefix.Type)) || !self.GetPrefixCategories().Contains(mPrefix.Category);
                        if (!alreadyInjected && mPrefix.CanRoll(self) && mPrefix.RollChance(self) > 0f)
                        {
                            ret = ret.Append(mPrefix.Type).ToArray();
                        }
                    }
                }
            }
            return ret;
        }

        public static bool On_Item_TryGetPrefixStatMultipliersForItem(Terraria.On_Item.orig_TryGetPrefixStatMultipliersForItem orig, Terraria.Item self, int rolledPrefix, out float dmg, out float kb, out float spd, out float size, out float shtspd, out float mcst, out int crt, out int tagdmg, out int arpen, out float value)
        {
            bool ret = orig.Invoke(self, rolledPrefix, out dmg, out kb, out spd, out size, out shtspd, out mcst, out crt, out tagdmg, out arpen, out value);
            foreach (PrefixCategory ctg in PrefixCategories)
            {
                foreach (ModPrefix mPrefix in PrefixLoader.GetPrefixesInCategory(ctg))
                {
                    if (mPrefix.Type == rolledPrefix)
                    {
                        mPrefix.ModifyValue(ref value);
                        break;
                    }
                }
            }
            return ret;
        }
        */

        public override PrefixCategory Category => PrefixCategory.Summon;
        public override float RollChance(Item item)
        {
            return 0.5f;
        }

        public override bool CanRoll(Item item)
        {
            if (item.DamageType == DamageClass.Summon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= 1.25f;
            knockbackMult *= 1.25f;
            useTimeMult *= 0.75f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.9f;
        }
    }

}
