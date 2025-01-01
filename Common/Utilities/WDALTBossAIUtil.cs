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
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using WeDoALittleTrolling.Content.NPCs;
using Terraria.Utilities;

namespace WeDoALittleTrolling.Common.Utilities
{
    internal static class WDALTBossAIUtil
    {
        //public const double rotationRoundingErrorGuard = 0.0325;
        public const double destroyerLiftoffLimit = 0.66;
        public const double destroyerAccelerationLimit = 0.33;

        /*
        public static void BossAI_CirclePlayer(NPC npc, Player target, float speed, bool clockwise = true)
        {
            npc.velocity = (target.Center - npc.Center).RotatedBy((clockwise ? (Math.PI * 1.5) + rotationRoundingErrorGuard : (Math.PI * 0.5) - rotationRoundingErrorGuard));
            npc.velocity = npc.velocity.SafeNormalize(Vector2.Zero);
            npc.velocity *= speed;
        }

        public static void BossAI_DashToPlayer(NPC npc, Player target, float speed)
        {
            npc.velocity = (target.Center - npc.Center);
            npc.velocity = npc.velocity.SafeNormalize(Vector2.Zero);
            npc.velocity *= speed;
        }
        */

        public static void BossAI_LunaticCultistExtras(NPC npc)
        {
            if(npc.ai[0] == 5f && npc.ai[1] == 30f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int avCount = 0;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].type == NPCID.AncientCultistSquidhead)
                    {
                        avCount++;
                    }
                }
                if (avCount < 20) //Cap max ancient visions at 20.
                {
                    NPC.NewNPCDirect(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, NPCID.AncientCultistSquidhead, 0, 0f, 0f, 0f, 0, npc.target);
                }
            }
        }

        public static void BossAI_EaterofWorldsExtras(NPC npc, ref UnifiedRandom random)
        {
            if (!Main.expertMode)
            {
                return;
            }
            //Replicate vanilla behavior as good as possible.
            if (random.NextBool(400) && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.TargetClosest();
                if (!Collision.CanHitLine(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                {
                    NPC.NewNPC(new EntitySource_Parent(npc), (int)(npc.position.X + (float)(npc.width / 2) + npc.velocity.X), (int)(npc.position.Y + (float)(npc.height / 2) + npc.velocity.Y), NPCID.VileSpitEaterOfWorlds, 0, 0f, 1f);
                }
            }
        }

        public static void BossAI_TheDestroyerExtras(NPC npc, ref UnifiedRandom random)
        {
            //Replicate vanilla behavior as good as possible.
            if (random.NextBool(1200) && Main.netMode != NetmodeID.MultiplayerClient && npc.life >= (int)Math.Round(npc.lifeMax * destroyerAccelerationLimit))
            {
                npc.TargetClosest();
                Vector2 posWithOffset = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)(npc.height / 2));
                float randomMultiplierX = Main.player[npc.target].position.X + ((float)Main.player[npc.target].width * 0.5f) + (float)random.Next(-16, 17) - posWithOffset.X;
                float randomMultiplierY = Main.player[npc.target].position.Y + ((float)Main.player[npc.target].height * 0.5f) + (float)random.Next(-16, 17) - posWithOffset.Y;
                float randomMultiplierLengh = 8f / (new Vector2(randomMultiplierX, randomMultiplierY).Length());
                randomMultiplierX = (randomMultiplierX * randomMultiplierLengh) + ((float)random.Next(-16, 17) * 0.04f);
                randomMultiplierY = (randomMultiplierY * randomMultiplierLengh) + ((float)random.Next(-16, 17) * 0.04f);
                posWithOffset.X += randomMultiplierX * 4f;
                posWithOffset.Y += randomMultiplierY * 4f;
                int damage = npc.GetAttackDamage_ForProjectiles(22f, 18f);
                int i = Projectile.NewProjectile(npc.GetSource_FromThis(), posWithOffset.X, posWithOffset.Y, randomMultiplierX, randomMultiplierY, ProjectileID.DeathLaser, damage, 0f, Main.myPlayer);
                Main.projectile[i].timeLeft = 300;
                npc.netUpdate = true;
            }
        }

        public static void BossAI_SkeletronPrimeExtras(NPC npc)
        {
            if (!Main.expertMode)
            {
                return;
            }
            bool shootFlag = true;
            int numArms = 0;
            if (!(npc.ai[1] == 0f) || !(Main.netMode != NetmodeID.MultiplayerClient))
            {
                shootFlag = false;
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if
                (
                    Main.npc[i].active &&
                    (
                        Main.npc[i].type == NPCID.PrimeCannon ||
                        Main.npc[i].type == NPCID.PrimeLaser ||
                        Main.npc[i].type == NPCID.PrimeSaw ||
                        Main.npc[i].type == NPCID.PrimeVice
                    )
                )
                {
                    numArms++;
                }
                if (Main.npc[i].active && Main.npc[i].type == NPCID.PrimeLaser)
                {
                    shootFlag = false;
                }
            }
            npc.defense += (numArms * 25);
            if (shootFlag)
            {
                if (Math.Abs(npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive - npc.GetGlobalNPC<WDALTNPCUtil>().lastActionTick) >= 60)
                {
                    npc.TargetClosest();
                    if (npc.target < 0 || npc.target > Main.player.Length)
                    {
                        return;
                    }
                    Vector2 gfxShootOffset1 = new Vector2(-17f, -10f);
                    Vector2 gfxShootOffset2 = new Vector2(18f, -10f);
                    Vector2 pos1 = npc.Center + gfxShootOffset1;
                    Vector2 pos2 = npc.Center + gfxShootOffset2;
                    Vector2 predictVelocity = Main.player[npc.target].velocity * (Vector2.Distance(npc.Center, Main.player[npc.target].Center) / (12f * (float)3)); //Roughly Predict where the target is going to be when the Laser reaches it
                    Vector2 shootVector1 = ((Main.player[npc.target].Center + predictVelocity) - pos1);
                    Vector2 shootVector2 = ((Main.player[npc.target].Center + predictVelocity) - pos2);
                    shootVector1 = shootVector1.SafeNormalize(Vector2.Zero);
                    pos1 += shootVector1;
                    shootVector1 *= 12f;
                    shootVector2 = shootVector2.SafeNormalize(Vector2.Zero);
                    pos2 += shootVector2;
                    shootVector2 *= 12f;
                    Projectile.NewProjectileDirect
                    (
                        npc.GetSource_FromAI(),
                        pos1,
                        shootVector1,
                        ProjectileID.DeathLaser,
                        (int)Math.Round(npc.damage * (0.25 - (0.05 * numArms))),
                        3.5f
                    );
                    Projectile.NewProjectileDirect
                    (
                        npc.GetSource_FromAI(),
                        pos2,
                        shootVector2,
                        ProjectileID.DeathLaser,
                        (int)Math.Round(npc.damage * (0.25 - (0.05 * numArms))),
                        3.5f
                    );
                    npc.GetGlobalNPC<WDALTNPCUtil>().lastActionTick = npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive;
                }
            }
            else
            {
                npc.GetGlobalNPC<WDALTNPCUtil>().lastActionTick = npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive;
            }
        }

        public static void BossAI_PlanteraShotgun(NPC npc, ref UnifiedRandom random)
        {
            if (!Main.expertMode)
            {
                return;
            }
            if (Main.player[npc.target].teleportTime > 0f)
            {
                Main.player[npc.target].AddBuff(BuffID.Venom, 240, true); //4s, X2 in Expert, X2.5 in Master
            }
            long timeSinceLastShot = (npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive - npc.GetGlobalNPC<WDALTNPCUtil>().lastActionTick);
            int shotDelay = 120;
            int damage = 32;
            if (npc.life < (npc.lifeMax / 4))
            {
                shotDelay = 60;
            }
            if (Main.player[npc.target].teleportTime > 0f || !Main.player[npc.target].ZoneJungle || (double)Main.player[npc.target].position.Y < Main.worldSurface * 16.0 || Main.player[npc.target].position.Y > (double)(Main.UnderworldLayer * 16.0))
            {
                shotDelay = 30;
            }
            if (timeSinceLastShot >= shotDelay && Main.netMode != NetmodeID.MultiplayerClient && (npc.life < (npc.lifeMax / 2)))
            {
                npc.GetGlobalNPC<WDALTNPCUtil>().lastActionTick = npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive;
                npc.TargetClosest();
                if (Main.player[npc.target].active && !Main.player[npc.target].dead)
                {
                    int amount = random.Next(4, 7);
                    float sprayIntensity = 16.0f; //Max Spraying in Tiles
                    for (int j = 0; j < amount; j++)
                    {
                        float randomModifierX = (random.NextFloat() - 0.5f);
                        float randomModifierY = (random.NextFloat() - 0.5f);
                        randomModifierX *= (sprayIntensity * 16.0f);
                        randomModifierY *= (sprayIntensity * 16.0f);
                        Vector2 vectorToTarget = new Vector2((Main.player[npc.target].Center.X + randomModifierX) - npc.Center.X, (Main.player[npc.target].Center.Y + randomModifierY) - npc.Center.Y);
                        vectorToTarget = vectorToTarget.SafeNormalize(Vector2.Zero);
                        Projectile proj = Projectile.NewProjectileDirect(npc.GetSource_FromThis(), npc.Center, vectorToTarget, ProjectileID.PoisonSeedPlantera, damage, 0f, Main.myPlayer);
                        proj.timeLeft = 300;
                        proj.extraUpdates = 1;
                        proj.GetGlobalProjectile<WDALTProjectileUtil>().speedyPlanteraPoisonSeed = true;
                    }
                }
            }
        }

        public static void BossAI_GolemExtras(NPC npc)
        {
            if (!Main.expertMode)
            {
                return;
            }
            if (npc.ai[0] == 1f && Math.Abs(npc.velocity.Y) == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.GetGlobalNPC<WDALTNPCUtil>().golemBoulderIteration = 1;
                npc.GetGlobalNPC<WDALTNPCUtil>().golemBoulderStartPosition = npc.Center;
            }
            if
            (
                npc.GetGlobalNPC<WDALTNPCUtil>().golemBoulderIteration > 0 &&
                (npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive - npc.GetGlobalNPC<WDALTNPCUtil>().lastActionTick) >= 5 &&
                Main.netMode != NetmodeID.MultiplayerClient
            )
            {
                int dmg = 75;
                Vector2 shootVector = (new Vector2(0f, -1f)) * 10f;
                Vector2 shootPos1 = npc.GetGlobalNPC<WDALTNPCUtil>().golemBoulderStartPosition;
                Vector2 shootPos2 = npc.GetGlobalNPC<WDALTNPCUtil>().golemBoulderStartPosition;
                float xOffset = (npc.GetGlobalNPC<WDALTNPCUtil>().golemBoulderIteration * 50f) + 50f;
                float yOffset = npc.height * 0.5f;
                shootPos1.X += xOffset;
                shootPos2.X -= xOffset;
                shootPos1.Y += yOffset;
                shootPos2.Y += yOffset;
                Projectile proj1 = Projectile.NewProjectileDirect
                (
                    npc.GetSource_FromAI(),
                    shootPos1,
                    shootVector,
                    ProjectileID.BoulderStaffOfEarth,
                    dmg,
                    5f
                );
                proj1.hostile = true;
                proj1.friendly = false;
                proj1.timeLeft = 480;
                proj1.GetGlobalProjectile<WDALTProjectileUtil>().hostileGolemBoulder = true;
                proj1.netUpdate = true;
                SoundEngine.PlaySound(SoundID.Item69, proj1.position);
                Projectile proj2 = Projectile.NewProjectileDirect
                (
                    npc.GetSource_FromAI(),
                    shootPos2,
                    shootVector,
                    ProjectileID.BoulderStaffOfEarth,
                    dmg,
                    5f
                );
                proj2.hostile = true;
                proj2.friendly = false;
                proj2.timeLeft = 480;
                proj2.GetGlobalProjectile<WDALTProjectileUtil>().hostileGolemBoulder = true;
                proj2.netUpdate = true;
                SoundEngine.PlaySound(SoundID.Item69, proj2.position);
                if (npc.GetGlobalNPC<WDALTNPCUtil>().golemBoulderIteration < 20)
                {
                    npc.GetGlobalNPC<WDALTNPCUtil>().golemBoulderIteration++;
                }
                else
                {
                    npc.GetGlobalNPC<WDALTNPCUtil>().golemBoulderIteration = 0;
                }
                npc.GetGlobalNPC<WDALTNPCUtil>().lastActionTick = npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive;
                /*
                float rotation = MathHelper.ToRadians((180f / (24f - 1f)));
                for (int i = 0; i < 24; i++)
                {
                    Projectile proj = Projectile.NewProjectileDirect
                    (
                        npc.GetSource_FromAI(),
                        npc.Center,
                        shootVector.RotatedBy(- (rotation * (float)i)),
                        ProjectileID.BoulderStaffOfEarth,
                        (int)Math.Round(npc.damage * 0.175f),
                        4f
                    );
                    proj.hostile = true;
                    proj.friendly = false;
                    proj.GetGlobalProjectile<WDALTProjectileUtil>().hostileGolemBoulder = true;
                    proj.netUpdate = true;
                    SoundEngine.PlaySound(SoundID.Item69, proj.position);
                }
                */
            }
        }
    }
}
