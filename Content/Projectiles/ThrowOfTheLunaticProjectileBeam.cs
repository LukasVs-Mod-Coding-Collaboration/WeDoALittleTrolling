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
        public static readonly float homingRange = 512f;
        public static readonly int moveSpeed = 8;
        public static readonly float correctionFactor = 0.30f;

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
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.usesIDStaticNPCImmunity = false;
            Projectile.idStaticNPCHitCooldown = -1;
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
                moveVector *= ((float)moveSpeed * correctionFactor);
                Projectile.velocity += moveVector;
                Projectile.velocity.Normalize();
                Projectile.velocity *= (float)moveSpeed;
            }
            Projectile.spriteDirection = Projectile.direction; //Fix wrong shading when shooting to the left.
        }
    }
}
