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

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class PhantomStaffProjectile : ModProjectile
    {
        public const float overlapCorrectionFactor = 0.02f;
        public const float detectionRange = 1024f;
        public const float detectionRangeOffset = 48f;
        public const float moveSpeed = 16f;
        public const float idleIdleness = 32f;
        public const float attackIdleness = 4f;
        public const float idleDistance = 32f;
        public static UnifiedRandom random = new UnifiedRandom();
        
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
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
            Projectile.localNPCHitCooldown = 24;
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
            Player owner = Main.player[Projectile.owner];
            if(!owner.active || owner.dead)
            {
                owner.ClearBuff(ModContent.BuffType<PhantomStaffBuff>());
                return;
            }
            if(owner.HasBuff(ModContent.BuffType<PhantomStaffBuff>()))
            {
                Projectile.timeLeft = 10;
            }
            Vector2 idlePos = owner.Center;
            idlePos.Y -= 128f;
            Vector2 vectorToIdlePos = idlePos - Projectile.Center;
            float distanceToIdlePos = vectorToIdlePos.Length();
            if (Main.myPlayer == owner.whoAmI && distanceToIdlePos > (detectionRange * 2f))
            {
                Projectile.position = idlePos;
                Projectile.velocity *= 0.16f;
                Projectile.netUpdate = true;
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile otherMinion = Main.projectile[i];
                if
                (
                    (i != Projectile.whoAmI) &&
                    (otherMinion.active) &&
                    (otherMinion.owner == Projectile.owner) &&
                    ((Math.Abs(Projectile.position.X - otherMinion.position.X) + Math.Abs(Projectile.position.Y - otherMinion.position.Y)) < Projectile.width)
                )
                {
                    if (Projectile.position.X < otherMinion.position.X)
                    {
                        Projectile.velocity.X -= overlapCorrectionFactor;
                    }
                    else
                    {
                        Projectile.velocity.X += overlapCorrectionFactor;
                    }
                    if (Projectile.position.Y < otherMinion.position.Y)
                    {
                        Projectile.velocity.Y -= overlapCorrectionFactor;
                    }
                    else
                    {
                        Projectile.velocity.Y += overlapCorrectionFactor;
                    }
                }
            }
            float distanceToTarget = detectionRange;
            bool targetDetected = false;
            Vector2 targetCenter = Projectile.Center;
            if(owner.HasMinionAttackTargetNPC)
            {
                NPC target = Main.npc[owner.MinionAttackTargetNPC];
                if(Vector2.Distance(target.Center, Projectile.Center) < (detectionRange * 2f))
                {
                    distanceToTarget = Vector2.Distance(target.Center, Projectile.Center);
                    targetCenter = target.Center;
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
                            targetDetected = true;
                        }
                    }
                }
            }
            if(targetDetected)
            {
                if(distanceToTarget > detectionRangeOffset)
                {
                    Vector2 moveVector = (targetCenter - Projectile.Center);
                    moveVector.Normalize();
                    moveVector *= moveSpeed;
                    Projectile.velocity = (((Projectile.velocity * (attackIdleness - 1f)) + moveVector) / attackIdleness);

                }
            }
            else
            {
                if(distanceToIdlePos > idleDistance)
                {
                    vectorToIdlePos.Normalize();
                    vectorToIdlePos *= moveSpeed;
                    Projectile.velocity = (((Projectile.velocity * (idleIdleness - 1f)) + vectorToIdlePos) / idleIdleness);
                }
            }
            if(Projectile.velocity.Length() == 0f)
            {
                Projectile.velocity.X = 1f;
                Projectile.velocity.Y = 1f;
                Projectile.velocity.Normalize();
                Projectile.velocity *= moveSpeed;
            }
        }

        public override void PostDraw(Color lightColor)
        {
            int rMax = (int)Math.Round(Projectile.width*Projectile.scale);
            double r = rMax * Math.Sqrt(random.NextDouble());
            double angle = random.NextDouble() * 2 * Math.PI;
            int xOffset = (int)Math.Round(r * Math.Cos(angle));
            int yOffset = (int)Math.Round(r * Math.Sin(angle));
            Vector2 dustPosition = Projectile.Center;
            dustPosition.X += xOffset;
            dustPosition.Y += yOffset;
            int dustType = random.Next(0, 8);
            switch (dustType)
            {
                case 0:
                    Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.LunarOre, null, 0, default, Projectile.scale);
                    newDust.noGravity = true;
                    break;
                default:
                    break;
            }
        }
    }
}
