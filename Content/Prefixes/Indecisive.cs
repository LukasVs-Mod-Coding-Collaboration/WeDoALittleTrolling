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
    public class Indecisive : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;
        public override float RollChance(Item item)
        {
            return 0.2f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.5f;
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.statDefense += 1;
            player.moveSpeed += 0.01f;
            player.statManaMax2 += 10;
            player.GetDamage(DamageClass.Generic) += 0.01f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.01f;
            player.GetCritChance(DamageClass.Generic) += 1.0f;
        }

        public LocalizedText AdditionalTooltip => this.GetLocalization(nameof(AdditionalTooltip));

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "PrefixAccessoryIndecisiveDescription", AdditionalTooltip.Value) {
                IsModifier = true,
            };
        }

        public override void SetStaticDefaults() {
            _ = AdditionalTooltip;
        }
    }

}
