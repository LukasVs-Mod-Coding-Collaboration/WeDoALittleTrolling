using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Prefixes;

namespace WeDoALittleTrolling.Content.Projectiles
{
    internal class GlobalProjectiles : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(projectile.TryGetOwner(out Player player))
            {
                Item item = player.HeldItem;
                if(item.prefix == ModContent.PrefixType<Leeching>() && !target.isLikeATownNPC)
                {
                  int healingAmount = (int)Math.Round(damageDone * 0.05);
                  player.Heal(healingAmount);
                }
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
    }
}
