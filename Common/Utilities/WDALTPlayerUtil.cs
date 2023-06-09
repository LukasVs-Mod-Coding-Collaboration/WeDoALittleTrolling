using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;

namespace WeDoALittleTrolling.Common.Utilities
{
    public class WDALTPlayerUtil : ModPlayer
    {
        public double lastLeechingHealTime;
        public Player player;

        public override void Initialize()
        {
            lastLeechingHealTime = 0;
            player = this.Player;
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            lastLeechingHealTime = 0;
        }

        public override bool CanUseItem(Item item)
        {
            // Anti-Poo-Block-Mechanism
            if(item.type == ItemID.PoopBlock)
            {
                PlayerDeathReason reason = new PlayerDeathReason();
                reason.SourceCustomReason = player.name + " tried to uglify the world.";
                player.KillMe(reason, 99999999999999, 0, false);
                return false;
            }
            // Anti-Landmine-Mechanism
            else if(item.type == ItemID.LandMine)
            {
                PlayerDeathReason reason = new PlayerDeathReason();
                reason.SourceCustomReason = player.name + " tried to teamtroll and had it backfire.";
                player.KillMe(reason, 99999999999999, 0, false);
                return false;
            }
            // Anti-Zapinator-Mechanism
            else if
            (
                item.type == ItemID.ZapinatorOrange ||
                item.type == ItemID.ZapinatorGray
            )
            {
                PlayerDeathReason reason = new PlayerDeathReason();
                reason.SourceCustomReason = player.name + " tried to use a bugged item.";
                player.KillMe(reason, 99999999999999, 0, false);
                return false;
            }
            return base.CanUseItem(item);
        }
        
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Item item = player.HeldItem;
            if
            (
                modifiers.DamageType == DamageClass.Magic
            )
            {
                if (item.prefix == ModContent.PrefixType<Supercritical>())
                {
                    modifiers.CritDamage *= 2.0f;
                }
            }
            if
            (
                modifiers.DamageType == DamageClass.Summon ||
                modifiers.DamageType == DamageClass.SummonMeleeSpeed ||
                modifiers.DamageType == DamageClass.MagicSummonHybrid
            )
            {
                if(hasPlayerAcessoryEquipped(ModContent.ItemType<SpookyEmblem>()))
                {
                    modifiers.ArmorPenetration += (3 * player.maxMinions);
                    Random random = new Random();
                    if(random.Next(0, 100) < (3 * player.maxMinions)) //(3 x <Player Minion Slots>)% Chance
                    {
                        modifiers.SetCrit();
                    }
                }
            }
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Item item = player.HeldItem;
            if
            (
                (
                    item.prefix == ModContent.PrefixType<Leeching>() ||
                    item.prefix == ModContent.PrefixType<Siphoning>()
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
                target.type != NPCID.TargetDummy &&
                target.canGhostHeal
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
                if(player.HasBuff(BuffID.MoonLeech) && item.prefix == ModContent.PrefixType<Leeching>())
                {
                    healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.1);
                }
                // Siphoning should always be
                // 75% less effective than Leeching
                // 1 Base Heal is still guaranteed
                if(item.prefix == ModContent.PrefixType<Siphoning>())
                {
                    healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.25);
                }
                if(item.prefix == ModContent.PrefixType<Leeching>())
                {
                    double timeSinceLastHeal = Math.Abs(Main.time - lastLeechingHealTime); // Use ABS to avoid negative time
                    if(timeSinceLastHeal >= player.itemAnimationMax) // Only heal player once every item use
                    {
                        player.Heal(healingAmount);
                        lastLeechingHealTime = Main.time;
                    }
                }
                else if(item.prefix == ModContent.PrefixType<Siphoning>())
                {
                    double timeSinceLastHeal = Math.Abs(Main.time - lastLeechingHealTime); // Use ABS to avoid negative time
                    if(timeSinceLastHeal >= player.itemAnimationMax) // Only give mana to player once every item use
                    {
                        if(player.statMana <= (player.statManaMax2 - healingAmount))
                        {
                            player.statMana += healingAmount;
                        }
                        player.ManaEffect(healingAmount);
                        lastLeechingHealTime = Main.time;
                    }
                }
            }
            base.OnHitNPC(target, hit, damageDone);
        }

        public bool hasPlayerAcessoryEquipped(int itemID)
        {
            int offset = 3;
            int loopLimit = 5;
            loopLimit += player.extraAccessorySlots;
            if(Main.masterMode)
            {
                loopLimit++;
            }
            for(int i = offset;i < (offset + loopLimit); i++) //Search through all accessory slots
            {
                if(player.armor[i].type == itemID)
                {
                    return true;
                }
            }
            return false;
        }

        public int getAmountOfEquippedAccessoriesWithPrefixFromPlayer(int prefixID) //Fancy name much smart, haha :P
        {
            int equippedAmount = 0;
            int offset = 3;
            int loopLimit = 5;
            loopLimit += player.extraAccessorySlots;
            if(Main.masterMode)
            {
                loopLimit++;
            }
            for(int i = offset;i < (offset + loopLimit); i++) //Search through all accessory slots
            {
                if(player.armor[i].prefix == prefixID)
                {
                    equippedAmount++;
                }
            }
            return equippedAmount;
        }

        public bool hasPlayerHelmetEquipped(int itemID)
        {
            int offset = 0;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public bool hasPlayerChestplateEquipped(int itemID)
        {
            int offset = 1;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public bool hasPlayerLeggingsEquipped(int itemID)
        {
            int offset = 2;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public bool isPlayerHoldingItemWithPrefix(int prefixID)
        {
            Item item = player.HeldItem;
            if(item.prefix == prefixID)
            {
                return true;
            }
            return false;
        }
    }
}
