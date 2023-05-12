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
                if
                (
                    item.prefix == ModContent.PrefixType<Leeching>() &&
                    !target.friendly && 
                    !target.CountsAsACritter && 
                    !target.isLikeATownNPC && 
                    target.type != NPCID.TargetDummy
                )
                {
                    // 1 Base Heal + 5% of damage done
                    int healingAmount = 1 + (int)Math.Round(damageDone * 0.05);
                    if(hit.Crit)
                    {
                        // Stop Sacling at ~480 Damage for Critical Hits
                        if(healingAmount > 24)
                        {
                            healingAmount = 24;
                        }
                    }
                    else
                    {
                        // Stop Sacling at ~160 Damage for Normal Hits
                        if(healingAmount > 8)
                        {
                            healingAmount = 8;
                        }
                    }
                    // Having Moon Bite means the effect still works, hoever,
                    // it will be 75% less effective
                    // 1 Base Heal is still guaranteed
                    if(player.HasBuff(BuffID.MoonLeech))
                    {
                        healingAmount = (int)Math.Round(healingAmount * 0.25);
                    }
                    player.Heal(healingAmount);
                }
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
    }
}
