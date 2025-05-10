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

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class ThrowOfTheLunaticProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 400f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 17.5f;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.penetrate = -1;
            Projectile.scale = 1.15f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.usesIDStaticNPCImmunity = false;
            Projectile.idStaticNPCHitCooldown = -1;
            Projectile.light = 0.8f;
        }

        public override void AI()
        {
            if (Projectile.owner == Main.myPlayer && Projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive % 20 == 0)
            {
                Vector2 velocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                velocity = velocity.SafeNormalize(Vector2.Zero);
                velocity *= Main.rand.Next(ThrowOfTheLunaticProjectileBeam.moveSpeed / 2, ThrowOfTheLunaticProjectileBeam.moveSpeed);
                Projectile.NewProjectile
                (
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    velocity,
                    ModContent.ProjectileType<ThrowOfTheLunaticProjectileBeam>(),
                    (int)Math.Round(Projectile.damage * 0.75),
                    Projectile.knockBack,
                    Projectile.owner
                );
            }
        }

        public override void PostAI()
        {
            if (Projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive % 5 == 0)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.AncientLight).noGravity = true;
            }
        }
    }
}
