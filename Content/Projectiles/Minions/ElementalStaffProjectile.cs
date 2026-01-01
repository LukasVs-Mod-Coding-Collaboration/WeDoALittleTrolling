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
    public class ElementalStaffProjectile : ModProjectile
    {
        public const float idleOverlapCorrectionFactor = 0.06f;
        public const float attackOverlapCorrectionFactor = 0.12f;
        public const float detectionRange = 768f;
        public const float detectionRangeOffset = 256f;
        public const float detectionRangeOffset2 = 128f;
        public const float idleMoveSpeed = 3f;
        public const float attackMoveSpeed = 9f;
        public const float idleInertia = 12f;
        public const float attackInertia = 6f;
        public const float idleDistance = 48f;
        public const float idleAccelerationFactor = 0.625f;
        public const float bulletSpeed = 24f;
        public const float bulletOffsetMultiplier = 24f; // Actual height of Projectile (32) - Hixbox height of Projectile (8) = 24
        public const float zeroVectorMaxLen = 0.0000152587890625f; // 2^-16 / Binary: 0.0000000000000001
        public static readonly Vector2 gfxShootOffset = new Vector2(0f, 0f);
        public long ticksAlive = 0;
        public long lastActionTick = 0;

        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionCannotBeFreed[Projectile.type] = false;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
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
            return false;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((long)ticksAlive);
            writer.Write((long)lastActionTick);
            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            ticksAlive = reader.ReadInt64();
            lastActionTick = reader.ReadInt64();
            base.ReceiveExtraAI(reader);
        }

        public override void AI()
        {
            AI_012_FrozenElemental(out Player ownerPlayer, out bool runAI);
            if (!runAI)
            {
                return;
            }
            AI_012_FrozenElemental_CoreTasks(ref ownerPlayer, out Vector2 idlePos, out Vector2 vectorToIdlePos, out float distanceToIdlePos);
            AI_012_FrozenElemental_SearchTargets(ref ownerPlayer, out float distanceToTarget, out bool targetDetected, out Vector2 targetCenter, out Vector2 targetVelocity);
            AI_012_FrozenElemental_CorrectOverlap(ref targetDetected);
            if (targetDetected)
            {
                AI_012_FrozenElemental_AttackAI(ref distanceToTarget, ref targetCenter, ref targetVelocity, ref targetDetected);
            }
            else
            {
                AI_012_FrozenElemental_IdleAI(ref distanceToIdlePos, ref vectorToIdlePos);
                AI_012_FrozenElemental_IdleCorrectFreeze(ref distanceToIdlePos);
            }
            AI_012_FrozenElemental_UpdateFrames();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void PostDraw(Color lightColor)
        {
            if (Main.rand.NextBool(30))
            {
                int rMax = (int)Projectile.width / 2;
                double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                double angle = Main.rand.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = Projectile.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.IceTorch, null, 0, default);
                newDust.noGravity = true;
            }
        }

        private void AI_012_FrozenElemental(out Player ownerPlayer, out bool runAI)
        {
            ticksAlive++;
            runAI = true;
            ownerPlayer = Main.player[Projectile.owner];
            if (!ownerPlayer.active || ownerPlayer.dead)
            {
                ownerPlayer.GetModPlayer<WDALTPlayer>().frozenElementalMinion = false;
                runAI = false;
            }
            if (ownerPlayer.GetModPlayer<WDALTPlayer>().frozenElementalMinion)
            {
                Projectile.timeLeft = 2;
            }
        }

        private void AI_012_FrozenElemental_CoreTasks(ref Player ownerPlayer, out Vector2 idlePos, out Vector2 vectorToIdlePos, out float distanceToIdlePos)
        {
            bool sync = false;
            idlePos = ownerPlayer.Center;
            idlePos.Y -= (Projectile.height * 3f);
            vectorToIdlePos = idlePos - Projectile.Center;
            distanceToIdlePos = vectorToIdlePos.Length();
            if (Projectile.owner == Main.myPlayer && distanceToIdlePos > (detectionRange * 2f))
            {
                Projectile.Center = idlePos;
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);
                Projectile.velocity *= idleMoveSpeed;
                sync = true;
            }
            if (sync && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
            }
        }

        private void AI_012_FrozenElemental_SearchTargets(ref Player ownerPlayer, out float distanceToTarget, out bool targetDetected, out Vector2 targetCenter, out Vector2 targetVelocity)
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

        private void AI_012_FrozenElemental_CorrectOverlap(ref bool targetDetected)
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

        private void AI_012_FrozenElemental_AttackAI(ref float distanceToTarget, ref Vector2 targetCenter, ref Vector2 targetVelocity, ref bool targetDetected)
        {
            bool sync = false;
            if (distanceToTarget > detectionRangeOffset)
            {
                Vector2 moveVector = (targetCenter - Projectile.Center);
                moveVector = moveVector.SafeNormalize(Vector2.Zero);
                moveVector *= attackMoveSpeed;
                Projectile.velocity += (moveVector / attackInertia);
                if (Projectile.velocity.Length() > attackMoveSpeed)
                {
                    Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);
                    Projectile.velocity *= attackMoveSpeed;
                }
            }
            else
            {
                float currentSpeed = Projectile.velocity.Length();
                if (distanceToTarget > detectionRangeOffset2)
                {
                    Vector2 moveVector = (targetCenter - Projectile.Center);
                    Projectile.velocity = moveVector;
                    Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);
                    Projectile.velocity *= currentSpeed;
                }
                if (Projectile.velocity == Vector2.Zero || Projectile.velocity.Length() < zeroVectorMaxLen)
                {
                    Projectile.velocity.X = (Main.rand.NextFloat() - 0.5f);
                    Projectile.velocity.Y = (Main.rand.NextFloat() - 0.5f);
                    sync = true;
                }
                currentSpeed = Projectile.velocity.Length();
                float minSpeed = (idleMoveSpeed / idleInertia) * 0.25f;
                if (currentSpeed > (idleMoveSpeed / idleInertia))
                {
                    Projectile.velocity *= idleAccelerationFactor;
                }
                else if (currentSpeed < minSpeed)
                {
                    Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);;
                    Projectile.velocity *= (idleMoveSpeed / idleInertia);
                }
                AI_012_FrozenElemental_CorrectOverlap(ref targetDetected);
            }
            bool cooldownFinished = ((ticksAlive - lastActionTick) >= (Projectile.localNPCHitCooldown));
            if (cooldownFinished)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector2 pos = Projectile.Center + gfxShootOffset;
                        // Roughly Predict where the target is going to be when the Laser reaches it
                        //
                        // <Distance to target - offset> == (distanceToTarget - bulletOffsetMultiplier)
                        // <distance the bullet travels per tick> == bulletSpeed
                        // <distance the target travels per tick> == targetVelocity
                        //
                        // => <Distance to target - offset> = <Number of ticks until the laser hits the entity> * <distance the bullet travels per tick>
                        // => <Number of ticks until the laser hits the entity> = <Distance to target - offset> / <distance the bullet travels per tick>
                        // => <Distance the entity will have travelled on bullet impact> = <distance the target travels per tick> * <Number of ticks until the laser hits the entity>
                        //
                        // <Number of ticks until the laser hits the entity> == ((distanceToTarget - bulletOffsetMultiplier) / bulletSpeed)
                        // <Distance the entity will have travelled on bullet impact> == targetVelocity * ((distanceToTarget - bulletOffsetMultiplier) / bulletSpeed)
                        Vector2 predictVelocity = targetVelocity * ((distanceToTarget - bulletOffsetMultiplier) / bulletSpeed);
                        Vector2 shootVector = ((targetCenter + predictVelocity) - pos);
                        shootVector = shootVector.SafeNormalize(Vector2.Zero);
                        pos += (shootVector * bulletOffsetMultiplier);
                        shootVector *= bulletSpeed;
                        float rotation = -1.5f + (i * 3f);
                        Projectile.NewProjectileDirect
                        (
                            Projectile.GetSource_FromAI(),
                            pos,
                            shootVector.RotatedBy(MathHelper.ToRadians(rotation)),
                            ModContent.ProjectileType<ElementalStaffProjectileBullet>(),
                            Projectile.damage,
                            Projectile.knockBack,
                            Projectile.owner
                        );
                    }
                }
                SoundEngine.PlaySound(SoundID.Item28, Projectile.Center);
                lastActionTick = ticksAlive;
            }
            if (sync && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
            }
        }

        private void AI_012_FrozenElemental_IdleAI(ref float distanceToIdlePos, ref Vector2 vectorToIdlePos)
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

        private void AI_012_FrozenElemental_IdleCorrectFreeze(ref float distanceToIdlePos)
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

        private void AI_012_FrozenElemental_UpdateFrames()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 12)
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
