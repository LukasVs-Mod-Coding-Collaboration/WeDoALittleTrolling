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
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using WeDoALittleTrolling.Content.Projectiles;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using System.Collections.Generic;

namespace WeDoALittleTrolling.Content.Prefixes
{

    public class Focusing : ModPrefix
    {

        public override PrefixCategory Category => PrefixCategory.Accessory;
        public override float RollChance(Item item)
        {
            if(!Main.hardMode)
            {
                return 0f;
            }
            return 0.50f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            //
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.25f;
        }

        public override void Apply(Item item)
        {
            //
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.GetDamage(DamageClass.Ranged) -= 0.04f;
            player.GetCritChance(DamageClass.Ranged) += 8;
        }

        public LocalizedText AdditionalTooltip => this.GetLocalization(nameof(AdditionalTooltip));
        public LocalizedText AdditionalTooltip2 => this.GetLocalization(nameof(AdditionalTooltip2));

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "PrefixAccessoryFocusingDescriptionNegative", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = true,
            };
            yield return new TooltipLine(Mod, "PrefixAccessoryFocusingDescriptionPositive", AdditionalTooltip2.Value)
            {
                IsModifier = true,
                IsModifierBad = false,
            };
        }

        public override void SetStaticDefaults()
        {
            _ = AdditionalTooltip;
            _ = AdditionalTooltip2;
        }
    }
}
