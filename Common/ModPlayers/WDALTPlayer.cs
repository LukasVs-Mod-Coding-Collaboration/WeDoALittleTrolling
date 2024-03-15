/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2024 LukasV-Coding

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
using WeDoALittleTrolling.Common.Utilities;
using Terraria.ModLoader.IO;
using Terraria.GameInput;

namespace WeDoALittleTrolling.Common.ModPlayers
{
    internal class WDALTPlayer : ModPlayer
    {
        public long lastLeechingHealTick;
        public int spookyBonus;
        public int dodgeChancePercent;
        public int wreckedResistanceStack;
        public int wreckedAccuracyStack;
        public int devastatedStack;
        public int beekeeperStack;
        public bool spookyEmblem;
        public bool spookyShield;
        public bool sorcerousMirror;
        public bool heartOfDespair;
        public int heartOfDespairDamageBonus;
        public bool soulPoweredShield;
        public bool searingSetBonus;
        public int searingSetBonusValue;
        public bool sandStepping;
        public bool gnomedStonedDebuff;
        public bool gnomedDebuff;
        public int gnomedDebuffTicksLeft;
        public bool hauntedDebuff;
        public int hauntedDebuffTicksLeft;
        public bool yoyoArtifact;
        public Player player;
        public long currentTick;
        public int chargeAccelerationTicks;
        public bool zoneWormCandle;
        public bool shroomiteGenesis;
        public int shroomiteGenesisOverheatTicks;
        public int shroomiteGenesisOverchannelTicks;
        public int shroomiteGenesisOverheatTimer;
        public bool lumintePhantomMinion;
        public bool frozenElementalMinion;
        public int unionMirrorTicks;
        public int weightedStack;
        public int acceleratedStack;
        public bool lifeforceEngineActivated;
        public int lifeforceEngineTicks;
        public int lifeforceEngineCooldown;
        public bool hasLifeforceEngine;
        public bool cornEmblem;
        public static UnifiedRandom random = new UnifiedRandom();

        public override void Initialize()
        {
            player = this.Player;
            lastLeechingHealTick = 0;
            spookyBonus = 0;
            dodgeChancePercent = 0;
            wreckedResistanceStack = 0;
            wreckedAccuracyStack = 0;
            devastatedStack = 0;
            beekeeperStack = 0;
            spookyEmblem = false;
            spookyShield = false;
            sorcerousMirror = false;
            heartOfDespair = false;
            heartOfDespairDamageBonus = 0;
            soulPoweredShield = false;
            searingSetBonus = false;
            searingSetBonusValue = 0;
            sandStepping = false;
            gnomedStonedDebuff = false;
            gnomedDebuff = false;
            gnomedDebuffTicksLeft = 0;
            hauntedDebuff = false;
            hauntedDebuffTicksLeft = 0;
            yoyoArtifact = false;
            currentTick = 0;
            chargeAccelerationTicks = 0;
            zoneWormCandle = false;
            shroomiteGenesis = false;
            shroomiteGenesisOverheatTicks = 0;
            shroomiteGenesisOverchannelTicks = 0;
            shroomiteGenesisOverheatTimer = 0;
            lumintePhantomMinion = false;
            frozenElementalMinion = false;
            unionMirrorTicks = 0;
            weightedStack = 0;
            acceleratedStack = 0;
            lifeforceEngineActivated = false;
            lifeforceEngineTicks = 0;
            lifeforceEngineCooldown = 0;
            hasLifeforceEngine = false;
            cornEmblem = false;
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("GnomedDebuff"))
            {
                gnomedDebuff = true;
                gnomedDebuffTicksLeft = tag.GetInt("GnomedDebuff");
            }
            if (tag.ContainsKey("HauntedDebuff"))
            {
                hauntedDebuff = true;
                hauntedDebuffTicksLeft = tag.GetInt("HauntedDebuff");
            }
            if (tag.ContainsKey("DevastatedStack"))
            {
                devastatedStack = tag.GetInt("DevastatedStack");
            }
            if (tag.ContainsKey("WreckedResistanceStack"))
            {
                wreckedResistanceStack = tag.GetInt("WreckedResistanceStack");
            }
            if (tag.ContainsKey("WreckedAccuracyStack"))
            {
                wreckedAccuracyStack = tag.GetInt("WreckedAccuracyStack");
            }
            base.LoadData(tag);
        }

