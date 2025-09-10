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

using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Items.Material;

namespace WeDoALittleTrolling.Content.NPCs
{
    public class FrozenPhantom : ModNPC
    {
        public long ticksAlive = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Ghost];
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                PortraitScale = 1f,
                Scale = 1f,
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Ghost);
            NPC.width = 22;
            NPC.height = 44;
            NPC.HitSound = SoundID.NPCHit36;
            NPC.DeathSound = SoundID.NPCDeath39;
            NPC.value = 1600f;
            NPC.knockBackResist = 0.2f;
            NPC.lifeMax = 450;
            NPC.damage = 60;
            NPC.defense = 30;
            NPC.scale = 1.4f;
            AIType = NPCID.Ghost;
            AnimationType = NPCID.Ghost;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.rand.NextBool(6))
            {
                target.AddBuff(BuffID.Frozen, 36, true); //0.6s, X2 in Expert Mode, X2.5 in Master Mode
            }
            else
            {
                target.AddBuff(BuffID.Chilled, 360, true); //6s, X2 in Expert Mode, X2.5 in Master Mode
            }
            base.OnHitPlayer(target, hurtInfo);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange
            (
                new IBestiaryInfoElement[]
                {
                    BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,
                    new FlavorTextBestiaryInfoElement("The aggressive Frozen Phantoms will haunt their targets through the endless ice...")
                }
            );
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            int dropAmountMin = 1;
            int dropAmountMax = 1;
            int chanceNumerator = 60; // 60% chance
            int chanceDenominator = 100;
            int itemID = ModContent.ItemType<FrozenEssence>();
            CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
            npcLoot.Add(drop);
            base.ModifyNPCLoot(npcLoot);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((long)ticksAlive);
            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            ticksAlive = reader.ReadInt64();
            base.ReceiveExtraAI(reader);
        }

        public override bool PreAI()
        {
            AI_022_FrozenPhantom();
            if
            (
                (ticksAlive % 135 < 15) &&
                Main.player[NPC.target] != null &&
                Main.player[NPC.target].active &&
                !Main.player[NPC.target].dead &&
                (
                    Main.player[NPC.target].itemAnimation > 0 ||
                    Main.player[NPC.target].aggro >= 0
                )
            )
            {
                NPC.knockBackResist = 0f;
                return false;
            }
            else if (Main.player[NPC.target] != null && (Main.player[NPC.target].dead || (Main.player[NPC.target].aggro < 0 && Main.player[NPC.target].itemAnimation == 0)))
            {
                NPC.EncourageDespawn(15);
            }
            NPC.knockBackResist = 0.2f;
            return base.PreAI();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if(Main.hardMode && spawnInfo.Player.ZoneSnow && spawnInfo.Player.ZoneRockLayerHeight && !spawnInfo.PlayerInTown && !spawnInfo.Player.GetModPlayer<WDALTPlayerUtil>().IsBehindHousingWall() && NPC.downedPlantBoss)
            {
                return 0.025f;
            }
            return base.SpawnChance(spawnInfo);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 60; i++)
            {
                int rMax = (int)NPC.width;
                double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                double angle = Main.rand.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = NPC.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                Vector2 dustVelocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                dustVelocity = dustVelocity.SafeNormalize(Vector2.Zero);
                dustVelocity *= 8f;
                Dust newDust = Dust.NewDustPerfect(dustPosition, 54, dustVelocity, 0, default);
                newDust.noGravity = true;
            }
            base.HitEffect(hit);
        }

        private void AI_022_FrozenPhantom()
        {
            ticksAlive++;
            Lighting.AddLight(NPC.Center, Color.Cyan.ToVector3() * 0.6f);
            if (ticksAlive % 135 == 0)
            {
                if
                (
                    Main.player[NPC.target] != null &&
                    Main.player[NPC.target].active &&
                    !Main.player[NPC.target].dead &&
                    (
                        Main.player[NPC.target].itemAnimation > 0 ||
                        Main.player[NPC.target].aggro >= 0 
                    )
                )
                {
                    Vector2 vectorToTarget = Main.player[NPC.target].Center - NPC.Center;
                    vectorToTarget = vectorToTarget.SafeNormalize(Vector2.Zero);
                    vectorToTarget *= 8f;
                    vectorToTarget.Y *= 1.375f;
                    NPC.velocity += vectorToTarget;
                    for (int i = 0; i < 128; i++)
                    {
                        int rMax = (int)NPC.width;
                        double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                        double angle = Main.rand.NextDouble() * 2 * Math.PI;
                        int xOffset = (int)Math.Round(r * Math.Cos(angle));
                        int yOffset = (int)Math.Round(r * Math.Sin(angle));
                        Vector2 dustPosition = NPC.Center;
                        dustPosition.X += xOffset;
                        dustPosition.Y += yOffset;
                        Vector2 dustVelocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                        dustVelocity = dustVelocity.SafeNormalize(Vector2.Zero);
                        dustVelocity *= 8f;
                        Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.IceTorch, dustVelocity, 0, default);
                        newDust.noGravity = true;
                    }
                    if (Main.dedServ && Main.netMode == NetmodeID.Server)
                    {
                        NPC.netUpdate = true;
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundID.Zombie53, NPC.Center);
                    }
                }
            }
        }
    }
}
*/