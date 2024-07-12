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
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class FrozenShard : ModProjectile
    {
        public const float homingRange = 512f;
        public const float correctionFactor = 0.30f;

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 480;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            this.DrawOffsetX = -2;
            this.DrawOriginOffsetX = 0f;
            this.DrawOriginOffsetY = -2;
        }

        public override void AI()
        {
            AI_024_FrozenShard();
        }

        public override bool PreKill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int rMax = (int)Projectile.width;
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
            SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
            return base.PreKill(timeLeft);
        }

        public override void PostDraw(Color lightColor)
        {
            if (Projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive % 2 == 0)
            {
                int rMax = (int)Projectile.width;
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

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        private void AI_024_FrozenShard()
        {
            float origVelocityLength = Projectile.velocity.Length();
            float lowestDistance = homingRange;
            bool targetDetected = false;
            Vector2 targetCenter = Vector2.Zero;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                float distance = Vector2.Distance(Projectile.Center, npc.Center);
                if ((distance < lowestDistance) && npc.CanBeChasedBy())
                {
                    targetCenter = npc.Center;
                    targetDetected = true;
                    lowestDistance = distance;
                }
            }
            if (targetDetected)
            {
                Vector2 moveVector = (targetCenter - Projectile.Center);
                moveVector.Normalize();
                moveVector *= (origVelocityLength * correctionFactor);
                Projectile.velocity += moveVector;
                Projectile.velocity.Normalize();
                Projectile.velocity *= origVelocityLength;
            }
            Projectile.spriteDirection = Projectile.direction = ((Projectile.velocity.X > 0f) ? 1 : -1); //Fix wrong shading when shooting to the left.
            float roatateOffset = (float)Math.PI / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + roatateOffset;
        }
    }
}
