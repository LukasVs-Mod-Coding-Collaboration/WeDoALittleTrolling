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
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class EmberBolt : ModProjectile
    {
        public static UnifiedRandom rnd = new UnifiedRandom();
        long ticksAlive = 0;

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 18;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 360;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 6;
            Projectile.light = 1.0f;
        }

        public override void AI()
        {
            ticksAlive++;
            Vector2 dustPosition = Projectile.Center;
            Vector2 dustVelocityCenter = Vector2.Zero;

            Vector2 dustVelocityMiddleUp = Projectile.velocity.RotatedBy(MathHelper.ToRadians(90));
            Vector2 dustVelocityMiddleDown = Projectile.velocity.RotatedBy(MathHelper.ToRadians(270));
            dustVelocityMiddleUp = dustVelocityMiddleUp.SafeNormalize(Vector2.Zero);
            dustVelocityMiddleUp *= 0.4f;
            dustVelocityMiddleDown = dustVelocityMiddleDown.SafeNormalize(Vector2.Zero);
            dustVelocityMiddleDown *= 0.4f;

            Vector2 dustVelocityOuterUp = Projectile.velocity.RotatedBy(MathHelper.ToRadians(90));
            Vector2 dustVelocityOuterDown = Projectile.velocity.RotatedBy(MathHelper.ToRadians(270));
            dustVelocityOuterUp = dustVelocityOuterUp.SafeNormalize(Vector2.Zero);
            dustVelocityOuterUp *= 0.8f;
            dustVelocityOuterDown = dustVelocityOuterDown.SafeNormalize(Vector2.Zero);
            dustVelocityOuterDown *= 0.8f;

            if (ticksAlive > 8)
            {
                Dust DustCenter = Dust.NewDustPerfect(dustPosition, DustID.Smoke, dustVelocityCenter, 0, Color.Yellow);
                DustCenter.noGravity = true;
            }
            if (ticksAlive > 10)
            {
                Dust DustMiddleUp = Dust.NewDustPerfect(dustPosition, DustID.Smoke, dustVelocityMiddleUp, 0, Color.Orange, 0.8f);
                Dust DustMiddleDown = Dust.NewDustPerfect(dustPosition, DustID.Smoke, dustVelocityMiddleDown, 0, Color.Orange, 0.8f);
                DustMiddleUp.noGravity = true;
                DustMiddleDown.noGravity = true;
            }
            if (ticksAlive > 12)
            {
                Dust DustOuterUp = Dust.NewDustPerfect(dustPosition, DustID.Smoke, dustVelocityOuterUp, 0, Color.Red, 0.6f);
                Dust DustOuterDown = Dust.NewDustPerfect(dustPosition, DustID.Smoke, dustVelocityOuterDown, 0, Color.Red, 0.6f);
                DustOuterUp.noGravity = true;
                DustOuterDown.noGravity = true;
            }

            int turnIndex = 0;
            if (rnd.NextBool(60))
            {
                if (rnd.NextBool(2))
                {
                    if (turnIndex < 4)
                    {
                        Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(8));
                        turnIndex++;
                    }
                }
                else
                {
                    if (turnIndex > -4)
                    {
                        Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(352));
                        turnIndex--;
                    }
                }

            }

        }

        public override bool PreKill(int timeLeft)
        {
            Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center, Projectile.velocity, ProjectileID.Volcano, 0, 0f, Projectile.owner);
            return base.PreKill(timeLeft);
        }
    }
}
