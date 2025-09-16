/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2025 LukasV-Coding

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
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.NPCs;
using WeDoALittleTrolling.Common.Utilities;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using Terraria.WorldBuilding;
using WeDoALittleTrolling.Common.Configs;

namespace WeDoALittleTrolling.Common.ModPlayers
{
    internal class WDALTPlayer : ModPlayer
    {
        public int spookyBonus;
        public int dodgeChancePercent;
        public bool spookyEmblem;
        public bool spookyShield;
        public bool sorcerousMirror;
        public bool heartOfDespair;
        public int heartOfDespairDamageBonus;
        public bool soulPoweredShield;
        public bool blazingShieldPrev;
        public bool blazingShield;
        public bool searingSetBonus;
        public int searingValue;
        public bool sandStepping;
        public bool gnomedStonedDebuff;
        public bool gnomedDebuff;
        public int gnomedDebuffTicksLeft;
        public bool hauntedDebuff;
        public int hauntedDebuffTicksLeft;
        //public bool yoyoArtifact;
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
        public bool wretchMinion;
        public int unionMirrorTicks;
        public int mirrorOfRecollectionTicks;
        public int weightedStack;
        public int conjuringStack;
        public int acceleratedStack;
        public int arcaneStack;
        public int siphonStack;
        public bool lifeforceEngineActivated;
        public int lifeforceEngineTicks;
        public int lifeforceEngineCooldown;
        public bool hasLifeforceEngine;
        public bool cornEmblem;
        public bool heartSeekerCharm;
        public bool tungstenThornNecklace;
        public bool isEmbersteelExplosionActive;
        public int embersteelBladeCharges;
        public int embersteelExplosionTicks;
        public Item embersteelExplosionSourceItem;
        public int embersteelExplosionDamage;
        public float embersteelExplosionKnockback;
        public static UnifiedRandom random = new UnifiedRandom();

        public override void Initialize()
        {
            player = this.Player;
            spookyBonus = 0;
            dodgeChancePercent = 0;
            spookyEmblem = false;
            spookyShield = false;
            sorcerousMirror = false;
            heartOfDespair = false;
            heartOfDespairDamageBonus = 0;
            soulPoweredShield = false;
            blazingShieldPrev = false;
            blazingShield = false;
            searingSetBonus = false;
            searingValue = 0;
            sandStepping = false;
            gnomedStonedDebuff = false;
            gnomedDebuff = false;
            gnomedDebuffTicksLeft = 0;
            hauntedDebuff = false;
            hauntedDebuffTicksLeft = 0;
            //yoyoArtifact = false;
            currentTick = 0;
            chargeAccelerationTicks = 0;
            zoneWormCandle = false;
            shroomiteGenesis = false;
            shroomiteGenesisOverheatTicks = 0;
            shroomiteGenesisOverchannelTicks = 0;
            shroomiteGenesisOverheatTimer = 0;
            lumintePhantomMinion = false;
            frozenElementalMinion = false;
            wretchMinion = false;
            unionMirrorTicks = 0;
            mirrorOfRecollectionTicks = 0;
            weightedStack = 0;
            conjuringStack = 0;
            acceleratedStack = 0;
            arcaneStack = 0;
            siphonStack = 0;
            lifeforceEngineActivated = false;
            lifeforceEngineTicks = 0;
            lifeforceEngineCooldown = 0;
            hasLifeforceEngine = false;
            cornEmblem = false;
            heartSeekerCharm = false;
            tungstenThornNecklace = false;
            isEmbersteelExplosionActive = false;
            embersteelBladeCharges = 0;
            embersteelExplosionTicks = 0;
            embersteelExplosionSourceItem = null;
            embersteelExplosionDamage = 0;
            embersteelExplosionKnockback = 0f;
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("WDALTGnomedDebuff"))
            {
                gnomedDebuff = true;
                gnomedDebuffTicksLeft = tag.GetInt("WDALTGnomedDebuff");
            }
            if (tag.ContainsKey("WDALTHauntedDebuff"))
            {
                hauntedDebuff = true;
                hauntedDebuffTicksLeft = tag.GetInt("WDALTHauntedDebuff");
            }
            base.LoadData(tag);
        }

