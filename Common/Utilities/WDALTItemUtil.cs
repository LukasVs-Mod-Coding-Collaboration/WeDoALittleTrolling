/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2023 LukasV-Coding

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
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;
using System.Collections.Generic;

namespace WeDoALittleTrolling.Common.Utilities
{
    internal class WDALTItemUtil : GlobalItem
    {
        public override bool InstancePerEntity => true;
        
        public bool attackSpeedRoundingErrorProtection = false;

        //Globally prevent player.itemTimeMax from being one too high due to rounding errors.
        public override float UseTimeMultiplier(Item item, Player player)
        {
            float multiplier = base.UseTimeMultiplier(item, player);
            if(attackSpeedRoundingErrorProtection)
            {
                DamageClass type = item.DamageType;
                float speedValue = player.GetTotalAttackSpeed(type);
                if (speedValue != 0f)
                {
                    float multiplierPerUseTime = multiplier * speedValue / (float)item.useTime;
                    multiplier = multiplier - multiplierPerUseTime; //Decrease itemTimeMax by one more
                }
            }
            return multiplier;
        }
    }
}
