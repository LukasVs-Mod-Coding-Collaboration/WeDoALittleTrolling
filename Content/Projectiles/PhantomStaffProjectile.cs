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
using Terraria.Utilities;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.AI;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class PhantomStaffProjectile : ModProjectile
    {
        public long lastActionTick;
        public static UnifiedRandom random = new UnifiedRandom();
        
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 70;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.minionSlots = 1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.ArmorPenetration = 60;
            Projectile.timeLeft = 10;
            Projectile.netImportant = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.light = 0.8f;
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
            WDALTProjectileAI.AI_003_Luminite_Phantom(Projectile, ref lastActionTick);
        }

        public override void PostDraw(Color lightColor)
        {
            int rMax = (int)Projectile.width;
            double r = rMax * Math.Sqrt(random.NextDouble());
            double angle = random.NextDouble() * 2 * Math.PI;
            int xOffset = (int)Math.Round(r * Math.Cos(angle));
            int yOffset = (int)Math.Round(r * Math.Sin(angle));
            Vector2 dustPosition = Projectile.Center;
            dustPosition.X += xOffset;
            dustPosition.Y += yOffset;
            int dustType = random.Next(0, 16);
            switch (dustType)
            {
                case 0:
                    Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.LunarOre, null, 0, default);
                    newDust.noGravity = true;
                    break;
                default:
                    break;
            }
        }
    }
}
