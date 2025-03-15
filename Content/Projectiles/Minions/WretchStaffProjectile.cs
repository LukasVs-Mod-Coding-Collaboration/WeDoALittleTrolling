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

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using WeDoALittleTrolling.Common.ModPlayers;
using System.IO;

namespace WeDoALittleTrolling.Content.Projectiles.Minions
{
    public class WretchStaffProjectile : ModProjectile
    {
        public const float idleOverlapCorrectionFactor = 0.04f;
        public const float attackOverlapCorrectionFactor = 0.02f;
        public const float detectionRange = 768f;
        public const float detectionRangeOffset = 36f;
        public const float idleMoveSpeed = 3f;
        public const float attackMoveSpeed = 12f;
        public const float idleInertia = 12f;
        public const float attackInertia = 6f;
        public const float idleDistance = 48f;
        public const float idleAccelerationFactor = 0.75f;
        public const float zeroVectorMaxLen = 0.0000152587890625f; // 2^-16 / Binary: 0.0000000000000001

        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 2;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.minion = true;
            Projectile.timeLeft *= 5;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.minionSlots = 1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 8;
            Projectile.netImportant = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.light = 0.8f;
            this.DrawOffsetX = -2;
            this.DrawOriginOffsetX = 0f;
            this.DrawOriginOffsetY = -2;
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
            AI_023_Wretch(out Player ownerPlayer, out bool runAI);
            if (!runAI)
            {
                return;
            }
            AI_023_Wretch_CoreTasks(ref ownerPlayer, out Vector2 idlePos, out Vector2 vectorToIdlePos, out float distanceToIdlePos);
            AI_023_Wretch_SearchTargets(ref ownerPlayer, out float distanceToTarget, out bool targetDetected, out Vector2 targetCenter, out Vector2 targetVelocity);
            AI_023_Wretch_CorrectOverlap(ref targetDetected);
            if (targetDetected)
            {
                AI_023_Wretch_AttackAI(ref distanceToTarget, ref targetCenter, ref targetVelocity);
                AI_023_Wretch_AttackCorrectFreeze(ref distanceToTarget);
            }
            else
            {
                AI_023_Wretch_IdleAI(ref distanceToIdlePos, ref vectorToIdlePos);
                AI_023_Wretch_IdleCorrectFreeze(ref distanceToIdlePos);
            }
            AI_023_Wretch_UpdateFrames();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            byte maxVal = (byte)Math.Max(Math.Max(lightColor.R, lightColor.G), lightColor.B);
            double mult = 255.0 / (double)maxVal;
            lightColor.R = (byte)Math.Floor((double)lightColor.R * mult);
            lightColor.G = (byte)Math.Floor((double)lightColor.G * mult);
            lightColor.B = (byte)Math.Floor((double)lightColor.B * mult);
            return lightColor;
        }

