using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class PhantomStaffProjectileBullet : ModProjectile
    {
        public override void SetDefaults() {
            Projectile.width = 12;
            Projectile.height = 48;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.light = 0.8f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Projectile.ArmorPenetration = 60;
            Projectile.extraUpdates = 1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20;
            AIType = ProjectileID.Bullet;
        }
    }
}