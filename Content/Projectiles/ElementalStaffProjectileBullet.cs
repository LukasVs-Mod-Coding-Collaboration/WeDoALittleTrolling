using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class ElementalStaffProjectileBullet : ModProjectile
    {
        public static readonly float homingRange = 512f;
        public static readonly float correctionFactor = 0.30f;

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 512;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }

        public override void AI()
        {
            AI_013_SimpleBullet_AutoAim();
        }

        public override bool PreKill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                int rMax = (int)Projectile.width;
                double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                double angle = Main.rand.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = Projectile.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.IceTorch, null, 0, default);
                newDust.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
            return base.PreKill(timeLeft);
        }

        public override void PostDraw(Color lightColor)
        {
            if (Projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive % 2 == 0)
            {
                int rMax = (int)Projectile.width;
                double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                double angle = Main.rand.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = Projectile.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.IceTorch, null, 0, default);
                newDust.noGravity = true;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        private void AI_013_SimpleBullet_AutoAim()
        {
            float origVelocityLength = Projectile.velocity.Length();
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
                moveVector.Normalize();
                moveVector *= (origVelocityLength * correctionFactor);
                Projectile.velocity += moveVector;
                Projectile.velocity.Normalize();
                Projectile.velocity *= origVelocityLength;
            }
            Projectile.spriteDirection = Projectile.direction; //Fix wrong shading when shooting to the left.
            float roatateOffset = (float)Math.PI / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + roatateOffset;
        }
    }
}
