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
            int[] NerfGroup45Percent =
            {
                ProjectileID.UnholyTridentHostile
            };
            if(NerfGroup25Percent.Contains(projectile.type))
            {
                modifiers.SourceDamage *= (float)0.75;
            }
            if(NerfGroup45Percent.Contains(projectile.type))
            {
                modifiers.SourceDamage *= (float)0.55;
            }
            base.ModifyHitPlayer(projectile, target, ref modifiers);
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            //Implement Spooky Emblem and Supercritical Prefix
            if(projectile.TryGetOwner(out Player player) && !target.isLikeATownNPC)
            {
                //Supercritical Prefix
                if
                (
                    modifiers.DamageType == DamageClass.Magic
                )
                {
                    if (WeDoALittleTrolling.isPlayerHoldingItemWithPrefix(player, ModContent.PrefixType<Supercritical>()))
                    {
                        modifiers.CritDamage *= (float)2.0;
                    }
                }
                //Spooky Emblem
                if
                (
                    modifiers.DamageType == DamageClass.Summon ||
                    modifiers.DamageType == DamageClass.SummonMeleeSpeed ||
                    modifiers.DamageType == DamageClass.MagicSummonHybrid ||
                    projectile.type == ProjectileID.SandnadoFriendly
                )
                {
                    if(WeDoALittleTrolling.hasPlayerAcessoryEquipped(player, ModContent.ItemType<SpookyEmblem>()))
                    {
                        modifiers.ArmorPenetration += (2 * (int)Math.Round(player.slotsMinions));
                        Random random = new Random();
                        if(random.Next(0, 100) < 32) //32% Chance
                        {
                            modifiers.SetCrit();
                        }
                    }
                }
            }
            base.ModifyHitNPC(projectile, target, ref modifiers);
        }

        //Implement Leeching and Siphoning Prefixes
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(projectile.TryGetOwner(out Player player))
            {
                if
                (
                    (
                        WeDoALittleTrolling.isPlayerHoldingItemWithPrefix(player, ModContent.PrefixType<Leeching>()) ||
                        WeDoALittleTrolling.isPlayerHoldingItemWithPrefix(player, ModContent.PrefixType<Siphoning>())
                    ) &&
                    (
                        hit.DamageType == DamageClass.Melee ||
                        hit.DamageType == DamageClass.MeleeNoSpeed ||
                        hit.DamageType == DamageClass.SummonMeleeSpeed ||
                        hit.DamageType == DamageClass.Magic
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
                    if
                    (
                        player.HasBuff(BuffID.MoonLeech) &&
                        WeDoALittleTrolling.isPlayerHoldingItemWithPrefix(player, ModContent.PrefixType<Leeching>())
                    )
                    {
                        healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.1);
                    }
                    // Siphoning should always be
                    // 75% less effective than Leeching
                    // 1 Base Heal is still guaranteed
                    if(WeDoALittleTrolling.isPlayerHoldingItemWithPrefix(player, ModContent.PrefixType<Siphoning>()))
                    {
                        healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.25);
                    }
                    if(WeDoALittleTrolling.isPlayerHoldingItemWithPrefix(player, ModContent.PrefixType<Leeching>()))
                    {
                        player.Heal(healingAmount);
                    }
                    else if(WeDoALittleTrolling.isPlayerHoldingItemWithPrefix(player, ModContent.PrefixType<Siphoning>()))
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
