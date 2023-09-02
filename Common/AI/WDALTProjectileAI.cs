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
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Utilities;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Projectiles;

namespace WeDoALittleTrolling.Common.AI
{
    internal static class WDALTProjectileAI
    {
        public static UnifiedRandom random = new UnifiedRandom();
        public static void AI_001_SimpleBullet(Projectile projectile)
        {
            projectile.spriteDirection = projectile.direction; //Fix wrong shading when shooting to the left.
            float roatateOffset = (float)Math.PI / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + roatateOffset;
        }

        public static void AI_002_ApollonAimAssist(Projectile projectile, ref Vector2 original_location)
        {
            Vector2 spawnCenter = projectile.Center;
            float lowest_distance = 9999;
            float maxBeamTravelX = Main.screenWidth/2;
            float maxBeamTravelY = Main.screenHeight/2;
            float origVelocityLength = projectile.velocity.Length();
            if(maxBeamTravelX > 1920/2)
            {
                maxBeamTravelX = 1920/2;
            }
            if(maxBeamTravelY > 1080/2)
            {
                maxBeamTravelY = 1080/2;
            }
            for(int i = 0; i < Main.npc.Length; i++)
            {
                NPC target = Main.npc[i];
                float shootToX                  = target.position.X + (float)target.width * 0.5f - spawnCenter.X;
                float shootToY                  = target.position.Y - spawnCenter.Y;
                Vector2 shootTo                 = new Vector2(shootToX, shootToY);
                float distance                  = shootTo.Length();
                Vector2 originalVector          = projectile.velocity;
                originalVector.Normalize();
                float x                         = originalVector.X;
                float y                         = originalVector.Y;
                float a                         = Math.Abs(original_location.X - target.position.X);
                float b                         = Math.Abs(original_location.Y - target.position.Y);
                float a_through_x               = Math.Abs(a/x);
                float b_through_y               = Math.Abs(b/y);
                float x_y_inaccuracy            = x*a_through_x;
                float y_y_inaccuracy            = y*a_through_x;
                float x_x_inaccuracy            = x*b_through_y;
                float y_x_inaccuracy            = y*b_through_y;
                Vector2 KonePositionY           = original_location + new Vector2(x_y_inaccuracy, y_y_inaccuracy);
                Vector2 KonePositionX           = original_location + new Vector2(x_x_inaccuracy, y_x_inaccuracy);
                Vector2 LineToTargetY           = new Vector2(target.position.X - KonePositionY.X, target.position.Y - KonePositionY.Y);
                Vector2 LineToTargetX           = new Vector2(target.position.X - KonePositionX.X, target.position.Y - KonePositionX.Y);
                float y_inaccuracy              = (float)System.Math.Sqrt((double)(LineToTargetY.X * LineToTargetY.X + LineToTargetY.Y * LineToTargetY.Y));
                float x_inaccuracy              = (float)System.Math.Sqrt((double)(LineToTargetX.X * LineToTargetX.X + LineToTargetX.Y * LineToTargetX.Y));
                float inaccuracy_tolerance      = 160;
                if
                (
                    (
                        Math.Abs(original_location.X - target.position.X) < maxBeamTravelX && //Configure detect zone in x coords
                        Math.Abs(original_location.Y - target.position.Y) < maxBeamTravelY     //Configure detect zone in y coords
                    ) &&
                    (
                        Math.Abs(original_location.X - target.position.X) > inaccuracy_tolerance ||   //Configure non-detect zone in x coords
                        Math.Abs(original_location.Y - target.position.Y) > inaccuracy_tolerance      //Configure non-detect zone in y coords
                    )&&
                    !target.friendly &&
                    !target.CountsAsACritter &&
                    !target.isLikeATownNPC &&
                    !target.dontTakeDamage &&
                    target.active &&
                    target.CanBeChasedBy() &&
                    distance < lowest_distance &&
                    (
                        y_inaccuracy < inaccuracy_tolerance ||
                        x_inaccuracy < inaccuracy_tolerance
                    )
                )
                {
                    Vector2 newVelocity = shootTo;
                    newVelocity.Normalize();
                    newVelocity *= origVelocityLength;
                    projectile.velocity = newVelocity;
                    lowest_distance = distance;
                }
            }
        }