        public override void SaveData(TagCompound tag)
        {
            if (gnomedDebuff)
            {
                tag["GnomedDebuff"] = gnomedDebuffTicksLeft;
            }
            if (hauntedDebuff)
            {
                tag["HauntedDebuff"] = hauntedDebuffTicksLeft;
            }
            if (devastatedStack > 0)
            {
                tag["DevastatedStack"] = devastatedStack;
            }
            if (wreckedResistanceStack > 0)
            {
                tag["WreckedResistanceStack"] = wreckedResistanceStack;
            }
            if (wreckedAccuracyStack > 0)
            {
                tag["WreckedAccuracyStack"] = wreckedAccuracyStack;
            }
            base.SaveData(tag);
        }

        private void ResetVariables()
        {
            spookyBonus = 0;
            dodgeChancePercent = 0;
            wreckedResistanceStack = 0;
            wreckedAccuracyStack = 0;
            devastatedStack = 0;
            beekeeperStack = 0;
            spookyEmblem = false;
            spookyShield = false;
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
            lumintePhantomMinion = false;
            frozenElementalMinion = false;
            unionMirrorTicks = 0;
            weightedStack = 0;
            acceleratedStack = 0;
            lifeforceEngineActivated = false;
            lifeforceEngineTicks = 0;
            lifeforceEngineCooldown = 0;
            hasLifeforceEngine = false;
            cornEmblem = false;
        }

