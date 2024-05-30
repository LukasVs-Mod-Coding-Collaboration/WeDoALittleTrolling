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
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Items.Accessories;
using SteelSeries.GameSense;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.NPCs;

namespace WeDoALittleTrolling.Common.Utilities
{
    internal class WDALTPlayerUtil : ModPlayer
    {
        public Player player;
        public static UnifiedRandom random = new UnifiedRandom();
        public long currentTick;
        public int[] hitAmountInLastSecondRingBuffer = new int[60];
        public int hitAmountInLastSecondRingBufferIndex = 0;
        public int currentHitAmount;
        
        public override void Initialize()
        {
            player = this.Player;
            currentTick = 0;
            currentHitAmount = 0;
            hitAmountInLastSecondRingBufferIndex = 0;
            for (int i = 0; i < hitAmountInLastSecondRingBuffer.Length; i++)
            {
                hitAmountInLastSecondRingBuffer[i] = 0;
            }
        }

        public override void PreUpdate()
        {
            currentTick++;
            if (hitAmountInLastSecondRingBufferIndex < (hitAmountInLastSecondRingBuffer.Length - 1))
            {
                hitAmountInLastSecondRingBufferIndex++;
            }
            else
            {
                hitAmountInLastSecondRingBufferIndex = 0;
            }
            hitAmountInLastSecondRingBuffer[hitAmountInLastSecondRingBufferIndex] = currentHitAmount;
            currentHitAmount = 0;
        }

        public int GetHitAmountInLastSecond()
        {
            int ret = 0;
            for (int i = 0; i < hitAmountInLastSecondRingBuffer.Length; i++)
            {
                ret += hitAmountInLastSecondRingBuffer[i];
            }
            return ret;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if
            (
                (
                    !target.friendly &&
                    !target.CountsAsACritter &&
                    !target.isLikeATownNPC &&
                    target.type != NPCID.TargetDummy &&
                    target.canGhostHeal &&
                    target.CanBeChasedBy()
                ) ||
                (
                    !target.active
                )
            )
            {
                currentHitAmount++;
            }
        }

        //SmartPVP(TM) Technology: Display actual pvp damage to clients on non-lethal hits and sync health
        //Other relevent code is found in WDALTNetworkingSystem
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if(Main.netMode == NetmodeID.MultiplayerClient && modifiers.PvP)
            {
                if(player.whoAmI != Main.myPlayer)
                {
                    modifiers.SetMaxDamage(1);
                }
            }
            base.ModifyHurt(ref modifiers);
        }
        
        public override void PostHurt(Player.HurtInfo info)
        {
            if(Main.netMode == NetmodeID.MultiplayerClient && info.PvP)
            {
                if(player.whoAmI == Main.myPlayer)
                {
                    ModPacket syncNetFinalDamagePacket = Mod.GetPacket();
                    syncNetFinalDamagePacket.Write(WDALTPacketTypeID.syncNetFinalDamage);
                    syncNetFinalDamagePacket.Write(player.statLife);
                    syncNetFinalDamagePacket.Write(Main.myPlayer);
                    syncNetFinalDamagePacket.Send();
                }
            }
            base.OnHurt(info);
        }

        public bool HasBlazingShield()
        {
            bool ret = false;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<BlazingShieldNPC>())
                {
                    if (Main.npc[i].GetGlobalNPC<WDALTNPCUtil>().BlazingShieldOwnerIndex == player.whoAmI)
                    {
                        if (ret)
                        {
                            if (Main.netMode == NetmodeID.SinglePlayer && player.whoAmI == Main.myPlayer)
                            {
                                Main.npc[i].StrikeInstantKill();
                            }
                            else if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
                            {
                                ModPacket clearBlazingShieldPacket = Mod.GetPacket();
                                clearBlazingShieldPacket.Write(WDALTPacketTypeID.clearBlazingShield);
                                clearBlazingShieldPacket.Write((int)Main.myPlayer);
                                clearBlazingShieldPacket.Send();
                            }
                        }
                        ret = true;
                    }
                }
            }
            return ret;
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

        public bool IsBehindHousingWall()
        {
            int posX = (int)(player.position.X + (float)(player.width / 2)) / 16;
            int posY = (int)(player.position.Y + (float)(player.height / 2)) / 16;
            if (Main.wallHouse[Main.tile[posX, posY].WallType])
            {
                return true;
            }
            return false;
        }

        public bool HasPlayerAcessoryEquipped(int itemID)
        {
            int offset = 3;
            int loopLimit = 5;
            loopLimit += player.extraAccessorySlots;
            if(Main.masterMode)
            {
                loopLimit++;
            }
            for(int i = offset;i < (offset + loopLimit); i++) //Search through all accessory slots
            {
                if(player.armor[i].type == itemID)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetAmountOfEquippedAccessoriesWithPrefixFromPlayer(int prefixID) //Fancy name much smart, haha :P
        {
            int equippedAmount = 0;
            int offset = 3;
            int loopLimit = 5;
            loopLimit += player.extraAccessorySlots;
            if(Main.masterMode)
            {
                loopLimit++;
            }
            for(int i = offset;i < (offset + loopLimit); i++) //Search through all accessory slots
            {
                if(player.armor[i].prefix == prefixID)
                {
                    equippedAmount++;
                }
            }
            return equippedAmount;
        }

        public bool HasPlayerHelmetEquipped(int itemID)
        {
            int offset = 0;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public bool HasPlayerChestplateEquipped(int itemID)
        {
            int offset = 1;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public bool HasPlayerLeggingsEquipped(int itemID)
        {
            int offset = 2;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }
    }
}
