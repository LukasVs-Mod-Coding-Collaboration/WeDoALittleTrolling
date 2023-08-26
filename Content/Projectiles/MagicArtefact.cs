using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks.Dataflow;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Content.Projectiles
{
    public class MagicArtefact : ModProjectile
    {
        public const float homingRange = 512f;
        public const float correctionFactor = 0.30f;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 1;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.ArmorPenetration = 30;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            AIType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            AI_005_MagicArtefact();
        }

        private void AI_005_MagicArtefact()
        {
            float origVelocityLength = Projectile.velocity.Length();
            float lowestDistance = homingRange;
            bool targetDetected = false;
            Vector2 targetCenter = Vector2.Zero;
            for(int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                float distance = Vector2.Distance(Projectile.Center, npc.Center);
                if((distance < lowestDistance) && npc.CanBeChasedBy())
                {
                    targetCenter = npc.Center;
                    targetDetected = true;
                    lowestDistance = distance;
                }
            }
            if(targetDetected)
            {
                Vector2 moveVector = (targetCenter - Projectile.Center);
                moveVector.Normalize();
                moveVector *= (origVelocityLength * correctionFactor);
                Projectile.velocity += moveVector;
                Projectile.velocity.Normalize();
                Projectile.velocity *= origVelocityLength;
            }
        }
    }
}
