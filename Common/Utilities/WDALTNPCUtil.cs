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
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using System.IO;
using WeDoALittleTrolling.Common.ModPlayers;
using System.Collections.Generic;
using Terraria.ID;
using WeDoALittleTrolling.Content.Projectiles;

namespace WeDoALittleTrolling.Common.Utilities
{
    internal class WDALTNPCUtil : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public int VileSpitTimeLeft;
        public long ticksAlive = 0;
        public long lastActionTick = 0;
        public bool nightmarePhantom = false;
        public int golemBoulderIteration = 0;
        public int BlazingShieldOwnerIndex = -1;
        public Vector2 golemBoulderStartPosition;
        public int freedomRoundDamageStack = 0;
        private bool __AI_IS_FROZEN = false;
        private int __AI_FROZEN_TICKS = 0;

        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
        }

        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            binaryWriter.Write((int)npc.damage);
            binaryWriter.Write((int)VileSpitTimeLeft);
            binaryWriter.Write((int)BlazingShieldOwnerIndex);
            binaryWriter.Write((bool)__AI_IS_FROZEN);
            binaryWriter.Write((int)__AI_FROZEN_TICKS);
            base.SendExtraAI(npc, bitWriter, binaryWriter);
        }

        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
        {
            npc.damage = binaryReader.ReadInt32();
            VileSpitTimeLeft = binaryReader.ReadInt32();
            BlazingShieldOwnerIndex = binaryReader.ReadInt32();
            bool ___TMP__AI_IS_FROZEN = binaryReader.ReadBoolean();
            __AI_FROZEN_TICKS = binaryReader.ReadInt32();
            if (__AI_IS_FROZEN != ___TMP__AI_IS_FROZEN)
            {
                if (___TMP__AI_IS_FROZEN)
                {
                    npc.velocity = Vector2.Zero;
                }
                npc.netUpdate = true;
            }
            __AI_IS_FROZEN = ___TMP__AI_IS_FROZEN;
            base.ReceiveExtraAI(npc, bitReader, binaryReader);
        }

        public override void PostAI(NPC npc)
        {
            ticksAlive++;
            base.PostAI(npc);
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.type == ModContent.ProjectileType<FreedomRound>())
            {
                modifiers.FlatBonusDamage += (3 * freedomRoundDamageStack);
                if (npc.lifeMax > 100000)
                {
                    modifiers.FlatBonusDamage += (2 * freedomRoundDamageStack);
                }
            }
            else
            {
                base.ModifyHitByProjectile(npc, projectile, ref modifiers);
            }
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.GetModPlayer<WDALTPlayer>().zoneWormCandle)
            {
                spawnRate = (int)((double)spawnRate * 0.25);
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.GetModPlayer<WDALTPlayer>().zoneWormCandle)
            {
                if ((Main.IsItRaining && spawnInfo.Player.ZoneForest) || spawnInfo.Player.ZoneNormalUnderground || spawnInfo.Player.ZoneNormalCaverns)
                {
                    pool[NPCID.Worm] = 5f;
                    pool[NPCID.GoldWorm] = 0.25f;
                }
                if (spawnInfo.Player.ZoneGlowshroom)
                {
                    pool[NPCID.TruffleWorm] = 2.5f;
                }
                if (spawnInfo.Player.ZoneUndergroundDesert)
                {
                    pool[NPCID.TombCrawlerHead] = 5f;
                    if (Main.hardMode)
                    {
                        pool[NPCID.DuneSplicerHead] = 5f;
                    }
                }
                if
                (
                    spawnInfo.Player.ZoneNormalUnderground ||
                    spawnInfo.Player.ZoneNormalCaverns ||
                    (
                        spawnInfo.Player.ZoneJungle &&
                        (
                            spawnInfo.Player.ZoneDirtLayerHeight ||
                            spawnInfo.Player.ZoneRockLayerHeight
                        )
                    )
                )
                {
                    pool[NPCID.GiantWormHead] = 5f;
                    if (Main.hardMode)
                    {
                        pool[NPCID.DiggerHead] = 5f;
                    }
                }
                if (spawnInfo.Player.ZoneCorrupt)
                {
                    pool[NPCID.DevourerHead] = 5f;
                    if (Main.hardMode)
                    {
                        pool[NPCID.SeekerHead] = 5f;
                    }
                }
                if (spawnInfo.Player.ZoneUnderworldHeight)
                {
                    pool[NPCID.BoneSerpentHead] = 5f;
                }
                if (!Main.IsItDay() && !Main.IsItRaining && spawnInfo.Player.ZoneOverworldHeight)
                {
                    pool[NPCID.EnchantedNightcrawler] = 2.5f;
                }
            }
        }

        public static void FreezeNPCForTicks(NPC npc, int ticks)
        {
            if (npc.dontCountMe || !npc.CanBeChasedBy())
            {
                return;
            }
            if (npc.GetGlobalNPC<WDALTNPCUtil>().__AI_FROZEN_TICKS <= 0)
            {
                npc.GetGlobalNPC<WDALTNPCUtil>().__AI_FROZEN_TICKS = ticks;
            }
            __AI_SYNC_SET_FROZEN(npc);
        }

        private static void __AI_SYNC_SET_FROZEN(NPC npc)
        {
            npc.GetGlobalNPC<WDALTNPCUtil>().__AI_IS_FROZEN = true;
            npc.velocity = Vector2.Zero;
            npc.netUpdate = true;
        }

        private static void __AI_SYNC_UNSET_FROZEN(NPC npc)
        {
            npc.GetGlobalNPC<WDALTNPCUtil>().__AI_IS_FROZEN = false;
            npc.netUpdate = true;
        }

        public override bool PreAI(NPC npc)
        {
            if (__AI_IS_FROZEN)
            {
                if (__AI_FROZEN_TICKS > 0)
                {
                    npc.velocity = Vector2.Zero;
                    __AI_FROZEN_TICKS--;
                }
                else
                {
                    __AI_SYNC_UNSET_FROZEN(npc);
                }
                return false;
            }
            return base.PreAI(npc);
        }
    }
}
