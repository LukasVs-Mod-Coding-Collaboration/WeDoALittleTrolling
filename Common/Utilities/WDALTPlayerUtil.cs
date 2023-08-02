/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2023 LukasV-Coding

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Items.Accessories;

namespace WeDoALittleTrolling.Common.Utilities
{
    public class WDALTPlayerUtil : ModPlayer
    {
        public double lastLeechingHealTime;
        public int spookyBonus2X;
        public int spookyBonus3X;
        public int dodgeChancePercent;
        public int wreckedResistanceStack;
        public int devastatedStack;
        public bool spookyEmblem;
        public bool sorcerousMirror;
        public bool heartOfDespair;
        public Player player;
        public Random random = new Random();
        
        public override void Initialize()
        {
            player = this.Player;
            lastLeechingHealTime = 0.0;
            spookyBonus2X = 0;
            spookyBonus3X = 0;
            dodgeChancePercent = 0;
            wreckedResistanceStack = 0;
            devastatedStack = 0;
            spookyEmblem = false;
            sorcerousMirror = false;
            heartOfDespair = false;
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            lastLeechingHealTime = 0.0;
            spookyBonus2X = 0;
            spookyBonus3X = 0;
            dodgeChancePercent = 0;
            wreckedResistanceStack = 0;
            devastatedStack = 0;
            spookyEmblem = false;
            sorcerousMirror = false;
            heartOfDespair = false;
        }

        public override void ResetEffects()
        {
            dodgeChancePercent = 0;
            spookyEmblem = false;
            sorcerousMirror = false;
            heartOfDespair = false;
            base.ResetEffects();
        }
        
        public override void PostUpdate()
        {
            GlobalItemList.ModifySetBonus(player);
            base.PostUpdate();
        }

        public override void UpdateEquips()
        {
            player.arcticDivingGear = true;
            base.UpdateEquips();
        }

        public override void PostUpdateEquips()
        {
            spookyBonus2X = player.maxMinions * 2;
            spookyBonus3X = player.maxMinions * 3;
            if(player.HasBuff(ModContent.BuffType<WreckedResistance>()))
            {
                float modifierWR = (float)(9 - (wreckedResistanceStack)) * 0.1f;
                player.endurance *= modifierWR;
            }
            else
            {
                wreckedResistanceStack = 0;
            }
            if(player.HasBuff(ModContent.BuffType<Devastated>()))
            {
                float modifierD = (float)(97 - (devastatedStack*3)) * 0.01f;
                player.statLifeMax2 = (int)Math.Round(player.statLifeMax2*modifierD);
                player.blackBelt = false;
                player.brainOfConfusionItem = null;
                dodgeChancePercent = 0;
            }
            else
            {
                devastatedStack = 0;
            }
            if(sorcerousMirror && player.HeldItem.DamageType == DamageClass.Magic)
            {
                player.aggro -= 400;
                player.statDefense += 12;
                player.lifeRegen += 6;
                player.GetCritChance(DamageClass.Generic) -= 12f;
                player.GetDamage(DamageClass.Summon) *= 0.88f;
                player.AddBuff(ModContent.BuffType<SorcerousMirrorBuff>(), 10, true);
            }
            else
            {
                player.ClearBuff(ModContent.BuffType<SorcerousMirrorBuff>());
            }
            base.PostUpdateEquips();
        }

        public override void PostUpdateBuffs()
        {
            base.PostUpdateBuffs();
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if(random.Next(0, 3) == 0 && sorcerousMirror && player.HeldItem.DamageType == DamageClass.Magic && !player.HasBuff(ModContent.BuffType<Devastated>())) // 1 in 3 chance
            {
                player.SetImmuneTimeForAllTypes(player.longInvince ? 120 : 80);
                return true;
            }
            if(random.Next(0, 100) < dodgeChancePercent && dodgeChancePercent > 0)
            {
                player.SetImmuneTimeForAllTypes(player.longInvince ? 120 : 80);
                return true;
            }
            return base.FreeDodge(info);
        }

