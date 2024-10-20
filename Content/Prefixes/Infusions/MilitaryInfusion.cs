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
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Content.Items.Weapons;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using System.Linq;
using Terraria.Localization;
using System.Collections.Generic;

namespace WeDoALittleTrolling.Content.Prefixes
{

    public class MilitaryInfusion : ModPrefix
    {
        /*public static readonly int[] CompatibleItemIDs =
        {
            ItemID.TacticalShotgun,
            ItemID.SniperRifle,
            ItemID.RocketLauncher
        };*/

        public override PrefixCategory Category => PrefixCategory.Ranged;
        public override float RollChance(Item item)
        {
            return 0.5f;
        }

        public override bool CanRoll(Item item)
        {
            if(/*CompatibleItemIDs.Contains(item.type) || */item.useAmmo == AmmoID.Bullet || item.useAmmo == AmmoID.Rocket)
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
            damageMult *= 1.30f;
            useTimeMult *= 0.7f;
            critBonus += 20;
            knockbackMult *= 1.20f;
            shootSpeedMult *= 1.20f;
        }

        public LocalizedText AdditionalTooltip => this.GetLocalization(nameof(AdditionalTooltip));

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "PrefixAcessoryMiningInfusionDescription", AdditionalTooltip.Value) {
                IsModifier = true,
            };
        }

        public override void SetStaticDefaults() {
            _ = AdditionalTooltip;
        }
    }
}
