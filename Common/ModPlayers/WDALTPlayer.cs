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
using Terraria.Utilities;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Items.Accessories;
using SteelSeries.GameSense;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.NPCs;

namespace WeDoALittleTrolling.Common.ModPlayers
{
    internal class WDALTPlayer : ModPlayer
    {
        public long lastLeechingHealTick;
        public int spookyBonus2X;
        public int spookyBonus3X;
        public int dodgeChancePercent;
        public int wreckedResistanceStack;
        public int wreckedAccuracyStack;
        public int devastatedStack;
        public int beekeeperStack;
        public bool spookyEmblem;
        public bool sorcerousMirror;
        public bool heartOfDespair;
        public int heartOfDespairDamageBonus;
        public bool soulPoweredShield;
        public bool searingSetBonus;
        public int searingSetBonusValue;
        public bool sandStepping;
        public bool gnomedStonedDebuff;
        public bool yoyoArtifact;
        public Player player;
        public long currentTick;
        public int chargeAccelerationTicks;
        public bool zoneWormCandle;
        public bool shroomiteGenesis;
        public int shroomiteGenesisOverheatTicks;
        public int shroomiteGenesisOverchannelTicks;
        public int shroomiteGenesisOverheatTimer;
        public static UnifiedRandom random = new UnifiedRandom();
        
        public override void Initialize()
        {
            player = this.Player;
            lastLeechingHealTick = 0;
            spookyBonus2X = 0;
            spookyBonus3X = 0;
            dodgeChancePercent = 0;
            wreckedResistanceStack = 0;
            wreckedAccuracyStack = 0;
            devastatedStack = 0;
            beekeeperStack = 0;
            spookyEmblem = false;
            sorcerousMirror = false;
            heartOfDespair = false;
            heartOfDespairDamageBonus = 0;
            soulPoweredShield = false;
            searingSetBonus = false;
            searingSetBonusValue = 0;
            sandStepping = false;
            gnomedStonedDebuff = false;
            yoyoArtifact = false;
            currentTick = 0;
            chargeAccelerationTicks = 0;
            zoneWormCandle = false;
            shroomiteGenesis = false;
            shroomiteGenesisOverheatTicks = 0;
            shroomiteGenesisOverchannelTicks = 0;
            shroomiteGenesisOverheatTimer = 0;
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            spookyBonus2X = 0;
            spookyBonus3X = 0;
            dodgeChancePercent = 0;
            wreckedResistanceStack = 0;
            wreckedAccuracyStack = 0;
            devastatedStack = 0;
            beekeeperStack = 0;
            spookyEmblem = false;
            sorcerousMirror = false;
            heartOfDespair = false;
            heartOfDespairDamageBonus = 0;
            soulPoweredShield = false;
            searingSetBonus = false;
            searingSetBonusValue = 0;
            sandStepping = false;
            gnomedStonedDebuff = false;
            yoyoArtifact = false;
            chargeAccelerationTicks = 0;
            zoneWormCandle = false;
            shroomiteGenesis = false;
            shroomiteGenesisOverheatTicks = 0;
            shroomiteGenesisOverchannelTicks = 0;
            shroomiteGenesisOverheatTimer = 0;
        }

        public override void ResetEffects()
        {
            dodgeChancePercent = 0;
            beekeeperStack = 0;
            spookyEmblem = false;
            sorcerousMirror = false;
            heartOfDespair = false;
            soulPoweredShield = false;
            searingSetBonus = false;
            sandStepping = false;
            gnomedStonedDebuff = false;
            yoyoArtifact = false;
            shroomiteGenesis = false;
            base.ResetEffects();
        }
        
        public override void PostUpdate()
        {
            GlobalItemList.ModifySetBonus(player);
            currentTick++;
            if (shroomiteGenesisOverheatTimer > 0)
            {
                shroomiteGenesisOverheatTimer--;
            }
            if (shroomiteGenesis && player.channel && player.HeldItem.DamageType == DamageClass.Ranged)
            {
                shroomiteGenesisOverchannelTicks++;
                if (shroomiteGenesisOverchannelTicks >= 1200)
                {
                    CombatText.NewText
                    (
                        new Rectangle
                        (
                            (int)player.position.X,
                            (int)player.position.Y,
                            player.width,
                            player.height
                        ),
                        new Color(255, 0, 0),
                        "Overheated!"
                    );
                    shroomiteGenesisOverheatTimer = 180;
                    shroomiteGenesisOverchannelTicks = 0;
                    player.channel = false;
                }
            }
            base.PostUpdate();
        }

        public override void PreUpdateBuffs()
        {
            if(player.whoAmI == Main.myPlayer)
            {
                if(WDALTSceneMetrics.HasWormCandle)
                {
                    zoneWormCandle = true;
                    player.AddBuff(ModContent.BuffType<WormCandleBuff>(), 2);
                }
                else
                {
                    zoneWormCandle = false;
                }
            }
        }

