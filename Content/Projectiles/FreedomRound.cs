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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class FreedomRound : ModProjectile
    {
        public const int trailDistance = 4;
        public static UnifiedRandom random = new UnifiedRandom();
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = (8 * trailDistance);
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 56;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.light = 1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 7;
            Projectile.scale = 1.2f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
            Projectile.knockBack = 8f;
            AIType = ProjectileID.Bullet;
        }

        public override bool PreKill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                float spawnYOffset = 20f;
                Vector2 spawnPos = new Vector2((Projectile.Center.X), (Projectile.Center.Y - spawnYOffset));
                //Vector2 spawnVelocity = new Vector2((random.NextFloat() - 0.5f), (random.NextFloat() - 0.5f));
                Vector2 spawnVelocity = Projectile.velocity * (-1f);
                spawnVelocity = spawnVelocity.SafeNormalize(Vector2.Zero);
                spawnVelocity *= 8f;
                Projectile.NewProjectileDirect(Projectile.GetSource_Death(), spawnPos, Vector2.Zero, ProjectileID.DD2ExplosiveTrapT3Explosion, 0, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectileDirect(Projectile.GetSource_Death(), Projectile.Center, spawnVelocity, ProjectileID.Volcano, 0, 0f, Projectile.owner);
            }
            for (int i = 0; i < 300; i++)
            {
                int rMax = (int)Math.Round(Projectile.width * Projectile.scale);
                double r = rMax * Math.Sqrt(random.NextDouble());
                double angle = random.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = Projectile.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                Vector2 dustVelocity = new Vector2((random.NextFloat() - 0.5f), (random.NextFloat() - 0.5f));
                dustVelocity = dustVelocity.SafeNormalize(Vector2.Zero);
                dustVelocity *= 8f;
                int dustTypeRandom = random.Next(0, 4);
                int dustType = DustID.Confetti_Blue;
                switch (dustTypeRandom)
                {
                    case 0:
                        dustType = DustID.Confetti_Blue;
                        break;
                    case 1:
                        dustType = DustID.Confetti_Green;
                        break;
                    case 2:
                        dustType = DustID.Confetti_Pink;
                        break;
                    case 3:
                        dustType = DustID.Confetti_Yellow;
                        break;
                    default:
                        dustType = DustID.Confetti_Blue;
                        break;
                }
                Dust newDust = Dust.NewDustPerfect(dustPosition, dustType, dustVelocity, 0, default, 1.5f);
                newDust.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            return base.PreKill(timeLeft);
        }
    }
}
