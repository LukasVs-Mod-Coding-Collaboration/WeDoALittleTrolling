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
using rail;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class ThrowOfTheLunaticProjectileBeam : ModProjectile
    {
        public const float homingRange = 512f;
        public const int moveSpeed = 10;
        public const float correctionFactor = 0.5f;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 400f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 17.5f;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = ProjAIStyleID.TerrarianBeam;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.penetrate = -1;
            Projectile.scale = 1.2f;
            Projectile.usesLocalNPCImmunity = false;
            Projectile.localNPCHitCooldown = -2;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            AIType = ProjectileID.TerrarianBeam;
        }

        public override void AI()
        {
            AI_010_LunaticBeam();
        }

        public override void PostAI()
        {
            if (Projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive % 5 == 0)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.AncientLight).noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Vector2 newVelocity = oldVelocity * (-1f);
            Projectile.velocity = newVelocity;
            return false;
        }

        private void AI_010_LunaticBeam()
        {
            float lowestDistance = homingRange;
            bool targetDetected = false;
            Vector2 targetCenter = Vector2.Zero;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                float distance = Vector2.Distance(Projectile.Center, npc.Center);
                if ((distance < lowestDistance) && npc.CanBeChasedBy())
                {
                    targetCenter = npc.Center;
                    targetDetected = true;
                    lowestDistance = distance;
                }
            }
            if (targetDetected)
            {
                Vector2 moveVector = (targetCenter - Projectile.Center);
                moveVector = moveVector.SafeNormalize(Vector2.Zero);
                moveVector *= ((float)moveSpeed * correctionFactor);
                Projectile.velocity += moveVector;
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);
                Projectile.velocity *= (float)moveSpeed;
            }
            Projectile.spriteDirection = Projectile.direction = ((Projectile.velocity.X > 0f) ? 1 : -1); //Fix wrong shading when shooting to the left.
        }
    }
}
