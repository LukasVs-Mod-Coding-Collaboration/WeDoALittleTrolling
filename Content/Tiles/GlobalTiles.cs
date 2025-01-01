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
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.Items.Tools;

namespace WeDoALittleTrolling.Content.Tiles
{
    internal class GlobalTiles : GlobalTile
    {
        public static void RegisterHooks()
        {
            On_Player.GetPickaxeDamage += On_Player_GetPickaxeDamage;
        }

        public static void UnregisterHooks()
        {
            On_Player.GetPickaxeDamage -= On_Player_GetPickaxeDamage;
        }

        public static int On_Player_GetPickaxeDamage(On_Player.orig_GetPickaxeDamage orig, Player self, int x, int y, int pickPower, int hitBufferIndex, Tile tileTarget)
        {
            if (tileTarget.TileType == TileID.LihzahrdAltar)
            {
                return 100;
            }
            if (self.HeldItem.type == ModContent.ItemType<BedrockBreaker>())
            {
                return 100;
            }
            return orig.Invoke(self, x, y, pickPower, hitBufferIndex, tileTarget);
        }
        /*
        //Code to Modify Tile Wire Activation
        public override void HitWire(int i, int j, int type)
        {
            int tileStyle = (Main.tile[i , j].TileFrameX / 36);
            if(type == TileID.Statues && tileStyle == 4) //Check if Tile is a Statue and if the Tile Style is 4 (That's the slime statue).
            {
                int spawnAmount = 1;
                for(int index = 0; index < spawnAmount; index++)
                {
                    NPC.NewNPC(new EntitySource_TileUpdate(i, j), i * 16 + Main.rand.Next(0, 16), j * 16 + Main.rand.Next(0, 16), NPCID.BlueSlime); //Main.rand.Next(0, 16) generates a random number between 0 and 15, we use that as a spawn position offset here.
                }
            }
        }
        */
    }
    /*
    //Code to Modify Tile Wire Activation
    public class SimeStatueDisableVanillaSpawning : GlobalNPC
    {
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if(npc.type == NPCID.BlueSlime && source is EntitySource_Wiring) //Check if npc is Blue Slime and if it was spawned by a statue
            {
                npc.active = false; //Prevent npc from spawning
            }
            base.OnSpawn(npc, source);
        }
    }
    */
}

