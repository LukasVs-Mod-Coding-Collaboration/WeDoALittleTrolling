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
                    (
                        item.prefix == ModContent.PrefixType<Leeching>() ||
                        item.prefix == ModContent.PrefixType<Siphoning>()
                    ) &&
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
                        // Stop Sacling at ~320 Damage for Critical Hits
                        if(healingAmount > 16)
                        {
                            healingAmount = 16;
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
                    // Having Moon Bite means the effect still works, however,
                    // it will be 90% less effective
                    // 1 Base Heal is still guaranteed
                    if(player.HasBuff(BuffID.MoonLeech))
                    {
                        healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.1);
                    }
                    if(item.prefix == ModContent.PrefixType<Leeching>())
                    {
                        player.Heal(healingAmount);
                    }
                    else if(item.prefix == ModContent.PrefixType<Siphoning>())
                    {
                        if(player.statMana <= (player.statManaMax2 - healingAmount))
                        {
                            player.statMana += healingAmount;
                        }
                        player.ManaEffect(healingAmount);
                    }
                }
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
    }
}
