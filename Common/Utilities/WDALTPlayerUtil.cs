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
        public Item heldItem;

        public override void PreUpdate()
        {
            heldItem = this.Player.HeldItem;
            base.PreUpdate();
        }
        public override void Initialize()
        {
            lastLeechingHealTime = 0;
            player = this.Player;
            heldItem = this.Player.HeldItem;
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            lastLeechingHealTime = 0;
        }
        
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if
            (
                modifiers.DamageType == DamageClass.Magic
            )
            {
                if (heldItem.prefix == ModContent.PrefixType<Supercritical>())
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
            if
            (
                (
                    heldItem.prefix == ModContent.PrefixType<Leeching>() ||
                    heldItem.prefix == ModContent.PrefixType<Siphoning>() ||
                    heldItem.type   == ItemID.ChlorophytePartisan
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
                if(player.HasBuff(BuffID.MoonLeech) && heldItem.prefix == ModContent.PrefixType<Leeching>())
                {
                    healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.1);
                }
                // Chlorophyte Partisan go BRRRR!!!
                if(heldItem.type == ItemID.ChlorophytePartisan && heldItem.prefix == ModContent.PrefixType<Leeching>())
                {
                    healingAmount *= 2;
                }
                if(heldItem.prefix == ModContent.PrefixType<Leeching>() || heldItem.type == ItemID.ChlorophytePartisan)
                {
                    double timeSinceLastHeal = Math.Abs(Main.time - lastLeechingHealTime); // Use ABS to avoid negative time
                    if(timeSinceLastHeal >= player.itemAnimationMax) // Only heal player once every item use
                    {
                        player.Heal(healingAmount);
                        lastLeechingHealTime = Main.time;
                    }
                }
                else if(heldItem.prefix == ModContent.PrefixType<Siphoning>())
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
            if(heldItem.prefix == prefixID)
            {
                return true;
            }
            return false;
        }
    }
}
