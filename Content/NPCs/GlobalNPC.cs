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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Armor;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using WeDoALittleTrolling.Common.ModSystems;

namespace WeDoALittleTrolling.Content.NPCs
{
    internal class GlobalNPCs : GlobalNPC
    {
        public override bool InstancePerEntity => false;
        public static UnifiedRandom random = new UnifiedRandom();
        public static readonly int[] NerfGroup25Percent =
        {
            NPCID.Derpling,
            NPCID.Antlion,
            NPCID.WalkingAntlion,
            NPCID.GiantWalkingAntlion,
            NPCID.TombCrawlerHead,
            NPCID.JungleCreeper,
            NPCID.JungleCreeperWall,
            NPCID.BlackRecluse,
            NPCID.BlackRecluseWall
        };
        public static readonly int[] NerfGroup35Percent =
        {
            NPCID.GiantTortoise,
            NPCID.IceTortoise,
            NPCID.GiantMossHornet,
            NPCID.BigMossHornet,
            NPCID.MossHornet,
            NPCID.LittleMossHornet,
            NPCID.TinyMossHornet
        };
        public static readonly int[] NerfGroup50Percent =
        {
            NPCID.RedDevil
        };
        public static readonly int[] KnockbackResistanceGroup =
        {
            NPCID.AngryTrapper,
            NPCID.BrainofCthulhu
        };
        public static readonly int[] InflictVenomDebuff1In1Group =
        {
            NPCID.AngryTrapper,
            NPCID.Moth
        };
        public static readonly int[] InflictPoisonDebuff1In1Group =
        {
            NPCID.Snatcher,
            NPCID.ManEater
        };
        public static readonly int[] InflictBleedingDebuff1In1Group =
        {
            NPCID.Shark,
            NPCID.SandShark,
            NPCID.PrimeSaw
        };
        public static readonly int[] InflictBleedingDebuff1In8Group =
        {
            NPCID.Herpling,
            NPCID.Wolf,
            NPCID.PirateCorsair,
            NPCID.PirateGhost
        };
        public static readonly int[] InflictSearingInferno1In1Group =
        {
            NPCID.Golem,
            NPCID.GolemHead,
            NPCID.GolemFistLeft,
            NPCID.GolemFistRight,
            NPCID.GolemHeadFree,
            NPCID.Lihzahrd,
            NPCID.LihzahrdCrawler,
            NPCID.FlyingSnake
        };
        public static readonly int[] InflictBrokenArmor1In1Group =
        {
            NPCID.PrimeSaw
        };
        public static readonly int[] InflictSlowness1In1Group =
        {
            NPCID.PrimeSaw
        };
        public static readonly int[] InflictCursed1In1Group =
        {
            NPCID.PrimeVice
        };
        public static readonly int[] InflictWreckedResistance1In1Group =
        {
            NPCID.SkeletronHand,
            NPCID.QueenSlimeBoss,
            NPCID.DukeFishron,
            NPCID.Plantera,
            NPCID.PlanterasHook,
            NPCID.PlanterasTentacle,
            NPCID.Spore,
            NPCID.HallowBoss,
            NPCID.CultistBoss,
            NPCID.DD2OgreT2,
            NPCID.DD2OgreT3,
            NPCID.DD2Betsy,
            NPCID.MourningWood,
            NPCID.Pumpking,
            NPCID.PumpkingBlade,
            NPCID.Everscream,
            NPCID.SantaNK1,
            NPCID.IceQueen,
            NPCID.MartianSaucer,
            NPCID.MartianSaucerCannon,
            NPCID.MartianSaucerCore,
            NPCID.MartianSaucerTurret,
            NPCID.Mimic,
            NPCID.IceMimic,
            NPCID.PresentMimic,
            NPCID.BigMimicCorruption,
            NPCID.BigMimicCrimson,
            NPCID.BigMimicHallow,
            NPCID.BigMimicJungle
        };
        public static readonly int[] InflictDevastated1In1Group =
        {
            NPCID.CultistDragonHead,
            NPCID.CultistDragonBody1,
            NPCID.CultistDragonBody2,
            NPCID.CultistDragonBody3,
            NPCID.CultistDragonBody4,
            NPCID.CultistDragonTail,
            NPCID.AncientCultistSquidhead,
            NPCID.AncientLight,
            NPCID.AncientDoom,
            NPCID.TheDestroyer,
            NPCID.WyvernHead,
            NPCID.WyvernLegs,
            NPCID.WyvernBody,
            NPCID.WyvernBody2,
            NPCID.WyvernBody3,
            NPCID.WyvernTail
        };

