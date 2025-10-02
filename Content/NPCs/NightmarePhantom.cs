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
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Configs;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Items.Material;

namespace WeDoALittleTrolling.Content.NPCs
{
    public class NightmarePhantom : ModNPC
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
            NPC.knockBackResist = 0f;
            NPC.lifeMax = 240;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.scale = 1.5f;
            if (Main.hardMode)
            {
                NPC.damage *= 2;
                NPC.lifeMax *= 2;
                NPC.defense *= 2;
            }
            if (NPC.downedPlantBoss)
            {
                NPC.damage *= 2;
                NPC.lifeMax *= 2;
                NPC.defense *= 2;
            }
            AIType = NPCID.Ghost;
            AnimationType = NPCID.Ghost;
            NPC.GetGlobalNPC<WDALTNPCUtil>().nightmarePhantom = true;
        }

        public static void RegisterHooks()
        {
            On_NPC.SetTargetTrackingValues += On_NPC_SetTargetTrackingValues;
        }

        public static void UnregisterHooks()
        {
            On_NPC.SetTargetTrackingValues -= On_NPC_SetTargetTrackingValues;
        }

        public static void On_NPC_SetTargetTrackingValues(On_NPC.orig_SetTargetTrackingValues orig, NPC self, bool faceTarget, float realDist, int tankTarget)
        {
            orig.Invoke(self, faceTarget, realDist, tankTarget);
            if
            (
                self.GetGlobalNPC<WDALTNPCUtil>().nightmarePhantom &&
                faceTarget &&
                Main.player[self.target] != null &&
                Main.player[self.target].active &&
                !Main.player[self.target].dead
            )
            {
                self.direction = 1;
                if ((float)(self.targetRect.X + self.targetRect.Width / 2) < self.position.X + (float)(self.width / 2))
                {
                    self.direction = -1;
                }
                self.directionY = 1;
                if ((float)(self.targetRect.Y + self.targetRect.Height / 2) < self.position.Y + (float)(self.height / 2))
                {
                    self.directionY = -1;
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.GetModPlayer<WDALTPlayer>().hauntedDebuffTicksLeft = 18000;
            target.GetModPlayer<WDALTPlayer>().hauntedDebuff = true;
            target.AddBuff(ModContent.BuffType<Haunted>(), 18000, true);
            Haunted.AnimateHaunted(target);
            base.OnHitPlayer(target, hurtInfo);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange
            (
                new IBestiaryInfoElement[]
                {
                    BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                    new FlavorTextBestiaryInfoElement("Even after death, the Nightmare Phantoms will still haunt their targets in the dark...")
                }
            );
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
            AI_013_NightmarePhantom();
            if
            (
                (ticksAlive % 75 < 15) &&
                Main.player[NPC.target] != null &&
                Main.player[NPC.target].active &&
                !Main.player[NPC.target].dead
            )
            {
                return false;
            }
            else if (Main.player[NPC.target] != null && Main.player[NPC.target].dead)
            {
                NPC.EncourageDespawn(15);
            }
            return base.PreAI();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ModContent.GetInstance<WDALTServerConfig>().DisableCustomTheConstant)
            {
                return base.SpawnChance(spawnInfo);
            }
            if
            (
                Main.dontStarveWorld &&
                (
                    spawnInfo.Player.ZoneGraveyard ||
                    (
                        !Main.IsItDay() &&
                        !WDALTPlayerUtil.IsBossActive() &&
                        spawnInfo.Player.TryGetModPlayer<WDALTPlayerUtil>(out WDALTPlayerUtil util) &&
                        !util.IsBehindHousingWall() &&
                        !spawnInfo.PlayerInTown &&
                        spawnInfo.Player.ZoneOverworldHeight &&
                        Main.moonPhase != 0 &&
                        !Main.bloodMoon
                    )
                )
            )
            {
                return 0.25f;
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
                Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Wraith, dustVelocity, 0, default);
                newDust.noGravity = true;
            }
            base.HitEffect(hit);
        }

        private void AI_013_NightmarePhantom()
        {
            ticksAlive++;
            Lighting.AddLight(NPC.Center, Color.Red.ToVector3() * 0.5f);
            if(ticksAlive % 75 == 0)
            {
                if
                (
                    Main.player[NPC.target] != null &&
                    Main.player[NPC.target].active &&
                    !Main.player[NPC.target].dead
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
                        Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.RedTorch, dustVelocity, 0, default);
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
