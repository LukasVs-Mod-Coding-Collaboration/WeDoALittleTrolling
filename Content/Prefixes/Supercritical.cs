/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2024 LukasV-Coding

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

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Content.Items.Weapons;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WeDoALittleTrolling.Content.Prefixes
{

    public class Supercritical : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.AnyWeapon;
        public override float RollChance(Item item)
        {
            return 0.5f;
        }

        public override bool CanRoll(Item item)
        {
            if
            (
                item.DamageType == DamageClass.Magic ||
                item.DamageType == DamageClass.MagicSummonHybrid ||
                item.DamageType == DamageClass.Ranged ||
                item.DamageType == DamageClass.Throwing
            )
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
            damageMult *= 0.75f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.8f;
        }

        public override void Apply(Item item)
        {
            //
        }

        public LocalizedText AdditionalTooltip => this.GetLocalization(nameof(AdditionalTooltip));

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            int critDamageBonus = 100;
            yield return new TooltipLine(Mod, "PrefixWeaponSupercriticalDescription", "+"+critDamageBonus+AdditionalTooltip.Value) {
                IsModifier = true,
            };
        }

        public override void SetStaticDefaults() {
            _ = AdditionalTooltip;
        }
    }
}