        public override void SaveData(TagCompound tag)
        {
            if (gnomedDebuff)
            {
                tag["WDALTGnomedDebuff"] = gnomedDebuffTicksLeft;
            }
            if (hauntedDebuff)
            {
                tag["WDALTHauntedDebuff"] = hauntedDebuffTicksLeft;
            }
            base.SaveData(tag);
        }

        private void ResetVariables()
        {
            spookyBonus = 0;
            dodgeChancePercent = 0;
            spookyEmblem = false;
            spookyShield = false;
            sorcerousMirror = false;
            heartOfDespair = false;
            heartOfDespairDamageBonus = 0;
            soulPoweredShield = false;
            blazingShieldPrev = false;
            blazingShield = false;
            searingSetBonus = false;
            searingValue = 0;
            sandStepping = false;
            gnomedStonedDebuff = false;
            //yoyoArtifact = false;
            chargeAccelerationTicks = 0;
            zoneWormCandle = false;
            shroomiteGenesis = false;
            shroomiteGenesisOverheatTicks = 0;
            shroomiteGenesisOverchannelTicks = 0;
            shroomiteGenesisOverheatTimer = 0;
            lumintePhantomMinion = false;
            frozenElementalMinion = false;
            wretchMinion = false;
            unionMirrorTicks = 0;
            mirrorOfRecollectionTicks = 0;
            weightedStack = 0;
            conjuringStack = 0;
            acceleratedStack = 0;
            arcaneStack = 0;
            siphonStack = 0;
            lifeforceEngineActivated = false;
            lifeforceEngineTicks = 0;
            lifeforceEngineCooldown = 0;
            hasLifeforceEngine = false;
            cornEmblem = false;
            heartSeekerCharm = false;
            tungstenThornNecklace = false;
            isEmbersteelExplosionActive = false;
            embersteelBladeCharges = 0;
            embersteelExplosionTicks = 0;
            embersteelExplosionSourceItem = null;
            embersteelExplosionDamage = 0;
            embersteelExplosionKnockback = 0f;
        }

