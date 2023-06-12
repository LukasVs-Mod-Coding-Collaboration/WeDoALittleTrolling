using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;

namespace WeDoALittleTrolling.Content.Projectiles
{
    internal class GlobalProjectiles : GlobalProjectile
    {
        
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            int[] NerfGroup25Percent =
            {
                ProjectileID.SandBallFalling,
                ProjectileID.Stinger
            };
            int[] NerfGroup50Percent =
            {
                ProjectileID.UnholyTridentHostile
            };
            if(NerfGroup25Percent.Contains(projectile.type))
            {
                modifiers.SourceDamage *= (float)0.75;
            }
            if(NerfGroup50Percent.Contains(projectile.type))
            {
                modifiers.SourceDamage *= (float)0.5;
            }
            base.ModifyHitPlayer(projectile, target, ref modifiers);
        }
    }
}