        public override void UpdateLifeRegen()
        {
            if(player.HasItem(ModContent.ItemType<HolyCharm>()))
            {
                player.buffImmune[ModContent.BuffType<SearingInferno>()] = true;
            }
            else
            {
                player.buffImmune[ModContent.BuffType<SearingInferno>()] = false;
            }
            player.buffImmune[ModContent.BuffType<WreckedResistance>()] = false;
            base.UpdateLifeRegen();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if(target.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                modifiers.SourceDamage *= (1.0f - SearingInferno.damageNerfMultiplier);
            }
            if
            (
                modifiers.DamageType == DamageClass.Magic ||
                modifiers.DamageType == DamageClass.MagicSummonHybrid ||
                modifiers.DamageType == DamageClass.Ranged
            )
            {
                if (player.HeldItem.prefix == ModContent.PrefixType<Supercritical>())
                {
                    if(Supercritical.BuffGroup.Contains(player.HeldItem.type))
                    {
                        modifiers.CritDamage += 3.0f;
                    }
                    else
                    {
                        modifiers.CritDamage += 2.0f;
                    }
                }
            }
            if
            (
                modifiers.DamageType == DamageClass.Summon ||
                modifiers.DamageType == DamageClass.SummonMeleeSpeed ||
                modifiers.DamageType == DamageClass.MagicSummonHybrid
            )
            {
                if(spookyEmblem)
                {
                    modifiers.ArmorPenetration += spookyBonus3X;
                    if(random.Next(0, 100) < spookyBonus3X) //(3 x <Player Minion Slots>)% Chance
                    {
                        modifiers.SetCrit();
                    }
                }
            }
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if
            (
                (
                    player.HeldItem.prefix == ModContent.PrefixType<Leeching>() ||
                    player.HeldItem.prefix == ModContent.PrefixType<Siphoning>() ||
                    player.HeldItem.type   == ItemID.ChlorophytePartisan
                ) &&
                (
                    hit.DamageType == DamageClass.Melee ||
                    hit.DamageType == DamageClass.MeleeNoSpeed ||
                    hit.DamageType == DamageClass.SummonMeleeSpeed ||
                    hit.DamageType == DamageClass.Magic ||
                    hit.DamageType == DamageClass.MagicSummonHybrid
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
                // it will be 75% less effective
                // 1 Base Heal is still guaranteed
                if(player.HasBuff(BuffID.MoonLeech) && (player.HeldItem.prefix == ModContent.PrefixType<Leeching>() || player.HeldItem.type == ItemID.ChlorophytePartisan))
                {
                    healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.25);
                }
                // Chlorophyte Partisan go BRRRR!!!
                if(player.HeldItem.type == ItemID.ChlorophytePartisan && player.HeldItem.prefix == ModContent.PrefixType<Leeching>())
                {
                    healingAmount *= 2;
                }
                if(player.HeldItem.prefix == ModContent.PrefixType<Leeching>() || player.HeldItem.type == ItemID.ChlorophytePartisan)
                {
                    double timeSinceLastHeal = Math.Abs(Main.time - lastLeechingHealTime); // Use ABS to avoid negative time
                    if(timeSinceLastHeal >= ((float)player.itemAnimationMax/3.0f)) // Only heal player 3 times every item use
                    {
                        player.Heal(healingAmount);
                        lastLeechingHealTime = Main.time;
                    }
                }
                else if(player.HeldItem.prefix == ModContent.PrefixType<Siphoning>())
                {
                    if(player.statMana <= (player.statManaMax2 - healingAmount))
                    {
                        player.statMana += healingAmount;
                    }
                    player.ManaEffect(healingAmount);
                }
            }
            base.OnHitNPC(target, hit, damageDone);
        }

        public bool HasPlayerAcessoryEquipped(int itemID)
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

        public int GetAmountOfEquippedAccessoriesWithPrefixFromPlayer(int prefixID) //Fancy name much smart, haha :P
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

        public bool HasPlayerHelmetEquipped(int itemID)
        {
            int offset = 0;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public bool HasPlayerChestplateEquipped(int itemID)
        {
            int offset = 1;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public bool HasPlayerLeggingsEquipped(int itemID)
        {
            int offset = 2;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }
    }
}
