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

using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;
using System.Collections.Generic;

namespace WeDoALittleTrolling.Content.Items
{
    internal class GlobalItemList : GlobalItem
    {
        public override bool InstancePerEntity => false;

        public static Random random = new Random();
        public static readonly int[] biomeCrateTypes =
        {
            ItemID.JungleFishingCrate,
            ItemID.JungleFishingCrateHard,
            ItemID.FloatingIslandFishingCrate,
            ItemID.FloatingIslandFishingCrateHard,
            ItemID.CorruptFishingCrate,
            ItemID.CorruptFishingCrateHard,
            ItemID.CrimsonFishingCrate,
            ItemID.CrimsonFishingCrateHard,
            ItemID.HallowedFishingCrate,
            ItemID.HallowedFishingCrateHard,
            ItemID.DungeonFishingCrate,
            ItemID.DungeonFishingCrateHard,
            ItemID.FrozenCrate,
            ItemID.FrozenCrateHard,
            ItemID.OasisCrate,
            ItemID.OasisCrateHard,
            ItemID.LavaCrate,
            ItemID.LavaCrateHard,
            ItemID.OceanCrate,
            ItemID.OceanCrateHard
        };
        
        public override bool CanUseItem(Item item, Player player)
        {
            // Anti-Poo-Block-Mechanism
            if
            (
                item.type == ItemID.PoopBlock ||
                item.type == ItemID.PoopWall
            )
            {
                player.chatOverhead.NewMessage(player.name + " tried to uglify the world.", 180);
                return false;
            }
            else if
            (
                item.type == ItemID.LandMine
            )
            {
                player.chatOverhead.NewMessage("You may not use this item.", 180);
                return false;
            }
            else if
            (
                item.type == ItemID.RodOfHarmony
            )
            {
                if(player.HasBuff(BuffID.ChaosState))
                {
                    if((player.statLife - (int)Math.Round((float)player.statLifeMax2/(float)7)) >= 0)
                    {
                        player.statLife -= (int)Math.Round((float)player.statLifeMax2/(float)7);
                    }
                    else
                    {
                        PlayerDeathReason reason = new PlayerDeathReason();
                        reason.SourceCustomReason = player.name+" didn't materialize";
                        player.KillMe(reason, 9999999999.0, 0, false);
                    }
                }
                player.AddBuff(BuffID.ChaosState, 180, true);
            }
            return base.CanUseItem(item, player);
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            if(item.type == ItemID.WormholePotion)
            {
                return false;
            }
            return base.ConsumeItem(item, player);
        }

        
        //Revert damage reduction from Spectre Hood
        public override void UpdateEquip(Item item, Player player)
        {
            if
            (
                item.type == ItemID.SpectreHood &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerChestplateEquipped(ItemID.SpectreRobe) &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.SpectrePants)
            )
            {
                player.GetDamage(DamageClass.Magic) += (float)0.4;
            }
            if(item.type == ItemID.SpectreHood)
            {
                player.statManaMax2 += 60;
            }
            base.UpdateEquip(item, player);
        }
        
