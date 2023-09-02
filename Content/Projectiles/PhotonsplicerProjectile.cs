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
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.AI;
using WeDoALittleTrolling.Content.Buffs;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class PhotonsplicerProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Spear);
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.tileCollide = false;
            Projectile.scale = (float)1.0;
        }

        public override bool PreAI()
        {
            WDALTProjectileAI.AI_004_PhotonSplicer(Projectile);
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<SearingInferno>(), 600, false);
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
