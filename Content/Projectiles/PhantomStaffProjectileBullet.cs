using Terraria;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.AI;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class PhantomStaffProjectileBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.light = 0.8f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.ArmorPenetration = 60;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        public override void AI()
        {
            WDALTProjectileAI.AI_001_SimpleBullet(Projectile);
        }
    }
}
