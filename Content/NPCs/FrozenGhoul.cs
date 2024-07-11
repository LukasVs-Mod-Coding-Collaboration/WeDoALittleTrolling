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

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Items.Material;

namespace WeDoALittleTrolling.Content.NPCs
{
    public class FrozenGhoul : ModNPC
    {
        public long ticksAlive = 0;
        public bool leapFlag = false;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.DesertGhoul];
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                PortraitScale = 1f,
                Scale = 1f,
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 44;
            NPC.aiStyle = 3;
            NPC.HitSound = SoundID.NPCHit27;
            NPC.DeathSound = SoundID.NPCDeath30;
            NPC.value = 1200f;
            NPC.knockBackResist = 0f;
            NPC.lifeMax = 900;
            NPC.damage = 80;
            NPC.defense = 40;
            NPC.scale = 1f;

            AIType = NPCID.DesertGhoul;
            AnimationType = NPCID.DesertGhoul;
            Banner = Item.NPCtoBanner(NPCID.DesertGhoul);
            BannerItem = Item.BannerToItem(Banner);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Frostburn, 480, true); //8s, X2 in Expert Mode, X2.5 in Master Mode
            base.OnHitPlayer(target, hurtInfo);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange
            (
                new IBestiaryInfoElement[]
                {
                    BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,
                    new FlavorTextBestiaryInfoElement("Ghouls frozen by the endless ice have obtained the ability to perform a powerful magic leap towards their victims.")
                }
            );
        }

        public override void AI()
        {
            AI_009_GhoulicLeaper();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if(Main.hardMode && spawnInfo.Player.ZoneSnow && spawnInfo.Player.ZoneRockLayerHeight && !spawnInfo.PlayerInTown && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                return 0.2f;
            }
            return base.SpawnChance(spawnInfo);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            int dropAmountMin = 1;
            int dropAmountMax = 1;
            int chanceNumerator = 40; // 40% chance
            int chanceDenominator = 100;
            int itemID = ModContent.ItemType<FrozenFossil>();
            CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
            npcLoot.Add(drop);
            base.ModifyNPCLoot(npcLoot);
        }

        private void AI_009_GhoulicLeaper()
        {
            ticksAlive++;
            Lighting.AddLight(NPC.Center, Color.Cyan.ToVector3() * 0.6f);
            int tickCount = (int)(ticksAlive % 60);
            if
            (
                (
                    tickCount == 50 || tickCount >= 54
                ) &&
                (
                    NPC.target >= 0 &&
                    NPC.target < Main.player.Length
                )
            )
            {
                if
                (
                    Main.player[NPC.target] != null &&
                    Main.player[NPC.target].active &&
                    !Main.player[NPC.target].dead
                )
                {
                    if(tickCount == 50)
                    {
                        NPC.velocity.Y -= 8f;
                    }
                    float distX = Math.Abs(Main.player[NPC.target].Center.X - NPC.Center.X);
                    bool hitLine =
                    Collision.CanHitLine
                    (
                        NPC.position,
                        NPC.width,
                        NPC.height,
                        Main.player[NPC.target].position,
                        Main.player[NPC.target].width,
                        Main.player[NPC.target].height
                    );
                    if(tickCount == 54)
                    {
                        if(hitLine)
                        {
                            leapFlag = true;
                        }
                        else
                        {
                            leapFlag = false;
                        }
                    }
                    if
                    (
                        tickCount >= 54 &&
                        leapFlag
                    )
                    {
                        Vector2 vectorToTarget = Main.player[NPC.target].Center - NPC.Center;
                        vectorToTarget.Normalize();
                        vectorToTarget *= 1.75f;
                        if(distX >= (18 * 16) && !hitLine && Main.player[NPC.target].Center.Y <= NPC.Center.Y)
                        {
                            vectorToTarget.Y *= 1.25f;
                        }
                        NPC.velocity += vectorToTarget;
                        if (vectorToTarget.X > 0)
                        {
                            NPC.direction = 1;
                        }
                        else if (vectorToTarget.X < 0)
                        {
                            NPC.direction = -1;
                        }
                        NPC.spriteDirection = NPC.direction;
                        if(tickCount == 54)
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
                                dustVelocity.Normalize();
                                dustVelocity *= 8f;
                                Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Ice, dustVelocity, 0, default);
                                newDust.noGravity = true;
                            }
                            SoundEngine.PlaySound(SoundID.Item27, NPC.Center);
                        }
                    }
                }
            }
        }
    }
}
