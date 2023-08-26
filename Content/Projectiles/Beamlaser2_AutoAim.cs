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
using System.IO;
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
        public long currentTick = 0;
        public long location_is_locked_tick = 9999;
        public bool location_is_locked = false;
        public override void SetStaticDefaults() {
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
            Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 99*(99+1); // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 1.0f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 99; // Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.ArmorPenetration = 250;

            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                if(k <= location_is_locked_tick && ((k % 4) == 0)) //efficiency: Only paint projectile for every fourth cahched position
                {
                    Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
                }
            }
            Vector2 drawPosOrig = (this.original_location - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color colorOrig = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture, drawPosOrig, null, colorOrig, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            return true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            this.original_location = Projectile.position;
            Projectile.netUpdate = true;
            //Auto-target AI
            AI_002_ApollonAimAssist();
            base.OnSpawn(source);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.WriteVector2(this.original_location);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            this.original_location = reader.ReadVector2();
        }

        public override bool ShouldUpdatePosition()
        {
            float maxBeamTravelX = Main.screenWidth/2;
            float maxBeamTravelY = Main.screenHeight/2;
            if(maxBeamTravelX > 1920/2)
            {
                maxBeamTravelX = 1920/2;
            }
            if(maxBeamTravelY > 1080/2)
            {
                maxBeamTravelY = 1080/2;
            }
            this.currentTick++;
            if(Math.Abs(this.Projectile.position.X - this.original_location.X) > maxBeamTravelX || //Configure max lengh of beam in x coords
               Math.Abs(this.Projectile.position.Y - this.original_location.Y) > maxBeamTravelY  || //Configure max lengh of beam in y coords
               this.location_is_locked)
            {
                this.location_is_locked = true;
                this.location_is_locked_tick = this.currentTick;
                this.Projectile.damage = 0;
                this.Projectile.position = this.original_location;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void AI_002_ApollonAimAssist()
        {
            Vector2 spawnCenter = Projectile.Center;
            float lowest_distance = 9999;
            float maxBeamTravelX = Main.screenWidth/2;
            float maxBeamTravelY = Main.screenHeight/2;
            float origVelocityLength = Projectile.velocity.Length();
            if(maxBeamTravelX > 1920/2)
            {
                maxBeamTravelX = 1920/2;
            }
            if(maxBeamTravelY > 1080/2)
            {
                maxBeamTravelY = 1080/2;
            }
            for(int i = 0; i < Main.npc.Length; i++)
            {
                NPC target = Main.npc[i];
                float shootToX                  = target.position.X + (float)target.width * 0.5f - spawnCenter.X;
                float shootToY                  = target.position.Y - spawnCenter.Y;
                Vector2 shootTo                 = new Vector2(shootToX, shootToY);
                float distance                  = shootTo.Length();
                Vector2 originalVector          = Projectile.velocity;
                originalVector.Normalize();
                float x                         = originalVector.X;
                float y                         = originalVector.Y;
                float a                         = Math.Abs(this.original_location.X - target.position.X);
                float b                         = Math.Abs(this.original_location.Y - target.position.Y);
                float a_through_x               = Math.Abs(a/x);
                float b_through_y               = Math.Abs(b/y);
                float x_y_inaccuracy            = x*a_through_x;
                float y_y_inaccuracy            = y*a_through_x;
                float x_x_inaccuracy            = x*b_through_y;
                float y_x_inaccuracy            = y*b_through_y;
                Vector2 KonePositionY           = original_location + new Vector2(x_y_inaccuracy, y_y_inaccuracy);
                Vector2 KonePositionX           = original_location + new Vector2(x_x_inaccuracy, y_x_inaccuracy);
                Vector2 LineToTargetY           = new Vector2(target.position.X - KonePositionY.X, target.position.Y - KonePositionY.Y);
                Vector2 LineToTargetX           = new Vector2(target.position.X - KonePositionX.X, target.position.Y - KonePositionX.Y);
                float y_inaccuracy              = (float)System.Math.Sqrt((double)(LineToTargetY.X * LineToTargetY.X + LineToTargetY.Y * LineToTargetY.Y));
                float x_inaccuracy              = (float)System.Math.Sqrt((double)(LineToTargetX.X * LineToTargetX.X + LineToTargetX.Y * LineToTargetX.Y));
                float inaccuracy_tolerance      = 160;
                if
                (
                    (
                        Math.Abs(this.original_location.X - target.position.X) < maxBeamTravelX && //Configure detect zone in x coords
                        Math.Abs(this.original_location.Y - target.position.Y) < maxBeamTravelY     //Configure detect zone in y coords
                    ) &&
                    (
                        Math.Abs(this.original_location.X - target.position.X) > inaccuracy_tolerance ||   //Configure non-detect zone in x coords
                        Math.Abs(this.original_location.Y - target.position.Y) > inaccuracy_tolerance      //Configure non-detect zone in y coords
                    )&&
                    !target.friendly &&
                    !target.CountsAsACritter &&
                    !target.isLikeATownNPC &&
                    !target.dontTakeDamage &&
                    target.active &&
                    target.CanBeChasedBy() &&
                    distance < lowest_distance &&
                    (
                        y_inaccuracy < inaccuracy_tolerance ||
                        x_inaccuracy < inaccuracy_tolerance
                    )
                )
                {
                    Vector2 newVelocity = shootTo;
                    newVelocity.Normalize();
                    newVelocity *= origVelocityLength;
                    Projectile.velocity = newVelocity;
                    lowest_distance = distance;
                }
            }
        }
    }
}
