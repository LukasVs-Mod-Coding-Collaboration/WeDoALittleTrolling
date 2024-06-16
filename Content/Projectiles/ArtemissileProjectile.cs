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
using System;
using Terraria;
using Terraria.Utilities;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Buffs;
using Terraria.Audio;
using WeDoALittleTrolling.Common.Utilities;
using Terraria.DataStructures;
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class ArtemissileProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.ai[0] = 0f;
            this.DrawOffsetX = -4;
            this.DrawOriginOffsetY = -4;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            AI_013_Artemissile();
            AI_013_Artemissile_UpdateFrames();
        }

        private void AI_013_Artemissile()
        {
            Projectile.spriteDirection = Projectile.direction = ((Projectile.velocity.X > 0f) ? 1 : -1); //Fix wrong shading when shooting to the left.
            float roatateOffset = (float)Math.PI / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + roatateOffset;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            base.ModifyDamageHitbox(ref hitbox);
        }

        private void AI_013_Artemissile_UpdateFrames()
        {
            bool stuckMode = (Projectile.ai[0] == 1f);
            Projectile.frameCounter++;
            if (stuckMode)
            {
                if (Projectile.frameCounter >= 15)
                {
                    Projectile.frameCounter = 0;
                    if (Projectile.frame > 0)
                    {
                        Projectile.frame = 0;
                    }
                    else
                    {
                        Projectile.frame = (Main.projFrames[Projectile.type] - 1);
                    }
                }
                
            }
            else
            {
                if (Projectile.frameCounter >= 30 || (Projectile.frameCounter >= 6 && Projectile.frame > 0))
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                    if (Projectile.frame >= (Main.projFrames[Projectile.type] - 1))
                    {
                        Projectile.frame = 0;
                    }
                }
            }
        }
    }
}