        public override void ModifyLuck(ref float luck)
        {
            if(player.HasBuff<Gnomed>())
            {
                luck -= 1f;
            }
            base.ModifyLuck(ref luck);
        }

        public override void PostUpdateEquips()
        {
            if(sandStepping)
            {
                player.maxRunSpeed += 2f;
            }
            if(sorcerousMirror && player.HeldItem.DamageType == DamageClass.Magic)
            {
                player.aggro -= 400;
                player.statDefense += 4;
                player.lifeRegen += 4;
                player.GetDamage(DamageClass.Generic) *= 0.84f;
                player.AddBuff(ModContent.BuffType<SorcerousMirrorBuff>(), 2, true);
            }
            else
            {
                player.ClearBuff(ModContent.BuffType<SorcerousMirrorBuff>());
            }
            if(searingSetBonus)
            {
                searingSetBonusValue = ((int)player.statDefense)/(int)4;
                float modifierSAR = (1f + (searingSetBonusValue * 0.01f));
                player.DefenseEffectiveness *= modifierSAR;
                player.GetDamage(DamageClass.Generic) *= modifierSAR;
            }
            if(player.HasBuff(ModContent.BuffType<WreckedResistance>()))
            {
                float modifierWR = (float)(90 - (wreckedResistanceStack * 10)) * 0.01f;
                player.endurance *= modifierWR;
            }
            else
            {
                wreckedResistanceStack = 0;
            }
            if(player.HasBuff(ModContent.BuffType<WreckedAccuracy>()))
            {
                float modifierWA = (float)(90 - (wreckedAccuracyStack * 10)) * 0.01f;
                player.GetCritChance(DamageClass.Generic) *= modifierWA;
            }
            else
            {
                wreckedAccuracyStack = 0;
            }
            if(player.HasBuff(ModContent.BuffType<Devastated>()))
            {
                float modifierD = (float)(90 - (devastatedStack*10)) * 0.01f;
                player.statLifeMax2 = (int)Math.Round(player.statLifeMax2*modifierD);
                player.DefenseEffectiveness *= modifierD;
                player.blackBelt = false;
                player.brainOfConfusionItem = null;
                dodgeChancePercent = 0;
            }
            else
            {
                devastatedStack = 0;
            }
            if(gnomedStonedDebuff)
            {
                if((Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.MultiplayerClient) && player.whoAmI == Main.myPlayer)
                {
                    Main.buffNoTimeDisplay[BuffID.Stoned] = true;
                }
                if(!player.buffImmune[BuffID.Stoned])
                {
                    if(player.HasBuff(BuffID.Stoned))
                    {
                        int stonedIndex = player.FindBuffIndex(BuffID.Stoned);
                        if(stonedIndex > -1 && stonedIndex < player.buffTime.Length)
                        {
                            player.buffTime[stonedIndex] = 10;
                        }
                    }
                    else
                    {
                        player.AddBuff(BuffID.Stoned, 10, true);
                    }
                }
            }
            else
            {
                if((Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.MultiplayerClient) && player.whoAmI == Main.myPlayer)
                {
                    Main.buffNoTimeDisplay[BuffID.Stoned] = false;
                }
            }
            spookyBonus2X = player.maxMinions * 2;
            spookyBonus3X = player.maxMinions * 3;
            heartOfDespairDamageBonus = (player.statLifeMax2 - player.statLife) / 5;
            base.PostUpdateEquips();
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if(random.NextBool(4) && sorcerousMirror && player.HeldItem.DamageType == DamageClass.Magic && !player.HasBuff(ModContent.BuffType<Devastated>())) // 1 in 4 chance
            {
                player.SetImmuneTimeForAllTypes(player.longInvince ? 120 : 80);
                return true;
            }
            if(random.Next(0, 100) < dodgeChancePercent && dodgeChancePercent > 0 && !player.HasBuff(ModContent.BuffType<Devastated>()))
            {
                player.SetImmuneTimeForAllTypes(player.longInvince ? 120 : 80);
                return true;
            }
            return base.FreeDodge(info);
        }

        public override void PreUpdate()
        {
            if (chargeAccelerationTicks > 0)
            {
                player.maxFallSpeed *= 3.5f;
                chargeAccelerationTicks--;
            }
        }