        public override void ResetEffects()
        {
            dodgeChancePercent = 0;
            spookyEmblem = false;
            spookyShield = false;
            sorcerousMirror = false;
            heartOfDespair = false;
            soulPoweredShield = false;
            blazingShield = false;
            searingSetBonus = false;
            searingValue = 0;
            sandStepping = false;
            gnomedStonedDebuff = false;
            //yoyoArtifact = false;
            shroomiteGenesis = false;
            lumintePhantomMinion = false;
            frozenElementalMinion = false;
            wretchMinion = false;
            weightedStack = 0;
            conjuringStack = 0;
            acceleratedStack = 0;
            arcaneStack = 0;
            hasLifeforceEngine = false;
            cornEmblem = false;
            heartSeekerCharm = false;
            tungstenThornNecklace = false;
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
            if (unionMirrorTicks > 0)
            {
                if (unionMirrorTicks == 1)
                {
                    UnionMirror.TeleportHome(player);
                }
                unionMirrorTicks--;
            }
            if (mirrorOfRecollectionTicks > 0)
            {
                if (mirrorOfRecollectionTicks == 1)
                {
                    MirrorOfRecollection.TeleportHome(player);
                }
                mirrorOfRecollectionTicks--;
            }
            if (lifeforceEngineTicks > 0)
            {
                lifeforceEngineTicks--;
            }
            if (lifeforceEngineCooldown > 0)
            {
                lifeforceEngineCooldown--;
            }
            if (isEmbersteelExplosionActive)
            {
                if (embersteelExplosionTicks > 0)
                {
                    if (player.whoAmI == Main.myPlayer && embersteelExplosionSourceItem != null)
                    {
                        if (embersteelExplosionTicks == 60)
                        {
                            Projectile.NewProjectileDirect
                            (
                                new EntitySource_ItemUse(player, embersteelExplosionSourceItem),
                                player.Center,
                                Vector2.Zero,
                                ModContent.ProjectileType<EmbersteelExplosion>(),
                                (int)Math.Round(player.GetTotalDamage(DamageClass.Melee).ApplyTo(embersteelExplosionDamage)),
                                (int)Math.Round(player.GetTotalKnockback(DamageClass.Melee).ApplyTo(embersteelExplosionKnockback))
                            );
                        }
                        if (embersteelExplosionTicks == 54)
                        {
                            Vector2 pos = new Vector2(player.Center.X + 45f, player.Center.Y);
                            for (int i = 0; i < 8; i++)
                            {
                                Projectile.NewProjectileDirect
                                (
                                    new EntitySource_ItemUse(player, embersteelExplosionSourceItem),
                                    pos.RotatedBy((double)i * Math.PI / 4.0, player.Center),
                                    Vector2.Zero,
                                    ModContent.ProjectileType<EmbersteelExplosion>(),
                                    (int)Math.Round(player.GetTotalDamage(DamageClass.Melee).ApplyTo(embersteelExplosionDamage)),
                                    (int)Math.Round(player.GetTotalKnockback(DamageClass.Melee).ApplyTo(embersteelExplosionKnockback))
                                );
                            }
                        }
                        if (embersteelExplosionTicks == 48)
                        {
                            Vector2 pos = new Vector2(player.Center.X + 90f, player.Center.Y);
                            for (int i = 0; i < 8; i++)
                            {
                                Projectile.NewProjectileDirect
                                (
                                    new EntitySource_ItemUse(player, embersteelExplosionSourceItem),
                                    pos.RotatedBy((double)i * Math.PI / 4.0, player.Center),
                                    Vector2.Zero,
                                    ModContent.ProjectileType<EmbersteelExplosion>(),
                                    (int)Math.Round(player.GetTotalDamage(DamageClass.Melee).ApplyTo(embersteelExplosionDamage)),
                                    (int)Math.Round(player.GetTotalKnockback(DamageClass.Melee).ApplyTo(embersteelExplosionKnockback))
                                );
                            }
                        }
                        if (embersteelExplosionTicks == 42)
                        {
                            Vector2 pos = new Vector2(player.Center.X + 135f, player.Center.Y);
                            for (int i = 0; i < 8; i++)
                            {
                                Projectile.NewProjectileDirect
                                (
                                    new EntitySource_ItemUse(player, embersteelExplosionSourceItem),
                                    pos.RotatedBy((double)i * Math.PI / 4.0, player.Center),
                                    Vector2.Zero,
                                    ModContent.ProjectileType<EmbersteelExplosion>(),
                                    (int)Math.Round(player.GetTotalDamage(DamageClass.Melee).ApplyTo(embersteelExplosionDamage)),
                                    (int)Math.Round(player.GetTotalKnockback(DamageClass.Melee).ApplyTo(embersteelExplosionKnockback))
                                );
                            }
                        }
                        if (embersteelExplosionTicks == 36)
                        {
                            Vector2 pos = new Vector2(player.Center.X + 180f, player.Center.Y);
                            for (int i = 0; i < 16; i++)
                            {
                                Projectile.NewProjectileDirect
                                (
                                    new EntitySource_ItemUse(player, embersteelExplosionSourceItem),
                                    pos.RotatedBy((double)i * Math.PI / 8.0, player.Center),
                                    Vector2.Zero,
                                    ModContent.ProjectileType<EmbersteelExplosion>(),
                                    (int)Math.Round(player.GetTotalDamage(DamageClass.Melee).ApplyTo(embersteelExplosionDamage)),
                                    (int)Math.Round(player.GetTotalKnockback(DamageClass.Melee).ApplyTo(embersteelExplosionKnockback))
                                );
                            }
                        }
                        if (embersteelExplosionTicks == 30)
                        {
                            Vector2 pos = new Vector2(player.Center.X + 225f, player.Center.Y);
                            for (int i = 0; i < 16; i++)
                            {
                                Projectile.NewProjectileDirect
                                (
                                    new EntitySource_ItemUse(player, embersteelExplosionSourceItem),
                                    pos.RotatedBy((double)i * Math.PI / 8.0, player.Center),
                                    Vector2.Zero,
                                    ModContent.ProjectileType<EmbersteelExplosion>(),
                                    (int)Math.Round(player.GetTotalDamage(DamageClass.Melee).ApplyTo(embersteelExplosionDamage)),
                                    (int)Math.Round(player.GetTotalKnockback(DamageClass.Melee).ApplyTo(embersteelExplosionKnockback))
                                );
                            }
                        }
                        if (embersteelExplosionTicks == 24)
                        {
                            Vector2 pos = new Vector2(player.Center.X + 270f, player.Center.Y);
                            for (int i = 0; i < 16; i++)
                            {
                                Projectile.NewProjectileDirect
                                (
                                    new EntitySource_ItemUse(player, embersteelExplosionSourceItem),
                                    pos.RotatedBy((double)i * Math.PI / 8.0, player.Center),
                                    Vector2.Zero,
                                    ModContent.ProjectileType<EmbersteelExplosion>(),
                                    (int)Math.Round(player.GetTotalDamage(DamageClass.Melee).ApplyTo(embersteelExplosionDamage)),
                                    (int)Math.Round(player.GetTotalKnockback(DamageClass.Melee).ApplyTo(embersteelExplosionKnockback))
                                );
                            }
                        }
                        if (embersteelExplosionTicks == 18)
                        {
                            Vector2 pos = new Vector2(player.Center.X + 315f, player.Center.Y);
                            for (int i = 0; i < 32; i++)
                            {
                                Projectile.NewProjectileDirect
                                (
                                    new EntitySource_ItemUse(player, embersteelExplosionSourceItem),
                                    pos.RotatedBy((double)i * Math.PI / 16.0, player.Center),
                                    Vector2.Zero,
                                    ModContent.ProjectileType<EmbersteelExplosion>(),
                                    (int)Math.Round(player.GetTotalDamage(DamageClass.Melee).ApplyTo(embersteelExplosionDamage)),
                                    (int)Math.Round(player.GetTotalKnockback(DamageClass.Melee).ApplyTo(embersteelExplosionKnockback))
                                );
                            }
                        }
                        if (embersteelExplosionTicks == 12)
                        {
                            Vector2 pos = new Vector2(player.Center.X + 360f, player.Center.Y);
                            for (int i = 0; i < 32; i++)
                            {
                                Projectile.NewProjectileDirect
                                (
                                    new EntitySource_ItemUse(player, embersteelExplosionSourceItem),
                                    pos.RotatedBy((double)i * Math.PI / 16.0, player.Center),
                                    Vector2.Zero,
                                    ModContent.ProjectileType<EmbersteelExplosion>(),
                                    (int)Math.Round(player.GetTotalDamage(DamageClass.Melee).ApplyTo(embersteelExplosionDamage)),
                                    (int)Math.Round(player.GetTotalKnockback(DamageClass.Melee).ApplyTo(embersteelExplosionKnockback))
                                );
                            }
                        }
                        if (embersteelExplosionTicks == 6)
                        {
                            Vector2 pos = new Vector2(player.Center.X + 405f, player.Center.Y);
                            for (int i = 0; i < 32; i++)
                            {
                                Projectile.NewProjectileDirect
                                (
                                    new EntitySource_ItemUse(player, embersteelExplosionSourceItem),
                                    pos.RotatedBy((double)i * Math.PI / 16.0, player.Center),
                                    Vector2.Zero,
                                    ModContent.ProjectileType<EmbersteelExplosion>(),
                                    (int)Math.Round(player.GetTotalDamage(DamageClass.Melee).ApplyTo(embersteelExplosionDamage)),
                                    (int)Math.Round(player.GetTotalKnockback(DamageClass.Melee).ApplyTo(embersteelExplosionKnockback))
                                );
                            }
                        }
                        embersteelExplosionTicks--;
                    }
                }
                else
                {
                    embersteelExplosionTicks = 0;
                    isEmbersteelExplosionActive = false;
                }
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
            if (blazingShield && player.whoAmI == Main.myPlayer)
            {
                if (!player.GetModPlayer<WDALTPlayerUtil>().HasBlazingShield())
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        NPC shield = NPC.NewNPCDirect(player.GetSource_FromThis(), (int)Math.Round((double)player.Center.X), (int)Math.Round((double)player.Center.Y), ModContent.NPCType<BlazingShieldNPC>());
                        shield.GetGlobalNPC<WDALTNPCUtil>().BlazingShieldOwnerIndex = Main.myPlayer;
                    }
                    else if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnBlazingShieldPacket = Mod.GetPacket();
                        spawnBlazingShieldPacket.Write(WDALTPacketTypeID.spawnBlazingShield);
                        spawnBlazingShieldPacket.Write((int)Main.myPlayer);
                        spawnBlazingShieldPacket.Send();
                    }
                }
            }
            else if (!blazingShield && blazingShieldPrev && player.whoAmI == Main.myPlayer)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        if (Main.npc[i].active && Main.npc[i].GetGlobalNPC<WDALTNPCUtil>().BlazingShieldOwnerIndex == Main.myPlayer)
                        {
                            Main.npc[i].StrikeInstantKill();
                        }
                    }
                }
                else if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket clearBlazingShieldPacket = Mod.GetPacket();
                    clearBlazingShieldPacket.Write(WDALTPacketTypeID.clearBlazingShield);
                    clearBlazingShieldPacket.Write((int)Main.myPlayer);
                    clearBlazingShieldPacket.Send();
                }
            }
            blazingShieldPrev = blazingShield;
            if (sandStepping)
            {
                player.maxRunSpeed += 2f;
            }
            if (sorcerousMirror && player.HeldItem.DamageType == DamageClass.Magic)
            {
                player.aggro -= 400;
                player.statDefense += 4;
                player.lifeRegen += 4;
                player.AddBuff(ModContent.BuffType<SorcerousMirrorBuff>(), 2, true);
            }
            else
            {
                player.ClearBuff(ModContent.BuffType<SorcerousMirrorBuff>());
            }
            if (searingValue > 0)
            {
                float modifierSAR = (1f + (searingValue * 0.01f));
                player.DefenseEffectiveness *= modifierSAR;
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
                player.statDefense += (spookyBonus * 2);
            }
            heartOfDespairDamageBonus = (player.statLifeMax2 - player.statLife) / 5;
            if (lifeforceEngineActivated)
            {
                if (hasLifeforceEngine && lifeforceEngineCooldown <= 0 && player.statLife < player.statLifeMax2)
                {
                    lifeforceEngineTicks = 300;
                    lifeforceEngineCooldown = 5400;
                    lifeforceEngineActivated = false;
                    player.AddBuff(ModContent.BuffType<LifeforceEngineBuff>(), 300, true);
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
                        dustVelocity = dustVelocity.SafeNormalize(Vector2.Zero);
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
                player.lifeRegen += Math.Abs(player.statDefense) * 2;
                player.statDefense -= Math.Abs(player.statDefense);               
            }
            if (acceleratedStack > 0)
            {
                float factor = 0.08f;
                player.moveSpeed += (factor * (float)acceleratedStack);
            }
            if (ModContent.GetInstance<WDALTServerConfig>().NoWingsChallenge)
            {
                player.wings = 0;
                player.wingsLogic = 0;
            }
            base.PostUpdateEquips();
        }

        public override void UpdateBadLifeRegen()
        {
            if (tungstenThornNecklace && player.HeldItem != null && player.HeldItem.DamageType != null && TungstenThornRing.supportedDamageClasses.Contains(player.HeldItem.DamageType))
            {
                player.moveSpeed *= 0.8f;
            }
            base.UpdateBadLifeRegen();
        }
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (info.DamageSource.SourceProjectileType == ProjectileID.Landmine) //Prevent Landmines from damaging players.
            {
                return true;
            }
            if (random.NextBool(4) && sorcerousMirror && player.HeldItem.DamageType == DamageClass.Magic) // 1 in 4 chance
            {
                player.SetImmuneTimeForAllTypes(player.longInvince ? 120 : 80);
                return true;
            }
            if (random.Next(0, 100) < dodgeChancePercent && dodgeChancePercent > 0)
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
                float modifierWeighted = 0.8f * weightedStack;
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
            base.UpdateLifeRegen();
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (spookyShield)
            {
                modifiers.FinalDamage *= (1f - (((float)spookyBonus * 1.0f) * 0.01f));
            }
            if (player.HasBuff(ModContent.BuffType<OnyxBlaze>()))
            {
                modifiers.SourceDamage *= OnyxBlaze.dmgTakenMult;
            }
            if (heartSeekerCharm && random.Next(0, 100) < DetermineHighestCrit())
            {
                modifiers.SourceDamage *= 2;
            }
            base.ModifyHurt(ref modifiers);          
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (player.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                modifiers.SourceDamage *= (1.0f - SearingInferno.damageNerfMultiplier);
            }
            if (sorcerousMirror && player.HeldItem.DamageType == DamageClass.Magic)
            {
                modifiers.SourceDamage *= 0.75f;
            }
            if
            (
                modifiers.DamageType == DamageClass.Magic ||
                modifiers.DamageType == DamageClass.MagicSummonHybrid ||
                modifiers.DamageType == DamageClass.Ranged ||
                modifiers.DamageType == DamageClass.Throwing
            )
            {
                if (player.HeldItem.prefix == ModContent.PrefixType<Supercritical>())
                {
                    modifiers.CritDamage += 2.0f;
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
                    modifiers.ArmorPenetration += (4f * spookyBonus);
                    if (random.Next(0, 100) < (spookyBonus * 2)) //(2 x <Player Minion Slots>)% Chance
                    {
                        modifiers.SetCrit();
                    }
                }
                if (conjuringStack > 0)
                {
                    modifiers.ArmorPenetration += (6f * conjuringStack);
                }
            }
            if (heartSeekerCharm && modifiers.DamageType.UseStandardCritCalcs && random.Next(0, 100) < DetermineHighestCrit())
            {
                modifiers.SetCrit();
            }
            if
            (
                tungstenThornNecklace &&
                TungstenThornRing.supportedDamageClasses.Contains(modifiers.DamageType)
            )
            {
                modifiers.ScalingArmorPenetration += 1f;
            }
            base.ModifyHitNPC(target, ref modifiers);
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
            /*if
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
                    projVelocity = projVelocity.SafeNormalize(Vector2.Zero);
                    projVelocity *= 12f;
                    int dmg = (int)Math.Round(proj.damage * 0.15);
                    Projectile.NewProjectileDirect(proj.GetSource_OnHit(target), projPosition, projVelocity, ModContent.ProjectileType<MagicArtifact>(), dmg, proj.knockBack, proj.owner);
                }
            }*/
            if (proj.type == ProjectileID.BlackBolt)
            {
                target.AddBuff(ModContent.BuffType<OnyxBlaze>(), 240, false);
            }
            base.OnHitNPCWithProj(proj, target, hit, damageDone);
        }

        public float DetermineHighestCrit()
        {
            float highestCrit = 0.0f;
            for (int i = 0; i < DamageClassLoader.DamageClassCount; i++)
            {
                DamageClass dClass = DamageClassLoader.GetDamageClass(i);
                if (dClass != null && dClass.UseStandardCritCalcs)
                {
                    float crit = player.GetCritChance(dClass);
                    if (crit > highestCrit)
                    {
                        highestCrit = crit;
                    }
                }
            }
            return highestCrit;
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
                    shroomiteGenesisOverheatTimer = 180;
                    shroomiteGenesisOverheatTicks = 0;
                    return false;
                }
                else if (item.type == ItemID.CoinGun)
                {
                    int chance = 1000;
                    if (source.AmmoItemIdUsed == ItemID.CopperCoin)
                    {
                        chance = 1;
                    }
                    if (source.AmmoItemIdUsed == ItemID.SilverCoin)
                    {
                        chance = 10;
                    }
                    if (source.AmmoItemIdUsed == ItemID.GoldCoin)
                    {
                        chance = 100;
                    }
                    if (source.AmmoItemIdUsed == ItemID.PlatinumCoin)
                    {
                        chance = 1000;
                    }
                    if (random.Next(0, 10000) < chance)
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
                            new Color(255, 127, 0),
                            "Jammed!"
                        );
                        shroomiteGenesisOverheatTimer = 60;
                        return false;
                    }
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
                    !target.friendly &&
                    !target.CountsAsACritter &&
                    !target.isLikeATownNPC &&
                    target.type != NPCID.TargetDummy &&
                    target.canGhostHeal
                ) &&
                (
                    target.CanBeChasedBy() ||
                    !target.active //Make final hit heal as well.
                )
            )
            {
                if
                (
                    (
                        hit.DamageType == DamageClass.Melee ||
                        hit.DamageType == DamageClass.MeleeNoSpeed ||
                        hit.DamageType == DamageClass.SummonMeleeSpeed
                    ) &&
                    (
                        player.HeldItem.prefix == ModContent.PrefixType<Leeching>() ||
                        player.HeldItem.type == ItemID.ChlorophytePartisan
                    )
                ) //Leeching
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
                    if (player.HasBuff(BuffID.MoonLeech))
                    {
                        healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.1);
                    }
                    int hitAmount = player.GetModPlayer<WDALTPlayerUtil>().GetHitAmountInLastSecond();
                    if (hitAmount > 3)
                    {
                        float modifier = 1f;
                        modifier -= (0.05f * Math.Abs(hitAmount - 3));
                        if (modifier < 0f)
                        {
                            modifier = 0f;
                        }
                        healingAmount = 1 + (int)Math.Round((healingAmount - 1) * modifier);
                    }
                    // Being Immune makes Leeching 75% less effective
                    bool flag = player.immune;
                    for (int i = 0; i < player.hurtCooldowns.Length; i++)
                    {
                        if (player.hurtCooldowns[i] > 0)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.25);
                    }
                    // Chlorophyte Partisan go BRRRR!!!
                    if (player.HeldItem.type == ItemID.ChlorophytePartisan && player.HeldItem.prefix == ModContent.PrefixType<Leeching>())
                    {
                        healingAmount *= 2;
                    }
                    player.Heal(healingAmount);
                }
                else if
                (
                    (
                        hit.DamageType == DamageClass.Magic ||
                        hit.DamageType == DamageClass.MagicSummonHybrid ||
                        (
                            WDALTModSystem.isThoriumModPresent &&
                            WDALTModSystem.MCIDIntegrity &&
                            (
                                hit.DamageType == WDALTModContentID.GetThoriumDamageClass(WDALTModContentID.ThoriumDamageClass_HealerDamage) ||
                                hit.DamageType == WDALTModContentID.GetThoriumDamageClass(WDALTModContentID.ThoriumDamageClass_HealerTool) ||
                                hit.DamageType == WDALTModContentID.GetThoriumDamageClass(WDALTModContentID.ThoriumDamageClass_HealerToolDamageHybrid)
                            )
                        )
                    ) &&
                    (
                        player.HeldItem.prefix == ModContent.PrefixType<Siphoning>()
                    )
                ) //Siphoning
                {
                    // 1 Base Siphon, 110% of mana cost
                    int manaSiphonAmount = (int)Math.Ceiling((double)player.GetManaCost(player.HeldItem) * 1.1);
                    if (manaSiphonAmount < 1)
                    {
                        manaSiphonAmount = 1;
                    }
                    float ratio = 1f / (1f + (float)siphonStack);
                    if (ratio < 0f)
                    {
                        ratio = 0f;
                    }
                    if (ratio > 1f)
                    {
                        ratio = 1f;
                    }
                    manaSiphonAmount = 1 + (int)Math.Round((manaSiphonAmount - 1) * ratio);
                    if (player.statMana <= (player.statManaMax2 - manaSiphonAmount))
                    {
                        player.statMana += manaSiphonAmount;
                    }
                    else
                    {
                        player.statMana = player.statManaMax2;
                    }
                    player.ManaEffect(manaSiphonAmount);
                    siphonStack++;
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

        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            if (arcaneStack > 0)
            {
                float factor = (arcaneStack * 0.05f);
                if (factor < 0f)
                {
                    factor = 0f;
                }
                if (factor > 1f)
                {
                    factor = 1f;
                }
                reduce -= factor;
            }
            base.ModifyManaCost(item, ref reduce, ref mult);
        }

        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            siphonStack = 0;
            base.OnConsumeMana(item, manaConsumed);
        }
    }
}