        public override void SetDefaults(NPC npc)
        {
            if(WDALTModSystem.isCalamityModPresent)
            {
                base.SetDefaults(npc);
                return;
            }
            if(NerfGroup25Percent.Contains(npc.type))
            {
                npc.damage = (int)Math.Round(npc.damage * 0.75);
            }
            if(NerfGroup35Percent.Contains(npc.type))
            {
                npc.damage = (int)Math.Round(npc.damage * 0.65);
            }
            if(NerfGroup50Percent.Contains(npc.type))
            {
                npc.damage = (int)Math.Round(npc.damage * 0.5);
            }
            if(KnockbackResistanceGroup.Contains(npc.type))
            {
                npc.knockBackResist = 0f;
            }

            //Boss buffs

            if(npc.type == NPCID.EyeofCthulhu)
            {
                npc.lifeMax *= 3;
                npc.damage = (int)Math.Round(npc.damage * 1.5);
            }
            if(npc.type == NPCID.KingSlime)
            {
                npc.lifeMax *= 3;
                npc.damage = (int)Math.Round(npc.damage * 1.5);
            }
            if
            (
                npc.type == NPCID.SkeletronHead ||
                npc.type == NPCID.SkeletronHand
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
            }
            if(npc.type == NPCID.BrainofCthulhu)
            {
                npc.lifeMax *= 2;
            }
            if(npc.type == NPCID.QueenBee)
            {
                npc.lifeMax *= 2;
            }
            if
            (
                npc.type == NPCID.WallofFlesh ||
                npc.type == NPCID.WallofFleshEye ||
                npc.type == NPCID.TheHungry ||
                npc.type == NPCID.TheHungryII
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
            }
            if
            (
                npc.type == NPCID.Plantera ||
                npc.type == NPCID.PlanterasHook ||
                npc.type == NPCID.PlanterasTentacle ||
                npc.type == NPCID.Spore
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.5);
            }
            if
            (
                npc.type == NPCID.Golem ||
                npc.type == NPCID.GolemHead ||
                npc.type == NPCID.GolemFistLeft ||
                npc.type == NPCID.GolemFistRight ||
                npc.type == NPCID.GolemHeadFree
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.5);
            }
            if(npc.type == NPCID.HallowBoss)
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.25);
            }
            if(npc.type == NPCID.DukeFishron)
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
            }
            if(npc.type == NPCID.CultistBoss)
            {
                npc.lifeMax *= 2;
            }
            if
            (
                npc.type == NPCID.MoonLordCore ||
                npc.type == NPCID.MoonLordHead ||
                npc.type == NPCID.MoonLordHand ||
                npc.type == NPCID.MoonLordFreeEye ||
                npc.type == NPCID.MoonLordLeechBlob
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.5);
            }
            if
            (
                npc.type == NPCID.SkeletronPrime ||
                npc.type == NPCID.PrimeCannon ||
                npc.type == NPCID.PrimeLaser
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
            }
            if
            (
                npc.type == NPCID.Retinazer ||
                npc.type == NPCID.Spazmatism
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
            }
            if
            (
                npc.type == NPCID.QueenSlimeBoss
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
            }
            if
            (
                npc.type == NPCID.EaterofWorldsHead ||
                npc.type == NPCID.EaterofWorldsBody ||
                npc.type == NPCID.EaterofWorldsTail
            )
            {
                npc.lifeMax *= 2;
            }
            if(npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                npc.dontTakeDamage = true;
            }
            if
            (
                npc.type == NPCID.TheDestroyer ||
                npc.type == NPCID.TheDestroyerBody ||
                npc.type == NPCID.TheDestroyerTail
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
            }
            if
            (
                npc.type == NPCID.DD2DarkMageT1 ||
                npc.type == NPCID.DD2DarkMageT3
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
            }
            if
            (
                npc.type == NPCID.DD2OgreT2 ||
                npc.type == NPCID.DD2OgreT3
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
            }
            if
            (
                npc.type == NPCID.DD2Betsy
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
            }
            if
            (
                npc.type == NPCID.PirateShip ||
                npc.type == NPCID.PirateShipCannon
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.5);
            }
            if
            (
                npc.type == NPCID.MourningWood
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
            }
            if
            (
                npc.type == NPCID.Pumpking ||
                npc.type == NPCID.PumpkingBlade
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
            }
            if
            (
                npc.type == NPCID.Everscream
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
            }
            if
            (
                npc.type == NPCID.SantaNK1
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
            }
            if
            (
                npc.type == NPCID.IceQueen
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
            }
            if
            (
                npc.type == NPCID.MartianSaucer ||
                npc.type == NPCID.MartianSaucerCannon ||
                npc.type == NPCID.MartianSaucerCore ||
                npc.type == NPCID.MartianSaucerTurret
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.5);
            }
            if(WDALTModSystem.isThoriumModPresent && WDALTModSystem.WDALTModContentIDIntegrity)
            {
                //Buff Thorium Bosses Accordingly
                if(npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_GTB)
                {
                    npc.lifeMax *= 2;
                }
                if(npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_QJ)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
                }
                if(npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_VC)
                {
                    npc.lifeMax *= 2;
                }
                if(npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_VC)
                {
                    npc.lifeMax *= 2;
                }
                if(npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_GES)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
                }
                if(npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_BC)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
                }
                if(npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_SCS)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
                    npc.damage = (int)Math.Round(npc.damage * 1.5);
                }
                if(npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_BS_V1 || npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_BS_V2)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
                }
                if(npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_FB_V1 || npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_FB_V2)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
                }
                if(npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_LI_V1 || npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_LI_V2)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
                }
                if
                (
                    npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_FO_V1 ||
                    npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_FO_V2 ||
                    npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_FO_V3
                )
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
                }
                if
                (
                    npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_AET ||
                    npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_OLD ||
                    npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_SFF ||
                    npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_DE
                )
                {
                    npc.lifeMax *= 5;
                }
            }
            base.SetDefaults(npc);
        }

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if(npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                npc.GetGlobalNPC<WDALTNPCUtil>().VileSpitTimeLeft = 300;
                npc.netUpdate = true;
            }
            base.OnSpawn(npc, source);
        }

        public override bool? CanCollideWithPlayerMeleeAttack(NPC npc, Player player, Item item, Rectangle meleeAttackHitbox)
        {
            if(npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                return false;
            }
            return base.CanCollideWithPlayerMeleeAttack(npc, player, item, meleeAttackHitbox);
        }

        public override bool PreAI(NPC npc)
        {
            if(npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                if(npc.GetGlobalNPC<WDALTNPCUtil>().VileSpitTimeLeft <= 0)
                {
                    npc.active = false;
                }
                if(Collision.SolidCollision(npc.position, npc.width+(int)npc.velocity.Length()+1, npc.height+(int)npc.velocity.Length()+1) && npc.velocity.Length() > 0f)
                {
                    npc.position += npc.velocity;
                    return false;
                }
            }
            return base.PreAI(npc);
        }

        public override void AI(NPC npc)
        {
            if
            (
                npc.type == NPCID.EaterofWorldsHead ||
                npc.type == NPCID.EaterofWorldsBody ||
                npc.type == NPCID.EaterofWorldsTail
            )
            {
                //Replicate vanilla behavior as good as possible.
                if(random.NextBool(300) && Main.expertMode && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.TargetClosest();
                    if(!Collision.CanHitLine(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                    {
                        NPC.NewNPC(new EntitySource_Parent(npc), (int)(npc.position.X + (float)(npc.width / 2) + npc.velocity.X), (int)(npc.position.Y + (float)(npc.height / 2) + npc.velocity.Y), NPCID.VileSpitEaterOfWorlds, 0, 0f, 1f);
                    }
                }
            }
            npc.GetGlobalNPC<WDALTNPCUtil>().VileSpitTimeLeft--;
            if(npc.type == NPCID.TheDestroyerBody)
            {
                //Replicate vanilla behavior as good as possible.
                if(random.NextBool(900) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.TargetClosest();
                    if(!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    {
                        Vector2 posWithOffset = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)(npc.height / 2));
                        float randomMultiplierX = Main.player[npc.target].position.X + ((float)Main.player[npc.target].width * 0.5f) + (float)random.Next(-16, 17) - posWithOffset.X;
                        float randomMultiplierY = Main.player[npc.target].position.Y + ((float)Main.player[npc.target].height * 0.5f) + (float)random.Next(-16, 17) - posWithOffset.Y;
                        float randomMultiplierLengh = 8f / (new Vector2(randomMultiplierX, randomMultiplierY).Length());
                        randomMultiplierX = (randomMultiplierX*randomMultiplierLengh) + ((float)random.Next(-16, 17) * 0.04f);
                        randomMultiplierY = (randomMultiplierY*randomMultiplierLengh) + ((float)random.Next(-16, 17) * 0.04f);
                        posWithOffset.X += randomMultiplierX * 4f;
                        posWithOffset.Y += randomMultiplierY * 4f;
                        int damage = npc.GetAttackDamage_ForProjectiles(22f, 18f);
                        int i = Projectile.NewProjectile(npc.GetSource_FromThis(), posWithOffset.X, posWithOffset.Y, randomMultiplierX, randomMultiplierY, ProjectileID.DeathLaser, damage, 0f, Main.myPlayer);
                        Main.projectile[i].timeLeft = 300;
                        npc.netUpdate = true;
                    }
                }
            }
            if(npc.type == NPCID.Plantera)
            {
                long timeSinceLastShot = (npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive - npc.GetGlobalNPC<WDALTNPCUtil>().lastActionTick);
                int shotDelay = 120;
                float dmg1 = 32f;
                float dmg2 = 24f;
                if(npc.life < (npc.lifeMax / 4))
                {
                    shotDelay = 90;
                }
                if (!Main.player[npc.target].ZoneJungle || (double)Main.player[npc.target].position.Y < Main.worldSurface * 16.0 || Main.player[npc.target].position.Y > (double)(Main.UnderworldLayer * 16.0))
                {
                    dmg1 *= 2;
                    dmg2 *= 2;
                    shotDelay = 30;
                }
                if(timeSinceLastShot >= shotDelay && Main.expertMode && Main.netMode != NetmodeID.MultiplayerClient && (npc.life < (npc.lifeMax / 2)))
                {
                    npc.GetGlobalNPC<WDALTNPCUtil>().lastActionTick = npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive;
                    npc.TargetClosest();
                    if(Main.player[npc.target].active && !Main.player[npc.target].dead)
                    {
                        int damage = npc.GetAttackDamage_ForProjectiles(dmg1, dmg2);
                        int amount = random.Next(4, 7);
                        float sprayIntensity = 16.0f; //Max Spraying in Tiles
                        for (int j = 0; j < amount; j++)
                        {
                            float randomModifierX = (random.NextFloat() - 0.5f);
                            float randomModifierY = (random.NextFloat() - 0.5f);
                            randomModifierX *= (sprayIntensity * 16.0f);
                            randomModifierY *= (sprayIntensity * 16.0f);
                            Vector2 vectorToTarget = new Vector2((Main.player[npc.target].Center.X + randomModifierX) - npc.Center.X, (Main.player[npc.target].Center.Y + randomModifierY) - npc.Center.Y);
                            vectorToTarget.Normalize();
                            int i = Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, vectorToTarget.X, vectorToTarget.Y, ProjectileID.PoisonSeedPlantera, damage, 0f, Main.myPlayer);
                            Main.projectile[i].timeLeft = 300;
                            Main.projectile[i].extraUpdates = 1;
                            Main.projectile[i].GetGlobalProjectile<WDALTProjectileUtil>().speedyPlanteraPoisonSeed = true;
                        }
                    }
                }
            }
            base.AI(npc);
        }

        public static void SetProjectileDefaults(NPC npc, Projectile projectile)
        {
            if(npc.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                projectile.damage = (int)Math.Round(projectile.damage * (1.0f - SearingInferno.damageNerfMultiplier));
                projectile.netUpdate = true;
            }
            if(WDALTModSystem.isCalamityModPresent)
            {
                return;
            }
            if(NerfGroup25Percent.Contains(npc.type))
            {
                projectile.damage = (int)Math.Round(projectile.damage * 0.75);
                projectile.netUpdate = true;
            }
            if(NerfGroup35Percent.Contains(npc.type))
            {
                projectile.damage = (int)Math.Round(projectile.damage * 0.65);
                projectile.netUpdate = true;
            }
            if(NerfGroup50Percent.Contains(npc.type))
            {
                projectile.damage = (int)Math.Round(projectile.damage * 0.5);
                projectile.netUpdate = true;
            }
            if(WDALTModSystem.isThoriumModPresent && WDALTModSystem.WDALTModContentIDIntegrity)
            {
                //Buff Thorium Bosses Accordingly
                if(npc.type == WDALTModSystem.WDALTModContentIDInstance.ThoriumBoss_SCS)
                {
                    projectile.damage = (int)Math.Round(projectile.damage * 1.5);
                }
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            npc.buffImmune[ModContent.BuffType<SearingInferno>()] = false;
            base.UpdateLifeRegen(npc, ref damage);
        }

        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if(npc.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                drawColor.R = 255;
                drawColor.G = 191;
                drawColor.B = 0;
                int xOffset = random.Next(-(npc.width/2), (npc.width/2));
                int yOffset = random.Next(-(npc.height/2), (npc.height/2));
                Vector2 dustPosition = npc.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                int dustType = random.Next(0, 2);
                switch(dustType)
                {
                    case 0:
                        Dust newDust1 = Dust.NewDustPerfect(dustPosition, DustID.SolarFlare);
                        newDust1.noGravity = true;
                        break;
                    case 1:
                        Dust newDust2 = Dust.NewDustPerfect(dustPosition, DustID.Ash);
                        newDust2.noGravity = true;
                        break;
                    default:
                        break;
                }
                
            }
            base.DrawEffects(npc, ref drawColor);
        }

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            ApplyDebuffsToPlayerBasedOnNPC(npc.type, target);
            if
            (
                target.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ModContent.ItemType<SearingHelmet>()) &&
                target.GetModPlayer<WDALTPlayerUtil>().HasPlayerChestplateEquipped(ModContent.ItemType<SearingBreastplate>()) &&
                target.GetModPlayer<WDALTPlayerUtil>().HasPlayerLeggingsEquipped(ModContent.ItemType<SearingLeggings>())
            )
            {
                npc.AddBuff(ModContent.BuffType<SearingInferno>(), 600, false);
            }
            if (npc.type == NPCID.Deerclops)
            {
                target.ClearBuff(BuffID.Frozen);
                target.ClearBuff(BuffID.Slow);
                target.buffImmune[BuffID.Frozen] = true;
                target.buffImmune[BuffID.Slow] = true;
            }
            if (npc.type == NPCID.PrimeVice)
            {
                Item itemToDrop = target.HeldItem;
                target.DropItem(target.GetSource_FromThis(), target.position, ref itemToDrop);
                SoundEngine.PlaySound(SoundID.Item71, target.position);
            }
            if (npc.type == NPCID.PrimeSaw)
            {
                SoundEngine.PlaySound(SoundID.Item22, target.position);
            }
        }

        public static void ApplyDebuffsToPlayerBasedOnNPC(int npcType, Player target)
        {
            if(InflictVenomDebuff1In1Group.Contains(npcType))
            {
                if(random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Venom, 240, true); //4s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictPoisonDebuff1In1Group.Contains(npcType))
            {
                if(random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Poisoned, 240, true); //4s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictBleedingDebuff1In1Group.Contains(npcType))
            {
                if(random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Bleeding, 960, true); //16s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictBleedingDebuff1In8Group.Contains(npcType))
            {
                if(random.Next(0, 8) == 0) //1 in 8 Chance
                {
                    target.AddBuff(BuffID.Bleeding, 480, true); //8s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictSearingInferno1In1Group.Contains(npcType))
            {
                if(random.Next(0, 1) == 0)
                {
                    target.AddBuff(ModContent.BuffType<SearingInferno>(), 240, true); //4s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictBrokenArmor1In1Group.Contains(npcType))
            {
                if(random.Next(0, 1) == 0)
                {
                    target.AddBuff(BuffID.BrokenArmor, 960, true); //16s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictSlowness1In1Group.Contains(npcType))
            {
                if(random.Next(0, 1) == 0)
                {
                    target.AddBuff(BuffID.Slow, 960, true); //16s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictCursed1In1Group.Contains(npcType))
            {
                if(random.Next(0, 1) == 0)
                {
                    target.AddBuff(BuffID.Cursed, 240, true); //4s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictWreckedResistance1In1Group.Contains(npcType))
            {
                if(random.Next(0, 1) == 0)
                {
                    target.AddBuff(ModContent.BuffType<WreckedResistance>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictDevastated1In1Group.Contains(npcType))
            {
                if(random.Next(0, 1) == 0)
                {
                    target.AddBuff(ModContent.BuffType<Devastated>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
            }
        }

        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
            foreach(IItemDropRule rule in globalLoot.Get())
            {
                if(rule is ItemDropWithConditionRule conditionRule)
                {
                    if(conditionRule.condition is Conditions.SoulOfLight)
                    {
                        conditionRule.chanceNumerator = 2;
                        conditionRule.chanceDenominator = 5;
                    }
                    if(conditionRule.condition is Conditions.SoulOfNight)
                    {
                        conditionRule.chanceNumerator = 2;
                        conditionRule.chanceDenominator = 5;
                    }
                }
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.ChaosElemental)
            {
                int chanceNumerator = 1; // 1% chance
                int chanceDenominator = 100;
                TryModifyRodOfDiscordDropChance(npcLoot, chanceNumerator, chanceDenominator);
            }
            if
            (
                npc.type == NPCID.Bee ||
                npc.type == NPCID.BeeSmall
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 1; // 1 in
                int chanceDenominator = 3; // 3 chance
                int itemID = ModContent.ItemType<Consumablebee>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }

            //Icy fossil drops

            if
            (
                npc.type == NPCID.PigronCorruption ||
                npc.type == NPCID.PigronCrimson ||
                npc.type == NPCID.PigronHallow
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 20; // 20% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<IcyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.ArmoredViking ||
                npc.type == NPCID.IceTortoise ||
                npc.type == NPCID.IceElemental ||
                npc.type == NPCID.IcyMerman
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 10; // 10% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<IcyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.IceMimic ||
                npc.type == NPCID.IceGolem
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 40; // 40% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<IcyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }

            //Dusty fossil drops

            if
            (
                npc.type == NPCID.DesertGhoul ||
                npc.type == NPCID.DesertGhoulCorruption ||
                npc.type == NPCID.DesertGhoulCrimson ||
                npc.type == NPCID.DesertGhoulHallow
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 20; // 20% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<DustyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.DesertBeast || //Basilisk
                npc.type == NPCID.DesertScorpionWalk || //Sand Poacher
                npc.type == NPCID.DesertScorpionWall || //Sand Poacher
                npc.type == NPCID.DesertLamiaDark ||
                npc.type == NPCID.DesertLamiaLight ||
                npc.type == NPCID.DuneSplicerHead
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 10; // 10% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<DustyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.DesertDjinn || //Desert Spirit
                npc.type == NPCID.SandElemental
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 40; // 40% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<DustyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }

            //Hellish fossil drops

            if
            (
                npc.type == NPCID.RedDevil
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 35; // 35% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<HellishFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.Lavabat
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 25; // 25% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<HellishFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.DemonTaxCollector
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 45; // 45% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<HellishFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }

        //Modify Rod of Discord drop chance. Are you kidding me, Re-Logic???!!!
        
        public static void TryModifyRodOfDiscordDropChance(NPCLoot npcLoot, int newChanceNumerator, int newChanceDenominator)
        {
            try
            {
                foreach (IItemDropRule rule in npcLoot.Get())
                {
                    if (rule is LeadingConditionRule leadingConditionRule)
                    {
                        try
                        {
                            foreach (IItemDropRuleChainAttempt chainedRuleAttempt in leadingConditionRule.ChainedRules)
                            {
                                try
                                {
                                    IItemDropRule chainedRule = chainedRuleAttempt.RuleToChain;
                                    if (chainedRule is CommonDrop drop)
                                    {
                                        if (drop.itemId == ItemID.RodofDiscord)
                                        {
                                            drop.chanceNumerator = newChanceNumerator;
                                            drop.chanceDenominator = newChanceDenominator;
                                        }
                                    }
                                    if (chainedRule is DropBasedOnExpertMode expertDropRule)
                                    {
                                        if (expertDropRule.ruleForNormalMode is CommonDrop normalDrop)
                                        {
                                            if (normalDrop.itemId == ItemID.RodofDiscord)
                                            {
                                                normalDrop.chanceNumerator = newChanceNumerator;
                                                normalDrop.chanceDenominator = newChanceDenominator;
                                            }
                                        }
                                        if (expertDropRule.ruleForExpertMode is CommonDrop expertDrop)
                                        {
                                            if (expertDrop.itemId == ItemID.RodofDiscord)
                                            {
                                                expertDrop.chanceNumerator = newChanceNumerator;
                                                expertDrop.chanceDenominator = newChanceDenominator;
                                            }
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }
    }
}
