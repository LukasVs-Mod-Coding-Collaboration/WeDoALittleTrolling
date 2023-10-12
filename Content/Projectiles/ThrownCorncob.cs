using System;
using Microsoft.Xna.Framework;
using rail;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class ThrownCorncob : ModProjectile
    {
        public static UnifiedRandom random = new UnifiedRandom();
        public int bounces = 0;
        public static readonly float gravityFactor = 0.25f;
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.usesIDStaticNPCImmunity = false;
            Projectile.idStaticNPCHitCooldown = -1;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            AI_011_ThrownCorncob();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            Projectile.velocity *= 0.75f;
            bounces++;
            if(bounces >= 5)
            {
                Projectile.Kill();
            }
            else
            {
                AnimateImpact();
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            AnimateImpact();
        }

        private void AnimateImpact()
        {
            for(int i = 0; i < 10; i++)
            {
                int rMax = (int)Math.Round(Projectile.width * Projectile.scale);
                double r = rMax * Math.Sqrt(random.NextDouble());
                double angle = random.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = Projectile.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                int dustType = random.Next(0, 1);
                switch (dustType)
                {
                    case 0:
                        Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Sandnado, null, 0, default, Projectile.scale);
                        newDust.noGravity = true;
                        break;
                    default:
                        break;
                }
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
        }

        private void AI_011_ThrownCorncob()
        {
            Projectile.velocity.Y += gravityFactor;
            Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * (float)Projectile.direction;
            Projectile.spriteDirection = Projectile.direction; //Fix wrong shading when shooting to the left.
        }
    }
}
