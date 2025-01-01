/*
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
/*

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using Terraria.Localization;
using System.Collections.Generic;

namespace WeDoALittleTrolling.Content.Prefixes
{

    public class Minute : ModPrefix
    {

        public override PrefixCategory Category => PrefixCategory.Melee;
        public override float RollChance(Item item)
        {
            return 1.5f;
        }

        public override bool CanRoll(Item item)
        {
            if (item.pick > 0 || item.hammer > 0 || item.axe > 0)
            {
                return true;
            }
            return false;
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            scaleMult *= 0.5f;
            useTimeMult *= 0.5f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.5f;
        }

        public override void Apply(Item item)
        {
            item.tileBoost = -2;
        }

        public LocalizedText AdditionalTooltip => this.GetLocalization(nameof(AdditionalTooltip));

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "PrefixToolMinuteDescription", AdditionalTooltip.Value) {
                IsModifier = true,
                IsModifierBad = true,
            };
        }

        public override void SetStaticDefaults() {
            _ = AdditionalTooltip;
        }
    }
}
*/