        public override void UpdateLifeRegen()
        {
            if(player.HasItem(ModContent.ItemType<HolyCharm>()) || searingSetBonus || (NPC.downedGolemBoss && soulPoweredShield))
            {
                player.buffImmune[ModContent.BuffType<SearingInferno>()] = true;
            }
            else
            {
                player.buffImmune[ModContent.BuffType<SearingInferno>()] = false;
            }
            player.buffImmune[ModContent.BuffType<WreckedResistance>()] = false;
            player.buffImmune[ModContent.BuffType<WreckedAccuracy>()] = false;
            player.buffImmune[ModContent.BuffType<Devastated>()] = false;
            base.UpdateLifeRegen();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if(player.HasBuff(ModContent.BuffType<SearingInferno>()))
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
                        modifiers.CritDamage += 2.5f;
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

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (beekeeperStack > 0)
            {
                spawnBees(target);
            }
            base.OnHitNPCWithItem(item, target, hit, damageDone);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (proj.type != ProjectileID.Bee && proj.type != ProjectileID.GiantBee && beekeeperStack > 0)
            {
                spawnBees(target);
            }
            if
            (
                yoyoArtifact &&
                target.CanBeChasedBy() &&
                (
                    proj.aiStyle == ProjAIStyleID.TerrarianBeam ||
                    proj.aiStyle == ProjAIStyleID.Yoyo
                )
            )
            {
                for (int i = 0; i < 4; i++)
                {
                    int rMax = (int)proj.width;
                    double r = rMax * Math.Sqrt(random.NextDouble());
                    double angle = random.NextDouble() * 2 * Math.PI;
                    int xOffset = (int)Math.Round(r * Math.Cos(angle));
                    int yOffset = (int)Math.Round(r * Math.Sin(angle));
                    Vector2 projPosition = proj.Center;
                    projPosition.X += xOffset;
                    projPosition.Y += yOffset;
                    Vector2 projVelocity = new Vector2((random.NextFloat() - 0.5f), (random.NextFloat() - 0.5f));
                    projVelocity.Normalize();
                    projVelocity *= 12f;
                    int dmg = (int)Math.Round(proj.damage * 0.15);
                    Projectile.NewProjectileDirect(proj.GetSource_OnHit(target), projPosition, projVelocity, ModContent.ProjectileType<MagicArtifact>(), dmg, proj.knockBack, proj.owner);
                }
            }
            base.OnHitNPCWithProj(proj, target, hit, damageDone);
        }

        public void spawnBees(NPC target)
        {
            int beeType = ProjectileID.Bee;
            int beeDamage = 10;

            for (int j = 0; j < beekeeperStack; j++)
            {

                if (random.NextBool(21) && player.strongBees)
                {
                    beeType = ProjectileID.GiantBee;
                    beeDamage = 20;
                }

                Projectile beekeeperStackBee = Projectile.NewProjectileDirect
                    (
                    player.GetSource_FromThis(),
                    new Vector2(target.Center.X, target.Center.Y),
                    new Vector2(random.NextFloat(-1f, 1f), random.NextFloat(-1f, 1f)),
                    beeType,
                    2 * beekeeperStack + beeDamage,
                    0,
                    player.whoAmI
                    );

                beekeeperStackBee.timeLeft = 300;
                beekeeperStackBee.penetrate = -1;
            }
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (shroomiteGenesis && item.DamageType == DamageClass.Ranged)
            {
                shroomiteGenesisOverheatTicks++;
                int limit = 24;
                if (item.useAnimation > 0 && !item.channel)
                {
                    limit = item.useAnimation;
                }
                if (shroomiteGenesisOverheatTicks >= 60 * (float)((float)24 / (float)limit))
                {
                    CombatText.NewText
                    (
                        new Rectangle
                        (
                            (int)player.position.X,
                            (int)player.position.Y,
                            player.width,
                            player.height
                        ),
                        new Color(255, 0, 0),
                        "Overheated!"
                    );
                    shroomiteGenesisOverheatTimer = 180;
                    shroomiteGenesisOverheatTicks = 0;
                    return false;
                }
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }

        public override bool CanUseItem(Item item)
        {
            if (item.DamageType == DamageClass.Ranged && shroomiteGenesisOverheatTimer > 0)
            {
                return false;
            }
            return base.CanUseItem(item);
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
                target.canGhostHeal &&
                target.CanBeChasedBy()
            )
            {
                // 1 Base Heal + 5% of damage done
                int healingAmount = 1 + (int)Math.Round(damageDone * 0.05);
                // Stop Sacling at ~160 Damage
                if(healingAmount > 8)
                {
                    healingAmount = 8;
                }
                // Having Moon Bite means the effect still works, however,
                // it will be 90% less effective
                // 1 Base Heal is still guaranteed
                if(player.HasBuff(BuffID.MoonLeech) && (player.HeldItem.prefix == ModContent.PrefixType<Leeching>() || player.HeldItem.type == ItemID.ChlorophytePartisan))
                {
                    healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.1);
                }
                // Chlorophyte Partisan go BRRRR!!!
                if(player.HeldItem.type == ItemID.ChlorophytePartisan && player.HeldItem.prefix == ModContent.PrefixType<Leeching>())
                {
                    healingAmount *= 2;
                }
                if(player.HeldItem.prefix == ModContent.PrefixType<Leeching>() || player.HeldItem.type == ItemID.ChlorophytePartisan)
                {
                    long ticksSinceLastHeal = Math.Abs(currentTick - lastLeechingHealTick);
                    if(ticksSinceLastHeal >= player.itemAnimationMax) // Only heal player one time every item use
                    {
                        player.Heal(healingAmount);
                        lastLeechingHealTick = currentTick;
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

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (shroomiteGenesis)
            {
                return false;
            }
            else
            {
                return base.CanConsumeAmmo(weapon, ammo);
            }

        }
    }
}
