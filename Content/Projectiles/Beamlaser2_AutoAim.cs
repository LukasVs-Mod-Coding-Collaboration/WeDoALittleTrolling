using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace  WeDoALittleTrolling.Content.Projectiles
{
    public class Beamlaser2_AutoAim : ModProjectile
    {
        public Vector2 original_location;
        public bool location_is_locked = false;
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Apollon Beam Laser");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9999; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }

        public override void SetDefaults() {
            Projectile.width = 10; // The width of projectile hitbox
            Projectile.height = 10; // The height of projectile hitbox
            Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 999999999; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 99*(99+1); // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 1.0f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 99; // Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.ArmorPenetration = 250;

            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

        public override bool PreDraw(ref Color lightColor) {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++) {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            this.original_location = Projectile.position;
            //Auto-target AI
            Vector2 spawnCenter = Projectile.Center;
            float lowest_distance = 9999;
            for(int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                float shootToX = target.position.X + (float)target.width * 0.5f - spawnCenter.X;
                float shootToY = target.position.Y - spawnCenter.Y;
                Vector2 shootTo = new Vector2(shootToX, shootToY);
                float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
                if
                (
                    (
                        Math.Abs(this.original_location.X - target.position.X) < 640 && //Configure max lengh of beam in x coords
                        Math.Abs(this.original_location.Y - target.position.Y) < 640     //Configure max lengh of beam in y coords
                    ) &&
                    !target.friendly && 
                    !target.CountsAsACritter && 
                    target.active &&
                    distance < lowest_distance
                )
                {
                    Vector2 newVelocity = shootTo;
                    newVelocity.Normalize();
                    Projectile.velocity = newVelocity;
                    lowest_distance = distance;
                }
            }
            base.OnSpawn(source);
        }

        public override bool ShouldUpdatePosition()
        {
            if(Math.Abs(this.Projectile.position.X - this.original_location.X) > 1280 || //Configure max lengh of beam in x coords
               Math.Abs(this.Projectile.position.Y - this.original_location.Y) > 768  || //Configure max lengh of beam in y coords
               this.location_is_locked)
            {
                this.location_is_locked = true;
                this.Projectile.damage = 0;
                this.Projectile.position = this.original_location;
                return false;
            }
            else
            {
                return true;
            }
        }

        /*
        public override void Kill(int timeLeft) {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
        */
    }
}
