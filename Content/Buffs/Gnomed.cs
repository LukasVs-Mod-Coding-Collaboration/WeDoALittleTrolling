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

using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.NPCs;
using ReLogic.Content;
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Buffs
{
    public class Gnomed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        
        public override void Update(Player player, ref int buffIndex)
        {
            if(Gnomed_ShouldTurnToStone(player))
            {
                player.GetModPlayer<WDALTPlayer>().gnomedStonedDebuff = true;
            }
            else
            {
                player.GetModPlayer<WDALTPlayer>().gnomedStonedDebuff = false;
            }
            if (player.GetModPlayer<WDALTPlayer>().gnomedDebuffTicksLeft > 0)
            {
                player.GetModPlayer<WDALTPlayer>().gnomedDebuffTicksLeft--;
            }
            else
            {
                player.GetModPlayer<WDALTPlayer>().gnomedDebuffTicksLeft = 0;
                player.GetModPlayer<WDALTPlayer>().gnomedDebuff = false;
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }

        public static bool Gnomed_ShouldTurnToStone(Player player)
        {
            if(player.buffImmune[BuffID.Stoned])
            {
                return false;
            }
            if(Main.remixWorld)
            {
                if((player.position.Y / 16f) > (float)(Main.maxTilesY - 350))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if(Main.dayTime)
            {
                if(WorldGen.InAPlaceWithWind(player.position, player.width, player.height))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
