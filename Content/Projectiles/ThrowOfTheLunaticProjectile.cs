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
            Projectile.localNPCHitCooldown = 20;
            Projectile.usesIDStaticNPCImmunity = false;
            Projectile.idStaticNPCHitCooldown = -1;
            Projectile.light = 0.8f;
        }

        public override void AI()
        {
            if (Projectile.owner == Main.myPlayer && Projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive % 20 == 0)
            {
                Vector2 velocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                velocity.Normalize();
                velocity *= Main.rand.Next(ThrowOfTheLunaticProjectileBeam.moveSpeed / 2, ThrowOfTheLunaticProjectileBeam.moveSpeed);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ModContent.ProjectileType<ThrowOfTheLunaticProjectileBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
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
