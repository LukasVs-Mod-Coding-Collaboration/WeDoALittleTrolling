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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Utilities;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeDoALittleQualityOfLife.Common.Utilities
{
    internal class WDALQOLPlayerUtil : ModPlayer
    {
        public Player player;
        
        public override void Initialize()
        {
            player = this.Player;
        }

        public static bool IsBossActive()
        {
            for(int i = 0;i < Main.npc.Length; i++)
            {
                if
                (
                    Main.npc[i].active &&
                    (
                        Main.npc[i].boss ||
                        Main.npc[i].type == NPCID.EaterofWorldsHead ||
                        Main.npc[i].type == NPCID.EaterofWorldsBody ||
                        Main.npc[i].type == NPCID.EaterofWorldsTail
                    )
                )
                {
                    return true;
                }
            }
            return false;
        }

        public override void OnRespawn()
        {
            if(!IsBossActive())
            {
                player.Heal(999999);
            }
            base.OnRespawn();
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if(!IsBossActive())
            {
                player.respawnTimer = 180;
            }
            base.Kill(damage, hitDirection, pvp, damageSource);
        }

        public override void UpdateEquips()
        {
            player.arcticDivingGear = true;
            base.UpdateEquips();
        }
    }
}