        public static void ModifySetBonus(Player player)
        {
            if
            (
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.SpectreHood) &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerChestplateEquipped(ItemID.SpectreRobe) &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.SpectrePants)
            )
            {
                player.setBonus = "Generates 20% of magic damage as healing force\nMagic damage done to enemies heals the player with lowest health";
            }
        }
        
        //Adjust Tooltips accordingly
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if(item.type == ItemID.SpectreHood)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "PrefixAccMaxMana", "Increases maximum mana by 60");
                tooltips.Add(extraManaLine);
            }
            if(item.type == ItemID.ChlorophytePartisan)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "PrefixWeaponLeechingDescription", "Recoveres 5% of damage as health");
                tooltips.Add(extraManaLine);
            }
            if(item.type == ItemID.Moondial)
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip0") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "Allows time to fast forward to dusk");
            }
            if(item.type == ItemID.Sundial)
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip0") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "Allows time to fast forward to dawn");
            }
            if(item.type == ItemID.WeatherVane)
            {
                TooltipLine tileFunctionLine = new TooltipLine(Mod, "TileFunctionDescription", "Allows rain to start, intesify and stop");
                tooltips.Add(tileFunctionLine);
            }
            if(item.type == ItemID.SkyMill)
            {
                TooltipLine tileFunctionLine = new TooltipLine(Mod, "TileFunctionDescription", "Allows wind to start, intesify, change direction and stop");
                tooltips.Add(tileFunctionLine);
            }
            if(item.type == ItemID.DjinnLamp)
            {
                TooltipLine tileFunctionLine = new TooltipLine(Mod, "TileFunctionDescription", "Allows sandstorms to start and stop");
                tooltips.Add(tileFunctionLine);
            }
            if
            (
                item.type == ItemID.StaffoftheFrostHydra ||
                item.type == ItemID.PoisonStaff ||
                item.type == ItemID.VenomStaff ||
                item.type == ItemID.SkyFracture ||
                item.type == ItemID.MeteorStaff ||
                item.type == ItemID.InfernoFork ||
                item.type == ItemID.BlizzardStaff ||
                item.type == ItemID.Razorpine ||
                item.type == ItemID.ApprenticeStaffT3
            )
            {
                TooltipLine extraCritChanceLine = new TooltipLine(Mod, "ProjectileHomingDescription", "Projectiles move towards the closest target");
                tooltips.Add(extraCritChanceLine);
            }
            if
            (
                item.type == ItemID.StaffoftheFrostHydra ||
                item.type == ItemID.QueenSpiderStaff ||
                item.type == ItemID.HoundiusShootius ||
                item.type == ItemID.RainbowCrystalStaff ||
                item.type == ItemID.MoonlordTurretStaff
            ) 
            {
                TooltipLine extraCritChanceLine = new TooltipLine(Mod, "ExtraCritChanceDescription", "Projectiles have a 30% chance to land a critical strike");
                tooltips.Add(extraCritChanceLine);
            }
            base.ModifyTooltips(item, tooltips);
        }

        public override void RightClick(Item item, Player player)
        {
            if
            (
                item.type == ItemID.IronCrate ||
                item.type == ItemID.IronCrateHard
            )
            {
                if(random.Next(0, 8) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((short)ItemID.Sundial);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.Sundial, 1);
                    }
                }
                if(random.Next(0, 8) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((short)ItemID.LifeCrystal);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.LifeCrystal, 1);
                    }
                }
            }
            if
            (
                item.type == ItemID.GoldenCrate ||
                item.type == ItemID.GoldenCrateHard
            )
            {
                if(random.Next(0, 4) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((short)ItemID.Sundial);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.Sundial, 1);
                    }
                }
                if
                (random.Next(0, 3) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((short)ItemID.LifeCrystal);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.LifeCrystal, 1);
                    }
                }
            }
            if
            (
                item.type == ItemID.OceanCrate ||
                item.type == ItemID.OceanCrateHard
            )
            {
                if(random.Next(0, 5) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((short)ItemID.WaterWalkingBoots);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.WaterWalkingBoots, 1);
                    }
                }
            }
            int amountGold = 0;
            short goldItemID = ItemID.GoldCoin;
            if (item.type == ItemID.WoodenCrate || item.type == ItemID.WoodenCrateHard)
            {
                amountGold = 5;
            }
            if (item.type == ItemID.IronCrate || item.type == ItemID.IronCrateHard)
            {
                amountGold = 10;
            }
            if (biomeCrateTypes.Contains(item.type))
            {
                amountGold = 12;
            }
            if (item.type == ItemID.GoldenCrate || item.type == ItemID.GoldenCrateHard)
            {
                amountGold = 20;
            }
            if (Main.hardMode)
            {
                amountGold *= 2;
            }
            if (NPC.downedPlantBoss)
            {
                amountGold *= 2;
            }
            if (item.type == ItemID.OceanCrateHard && NPC.downedFishron && player.anglerQuestsFinished >= 50)
            {
                amountGold = 4;
                goldItemID = ItemID.PlatinumCoin;
            }
            if
            (
                item.type == ItemID.WoodenCrate ||
                item.type == ItemID.WoodenCrateHard ||
                item.type == ItemID.IronCrate ||
                item.type == ItemID.IronCrateHard ||
                item.type == ItemID.GoldenCrate ||
                item.type == ItemID.GoldenCrateHard ||
                biomeCrateTypes.Contains(item.type)
            )
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket spawnCrateItemPacket = Mod.GetPacket();
                    spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                    spawnCrateItemPacket.Write((short)goldItemID);
                    spawnCrateItemPacket.Write((int)player.width);
                    spawnCrateItemPacket.Write((int)player.height);
                    spawnCrateItemPacket.Write((int)amountGold); //Drop amount
                    spawnCrateItemPacket.WriteVector2(player.position);
                    spawnCrateItemPacket.Send();
                }
                else if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, goldItemID, amountGold);
                }
            }
            if(item.type == ItemID.JungleFishingCrateHard && NPC.downedMechBossAny)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket spawnCrateItemPacket = Mod.GetPacket();
                    spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                    spawnCrateItemPacket.Write((short)ItemID.LifeFruit);
                    spawnCrateItemPacket.Write((int)player.width);
                    spawnCrateItemPacket.Write((int)player.height);
                    spawnCrateItemPacket.Write((int)1); //Drop amount
                    spawnCrateItemPacket.WriteVector2(player.position);
                    spawnCrateItemPacket.Send();
                }
                else if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.LifeFruit, 1);
                }
            }
        }

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.Phantasm) 
            {
                item.damage = 70;
            }
            if (item.type == ItemID.FetidBaghnakhs) 
            {
                item.damage = 70;
                item.useTime = 7;
                item.useAnimation = 7;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.ChlorophyteShotbow)
            {
                item.damage = 40;
            }
            if (item.type == ItemID.DD2PhoenixBow)
            {
                item.damage = 40;
            }
            if (item.type == ItemID.CactusBreastplate) 
            {
                item.defense += 2;
            }
            if (item.type == ItemID.CactusSword) 
            {
                item.useTime = 25;
                item.useAnimation = 25;
                item.shootsEveryUse = true;
                item.knockBack = 5f;
            }
            if (item.type == ItemID.EnchantedSword) 
            {
                item.damage = 24;
                item.useTime = 18;
                item.useAnimation = 18;
                item.shootsEveryUse = true;
                item.knockBack = 5.25f;
            }
            if (item.type == ItemID.FalconBlade) 
            {
                item.damage = 30;
                item.useTime = 15;
                item.useAnimation = 15;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.BeamSword) 
            {
                item.useTime = 30;
                item.useAnimation = 15;
                item.attackSpeedOnlyAffectsWeaponAnimation = false;
                item.GetGlobalItem<WDALTItemUtil>().attackSpeedRoundingErrorProtection = true;
            }
            if (item.type == ItemID.BeesKnees) 
            {
                item.damage = 26;
            }
            if (item.type == ItemID.ClockworkAssaultRifle) 
            {
                item.damage = 19;
            }
            if (item.type == ItemID.Marrow) 
            {
                item.damage = 64;
                item.useTime = 16;
                item.useAnimation = 16;
                item.shootsEveryUse = true;
                item.crit = 12;
                item.ArmorPenetration = 32;
            }
            if (item.type == ItemID.FlintlockPistol) 
            {
                item.damage = 15;
            }
            if (item.type == ItemID.FlareGun)
            {
                item.damage = 8;
                item.crit = 4;
            }
            if (item.type == ItemID.ChristmasTreeSword)
            {
                item.damage = 95;
            }
            if (item.type == ItemID.ZapinatorOrange)
            {
                item.buffType = BuffID.Electrified;
                item.buffTime = 240;
            }
            if (item.type == ItemID.ZapinatorGray)
            {
                item.buffType = BuffID.Electrified;
                item.buffTime = 240;
            }
            if (item.type == ItemID.LastPrism)
            {
                item.mana = 8;
            }
            if (item.type == ItemID.ClingerStaff)
            {
                item.damage = 50;
            }
            if (item.type == ItemID.RazorbladeTyphoon)
            {
                item.useTime = 20;
                item.useAnimation = 20;
                item.shootsEveryUse = true;
                item.mana = 10;
                item.damage = 60;
            }
            if (item.type == ItemID.OpticStaff)
            {
                item.damage = 32;
            }
            if (item.type == ItemID.XenoStaff)
            {
                item.damage = 44;
            }
            if (item.type == ItemID.StardustDragonStaff)
            {
                item.damage = 50;
            }
            if (item.type == ItemID.StardustCellStaff)
            {
                item.damage = 75;
            }
            if (item.type == ItemID.EmpressBlade) //Terraprisma
            {
                item.damage = 100;
            }
            if
            (
                item.type == ItemID.StardustHelmet ||
                item.type == ItemID.StardustBreastplate ||
                item.type == ItemID.StardustLeggings ||
                item.type == ItemID.NebulaHelmet ||
                item.type == ItemID.NebulaBreastplate ||
                item.type == ItemID.NebulaLeggings
            )
            {
                item.defense += 2;
            }
            if (item.type == ItemID.TikiMask)
            {
                item.defense += 1;
                item.shopCustomPrice = 300000;
            }
            if
            (
                item.type == ItemID.TikiShirt ||
                item.type == ItemID.TikiPants
            )
            {
                item.shopCustomPrice = 300000;
            }
            if
            (
                item.type == ItemID.SpookyHelmet ||
                item.type == ItemID.SpookyLeggings
            )
            {
                item.defense += 2;
            }
            if (item.type == ItemID.SpookyBreastplate)
            {
                item.defense += 4;
            }
            if (item.type == ItemID.SpectreRobe)
            {
                item.defense += 2;
            }
            
            // Buff all pre-hardmode summon armors

            if
            (
                item.type == ItemID.BeeHeadgear ||
                item.type == ItemID.BeeBreastplate ||
                item.type == ItemID.BeeGreaves ||
                item.type == ItemID.FossilHelm ||
                item.type == ItemID.FossilShirt ||
                item.type == ItemID.FossilPants ||
                item.type == ItemID.ObsidianHelm ||
                item.type == ItemID.ObsidianShirt ||
                item.type == ItemID.ObsidianPants ||
                item.type == ItemID.FlinxFurCoat
            )
            {
                item.defense += 1;
            }

            if
            (
                item.type == ItemID.SpiderMask ||
                item.type == ItemID.SpiderBreastplate ||
                item.type == ItemID.SpiderGreaves ||
                item.type == ItemID.AncientBattleArmorHat || //Forbidden Mask
                item.type == ItemID.AncientBattleArmorShirt || //Forbidden Robes
                item.type == ItemID.AncientBattleArmorPants //Forbidden Treads
            )
            {
                item.defense += 2;
            }


            // Buff pre-hardmode whips

            if
            (
                item.type == ItemID.BlandWhip || //Leather Whip
                item.type == ItemID.ThornWhip || //Snapthorn
                item.type == ItemID.BoneWhip //Spinal Tap
            )
            {
                item.damage += 5;
            }

            // Buff hardmode whips

            if
            (
                item.type == ItemID.FireWhip || //Firecracker
                item.type == ItemID.CoolWhip ||
                item.type == ItemID.SwordWhip //Durendal
            )
            {
                item.damage += 10;
            }
            if(item.type == ItemID.ScytheWhip) //Dark Harvest
            {
                item.damage += 25;
                item.useTime = 25;
                item.useAnimation = 25;
                item.shootsEveryUse = true;
            }
            if(item.type == ItemID.MaceWhip) //Morning Star
            {
                item.damage += 15;
            }
            if(item.type == ItemID.RainbowWhip) //Kaleidoscope
            {
                item.damage += 20;
            }

            if
            (
                item.type == ItemID.ElectrosphereLauncher ||
                item.type == ItemID.SniperRifle
            )
            {
                item.autoReuse = true;
            }
            if (item.type == ItemID.StarWrath)
            {
                item.damage = 220;
            }
            if (item.type == ItemID.TrueExcalibur)
            {
                item.damage = 125;
                item.useTime = 16;
                item.useAnimation = 16;
                item.shootsEveryUse = true;
                item.knockBack = 14;
            }
            if (item.type == ItemID.TrueNightsEdge)
            {
                item.damage = 75;
                item.useTime = 32;
                item.useAnimation = 32;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.TerraBlade)
            {
                item.damage = 100;
                item.useTime = 16;
                item.useAnimation = 16;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.DayBreak)
            {
                item.useTime = 12;
                item.useAnimation = 12;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.SolarEruption)
            {
                item.damage = 125;
            }
            if (item.type == ItemID.IllegalGunParts)
            {
                item.shopCustomPrice = 80000;
            }
            if (item.type == ItemID.AntlionClaw) //Mandible Blade
            {
                item.damage = 20;
            }
            if (item.type == ItemID.ThunderSpear)
            {
                item.damage = 20;
            }
            if (item.type == ItemID.ThunderStaff)
            {
                item.damage = 24;
            }
            if (item.type == ItemID.Starfury)
            {
                item.damage = 18;
                item.useTime = 20;
                item.useAnimation = 20;
                item.shootsEveryUse = true;
            }
            if
            (
                item.type == ItemID.JackOLanternMask ||
                item.type == ItemID.SWATHelmet ||
                item.type == ItemID.RainbowCrystalStaff ||
                item.type == ItemID.RainbowWhip
            )
            {
                item.material = true;
            }
            if (item.type == ItemID.Smolstar) //Blade Staff
            {
                item.damage = 8;
            }
            if (item.type == ItemID.ChlorophytePartisan)
            {
                item.damage = 60;
            }
            if (item.type == ItemID.ChlorophyteClaymore)
            {
                item.useTime = 52;
                item.useAnimation = 26;
                item.attackSpeedOnlyAffectsWeaponAnimation = false;
                item.GetGlobalItem<WDALTItemUtil>().attackSpeedRoundingErrorProtection = true;
            }
            if (item.type == ItemID.Trimarang)
            {
                item.damage = 28;
                item.useTime = 15;
                item.useAnimation = 15;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.Flamarang)
            {
                item.damage = 64;
                item.useTime = 30;
                item.useAnimation = 30;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.DeathSickle)
            {
                item.damage = 65;
            }
            if (item.type == ItemID.FlowerofFire) 
            {
                item.mana = 8;
                item.autoReuse = true;
            }
            if (item.type == ItemID.UnholyTrident) 
            {
                item.mana = 12;
                item.autoReuse = true;
            }

            // Buff all pre-hardmode summons

            if
            (
                item.type == ItemID.AbigailsFlower ||
                item.type == ItemID.BabyBirdStaff || //Finch Staff
                item.type == ItemID.FlinxStaff ||
                item.type == ItemID.SlimeStaff ||
                item.type == ItemID.VampireFrogStaff ||
                item.type == ItemID.HornetStaff ||
                item.type == ItemID.ImpStaff
            )
            {
                item.damage += 2;
            }

            if (item.type == ItemID.ReaverShark)
            {
                item.pick = 100;
            }

            // Buff Ice-Biome related stuff
            // Pre-Hardmode
            if (item.type == ItemID.IceSickle)
            {
                item.damage = 55;
                item.autoReuse = true;
            }
            if (item.type == ItemID.FrostStaff)
            {
                item.damage = 55;
                item.autoReuse = true;
                item.mana = 8;
            }
            if (item.type == ItemID.IceBoomerang)
            {
                item.damage = 24;
                item.autoReuse = true;
                item.useTime = 15;
                item.useAnimation = 15;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.IceBlade)
            {
                item.damage = 18;
                item.autoReuse = true;
                item.useTime = 20;
                item.useAnimation = 20;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.SnowballCannon)
            {
                item.damage = 15;
                item.autoReuse = true;
                item.useTime = 15;
                item.useAnimation = 15;
                item.shootsEveryUse = true;
            }
            //Hardmode
            if (item.type == ItemID.Frostbrand)
            {
                item.damage = 50;
                item.useTime = 22;
                item.useAnimation = 22;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.IceBow) 
            {
                item.damage = 50;
            }
            if (item.type == ItemID.FlowerofFrost) 
            {
                item.mana = 8;
                item.autoReuse = true;
            }
            if (item.type == ItemID.StaffoftheFrostHydra) 
            {
                item.damage = 80;
            }
            if (item.type == ItemID.QueenSpiderStaff)
            {
                item.damage = 60;
            }
            if (item.type == ItemID.HoundiusShootius)
            {
                item.damage = 40;
            }
            if (item.type == ItemID.RainbowCrystalStaff)
            {
                item.damage = 140;
            }
            if (item.type == ItemID.MoonlordTurretStaff)
            {
                item.damage = 120;
            }
            if
            (
                item.type == ItemID.PoisonStaff ||
                item.type == ItemID.VenomStaff
            )
            {
                item.damage = 50;
                item.crit = 6;
            }
            if
            (
                item.type == ItemID.SkyFracture
            )
            {
                item.damage = 48;
            }

            // Make boss summoning items non-consumable

            if
            (
                item.type == ItemID.SlimeCrown ||
                item.type == ItemID.SuspiciousLookingEye ||
                item.type == ItemID.WormFood ||
                item.type == ItemID.BloodySpine ||
                item.type == ItemID.Abeemination ||
                item.type == ItemID.DeerThing ||
                item.type == ItemID.MechanicalWorm ||
                item.type == ItemID.MechanicalEye ||
                item.type == ItemID.MechanicalSkull ||
                item.type == ItemID.MechdusaSummon ||
                item.type == ItemID.CelestialSigil
            )
            {
                item.consumable = false;
                item.maxStack = 1;
            }
            if(item.type == ItemID.WormholePotion)
            {
                item.consumable = false;
                item.maxStack = 1;
            }
        }
    }
}
