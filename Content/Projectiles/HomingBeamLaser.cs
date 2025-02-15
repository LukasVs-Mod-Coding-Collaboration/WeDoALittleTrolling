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
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class HomingBeamLaser : ModProjectile
    {
        NPC target;
        Vector2 laserDrawOffset;
        bool isAHit;
        bool hasDealtDamage;

        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90; 
            Projectile.alpha = 0; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 1.0f; // How much light emit around the projectile
            Projectile.ignoreWater = true; 
            Projectile.tileCollide = false; 
            Projectile.ArmorPenetration = 999;
            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

        public void TransferNecessaryInformation(NPC determinedTarget, Vector2 velocityGiven, bool didProjectileHit)
        {
            target = determinedTarget;
            velocityGiven.SafeNormalize(Vector2.Zero);
            laserDrawOffset = velocityGiven;
            isAHit = didProjectileHit;
        }

        public override void AI()
        {
            Player ownerOfThis = Main.player[Projectile.owner];
            if (isAHit)
            {
                if
                (
                target != null &&
                !target.friendly &&
                !target.CountsAsACritter &&
                !target.isLikeATownNPC &&
                !target.dontTakeDamage &&
                target.active &&
                target.CanBeChasedBy()
                )
                {
                    Projectile.Center = target.Center;
                }
            }
            else
            {
                Vector2 rayEndLocation = laserDrawOffset * 256;
                rayEndLocation.Y -= 6;
                Projectile.Center = ownerOfThis.Center + rayEndLocation;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hasDealtDamage = true;
            base.OnHitNPC(target, hit, damageDone);
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (hasDealtDamage || !isAHit)
            {
                return false;
            }
            else
            { 
                return base.CanHitNPC(target);
            }
            
        }




        /*
        public override void OnSpawn(IEntitySource source
        {
            this.original_location = Projectile.position;
            Projectile.netUpdate = true;
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
        */
    }
}