        public override void PostDraw(Color lightColor)
        {
            if (Main.rand.NextBool(15))
            {
                int rMax = (int)Projectile.width / 2;
                double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                double angle = Main.rand.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = Projectile.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Adamantite, null, 0, default);
                newDust.noGravity = true;
            }
        }

        private void AI_023_Wretch(out Player ownerPlayer, out bool runAI)
        {
            runAI = true;
            ownerPlayer = Main.player[Projectile.owner];
            if (!ownerPlayer.active || ownerPlayer.dead)
            {
                ownerPlayer.GetModPlayer<WDALTPlayer>().wretchMinion = false;
                runAI = false;
            }
            if (ownerPlayer.GetModPlayer<WDALTPlayer>().wretchMinion)
            {
                Projectile.timeLeft = 2;
            }
        }

        private void AI_023_Wretch_CoreTasks(ref Player ownerPlayer, out Vector2 idlePos, out Vector2 vectorToIdlePos, out float distanceToIdlePos)
        {
            bool sync = false;
            idlePos = ownerPlayer.Center;
            idlePos.Y -= (Projectile.height * 3f);
            vectorToIdlePos = idlePos - Projectile.Center;
            distanceToIdlePos = vectorToIdlePos.Length();
            Projectile.spriteDirection = Projectile.direction = ((Projectile.velocity.X > 0f) ? 1 : -1); //Fix wrong shading when shooting to the left.
            float roatateOffset = (float)Math.PI / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + roatateOffset;
            if (Projectile.owner == Main.myPlayer && distanceToIdlePos > (detectionRange * 2f))
            {
                Projectile.Center = idlePos;
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);;
                Projectile.velocity *= idleMoveSpeed;
                sync = true;
            }
            if (sync && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
            }
        }

        private void AI_023_Wretch_SearchTargets(ref Player ownerPlayer, out float distanceToTarget, out bool targetDetected, out Vector2 targetCenter, out Vector2 targetVelocity)
        {
            distanceToTarget = detectionRange;
            targetDetected = false;
            targetCenter = Projectile.Center;
            targetVelocity = Vector2.Zero;
            if (ownerPlayer.HasMinionAttackTargetNPC)
            {
                NPC target = Main.npc[ownerPlayer.MinionAttackTargetNPC];
                float currentDistance = Vector2.Distance(target.Center, Projectile.Center);
                bool collisionLine = Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1);
                if (currentDistance < (detectionRange * 2f) && collisionLine && target.CanBeChasedBy())
                {
                    distanceToTarget = Vector2.Distance(target.Center, Projectile.Center);
                    targetCenter = target.Center;
                    targetVelocity = target.velocity;
                    targetDetected = true;
                }
            }
            if (!targetDetected)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (!Main.npc[i].CanBeChasedBy())
                    {
                        continue;
                    }
                    NPC target = Main.npc[i];
                    float currentDistance = Vector2.Distance(target.Center, Projectile.Center);
                    bool collisionLine = Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1);
                    if (currentDistance < distanceToTarget && collisionLine)
                    {
                        distanceToTarget = currentDistance;
                        targetCenter = target.Center;
                        targetVelocity = target.velocity;
                        targetDetected = true;
                    }
                }
            }
        }

        private void AI_023_Wretch_CorrectOverlap(ref bool targetDetected)
        {
            float cFactor = attackOverlapCorrectionFactor;
            if (!targetDetected)
            {
                cFactor = idleOverlapCorrectionFactor;
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile otherMinion = Main.projectile[i];
                bool proximity = ((Math.Abs(Projectile.Center.X - otherMinion.Center.X) + Math.Abs(Projectile.Center.Y - otherMinion.Center.Y)) < (Projectile.width));
                if
                (
                    (i != Projectile.whoAmI) &&
                    (otherMinion.active) &&
                    (otherMinion.owner == Projectile.owner) &&
                    (otherMinion.minion) &&
                    (proximity)
                )
                {
                    if (Projectile.Center.X < otherMinion.Center.X)
                    {
                        Projectile.velocity.X -= cFactor;
                    }
                    else
                    {
                        Projectile.velocity.X += cFactor;
                    }
                    if (Projectile.Center.Y < otherMinion.Center.Y)
                    {
                        Projectile.velocity.Y -= cFactor;
                    }
                    else
                    {
                        Projectile.velocity.Y += cFactor;
                    }
                }
            }
        }

        private void AI_023_Wretch_AttackAI(ref float distanceToTarget, ref Vector2 targetCenter, ref Vector2 targetVelocity)
        {
            if (distanceToTarget > detectionRangeOffset)
            {
                Vector2 moveVector = (targetCenter - Projectile.Center);
                //Anti-Circle-Algorithm: If circular movement is detected, discard previous velocity.
                float correctionAngle = MathHelper.ToDegrees((float)Math.Acos(Vector2.Dot(moveVector, Projectile.velocity) / (moveVector.Length() * Projectile.velocity.Length()))); //Correction Angle in Degrees
                if (Math.Abs(correctionAngle - 90f) < 15f)
                {
                    Projectile.velocity = Vector2.Zero;
                }
                moveVector = moveVector.SafeNormalize(Vector2.Zero);
                moveVector *= attackMoveSpeed;
                Projectile.velocity += (moveVector / attackInertia);
                if (Projectile.velocity.Length() > attackMoveSpeed)
                {
                    Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);;
                    Projectile.velocity *= attackMoveSpeed;
                }
            }
        }

        private void AI_023_Wretch_AttackCorrectFreeze(ref float distanceToTarget)
        {
            bool sync = false;
            if (Projectile.velocity == Vector2.Zero || Projectile.velocity.Length() < zeroVectorMaxLen)
            {
                Projectile.velocity.X = (Main.rand.NextFloat() - 0.5f);
                Projectile.velocity.Y = (Main.rand.NextFloat() - 0.5f);
                sync = true;
            }
            float currentSpeed = Projectile.velocity.Length();
            float minSpeed = (attackMoveSpeed / attackInertia) * 0.25f;
            if (currentSpeed < minSpeed && distanceToTarget <= detectionRangeOffset)
            {
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);;
                Projectile.velocity *= (attackMoveSpeed / attackInertia);
            }
            if (sync && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
            }
        }

        private void AI_023_Wretch_IdleAI(ref float distanceToIdlePos, ref Vector2 vectorToIdlePos)
        {
            if (distanceToIdlePos > idleDistance)
            {
                //Smooth-Slowdown-Algorithm: Smoothly slow down from attack to idle speed.
                float speedFactor = ((distanceToIdlePos * idleAccelerationFactor) / idleDistance);
                if (speedFactor < 1f) //Make sure speed will be at least idleMoveSpeed
                {
                    speedFactor = 1f;
                }
                vectorToIdlePos = vectorToIdlePos.SafeNormalize(Vector2.Zero);
                vectorToIdlePos *= (idleMoveSpeed * speedFactor);
                Projectile.velocity += (vectorToIdlePos / idleInertia);
                if (Projectile.velocity.Length() > (idleMoveSpeed * speedFactor)) //Smoothly accelerate/decelerate based on distance to idle position
                {
                    Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);;
                    Projectile.velocity *= (idleMoveSpeed * speedFactor);
                }
                if (Projectile.velocity.Length() > attackMoveSpeed) //Cap movement speed at attack movement speed
                {
                    Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);;
                    Projectile.velocity *= attackMoveSpeed;
                }
            }
        }

        private void AI_023_Wretch_IdleCorrectFreeze(ref float distanceToIdlePos)
        {
            bool sync = false;
            if (Projectile.velocity == Vector2.Zero || Projectile.velocity.Length() < zeroVectorMaxLen)
            {
                Projectile.velocity.X = (Main.rand.NextFloat() - 0.5f);
                Projectile.velocity.Y = (Main.rand.NextFloat() - 0.5f);
                sync = true;
            }
            float currentSpeed = Projectile.velocity.Length();
            float minSpeed = (idleMoveSpeed / idleInertia) * 0.25f;
            if (currentSpeed < minSpeed && distanceToIdlePos <= idleDistance)
            {
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);;
                Projectile.velocity *= (idleMoveSpeed / idleInertia);
            }
            if (sync && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
            }
        }

        private void AI_023_Wretch_UpdateFrames()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
        }
    }
}
