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
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class PhotonsplicerProjectile : ModProjectile
    {
        protected virtual float SpearLengh => (float)Math.Sqrt((200*200)+(200*200));
        protected virtual float HoldoutRangeMin => (SpearLengh/2 - (float)64.0) + (float)32.0;
        protected virtual float HoldoutRangeMax => (SpearLengh/2 + (float)64.0) + (float)32.0;

        public override void SetDefaults() {
            Projectile.CloneDefaults(ProjectileID.Spear);
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.tileCollide = false;
            Projectile.scale = (float)1.0;
        }

        public override bool PreAI() {
            Player player = Main.player[Projectile.owner];
            int duration = player.itemAnimationMax;

            player.heldProj = Projectile.whoAmI;

            // Reset projectile time left if necessary
            if (Projectile.timeLeft > duration) {
                Projectile.timeLeft = duration;
            }
            // Stop projectile after having travelled half way back, so it seems less like a piston.
            if (Projectile.timeLeft <= duration * 0.25)
            {
                Projectile.velocity = new Vector2((float)0.0, (float)0.0);
            }

            Projectile.velocity = Vector2.Normalize(Projectile.velocity);

            float halfDuration = duration * (float)0.5;
            float progress;

            if (Projectile.timeLeft < halfDuration)
            {
                progress = Projectile.timeLeft / halfDuration;
            }
            else
            {
                progress = ((duration - Projectile.timeLeft) / halfDuration);
            }

            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

            // Apply proper rotation to the sprite.
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.ToRadians((float)45.0);
            }
            else
            {
                Projectile.rotation += MathHelper.ToRadians((float)135.0);
            }

            return false;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            Vector2 vOffset = Projectile.velocity * 16;
            Point pOffset = new Point();
            pOffset.X = - (int)Math.Round(vOffset.X);
            pOffset.Y = - (int)Math.Round(vOffset.Y);
            hitbox.Offset(pOffset);
            base.ModifyDamageHitbox(ref hitbox);
        }

    }
}
