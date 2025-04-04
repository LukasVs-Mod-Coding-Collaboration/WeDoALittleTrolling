﻿/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2025 LukasV-Coding

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

namespace WeDoALittleTrolling.Content.Prefixes
{
    public class Watchful : ModPrefix
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

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.4f;
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.maxTurrets += 1;
            player.GetDamage(DamageClass.Generic) -= 0.04f;
        }

        public LocalizedText AdditionalTooltip => this.GetLocalization(nameof(AdditionalTooltip));
        public LocalizedText AdditionalTooltip2 => this.GetLocalization(nameof(AdditionalTooltip2));

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "PrefixAccessoryWatchfulDescriptionNegative", AdditionalTooltip.Value)
            {
                IsModifier = true,
                IsModifierBad = true,
            };
            yield return new TooltipLine(Mod, "PrefixAccessoryWatchfulDescriptionPositive", AdditionalTooltip2.Value)
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
