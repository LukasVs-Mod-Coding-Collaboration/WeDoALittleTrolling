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
        // Define the range of the Spear Projectile. These are overrideable properties, in case you'll want to make a class inheriting from this one.
        protected virtual float SpearLengh => (float)Math.Sqrt((200*200)+(200*200));
        protected virtual float HoldoutRangeMin => (SpearLengh/2 - (float)64.0) + (float)48.0;
        protected virtual float HoldoutRangeMax => (SpearLengh/2 + (float)64.0) + (float)48.0;

        public override void SetDefaults() {
            Projectile.CloneDefaults(ProjectileID.Spear); // Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.scale = (float)1.0;
        }

        public override bool PreAI() {
            Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            int duration = player.itemAnimationMax + 8; // Define the duration the projectile will exist in frames

            player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

            // Reset projectile time left if necessary
            if (Projectile.timeLeft > duration) {
                Projectile.timeLeft = duration;
            }
            if (Projectile.timeLeft < 8)
            {
                Projectile.velocity = new Vector2((float)0.0, (float)0.0);
            }

            Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

            float halfDuration = duration * (float)0.5;
            float progress;

            // Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
            if (Projectile.timeLeft < halfDuration) {
                progress = Projectile.timeLeft / halfDuration;
            }
            else {
                progress = ((duration - Projectile.timeLeft) / halfDuration);
            }

            // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

            // Apply proper rotation to the sprite.
            if (Projectile.spriteDirection == -1) {
                // If sprite is facing left, rotate 45 degrees
                Projectile.rotation += MathHelper.ToRadians((float)45.0);
            }
            else {
                // If sprite is facing right, rotate 135 degrees
                Projectile.rotation += MathHelper.ToRadians((float)135.0);
            }

            return false; // Don't execute vanilla AI.
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
