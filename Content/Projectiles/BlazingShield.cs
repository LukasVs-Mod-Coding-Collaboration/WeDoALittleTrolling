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
    public class BlazingShield : ModProjectile
    {
        int rotationsPerSecond = 1; // Change to adjust spinning speed
        int currentDegree = 0;
        int baseDegreeMultiplier = 6;
        int distanceFromPlayer = 64;
        Player shieldOwner;


        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.penetrate = 1024;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 1200;
            Projectile.ArmorPenetration = 1024;
            Projectile.knockBack = 5f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }

        public override void AI()
        {
            shieldOwner = Main.player[Projectile.owner];
            Vector2 direction = new Vector2(
                                            (float)Math.Cos(MathHelper.ToRadians(currentDegree)),
                                            (float)Math.Sin(MathHelper.ToRadians(currentDegree))
                                            );
            direction = direction.SafeNormalize(Vector2.Zero);
            Vector2 position = shieldOwner.Center + (direction * distanceFromPlayer);
            Projectile.Center = position;
            currentDegree = currentDegree + (rotationsPerSecond * baseDegreeMultiplier);
            if (currentDegree >= 360)
            {
                currentDegree = currentDegree % 360;
            }
        }
    }
}