        public override void ResetEffects()
        {
            dodgeChancePercent = 0;
            beekeeperStack = 0;
            spookyEmblem = false;
            spookyShield = false;
            sorcerousMirror = false;
            heartOfDespair = false;
            soulPoweredShield = false;
            searingSetBonus = false;
            sandStepping = false;
            gnomedStonedDebuff = false;
            yoyoArtifact = false;
            shroomiteGenesis = false;
            lumintePhantomMinion = false;
            frozenElementalMinion = false;
            weightedStack = 0;
            acceleratedStack = 0;
            hasLifeforceEngine = false;
            cornEmblem = false;
            base.ResetEffects();
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (WDALTKeybindSystem.LifeforceEngineKeybind.JustPressed)
            {
                lifeforceEngineActivated = true;
            }
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
                    shroomiteGenesisOverheatTimer = 120;
                    shroomiteGenesisOverchannelTicks = 0;
                    player.channel = false;
                }
            }
            if (unionMirrorTicks > 0)
            {
                if (unionMirrorTicks == 1)
                {
                    UnionMirror.TeleportHome(player);
                }
                unionMirrorTicks--;
            }
            if (lifeforceEngineTicks > 0)
            {
                lifeforceEngineTicks--;
            }
            if (lifeforceEngineCooldown > 0)
            {
                lifeforceEngineCooldown--;
            }
            base.PostUpdate();
        }

        public override void PreUpdateBuffs()
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (WDALTSceneMetrics.HasWormCandle)
                {
                    zoneWormCandle = true;
                    player.AddBuff(ModContent.BuffType<WormCandleBuff>(), 2);
                }
                else
                {
                    zoneWormCandle = false;
                }
                if (player.HeldItem.prefix == ModContent.PrefixType<MilitaryInfusion>())
                {
                    player.AddBuff(BuffID.Dangersense, 2, true);
                    player.AddBuff(BuffID.Hunter, 2, true);
                    player.AddBuff(BuffID.NightOwl, 2, true);
                }
                if (gnomedDebuff && !player.HasBuff(ModContent.BuffType<Gnomed>()))
                {
                    player.AddBuff(ModContent.BuffType<Gnomed>(), gnomedDebuffTicksLeft, true);
                }
                if (hauntedDebuff && !player.HasBuff(ModContent.BuffType<Haunted>()))
                {
                    player.AddBuff(ModContent.BuffType<Haunted>(), hauntedDebuffTicksLeft, true);
                }
            }
        }

        public override void ModifyLuck(ref float luck)
        {
            if (player.HasBuff<Gnomed>())
            {
                luck -= 5f;
            }
            base.ModifyLuck(ref luck);
        }

        public override void PostUpdateEquips()
        {
            if (sandStepping)
            {
                player.maxRunSpeed += 2f;
            }
            if (sorcerousMirror && player.HeldItem.DamageType == DamageClass.Magic)
            {
                player.aggro -= 400;
                player.statDefense += 4;
                player.lifeRegen += 4;
                player.GetDamage(DamageClass.Generic) *= 0.76f;
                player.AddBuff(ModContent.BuffType<SorcerousMirrorBuff>(), 2, true);
            }
            else
            {
                player.ClearBuff(ModContent.BuffType<SorcerousMirrorBuff>());
            }
            if (searingSetBonus)
            {
                searingSetBonusValue = ((int)player.statDefense) / (int)4;
                float modifierSAR = (1f + (searingSetBonusValue * 0.01f));
                player.DefenseEffectiveness *= modifierSAR;
                player.GetDamage(DamageClass.Generic) *= modifierSAR;
            }
            if (player.HasBuff(ModContent.BuffType<WreckedResistance>()))
            {
                float modifierWR = (float)(90 - (wreckedResistanceStack * 10)) * 0.01f;
                player.endurance *= modifierWR;
            }
            else
            {
                wreckedResistanceStack = 0;
            }
            if (player.HasBuff(ModContent.BuffType<WreckedAccuracy>()))
            {
                float modifierWA = (float)(90 - (wreckedAccuracyStack * 10)) * 0.01f;
                for (int i = 0; i < DamageClassLoader.DamageClassCount; i++)
                {
                    DamageClass c = DamageClassLoader.GetDamageClass(i);
                    if (c != null)
                    {
                        player.GetCritChance(c) *= modifierWA;
                    }
                }
                player.GetCritChance(DamageClass.Generic) -= (float)player.HeldItem.crit * (1f - modifierWA);
            }
            else
            {
                wreckedAccuracyStack = 0;
            }
            if (player.HasBuff(ModContent.BuffType<Devastated>()))
            {
                float modifierD = (float)(90 - (devastatedStack * 10)) * 0.01f;
                player.statLifeMax2 = (int)Math.Round(player.statLifeMax2 * modifierD);
                player.DefenseEffectiveness *= modifierD;
                player.blackBelt = false;
                player.brainOfConfusionItem = null;
                dodgeChancePercent = 0;
            }
            else
            {
                devastatedStack = 0;
            }
            if (gnomedStonedDebuff)
            {
                if (!player.buffImmune[BuffID.Stoned])
                {
                    int chanceDenominator = 5;
                    if (Main.drunkWorld)
                    {
                        chanceDenominator = 3;
                    }
                    if (!player.HasBuff(BuffID.Stoned) && currentTick % 60 == 0 && random.NextBool(chanceDenominator))
                    {
                        player.AddBuff(BuffID.Stoned, 300, true);
                    }
                }
            }
            spookyBonus = player.maxMinions;
            if (spookyShield)
            {
                player.statDefense += (spookyBonus * 3);
            }
            heartOfDespairDamageBonus = (player.statLifeMax2 - player.statLife) / 5;
            if (lifeforceEngineActivated)
            {
                if (hasLifeforceEngine && lifeforceEngineCooldown <= 0 && player.statLife < player.statLifeMax2)
                {
                    lifeforceEngineTicks = 300;
                    lifeforceEngineCooldown = 7500;
                    lifeforceEngineActivated = false;
                    for (int i = 0; i < 60; i++)
                    {
                        int rMax = (int)player.width;
                        double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                        double angle = Main.rand.NextDouble() * 2 * Math.PI;
                        int xOffset = (int)Math.Round(r * Math.Cos(angle));
                        int yOffset = (int)Math.Round(r * Math.Sin(angle));
                        Vector2 dustPosition = player.Center;
                        dustPosition.X += xOffset;
                        dustPosition.Y += yOffset;
                        Vector2 dustVelocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                        dustVelocity.Normalize();
                        dustVelocity *= 16f;
                        Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Electric, dustVelocity, 0, default);
                        newDust.noGravity = true;
                    }
                    SoundEngine.PlaySound(SoundID.Item22, player.Center);
                }
                else
                {
                    lifeforceEngineActivated = false;
                }
            }
            if (lifeforceEngineTicks > 0)
            {
                player.lifeRegen += Math.Abs(player.statDefense);
                player.statDefense -= Math.Abs(player.statDefense);
            }
            base.PostUpdateEquips();
        }

        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            if (info.DamageSource.SourceProjectileType == ProjectileID.PhantasmalDeathray)
            {
                if (info.DamageSource.SourceProjectileLocalIndex >= 0 && info.DamageSource.SourceProjectileLocalIndex < Main.projectile.Length)
                {
                    if (Main.projectile[info.DamageSource.SourceProjectileLocalIndex].GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentNPC(out NPC npc))
                    {
                        if (npc.type == NPCID.MoonLordHead && Main.masterMode)
                        {
                            Devastated.AnimateDisintegration(player);
                            Devastated.DisintegratePlayer(player);
                            return true;
                        }
                    }
                }
            }
            return base.ConsumableDodge(info);
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (info.DamageSource.SourceProjectileType == ProjectileID.PhantasmalDeathray)
            {
                if (info.DamageSource.SourceProjectileLocalIndex >= 0 && info.DamageSource.SourceProjectileLocalIndex < Main.projectile.Length)
                {
                    if (Main.projectile[info.DamageSource.SourceProjectileLocalIndex].GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentNPC(out NPC npc))
                    {
                        if (npc.type == NPCID.MoonLordHead && Main.masterMode)
                        {
                            Devastated.AnimateDisintegration(player);
                            Devastated.DisintegratePlayer(player);
                            return true;
                        }
                    }
                }
            }
            if (info.DamageSource.SourceProjectileType == ProjectileID.Landmine) //Prevent Landmines from damaging players.
            {
                return true;
            }
            if (random.NextBool(4) && sorcerousMirror && player.HeldItem.DamageType == DamageClass.Magic && !player.HasBuff(ModContent.BuffType<Devastated>())) // 1 in 4 chance
            {
                player.SetImmuneTimeForAllTypes(player.longInvince ? 120 : 80);
                return true;
            }
            if (random.Next(0, 100) < dodgeChancePercent && dodgeChancePercent > 0 && !player.HasBuff(ModContent.BuffType<Devastated>()))
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
            else if (weightedStack > 0)
            {
                float modifierWeighted = 2f * weightedStack;
                player.maxFallSpeed += modifierWeighted;
            }
        }

        public override void UpdateLifeRegen()
        {
            if (player.HasItem(ModContent.ItemType<HolyCharm>()) || searingSetBonus || (NPC.downedGolemBoss && soulPoweredShield))
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

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (spookyShield)
            {
                modifiers.FinalDamage *= (1f - (((float)spookyBonus * 1.5f) * 0.01f));
            }
            base.ModifyHurt(ref modifiers);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (player.HasBuff(ModContent.BuffType<SearingInferno>()))
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
                    if (Supercritical.BuffGroup.Contains(player.HeldItem.type))
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
                if (spookyEmblem)
                {
                    modifiers.ArmorPenetration += (spookyBonus * 3);
                    if (random.Next(0, 100) < (spookyBonus * 3)) //(3 x <Player Minion Slots>)% Chance
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

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPCWithProj(proj, target, ref modifiers);
            //Dirty hack to work around OnHitNPC() not being called for Influx Waver projectiles...
            if (proj.type == ProjectileID.InfluxWaver)
            {
                OnHitNPC
                (
                    target,
                    modifiers.ToHitInfo
                    (
                        proj.originalDamage,
                        ((modifiers.CritDamage == modifiers.NonCritDamage) ? false : true),
                        proj.knockBack
                    ),
                    proj.damage
                );
            }
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
                int limit = 20;
                if (item.useAnimation > 0 && !item.channel)
                {
                    limit = item.useAnimation;
                }
                if (shroomiteGenesisOverheatTicks >= 60 * (float)((float)20 / (float)limit))
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
                    shroomiteGenesisOverheatTimer = 120;
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
                    player.HeldItem.type == ItemID.ChlorophytePartisan
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
                // Stop Sacling at ~200 Damage
                if (healingAmount > 10)
                {
                    healingAmount = 10;
                }
                // Having Moon Bite means the effect still works, however,
                // it will be 90% less effective
                // 1 Base Heal is still guaranteed
                if (player.HasBuff(BuffID.MoonLeech) && (player.HeldItem.prefix == ModContent.PrefixType<Leeching>() || player.HeldItem.type == ItemID.ChlorophytePartisan))
                {
                    healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.1);
                }
                // Chlorophyte Partisan go BRRRR!!!
                if (player.HeldItem.type == ItemID.ChlorophytePartisan && player.HeldItem.prefix == ModContent.PrefixType<Leeching>())
                {
                    healingAmount *= 2;
                }
                if (player.HeldItem.prefix == ModContent.PrefixType<Leeching>() || player.HeldItem.type == ItemID.ChlorophytePartisan)
                {
                    long ticksSinceLastHeal = Math.Abs(currentTick - lastLeechingHealTick);
                    if (ticksSinceLastHeal >= player.itemAnimationMax) // Only heal player one time every item use
                    {
                        player.Heal(healingAmount);
                        lastLeechingHealTick = currentTick;
                    }
                }
                else if (player.HeldItem.prefix == ModContent.PrefixType<Siphoning>())
                {
                    if (player.statMana <= (player.statManaMax2 - healingAmount))
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
