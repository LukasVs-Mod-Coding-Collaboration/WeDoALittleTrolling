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
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class PharaohsCursebladeBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 12;
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            // Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                    Color drawLightColor = lightColor;
                    Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(drawLightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    SpriteEffects effects = SpriteEffects.None;
                    float scale = Projectile.scale - (0.1f * k);
                    if (Projectile.oldSpriteDirection[k] < 0)
                    {
                        effects = SpriteEffects.FlipHorizontally;
                    }
                    Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, scale, effects, 0);
            }
            return true;
        }

        public override bool PreKill(int timeLeft)
        {
            for (int i = 0; i < 80; i++)
            {
                int rMax = (int)Projectile.width / 2;
                double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                double angle = Main.rand.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = Projectile.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.GemSapphire, null, 0, default);
                newDust.noGravity = true;
            }
            return base.PreKill(timeLeft);
        }

        public override void AI()
        {
            AI_001_SimpleBullet();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        private void AI_001_SimpleBullet()
        {
            Projectile.spriteDirection = Projectile.direction = ((Projectile.velocity.X > 0f) ? 1 : -1); //Fix wrong shading when shooting to the left.
            float roatateOffset = (float)Math.PI / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + roatateOffset;
            if (Projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive < 6)
            {
                Projectile.tileCollide = false;
            }
            else
            {
                Projectile.tileCollide = true;
            }
            int rMax = (int)Projectile.width / 2;
            double r = rMax * Math.Sqrt(Main.rand.NextDouble());
            double angle = Main.rand.NextDouble() * 2 * Math.PI;
            int xOffset = (int)Math.Round(r * Math.Cos(angle));
            int yOffset = (int)Math.Round(r * Math.Sin(angle));
            Vector2 dustPosition = Projectile.Center;
            dustPosition.X += xOffset;
            dustPosition.Y += yOffset;
            Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.GemSapphire, null, 0, default);
            newDust.noGravity = true;
        }
    }
}
