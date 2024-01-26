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
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;

namespace WeDoALittleTrolling.Content.Prefixes
{

    public class The : ModPrefix
    {

        //public virtual float Power => 1f;
        // Change your category this way, defaults to PrefixCategory.Custom. Affects which items can get this prefix.
        public override PrefixCategory Category => PrefixCategory.Melee;
        public override float RollChance(Item item)
        {
            return 0.01f;
        }

        // Determines if it can roll at all.
        public override bool CanRoll(Item item)
        {
            return true;
        }

        // Use this function to modify these stats for items which have this prefix:
        // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            knockbackMult *= 1.01f;
        }

        // Modify the cost of items with this modifier with this function.
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 25f;
        }

        // This is used to modify most other stats of items which have this modifier.
        public override void Apply(Item item)
        {
            //
        }
    }
}