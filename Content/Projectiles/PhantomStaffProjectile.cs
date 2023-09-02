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

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Utilities;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Buffs;
using Terraria.Audio;
using WeDoALittleTrolling.Common.Utilities;
using Terraria.DataStructures;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class PhantomStaffProjectile : ModProjectile
    {
        public const float idleOverlapCorrectionFactor = 0.04f;
        public const float attackOverlapCorrectionFactor = 0.02f;
        public const float detectionRange = 1024f;
        public const float detectionRangeOffset = 50f;
        public const float idleMoveSpeed = 4f;
        public const float attackMoveSpeed = 16f;
        public const float idleInertia = 8f;
        public const float attackInertia = 2f;
        public const float idleDistance = 64f;
        public const float idleAccelerationFactor = 0.75f;
        public const float bulletSpeed = 32f;
        public static readonly int projWidth = 50;
        public static readonly int projHeight = 70;
        public static readonly float bulletOffsetMultiplier = 36f;
        public static readonly Vector2 gfxShootOffset1 = new Vector2(-8f, -13f);
        public static readonly Vector2 gfxShootOffset2 = new Vector2(9f, -13f);
        public long lastActionTick;
        public static UnifiedRandom random = new UnifiedRandom();
        
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = projWidth;
            Projectile.height = projHeight;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.minionSlots = 1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.ArmorPenetration = 60;
            Projectile.timeLeft = 10;
            Projectile.netImportant = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.light = 0.8f;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            AI_003_Luminite_Phantom();
        }

        public override void PostDraw(Color lightColor)
        {
            int rMax = (int)Projectile.width;
            double r = rMax * Math.Sqrt(random.NextDouble());
            double angle = random.NextDouble() * 2 * Math.PI;
            int xOffset = (int)Math.Round(r * Math.Cos(angle));
            int yOffset = (int)Math.Round(r * Math.Sin(angle));
            Vector2 dustPosition = Projectile.Center;
            dustPosition.X += xOffset;
            dustPosition.Y += yOffset;
            int dustType = random.Next(0, 16);
            switch (dustType)
            {
                case 0:
                    Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.LunarOre, null, 0, default);
                    newDust.noGravity = true;
                    break;
                default:
                    break;
            }
        }

        private void AI_003_Luminite_Phantom()
        {
            Player ownerPlayer = Main.player[Projectile.owner];
            if(!ownerPlayer.active || ownerPlayer.dead)
            {
                ownerPlayer.ClearBuff(ModContent.BuffType<PhantomStaffBuff>());
                return;
            }
            if(ownerPlayer.HasBuff(ModContent.BuffType<PhantomStaffBuff>()))
            {
                Projectile.timeLeft = 10;
            }
            Vector2 idlePos = ownerPlayer.Center;
            idlePos.Y -= (Projectile.height * 3f);
            Vector2 vectorToIdlePos = idlePos - Projectile.Center;
            float distanceToIdlePos = vectorToIdlePos.Length();
            if (Projectile.owner == Main.myPlayer && distanceToIdlePos > (detectionRange * 2f))
            {
                Projectile.position = idlePos;
                Projectile.velocity *= 0.16f;
                Projectile.netUpdate = true;
            }
            float distanceToTarget = detectionRange;
            bool targetDetected = false;
            Vector2 targetCenter = Projectile.Center;
            Vector2 targetVelocity = Vector2.Zero;
            if(ownerPlayer.HasMinionAttackTargetNPC)
            {
                NPC target = Main.npc[ownerPlayer.MinionAttackTargetNPC];
                if(Vector2.Distance(target.Center, Projectile.Center) < (detectionRange * 2f))
                {
                    distanceToTarget = Vector2.Distance(target.Center, Projectile.Center);
                    targetCenter = target.Center;
                    targetVelocity = target.velocity;
                    targetDetected = true;
                }
            }
            if(!targetDetected)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC target = Main.npc[i];
                    if(target.CanBeChasedBy())
                    {
                        float currentDistance = Vector2.Distance(target.Center, Projectile.Center);
                        if(currentDistance < distanceToTarget)
                        {
                            distanceToTarget = currentDistance;
                            targetCenter = target.Center;
                            targetVelocity = target.velocity;
                            targetDetected = true;
                        }
                    }
                }
            }
            float cFactor = attackOverlapCorrectionFactor;
            if(!targetDetected)
            {
                cFactor = idleOverlapCorrectionFactor;
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile otherMinion = Main.projectile[i];
                if
                (
                    (i != Projectile.whoAmI) &&
                    (otherMinion.active) &&
                    (otherMinion.owner == Projectile.owner) &&
                    ((Math.Abs(Projectile.position.X - otherMinion.position.X) + Math.Abs(Projectile.position.Y - otherMinion.position.Y)) < (Projectile.width))
                )
                {
                    if (Projectile.position.X < otherMinion.position.X)
                    {
                        Projectile.velocity.X -= cFactor;
                    }
                    else
                    {
                        Projectile.velocity.X += cFactor;
                    }
                    if (Projectile.position.Y < otherMinion.position.Y)
                    {
                        Projectile.velocity.Y -= cFactor;
                    }
                    else
                    {
                        Projectile.velocity.Y += cFactor;
                    }
                }
            }
            if(targetDetected)
            {
                if(distanceToTarget > detectionRangeOffset)
                {
                    Vector2 moveVector = (targetCenter - Projectile.Center);
                    moveVector.Normalize();
                    moveVector *= attackMoveSpeed;
                    Projectile.velocity += (moveVector/attackInertia);
                    if(Projectile.velocity.Length() > attackMoveSpeed)
                    {
                        Projectile.velocity.Normalize();
                        Projectile.velocity *= attackMoveSpeed;
                    }
                }
                if
                (
                    (Math.Abs(Projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive - lastActionTick) >= Projectile.localNPCHitCooldown) &&
                    (distanceToTarget > detectionRangeOffset * 2)
                )
                {
                    if(Projectile.owner == Main.myPlayer)
                    {
                        Vector2 pos1 = Projectile.Center + gfxShootOffset1;
                        Vector2 pos2 = Projectile.Center + gfxShootOffset2;
                        Vector2 predictVelocity = targetVelocity * ((distanceToTarget - bulletOffsetMultiplier) / bulletSpeed); //Roughly Predict where the target is going to be when the Laser reaches it
                        Vector2 shootVector1 = ((targetCenter + predictVelocity) - pos1);
                        Vector2 shootVector2 = ((targetCenter + predictVelocity) - pos2);
                        int dmg = (int)Math.Round(Projectile.damage * 0.5); //We shoot 2 projectiles so only 0.5x damage per projectile.
                        shootVector1.Normalize();
                        pos1 += (shootVector1 * bulletOffsetMultiplier);
                        shootVector1 *= bulletSpeed;
                        shootVector2.Normalize();
                        pos2 += (shootVector2 * bulletOffsetMultiplier);
                        shootVector2 *= bulletSpeed;
                        Projectile.NewProjectileDirect
                        (
                            Projectile.GetSource_FromAI(),
                            pos1,
                            shootVector1,
                            ModContent.ProjectileType<PhantomStaffProjectileBullet>(),
                            dmg,
                            Projectile.knockBack,
                            Projectile.owner
                        );
                        Projectile.NewProjectileDirect
                        (
                            Projectile.GetSource_FromAI(),
                            pos2,
                            shootVector2,
                            ModContent.ProjectileType<PhantomStaffProjectileBullet>(),
                            dmg,
                            Projectile.knockBack,
                            Projectile.owner
                        );
                    }
                    SoundEngine.PlaySound(SoundID.NPCHit44, Projectile.Center);
                    lastActionTick = Projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive;
                }
            }
            else
            {
                if(distanceToIdlePos > idleDistance)
                {
                    vectorToIdlePos.Normalize();
                    float speedFactor = ((distanceToIdlePos * idleAccelerationFactor) / idleDistance);
                    if(speedFactor < 1f) //Make sure speed will be at least idleMoveSpeed
                    {
                        speedFactor = 1f;
                    }
                    vectorToIdlePos *= (idleMoveSpeed * speedFactor);
                    Projectile.velocity += (vectorToIdlePos/idleInertia);
                    if(Projectile.velocity.Length() > (idleMoveSpeed * speedFactor)) //Smoothly accelerate/decelerate based on distance to idle position
                    {
                        Projectile.velocity.Normalize();
                        Projectile.velocity *= (idleMoveSpeed * speedFactor);
                    }
                    if(Projectile.velocity.Length() > attackMoveSpeed) //Cap movement speed at attack movement speed
                    {
                        Projectile.velocity.Normalize();
                        Projectile.velocity *= attackMoveSpeed;
                    }
                }
            }
            if(Projectile.velocity.Length() == 0f)
            {
                Projectile.velocity.X = (random.NextFloat() - 0.5f);
                Projectile.velocity.Y = (random.NextFloat() - 0.5f);
                Projectile.velocity.Normalize();
                Projectile.velocity *= idleMoveSpeed;
            }
        }
    }
}
