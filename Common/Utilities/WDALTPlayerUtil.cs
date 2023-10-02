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

namespace WeDoALittleTrolling.Common.Utilities
{
    internal class WDALTPlayerUtil : ModPlayer
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
            base.ResetEffects();
        }
        
        public override void PostUpdate()
        {
            GlobalItemList.ModifySetBonus(player);
            currentTick++;
            base.PostUpdate();
        }

        public override void ModifyLuck(ref float luck)
        {
            if(player.HasBuff<Gnomed>())
            {
                luck -= 1f;
            }
            base.ModifyLuck(ref luck);
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
                float modifierWR = (float)(90 - (wreckedResistanceStack * 10)) * 0.01f;
                player.endurance *= modifierWR;
            }
            else
            {
                wreckedResistanceStack = 0;
            }
            if(player.HasBuff(ModContent.BuffType<WreckedAccuracy>()))
            {
                float modifierWA = (float)(10 + (wreckedAccuracyStack * 10));
                player.GetCritChance(DamageClass.Generic) -= modifierWA;
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
            if(sorcerousMirror && player.HeldItem.DamageType == DamageClass.Magic)
            {
                player.aggro -= 400;
                player.statDefense += 4;
                player.lifeRegen += 4;
                player.GetCritChance(DamageClass.Generic) -= 4f;
                player.GetDamage(DamageClass.Summon) *= 0.96f;
                player.AddBuff(ModContent.BuffType<SorcerousMirrorBuff>(), 10, true);
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
            heartOfDespairDamageBonus = (player.statLifeMax2 - player.statLife) / 5;
            if(sandStepping)
            {
                player.maxRunSpeed += 2f;
            }
            base.PostUpdateEquips();
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if(damageSource.SourceNPCIndex > -1 && damageSource.SourceNPCIndex < Main.npc.Length) //Check if PlayerDeathEvent was triggered by a NPC
            {
                int[] LeFisheIDs =
                {
                    NPCID.FungoFish,
                    NPCID.Piranha,
                    NPCID.AnglerFish,
                    NPCID.DukeFishron,
                    NPCID.FlyingFish,
                    NPCID.CorruptGoldfish,
                    NPCID.CrimsonGoldfish,
                    NPCID.Arapaima
                };

                int[] BeeIDs =
                {
                    NPCID.Bee,
                    NPCID.BeeSmall,
                    NPCID.QueenBee
                };

                int[] BoCIDs =
                {
                    NPCID.BrainofCthulhu,
                    NPCID.Creeper
                };

                int[] BlubbyIDs =
                {
                    NPCID.Plantera,
                    NPCID.PlanterasHook,
                    NPCID.PlanterasTentacle
                };

                int[] SansIDs =
                {
                    NPCID.SkeletronHead,
                    NPCID.SkeletronHand,
                    NPCID.Skeleton,
                    NPCID.PrimeSaw,
                    NPCID.PrimeVice
                };

                if (LeFisheIDs.Contains(Main.npc[damageSource.SourceNPCIndex].type) && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " was tragically slain by le fishe.";
                }
                if (Main.npc[damageSource.SourceNPCIndex].type == NPCID.TheDestroyer && random.NextBool(4))
                {
                    damageSource.SourceCustomReason = player.name + " was eated.";
                }
                if (BeeIDs.Contains(Main.npc[damageSource.SourceNPCIndex].type) && random.NextBool(100))
                {
                    damageSource.SourceCustomReason = player.name + " does not prefer their bees roasted.";
                }
                if (Main.npc[damageSource.SourceNPCIndex].type == NPCID.Gnome && random.NextBool(3))
                {
                    damageSource.SourceCustomReason = player.name + " was gnomed.";
                }
                if (Main.npc[damageSource.SourceNPCIndex].type == NPCID.SkeletonSniper && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " discovered the meaning of stream sniping.";
                }
                if (BoCIDs.Contains(Main.npc[damageSource.SourceNPCIndex].type) && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " had a seizure and passed away.";
                }
                if (BlubbyIDs.Contains(Main.npc[damageSource.SourceNPCIndex].type) && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " tried to touch grass.";
                }
                if (SansIDs.Contains(Main.npc[damageSource.SourceNPCIndex].type) && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " had a bad time.";
                }
                if (Main.npc[damageSource.SourceNPCIndex].type == NPCID.SkeletronPrime && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " was forced to work at amazon.";
                }
            }
            if(damageSource.SourceProjectileType == ProjectileID.SniperBullet && random.NextBool(5))
            {
                damageSource.SourceCustomReason = player.name + " discovered the meaning of stream sniping.";
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        public override void PostUpdateBuffs()
        {
            base.PostUpdateBuffs();
        }

        //SmartPVP(TM) Technology: Display actual pvp damage to clients on non-lethal hits and sync health
        //Other relevent code is found in WDALTNetworkingSystem
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if(Main.netMode == NetmodeID.MultiplayerClient && modifiers.PvP)
            {
                if(player.whoAmI != Main.myPlayer)
                {
                    modifiers.SetMaxDamage(1);
                }
            }
            base.ModifyHurt(ref modifiers);
        }
        
        public override void PostHurt(Player.HurtInfo info)
        {
            if(Main.netMode == NetmodeID.MultiplayerClient && info.PvP)
            {
                if(player.whoAmI == Main.myPlayer)
                {
                    ModPacket syncNetFinalDamagePacket = Mod.GetPacket();
                    syncNetFinalDamagePacket.Write(WDALTPacketTypeID.syncNetFinalDamage);
                    syncNetFinalDamagePacket.Write(player.statLife);
                    syncNetFinalDamagePacket.Write(Main.myPlayer);
                    syncNetFinalDamagePacket.Send();
                }
            }
            base.OnHurt(info);
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if(random.Next(0, 4) == 0 && sorcerousMirror && player.HeldItem.DamageType == DamageClass.Magic && !player.HasBuff(ModContent.BuffType<Devastated>())) // 1 in 4 chance
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

        public override void PreUpdate()
        {
            if (chargeAccelerationTicks > 0)
            {
                player.maxFallSpeed *= 2.5f;
                chargeAccelerationTicks--;
            }
        }

        public override void OnRespawn()
        {
            player.Heal(999999);
            base.OnRespawn();
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            bool shortRespawn = true;
            for(int i = 0;i < Main.npc.Length; i++)
            {
                if
                (
                    Main.npc[i].active &&
                    (
                        Main.npc[i].boss ||
                        Main.npc[i].type == NPCID.EaterofWorldsHead ||
                        Main.npc[i].type == NPCID.EaterofWorldsBody ||
                        Main.npc[i].type == NPCID.EaterofWorldsTail
                    )
                )
                {
                    shortRespawn = false;
                }
            }
            if(shortRespawn)
            {
                player.respawnTimer = 180;
            }
            base.Kill(damage, hitDirection, pvp, damageSource);
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
                        modifiers.CritDamage += 1.5f;
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
                (
                    proj.type == ProjectileID.TerrarianBeam ||
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
                    int dmg = (int)Math.Round(proj.damage * 0.33);
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
                    long ticksSinceLastHeal = Math.Abs(currentTick - lastLeechingHealTick);
                    if(ticksSinceLastHeal >= ((double)player.itemAnimationMax/2.0)) // Only heal player 2 times every item use
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
