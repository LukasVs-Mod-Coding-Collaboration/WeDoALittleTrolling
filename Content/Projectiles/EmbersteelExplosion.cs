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
    public class EmbersteelExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 90;
            Projectile.height = 90;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 28;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = false;
            Projectile.localNPCHitCooldown = -2;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 28;
            Projectile.light = 0.9f;
            this.DrawOffsetX = -4;
            this.DrawOriginOffsetX = 0f;
            this.DrawOriginOffsetY = -4;
        }

        public override void AI()
        {
            AI_001_SimpleBullet();
            if (!Main.dedServ && (Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.MultiplayerClient) && Projectile.timeLeft == 28)
            {
                SoundEngine.PlaySound(SoundID.Item62, Projectile.Center);
            }
        }

        private void AI_001_SimpleBullet()
        {
            Projectile.spriteDirection = Projectile.direction = ((Projectile.velocity.X > 0f) ? 1 : -1); //Fix wrong shading when shooting to the left.
            float roatateOffset = (float)Math.PI / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + roatateOffset;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
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