        private const float CONST_003_IdleOverlapCorrectionFactor = 0.04f;
        private const float CONST_003_AttackOverlapCorrectionFactor = 0.02f;
        private const float CONST_003_DetectionRange = 1024f;
        private const float CONST_003_DetectionRangeOffset = 50f;
        private const float CONST_003_IdleMoveSpeed = 4f;
        private const float CONST_003_AttackMoveSpeed = 16f;
        private const float CONST_003_IdleInertia = 8f;
        private const float CONST_003_AttackInertia = 2f;
        private const float CONST_003_IdleDistance = 64f;
        private const float CONST_003_IdleAccelerationFactor = 0.75f;
        private const float CONST_003_BulletSpeed = 32f;
        private const float CONST_003_BulletOffsetMultiplier = 36f;
        private static readonly Vector2 CONST_003_GfxShootOffset1 = new Vector2(-8f, -13f);
        private static readonly Vector2 CONST_003_GfxShootOffset2 = new Vector2(9f, -13f);
        public static void AI_003_Luminite_Phantom(Projectile projectile, ref long lastActionTick)
        {
            Player ownerPlayer = Main.player[projectile.owner];
            if(!ownerPlayer.active || ownerPlayer.dead)
            {
                ownerPlayer.ClearBuff(ModContent.BuffType<PhantomStaffBuff>());
                return;
            }
            if(ownerPlayer.HasBuff(ModContent.BuffType<PhantomStaffBuff>()))
            {
                projectile.timeLeft = 10;
            }
            Vector2 idlePos = ownerPlayer.Center;
            idlePos.Y -= (projectile.height * 3f);
            Vector2 vectorToIdlePos = idlePos - projectile.Center;
            float distanceToIdlePos = vectorToIdlePos.Length();
            if (projectile.owner == Main.myPlayer && distanceToIdlePos > (CONST_003_DetectionRange * 2f))
            {
                projectile.position = idlePos;
                projectile.velocity *= 0.16f;
                projectile.netUpdate = true;
            }
            float distanceToTarget = CONST_003_DetectionRange;
            bool targetDetected = false;
            Vector2 targetCenter = projectile.Center;
            Vector2 targetVelocity = Vector2.Zero;
            if(ownerPlayer.HasMinionAttackTargetNPC)
            {
                NPC target = Main.npc[ownerPlayer.MinionAttackTargetNPC];
                if(Vector2.Distance(target.Center, projectile.Center) < (CONST_003_DetectionRange * 2f))
                {
                    distanceToTarget = Vector2.Distance(target.Center, projectile.Center);
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
                        float currentDistance = Vector2.Distance(target.Center, projectile.Center);
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
            float cFactor = CONST_003_AttackOverlapCorrectionFactor;
            if(!targetDetected)
            {
                cFactor = CONST_003_IdleOverlapCorrectionFactor;
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile otherMinion = Main.projectile[i];
                if
                (
                    (i != projectile.whoAmI) &&
                    (otherMinion.active) &&
                    (otherMinion.owner == projectile.owner) &&
                    ((Math.Abs(projectile.position.X - otherMinion.position.X) + Math.Abs(projectile.position.Y - otherMinion.position.Y)) < (projectile.width))
                )
                {
                    if (projectile.position.X < otherMinion.position.X)
                    {
                        projectile.velocity.X -= cFactor;
                    }
                    else
                    {
                        projectile.velocity.X += cFactor;
                    }
                    if (projectile.position.Y < otherMinion.position.Y)
                    {
                        projectile.velocity.Y -= cFactor;
                    }
                    else
                    {
                        projectile.velocity.Y += cFactor;
                    }
                }
            }
            if(targetDetected)
            {
                if(distanceToTarget > CONST_003_DetectionRangeOffset)
                {
                    Vector2 moveVector = (targetCenter - projectile.Center);
                    moveVector.Normalize();
                    moveVector *= CONST_003_AttackMoveSpeed;
                    projectile.velocity += (moveVector/CONST_003_AttackInertia);
                    if(projectile.velocity.Length() > CONST_003_AttackMoveSpeed)
                    {
                        projectile.velocity.Normalize();
                        projectile.velocity *= CONST_003_AttackMoveSpeed;
                    }
                }
                if
                (
                    (Math.Abs(projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive - lastActionTick) >= projectile.localNPCHitCooldown) &&
                    (distanceToTarget > CONST_003_DetectionRangeOffset * 2)
                )
                {
                    if(projectile.owner == Main.myPlayer)
                    {
                        Vector2 pos1 = projectile.Center + CONST_003_GfxShootOffset1;
                        Vector2 pos2 = projectile.Center + CONST_003_GfxShootOffset2;
                        Vector2 predictVelocity = targetVelocity * ((distanceToTarget - CONST_003_BulletOffsetMultiplier) / CONST_003_BulletSpeed); //Roughly Predict where the target is going to be when the Laser reaches it
                        Vector2 shootVector1 = ((targetCenter + predictVelocity) - pos1);
                        Vector2 shootVector2 = ((targetCenter + predictVelocity) - pos2);
                        int dmg = (int)Math.Round(projectile.damage * 0.5); //We shoot 2 projectiles so only 0.5x damage per projectile.
                        shootVector1.Normalize();
                        pos1 += (shootVector1 * CONST_003_BulletOffsetMultiplier);
                        shootVector1 *= CONST_003_BulletSpeed;
                        shootVector2.Normalize();
                        pos2 += (shootVector2 * CONST_003_BulletOffsetMultiplier);
                        shootVector2 *= CONST_003_BulletSpeed;
                        Projectile.NewProjectileDirect
                        (
                            projectile.GetSource_FromAI(),
                            pos1,
                            shootVector1,
                            ModContent.ProjectileType<PhantomStaffProjectileBullet>(),
                            dmg,
                            projectile.knockBack,
                            projectile.owner
                        );
                        Projectile.NewProjectileDirect
                        (
                            projectile.GetSource_FromAI(),
                            pos2,
                            shootVector2,
                            ModContent.ProjectileType<PhantomStaffProjectileBullet>(),
                            dmg,
                            projectile.knockBack,
                            projectile.owner
                        );
                    }
                    SoundEngine.PlaySound(SoundID.NPCHit44, projectile.Center);
                    lastActionTick = projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive;
                }
            }
            else
            {
                if(distanceToIdlePos > CONST_003_IdleDistance)
                {
                    vectorToIdlePos.Normalize();
                    float speedFactor = ((distanceToIdlePos * CONST_003_IdleAccelerationFactor) / CONST_003_IdleDistance);
                    if(speedFactor < 1f) //Make sure speed will be at least idleMoveSpeed
                    {
                        speedFactor = 1f;
                    }
                    vectorToIdlePos *= (CONST_003_IdleMoveSpeed * speedFactor);
                    projectile.velocity += (vectorToIdlePos/CONST_003_IdleInertia);
                    if(projectile.velocity.Length() > (CONST_003_IdleMoveSpeed * speedFactor)) //Smoothly accelerate/decelerate based on distance to idle position
                    {
                        projectile.velocity.Normalize();
                        projectile.velocity *= (CONST_003_IdleMoveSpeed * speedFactor);
                    }
                    if(projectile.velocity.Length() > CONST_003_AttackMoveSpeed) //Cap movement speed at attack movement speed
                    {
                        projectile.velocity.Normalize();
                        projectile.velocity *= CONST_003_AttackMoveSpeed;
                    }
                }
            }
            if(projectile.velocity.Length() == 0f)
            {
                projectile.velocity.X = (random.NextFloat() - 0.5f);
                projectile.velocity.Y = (random.NextFloat() - 0.5f);
                projectile.velocity.Normalize();
                projectile.velocity *= CONST_003_IdleMoveSpeed;
            }
        }

        private static readonly float CONST_004_SpearLengh = (float)Math.Sqrt((200*200)+(200*200));
        private static readonly float CONST_004_HoldoutRangeMin = (CONST_004_SpearLengh/2 - (float)64.0) + (float)32.0;
        private static readonly float CONST_004_HoldoutRangeMax = (CONST_004_SpearLengh/2 + (float)64.0) + (float)32.0;
        public static void AI_004_PhotonSplicer(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            int duration = player.itemAnimationMax;

            player.heldProj = projectile.whoAmI;

            // Reset projectile time left if necessary
            if (projectile.timeLeft > duration) {
                projectile.timeLeft = duration;
            }
            // Stop projectile after having travelled half way back, so it seems less like a piston.
            if (projectile.timeLeft <= duration * 0.25)
            {
                projectile.velocity = new Vector2((float)0.0, (float)0.0);
            }

            projectile.velocity = Vector2.Normalize(projectile.velocity);

            float halfDuration = duration * (float)0.5;
            float progress;

            if (projectile.timeLeft < halfDuration)
            {
                progress = projectile.timeLeft / halfDuration;
            }
            else
            {
                progress = ((duration - projectile.timeLeft) / halfDuration);
            }

            projectile.Center = player.MountedCenter + Vector2.SmoothStep(projectile.velocity * CONST_004_HoldoutRangeMin, projectile.velocity * CONST_004_HoldoutRangeMax, progress);

            // Apply proper rotation to the sprite.
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation += MathHelper.ToRadians((float)45.0);
            }
            else
            {
                projectile.rotation += MathHelper.ToRadians((float)135.0);
            }
        }

        private const float CONST_005_HomingRange = 512f;
        private const float CONST_005_CorrectionFactor = 0.30f;
        public static void AI_005_MagicArtefact(Projectile projectile)
        {
            float origVelocityLength = projectile.velocity.Length();
            float lowestDistance = CONST_005_HomingRange;
            bool targetDetected = false;
            Vector2 targetCenter = Vector2.Zero;
            for(int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                float distance = Vector2.Distance(projectile.Center, npc.Center);
                if((distance < lowestDistance) && npc.CanBeChasedBy())
                {
                    targetCenter = npc.Center;
                    targetDetected = true;
                    lowestDistance = distance;
                }
            }
            if(targetDetected)
            {
                Vector2 moveVector = (targetCenter - projectile.Center);
                moveVector.Normalize();
                moveVector *= (origVelocityLength * CONST_005_CorrectionFactor);
                projectile.velocity += moveVector;
                projectile.velocity.Normalize();
                projectile.velocity *= origVelocityLength;
            }
            projectile.spriteDirection = projectile.direction; //Fix wrong shading when shooting to the left.
        }

        private const float CONST_006_GravityFactor = 0.05f;
        public static void AI_006_GloriousDemise(Projectile projectile)
        {
            projectile.velocity.Y += CONST_006_GravityFactor;
            projectile.spriteDirection = projectile.direction; //Fix wrong shading when shooting to the left.
        }
    }
}
