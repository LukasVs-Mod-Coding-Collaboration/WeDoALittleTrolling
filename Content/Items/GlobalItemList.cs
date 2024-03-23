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

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Prefixes;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;
using Terraria.Utilities;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;
using WeDoALittleTrolling.Content.Tiles;
using Terraria.GameContent.Items;

namespace WeDoALittleTrolling.Content.Items
{
    internal class GlobalItemList : GlobalItem
    {
        public override bool InstancePerEntity => false;

        public static UnifiedRandom random = new UnifiedRandom();
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

        public static readonly int[] fishronWeapons =
        {
            ItemID.Tsunami,
            ItemID.TempestStaff,
            ItemID.RazorbladeTyphoon,
            ItemID.Flairon,
            ItemID.BubbleGun
        };

        public static readonly int[] empressWeapons =
        {
            ItemID.FairyQueenMagicItem,
            ItemID.PiercingStarlight,
            ItemID.RainbowWhip,
            ItemID.FairyQueenRangedItem,
            ItemID.SparkleGuitar,
            ItemID.EmpressBlade
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
            //I'm sorry, hit
            /*
            else if
            (
                item.type == ItemID.LandMine
            )
            {
                player.chatOverhead.NewMessage("You may not use this item.", 180);
                return false;
            }
            */
            return base.CanUseItem(item, player);
        }

        public static void RegisterHooks()
        {
            On_Player.ItemCheck_UseTeleportRod += On_Player_ItemCheck_UseTeleportRod;
        }

        public static void UnregisterHooks()
        {
            On_Player.ItemCheck_UseTeleportRod -= On_Player_ItemCheck_UseTeleportRod;
        }

        public static void On_Player_ItemCheck_UseTeleportRod(On_Player.orig_ItemCheck_UseTeleportRod orig, Player self, Item sItem)
        {
            bool harmony = (sItem.type == ItemID.RodOfHarmony);
            if (harmony)
            {
                Item imanginaryRodOfDiscord = new Item(ItemID.RodofDiscord, 1, 0);
                orig.Invoke(self, imanginaryRodOfDiscord);
                if (self.itemAnimation > 0 && self.chaosState)
                {
                    int idx = self.FindBuffIndex(BuffID.ChaosState);
                    if (idx >= 0 && idx < self.buffTime.Length)
                    {
                        self.buffTime[idx] = 180;
                    }
                }
            }
            orig.Invoke(self, sItem);
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            if (GlobalTiles.BuffTilesItemIDs.Contains(item.type))
            {
                SoundStyle buffActivateSoundStyle = SoundID.Item4;
                bool playSound = true;
                switch (item.type)
                {
                    case ItemID.WarTable:
                        buffActivateSoundStyle = SoundID.Item4;
                        break;
                    case ItemID.BewitchingTable:
                        buffActivateSoundStyle = SoundID.Item4;
                        break;
                    case ItemID.SharpeningStation:
                        buffActivateSoundStyle = SoundID.Item37;
                        break;
                    case ItemID.CrystalBall:
                        buffActivateSoundStyle = SoundID.Item4;
                        break;
                    case ItemID.AmmoBox:
                        buffActivateSoundStyle = SoundID.Item149;
                        break;
                    case ItemID.SliceOfCake:
                        buffActivateSoundStyle = SoundID.Item2;
                        break;
                    default:
                        playSound = false;
                        break;
                }
                if (playSound)
                {
                    SoundEngine.PlaySound(buffActivateSoundStyle, player.Center);
                }
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
            if (item.type == ItemID.SpectreHood)
            {
                player.statManaMax2 += 60;
            }
            if
            (
                item.type == ItemID.MiningHelmet &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerChestplateEquipped(ItemID.MiningShirt) &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.MiningPants)
            )
            {
                player.statDefense += 5;
            }
            if
            (
                item.type == ItemID.ChlorophyteMask ||
                item.type == ItemID.ChlorophyteHelmet ||
                item.type == ItemID.ChlorophyteHeadgear ||
                item.type == ItemID.ChlorophytePlateMail ||
                item.type == ItemID.ChlorophyteGreaves
            )
            {
                player.lifeRegen += 2;
            }
            base.UpdateEquip(item, player);
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.prefix == PrefixID.Arcane)
            {
                player.statManaMax2 += 80;
            }
            if (item.prefix == PrefixID.Warding)
            {
                player.endurance += 0.02f;
            }
            if (item.type == ItemID.AvengerEmblem)
            {
                player.GetAttackSpeed(DamageClass.Generic) += 0.06f;
            }
            if (item.type == ItemID.DestroyerEmblem)
            {
                player.GetCritChance(DamageClass.Generic) += 2f;
            }
            if (item.type == ItemID.SniperScope)
            {
                player.GetDamage(DamageClass.Ranged) += 0.02f;
            }
            if (item.type == ItemID.ReconScope)
            {
                player.GetDamage(DamageClass.Ranged) += 0.07f;
                player.GetCritChance(DamageClass.Ranged) += 5f;
            }
            if (item.type == ItemID.PygmyNecklace)
            {
                player.maxMinions += 1;
            }
            if (item.type == ItemID.AnkhShield)
            {
                player.statDefense += 4;
                player.DefenseEffectiveness *= 1.16f;
            }
            base.UpdateAccessory(item, player, hideVisual);
        }

        /* No Wings Challenge Code
        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
        {
            if(item.wingSlot > -1 && item.wingSlot < ArmorIDs.Wing.Sets.Stats.Length)
            {
                if(ArmorIDs.Wing.Sets.Stats[item.wingSlot].FlyTime > 0)
                {
                    return false;
                }
            }
            return base.CanEquipAccessory(item, player, slot, modded);
        }
        */

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                (
                    equippedItem.type == ItemID.SniperScope ||
                    equippedItem.type == ItemID.ReconScope
                ) &&
                (
                    incomingItem.type == ItemID.SniperScope ||
                    incomingItem.type == ItemID.ReconScope
                )
            )
            {
                return false;
            }
            if
            (
                (
                    equippedItem.type == ItemID.WormScarf ||
                    equippedItem.type == ItemID.BrainOfConfusion ||
                    equippedItem.type == ModContent.ItemType<SoulPoweredShield>() ||
                    equippedItem.type == ModContent.ItemType<CrimsonAssassinGear>()
                ) &&
                (
                    incomingItem.type == ItemID.WormScarf ||
                    incomingItem.type == ItemID.BrainOfConfusion ||
                    incomingItem.type == ModContent.ItemType<SoulPoweredShield>() ||
                    incomingItem.type == ModContent.ItemType<CrimsonAssassinGear>()
                )
            )
            {
                return false;
            }
            return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
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
            if
            (
                (
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.HallowedHelmet) ||
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.HallowedHeadgear) ||
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.HallowedMask) ||
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.HallowedHood) ||
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.AncientHallowedHelmet) ||
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.AncientHallowedHeadgear) ||
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.AncientHallowedMask) ||
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.AncientHallowedHood)
                ) &&
                (
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerChestplateEquipped(ItemID.HallowedPlateMail) ||
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerChestplateEquipped(ItemID.AncientHallowedPlateMail)
                ) &&
                (
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.HallowedGreaves) ||
                    player.GetModPlayer<WDALTPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.AncientHallowedGreaves)
                )
            )
            {
                player.setBonus += "\nThis works even when the Devastated debuff is active";
            }
            if
            (
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.BeetleHelmet) &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerChestplateEquipped(ItemID.BeetleShell) &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.BeetleLeggings)
            )
            {
                player.setBonus += "\nThis is not affected by the Wrecked Resistance debuff";
            }
            if
            (
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.SolarFlareHelmet) &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerChestplateEquipped(ItemID.SolarFlareBreastplate) &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.SolarFlareLeggings)
            )
            {
                player.setBonus += "\nThis is not affected by the Wrecked Resistance debuff";
            }
            if
            (
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerHelmetEquipped(ItemID.MiningHelmet) &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerChestplateEquipped(ItemID.MiningShirt) &&
                player.GetModPlayer<WDALTPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.MiningPants)
            )
            {
                player.setBonus += "\n5 defense";
            }
        }

        //Adjust Tooltips accordingly
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.prefix == PrefixID.Arcane)
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "PrefixAccMaxMana") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "+100 mana");
            }
            if (item.prefix == PrefixID.Warding)
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "PrefixAccDefense") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "+4 defense\n+2% reduced damage taken");
            }
            if (item.type == ItemID.SpectreHood)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "PrefixAccMaxMana", "Increases maximum mana by 60");
                tooltips.Add(extraManaLine);
            }
            if (item.type == ItemID.ScytheWhip) //Dark Harvest
            {
                TooltipLine extraLoreLine = new TooltipLine(Mod, "Lore", "\"The harvest is bountiful this year\"");
                tooltips.Add(extraLoreLine);
            }
            if (item.type == ItemID.ChlorophytePartisan)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "WeaponLeechingDescription", "Recoveres 5% of damage as health");
                tooltips.Add(extraManaLine);
            }
            if (item.type == ItemID.ThunderStaff)
            {
                TooltipLine extraThunderspellLine = new TooltipLine(Mod, "WeaponLeechingDescription", "\"I CAST THUNDERSPELL!!\"");
                tooltips.Add(extraThunderspellLine);
            }
            if
            (
                item.type == ItemID.ChlorophyteMask ||
                item.type == ItemID.ChlorophyteHelmet ||
                item.type == ItemID.ChlorophyteHeadgear ||
                item.type == ItemID.ChlorophytePlateMail ||
                item.type == ItemID.ChlorophyteGreaves
            )
            {
                TooltipLine extraLifeRegenLine = new TooltipLine(Mod, "ArmorLifeRegenDescription", "Slowly regenerates life");
                tooltips.Add(extraLifeRegenLine);
            }
            if (item.type == ItemID.ChlorophyteShotbow)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "WeaponArrowConversionDescription", "Converts wooden arrows into chlorophyte arrows");
                tooltips.Add(extraManaLine);
            }
            if (item.type == ItemID.MoltenFury)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "WeaponArrowConversionDescription", "Converts wooden arrows into hellfire arrows");
                tooltips.Add(extraManaLine);
            }
            if (item.type == ItemID.DaedalusStormbow)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "WeaponArrowConversionDescription", "Converts wooden arrows into holy arrows");
                tooltips.Add(extraManaLine);
            }
            if (item.type == ItemID.Moondial)
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip0") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "Allows time to fast forward to dusk");
            }
            if (item.type == ItemID.Sundial)
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip0") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "Allows time to fast forward to dawn");
            }
            if (item.type == ItemID.WeatherVane)
            {
                TooltipLine tileFunctionLine = new TooltipLine(Mod, "TileFunctionDescription", "Allows rain to start, intesify and stop");
                tooltips.Add(tileFunctionLine);
            }
            if (item.type == ItemID.SkyMill)
            {
                TooltipLine tileFunctionLine = new TooltipLine(Mod, "TileFunctionDescription", "Allows wind to start, intesify, change direction and stop");
                tooltips.Add(tileFunctionLine);
            }
            if (item.type == ItemID.DjinnLamp)
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
                item.type == ItemID.FrostStaff ||
                item.type == ItemID.UnholyTrident ||
                item.type == ItemID.BookStaff ||
                item.type == ItemID.LunarFlareBook ||
                item.type == ItemID.BubbleGun
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
                TooltipLine extraCritChanceLine = new TooltipLine(Mod, "ExtraCritChanceDescription", "Projectiles have a 15% chance to land a critical strike");
                tooltips.Add(extraCritChanceLine);
            }
            if
            (
                item.type == ItemID.RazorbladeTyphoon ||
                item.type == ItemID.BubbleGun
            )
            {
                TooltipLine extraCritChanceLine = new TooltipLine(Mod, "ExtraArmorPenetrationDescription", "Ignores 20 points of enemy Defense");
                tooltips.Add(extraCritChanceLine);
            }
            if
            (
                item.type == ItemID.PygmyNecklace
            )
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip0") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "Increases your max number of minions by 2");
            }
            if
            (
                item.type == ItemID.AvengerEmblem
            )
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip0") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "12% increased damage\n6% increased attack speed");
            }
            if
            (
                item.type == ItemID.DestroyerEmblem
            )
            {
                List<TooltipLine> infoLine2 = tooltips.FindAll(t => (t.Name == "Tooltip1") && (t.Mod == "Terraria"));
                infoLine2.ForEach(t => t.Text = "10% increased critical strike chance");
            }
            if
            (
                item.type == ItemID.SniperScope
            )
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip1") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "12% increased ranged damage\n10% increased ranged critical strike chance");
            }
            if
            (
                item.type == ItemID.ReconScope
            )
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip1") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "17% increased ranged damage\n15% increased ranged critical strike chance");
            }
            if
            (
                item.type == ItemID.AnkhShield
            )
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Defense") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "8 defense\n16% increased defense effectiveness");
            }
            if
            (
                item.type == ItemID.RodOfHarmony
            )
            {
                TooltipLine chaosStateLine = new TooltipLine(Mod, "ChaosState", "Causes the chaos state");
                tooltips.Add(chaosStateLine);
            }
            if (item.DamageType == DamageClass.Throwing)
            {
                TooltipLine throwerClassExtraDmgLine = new TooltipLine(Mod, "WDALTPowerup", "+50% damage (From WeDoALittleTrolling)")
                {
                    IsModifier = true,
                    IsModifierBad = false,
                };
                tooltips.Add(throwerClassExtraDmgLine);
            }
            if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity && item.DamageType != null && item.DamageType.Mod != null && item.DamageType.Mod.Name != null)
            {
                if (item.DamageType.Mod.Name == WDALTModSystem.thoriumModName)
                {
                    TooltipLine thoriumClassExtraDmgLine = new TooltipLine(Mod, "WDALTPowerup", "+50% damage (From WeDoALittleTrolling)")
                    {
                        IsModifier = true,
                        IsModifierBad = false,
                    };
                    tooltips.Add(thoriumClassExtraDmgLine);
                }
            }
            base.ModifyTooltips(item, tooltips);
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (item.DamageType == DamageClass.Throwing)
            {
                damage += 0.5f;
            }
            if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity && item.DamageType != null && item.DamageType.Mod != null && item.DamageType.Mod.Name != null)
            {
                if (item.DamageType.Mod.Name == WDALTModSystem.thoriumModName)
                {
                    damage += 0.5f;
                }
            }
            base.ModifyWeaponDamage(item, player, ref damage);
        }

        public override void RightClick(Item item, Player player)
        {
            if
            (
                item.type == ItemID.IronCrate ||
                item.type == ItemID.IronCrateHard
            )
            {
                if (random.Next(0, 8) == 0)
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
                if (random.Next(0, 8) == 0)
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
                if (random.Next(0, 4) == 0)
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
                if (random.Next(0, 5) == 0)
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
            if
            (
                item.type == ItemID.OasisCrate ||
                item.type == ItemID.OasisCrateHard
            )
            {
                if (random.Next(0, 5) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((short)ItemID.SandstorminaBottle);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.SandstorminaBottle, 1);
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
            if (item.type == ItemID.OceanCrateHard && NPC.downedFishron && player.anglerQuestsFinished >= 100)
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
            if (item.type == ItemID.JungleFishingCrateHard && NPC.downedMechBossAny)
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

        /*
            You MUST NOT override an item's name while it's being
            rendered as an entity in a world!
            Doing so anyways will result in CATASTROPHIC multiplayer bugs
            such as random items being renamed to your custom name
            despite not even having the correct item id!
            Due to this, we need override item names when the player
            picks up an item and/or the item is
            rendered in a player's inventory.
            TL;DR:
            Calling Item.SetNameOverride() inside SetDefaults()
            and/or PreDrawInWorld() is STRICTLY PROHIBITED!
            Use OnPickup() and/or PreDrawInInventory() instead.
        */

        public override bool OnPickup(Item item, Player player)
        {
            if
            (
                (
                    Main.netMode == NetmodeID.SinglePlayer ||
                    Main.netMode == NetmodeID.MultiplayerClient
                ) &&
                player.whoAmI == Main.myPlayer
            )
            {
                AdjustItemName(item);
            }
            return base.OnPickup(item, player);
        }

        public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if
            (
                Main.netMode == NetmodeID.SinglePlayer ||
                Main.netMode == NetmodeID.MultiplayerClient
            )
            {
                AdjustItemName(item);
            }
            return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        /*
            Calling @function AdjustItemName() from SetDefaults() and/or PreDrawInWorld() is STRICTLY PROHIBITED!
        */
        public static void AdjustItemName(Item item)
        {
            if
            (
                item.type == ItemID.AncientBattleArmorHat//Forbidden Mask
            )
            {
                item.SetNameOverride("Ancient Battle Mask");
            }
            if
            (
                item.type == ItemID.AncientBattleArmorShirt
            )
            {
                item.SetNameOverride("Ancient Battle Robes");
            }
            if
            (
                item.type == ItemID.AncientBattleArmorPants //Forbidden Treads
            )
            {
                item.SetNameOverride("Ancient Battle Treads");
            }
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (item.type == ItemID.MoltenFury)
            {
                if (type == ProjectileID.FireArrow)
                {
                    type = ProjectileID.HellfireArrow;
                }
            }
            if (item.type == ItemID.ChlorophyteShotbow)
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ProjectileID.ChlorophyteArrow;
                }
            }
            if (item.type == ItemID.DaedalusStormbow)
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ProjectileID.HolyArrow;
                }
            }
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override void SetStaticDefaults()
        {
            PrefixLegacy.ItemSets.GunsBows[ItemID.FlareGun] = true;
        }

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.Phantasm)
            {
                item.damage = 50;
                item.crit = 1;
            }
            if (item.type == ItemID.VortexBeater)
            {
                item.damage = 60;
                item.crit = 2;
            }
            if (item.type == ItemID.FetidBaghnakhs)
            {
                item.useTime = 7;
                item.useAnimation = 7;
            }
            if (item.type == ItemID.LandMine)
            {
                item.value = Item.buyPrice(silver: 50);
            }
            if (item.type == ItemID.ChlorophyteShotbow)
            {
                if (item.damage <= 40)
                {
                    item.damage = 40;
                }
                item.useTime = 16;
                item.useAnimation = 16;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.Megashark)
            {
                item.damage = 30;
                item.useTime = 6;
                item.useAnimation = 6;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.DD2PhoenixBow)
            {
                item.damage = 44;
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
            if (item.type == ItemID.BoneSword)
            {
                if (item.damage <= 24)
                {
                    item.damage = 24;
                }
                item.useTime = 20;
                item.useAnimation = 20;
                item.shootsEveryUse = true;
                item.shoot = ProjectileID.BoneGloveProj;
                item.shootSpeed = 10f;
            }
            if (item.type == ItemID.EnchantedSword)
            {
                if (item.damage <= 24)
                {
                    item.damage = 24;
                }
                item.useTime = 18;
                item.useAnimation = 18;
                item.shootsEveryUse = true;
                item.knockBack = 5.25f;
            }
            if (item.type == ItemID.TheRottedFork)
            {
                if (item.damage <= 20)
                {
                    item.damage = 20;
                }
                item.useTime = 24;
                item.useAnimation = 24;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.FlamingMace)
            {
                if (item.damage <= 24)
                {
                    item.damage = 24;
                }
            }
            if (item.type == ItemID.BlueMoon)
            {
                if (item.damage <= 64)
                {
                    item.damage = 64;
                }
            }
            if (item.type == ItemID.Sunfury)
            {
                if (item.damage <= 74)
                {
                    item.damage = 74;
                }
            }
            if (item.type == ItemID.DarkLance)
            {
                if (item.damage <= 40)
                {
                    item.damage = 40;
                }
                item.useTime = 20;
                item.useAnimation = 20;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.Gungnir)
            {
                if (item.damage <= 95)
                {
                    item.damage = 95;
                }
            }
            if (item.type == ItemID.MonkStaffT1)
            {
                if (item.damage <= 70)
                {
                    item.damage = 70;
                }
            }
            if (item.type == ItemID.MonkStaffT2)
            {
                if (item.damage <= 90)
                {
                    item.damage = 90;
                }
            }
            if (item.type == ItemID.MonkStaffT3)
            {
                if (item.damage <= 150)
                {
                    item.damage = 150;
                }
            }
            if (item.type == ItemID.Terragrim)
            {
                if (item.damage <= 20)
                {
                    item.damage = 20;
                }
            }
            if (item.type == ItemID.Arkhalis)
            {
                if (item.damage <= 35)
                {
                    item.damage = 35;
                }
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
            if (item.type == ItemID.ClockworkAssaultRifle)
            {
                item.damage = 24;
            }
            if (item.type == ItemID.Marrow)
            {
                if (item.damage <= 64)
                {
                    item.damage = 64;
                }
                item.useTime = 16;
                item.useAnimation = 16;
                item.shootsEveryUse = true;
                item.crit = 4;
            }
            if (item.type == ItemID.FlintlockPistol)
            {
                item.damage = 15;
            }
            if (item.type == ItemID.FlareGun)
            {
                item.damage = 12;
                item.crit = 4;
                item.knockBack = 2f;
                item.DamageType = DamageClass.Ranged;
            }
            if (item.type == ItemID.ChristmasTreeSword)
            {
                item.shootSpeed = 10f;
            }
            if (item.type == ItemID.TheHorsemansBlade)
            {
                item.useTime = 15;
                item.useAnimation = 15;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.BatScepter)
            {
                item.damage = 50;
            }
            if (item.type == ItemID.CandyCornRifle)
            {
                item.ArmorPenetration = 40;
                item.damage = 80;
                item.useTime = 6;
                item.useAnimation = 6;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.JackOLanternLauncher)
            {
                item.damage = 80;
                item.useTime = 24;
                item.useAnimation = 24;
                item.crit = 20;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.NailGun)
            {
                item.damage = 100;
            }
            if (item.type == ItemID.MagnetSphere)
            {
                item.damage = 64;
            }
            /*
            if (item.type == ItemID.ReaverShark)
            {
                item.pick = 100;
            }
            */
            /*
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
            */
            if (item.type == ItemID.LunarFlareBook)
            {
                item.damage = 120;
                item.mana = 8;
            }
            if (item.type == ItemID.LastPrism)
            {
                item.damage = 100;
                item.mana = 8;
            }
            if (item.type == ItemID.ClingerStaff)
            {
                item.damage = 60;
                item.knockBack = 10f;
            }
            if (item.type == ItemID.RazorbladeTyphoon)
            {
                item.damage = 100;
                item.useTime = 20;
                item.useAnimation = 20;
                item.shootsEveryUse = true;
                item.ArmorPenetration = 20;
                item.mana = 10;
            }
            if (item.type == ItemID.BubbleGun)
            {
                item.damage = 64;
                item.useTime = 8;
                item.useAnimation = 8;
                item.shootsEveryUse = true;
                item.ArmorPenetration = 20;
                item.mana = 4;
            }
            if (item.type == ItemID.OpticStaff)
            {
                item.damage = 32;
            }
            if
            (
                item.type == ItemID.PygmyStaff ||
                item.type == ItemID.StormTigerStaff ||
                item.type == ItemID.DeadlySphereStaff ||
                item.type == ItemID.TempestStaff ||
                item.type == ItemID.RavenStaff
            )
            {
                item.damage += 10;
            }
            if (item.type == ItemID.XenoStaff)
            {
                item.damage = 42;
            }
            if (item.type == ItemID.Xenopopper)
            {
                item.damage = 48;
                item.useTime = 20;
                item.useAnimation = 20;
                item.shootsEveryUse = true;
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
                item.damage = 125;
            }
            if
            (
                item.type == ItemID.StardustHelmet ||
                item.type == ItemID.StardustBreastplate ||
                item.type == ItemID.StardustLeggings ||
                item.type == ItemID.NebulaHelmet ||
                item.type == ItemID.NebulaBreastplate ||
                item.type == ItemID.NebulaLeggings ||
                item.type == ItemID.VortexHelmet ||
                item.type == ItemID.VortexBreastplate ||
                item.type == ItemID.VortexLeggings ||
                item.type == ItemID.SolarFlareHelmet ||
                item.type == ItemID.SolarFlareBreastplate ||
                item.type == ItemID.SolarFlareLeggings
            )
            {
                item.defense += 2;
            }
            if
            (
                item.type == ItemID.TikiMask ||
                item.type == ItemID.TikiShirt ||
                item.type == ItemID.TikiPants
            )
            {
                item.shopCustomPrice = 300000;
            }
            if
            (
                item.type == ItemID.SpookyHelmet ||
                item.type == ItemID.SpookyLeggings ||
                item.type == ItemID.SpookyBreastplate
            )
            {
                item.defense += 2;
            }
            if (item.type == ItemID.SpectreMask)
            {
                item.defense += 4;
            }
            if (item.type == ItemID.SpectreRobe)
            {
                item.defense += 2;
            }
            if
            (
                item.type == ItemID.ShroomiteHeadgear ||
                item.type == ItemID.ShroomiteMask ||
                item.type == ItemID.ShroomiteHelmet
            )
            {
                item.defense += 5;
            }
            if
            (
                item.type == ItemID.ShroomiteBreastplate ||
                item.type == ItemID.ShroomiteLeggings
            )
            {
                item.defense += 4;
            }
            if (item.type == ItemID.BeetleShell)
            {
                item.defense += 4;
            }
            if (item.type == ItemID.BeetleLeggings)
            {
                item.defense += 3;
            }

            // Buff all pre-hardmode summon armors

            if
            (
                item.type == ItemID.BeeHeadgear ||
                item.type == ItemID.BeeBreastplate ||
                item.type == ItemID.BeeGreaves ||
                item.type == ItemID.ObsidianHelm ||
                item.type == ItemID.ObsidianShirt ||
                item.type == ItemID.ObsidianPants ||
                item.type == ItemID.FlinxFurCoat
            )
            {
                item.defense += 2;
            }
            if
            (
                item.type == ItemID.FossilHelm ||
                item.type == ItemID.FossilShirt ||
                item.type == ItemID.FossilPants
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
                item.defense += 4;
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

            if (item.type == ItemID.FireWhip) //Firecracker
            {
                item.damage += 10;
            }
            if (item.type == ItemID.CoolWhip) 
            {
                item.damage += 15;
            }
            if (item.type == ItemID.SwordWhip) //Durendal
            {
                item.damage += 20;
            }
            if (item.type == ItemID.ScytheWhip) //Dark Harvest
            {
                item.damage += 25;
                item.useTime = 25;
                item.useAnimation = 25;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.MaceWhip) //Morning Star
            {
                item.damage += 15;
            }
            if (item.type == ItemID.RainbowWhip) //Kaleidoscope
            {
                item.damage += 20;
            }

            if
            (
                item.type == ItemID.ElectrosphereLauncher
            )
            {
                item.autoReuse = true;
            }
            if (item.type == ItemID.StarWrath)
            {
                item.damage = 110;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.Meowmere)
            {
                item.damage = 300;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.Terrarian)
            {
                item.damage = 165;
            }
            if (item.type == ItemID.Seedler)
            {
                item.damage = 80;
                item.useTime = 24;
                item.useAnimation = 24;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.TrueExcalibur)
            {
                item.damage = 125;
                item.useTime = 16;
                item.useAnimation = 16;
                item.shootsEveryUse = true;
                item.knockBack = 14;
            }
            if (item.type == ItemID.Excalibur)
            {
                item.damage = 115;
            }
            if (item.type == ItemID.TrueNightsEdge)
            {
                if (item.damage <= 75)
                {
                    item.damage = 75;
                }
                item.useTime = 32;
                item.useAnimation = 32;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.TerraBlade)
            {
                item.damage = 90;
            }
            if (item.type == ItemID.DD2SquireBetsySword)
            {
                item.useTime = 16;
                item.useAnimation = 16;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.InfluxWaver)
            {
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.PossessedHatchet)
            {
                item.damage = 90;
                item.useTime = 12;
                item.useAnimation = 12;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.PaladinsHammer)
            {
                item.damage = 100;
                item.useTime = 12;
                item.useAnimation = 12;
                item.shootsEveryUse = true;
                item.shootSpeed = 24f;
            }
            if (item.type == ItemID.DayBreak)
            {
                item.useTime = 14;
                item.useAnimation = 14;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.SolarEruption)
            {
                if (item.damage <= 125)
                {
                    item.damage = 125;
                }
            }
            if (item.type == ItemID.IllegalGunParts)
            {
                item.shopCustomPrice = 80000;
            }
            if (item.type == ItemID.AntlionClaw) //Mandible Blade
            {
                if (item.damage <= 20)
                {
                    item.damage = 20;
                }
            }
            if (item.type == ItemID.ChainKnife)
            {
                item.damage = 16;
                if(item.Variant == ItemVariants.StrongerVariant)
                {
                    item.damage = 75;
                }
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
            if (item.type == ItemID.SpiderStaff)
            {
                if (item.damage <= 32)
                {
                    item.damage = 32;
                }
            }
            if (item.type == ItemID.Smolstar) //Blade Staff
            {
                if (item.damage <= 8)
                {
                    item.damage = 8;
                }
            }
            if (item.type == ItemID.ChlorophytePartisan)
            {
                if (item.damage <= 60)
                {
                    item.damage = 60;
                }
            }
            if
            (
                item.type == ItemID.ChlorophyteHeadgear ||
                item.type == ItemID.ChlorophyteHelmet ||
                item.type == ItemID.ChlorophyteMask ||
                item.type == ItemID.ChlorophytePlateMail ||
                item.type == ItemID.ChlorophyteGreaves
            )
            {
                item.defense += 3;
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
                if (item.damage <= 60)
                {
                    item.damage = 60;
                }
                item.crit = 2;
            }
            if (item.type == ItemID.TitaniumSword)
            {
                item.damage = 100;
            }
            if (item.type == ItemID.TitaniumTrident)
            {
                item.damage = 90;
            }
            if (item.type == ItemID.TitaniumWaraxe)
            {
                item.damage = 140;
            }
            if (item.type == ItemID.TitaniumRepeater)
            {
                item.useTime = 12;
                item.useAnimation = 12;
            }
            if (item.type == ItemID.TitaniumHelmet)
            {
                item.defense += 10;
            }
            if (item.type == ItemID.TitaniumMask)
            {
                item.defense += 3;
            }
            if (item.type == ItemID.AdamantiteHelmet)
            {
                item.defense += 6;
            }
            if (item.type == ItemID.AdamantiteSword)
            {
                item.damage = 100;
            }
            if (item.type == ItemID.AdamantiteGlaive)
            {
                item.damage = 90;
            }
            if (item.type == ItemID.AdamantiteWaraxe)
            {
                item.damage = 140;
            }
            if (item.type == ItemID.AdamantiteRepeater)
            {
                item.useTime = 13;
                item.useAnimation = 13;
            }
            if (item.type == ItemID.HallowedRepeater)
            {
                item.useTime = 12;
                item.useAnimation = 12;
            }
            if (item.type == ItemID.FlowerofFire)
            {
                item.mana = 8;
                item.autoReuse = true;
            }
            if (item.type == ItemID.UnholyTrident)
            {
                item.damage = 60;
                item.mana = 20;
                item.autoReuse = true;
            }
            if
            (
                item.type == ItemID.PoisonStaff ||
                item.type == ItemID.VenomStaff
            )
            {
                if (item.damage <= 50)
                {
                    item.damage = 40;
                }
            }
            if (item.type == ItemID.BlizzardStaff)
            {
                item.damage = 64;
                item.UseSound = SoundID.Item9;
            }
            if (item.type == ItemID.NebulaBlaze)
            {
                item.damage = 160;
                item.mana = 8;
            }
            if (item.type == ItemID.NebulaArcanum)
            {
                item.damage = 80;
                item.mana = 20;
            }

            // Buff all pre-hardmode summons

            if
            (
                item.type == ItemID.AbigailsFlower ||
                item.type == ItemID.BabyBirdStaff || //Finch Staff
                item.type == ItemID.FlinxStaff ||
                item.type == ItemID.SlimeStaff ||
                item.type == ItemID.HornetStaff ||
                item.type == ItemID.VampireFrogStaff ||
                item.type == ItemID.ImpStaff
            )
            {
                item.damage += 4;
            }

            // Buff Ice-Biome related stuff
            // Pre-Hardmode
            if (item.type == ItemID.IceSickle)
            {
                if (item.damage <= 55)
                {
                    item.damage = 55;
                }
                item.crit = 1;
                item.autoReuse = true;
            }
            if (item.type == ItemID.FrostStaff)
            {
                if (item.damage <= 55)
                {
                    item.damage = 55;
                }
                item.autoReuse = true;
                item.crit = 1;
                item.mana = 8;
            }
            if (item.type == ItemID.IceBoomerang)
            {
                if (item.damage <= 24)
                {
                    item.damage = 24;
                }
                item.autoReuse = true;
                item.useTime = 15;
                item.useAnimation = 15;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.IceBlade)
            {
                if (item.damage <= 18)
                {
                    item.damage = 18;
                }
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
                if (item.damage <= 50)
                {
                    item.damage = 50;
                }
                item.useTime = 22;
                item.useAnimation = 22;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.IceBow)
            {
                if (item.damage <= 50)
                {
                    item.damage = 50;
                }
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
            if (item.type == ItemID.SkyFracture)
            {
                if (item.damage <= 45)
                {
                    item.damage = 45;
                }
                item.crit = 26;
            }
            if (item.type == ItemID.MeteorStaff)
            {
                item.damage = 60;
            }
            if (item.type == ItemID.BookStaff)
            {
                item.damage = 45;
                item.mana = 12;
            }
            if (item.type == ItemID.Uzi)
            {
                item.damage = 36;
                item.useTime = 8;
                item.useAnimation = 8;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.VenusMagnum)
            {
                item.damage = 56;
                item.useTime = 8;
                item.useAnimation = 8;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.LeafBlower)
            {
                item.damage = 56;
                item.useTime = 6;
                item.useAnimation = 6;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.TacticalShotgun)
            {
                item.damage = 36;
                item.useTime = 32;
                item.useAnimation = 32;
                item.autoReuse = true;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.SniperRifle)
            {
                item.damage = 240;
                item.crit = 28;
                item.useTime = 32;
                item.useAnimation = 32;
                item.autoReuse = true;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.Tsunami)
            {
                item.damage = 55;
                item.crit = 1;
                item.useTime = 25;
                item.useAnimation = 25;
                item.shootsEveryUse = true;
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
                item.type == ItemID.CelestialSigil ||
                item.type == ItemID.PumpkinMoonMedallion ||
                item.type == ItemID.NaughtyPresent ||
                item.type == ItemID.GoblinBattleStandard ||
                item.type == ItemID.PirateMap
            )
            {
                item.consumable = false;
                item.maxStack = 1;
            }

            //Pre-Hardmode Ranger Rebalance

            if (item.type == ItemID.BeesKnees)
            {
                item.damage = 24;
                item.useTime = 22;
                item.useAnimation = 22;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.DemonBow)
            {
                item.damage = 16;
                item.useTime = 24;
                item.useAnimation = 24;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.HellwingBow)
            {
                item.damage = 24;
                item.useTime = 12;
                item.useAnimation = 12;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.MoltenFury)
            {
                item.damage = 32;
                item.shootSpeed = 12f;
                item.useTime = 20;
                item.useAnimation = 20;
                item.shootsEveryUse = true;
            }

            if (item.type == ItemID.Celeb2)
            {
                if (item.damage <= 70)
                {
                    item.damage = 70;
                }
            }
            if (item.type == ItemID.FireworksLauncher)
            {
                if (item.damage <= 35)
                {
                    item.damage = 35;
                }
            }

            //Buff Shortswords
            int[] shortSwordsToBuff =
            {
                ItemID.CopperShortsword,
                ItemID.TinShortsword,
                ItemID.IronShortsword,
                ItemID.LeadShortsword,
                ItemID.SilverShortsword,
                ItemID.TungstenShortsword,
                ItemID.GoldShortsword,
                ItemID.PlatinumShortsword,
                ItemID.Gladius
            };
            if (shortSwordsToBuff.Contains(item.type))
            {
                item.damage += 4;
                item.knockBack += 1.5f;
            }

            //Make Buff Furniture give their Buffs when used
            if (GlobalTiles.BuffTilesItemIDs.Contains(item.type))
            {
                item.buffTime = 108000;
                switch (item.type)
                {
                    case ItemID.WarTable:
                        item.buffType = BuffID.WarTable;
                        break;
                    case ItemID.BewitchingTable:
                        item.buffType = BuffID.Bewitched;
                        break;
                    case ItemID.SharpeningStation:
                        item.buffType = BuffID.Sharpened;
                        break;
                    case ItemID.CrystalBall:
                        item.buffType = BuffID.Clairvoyance;
                        break;
                    case ItemID.AmmoBox:
                        item.buffType = BuffID.AmmoBox;
                        break;
                    case ItemID.SliceOfCake:
                        item.buffType = BuffID.SugarRush;
                        item.buffTime = 7200;
                        break;
                    default:
                        item.buffTime = 0;
                        break;
                }
            }
        }

        public override bool ReforgePrice(Item item, ref int reforgePrice, ref bool canApplyDiscount)
        {
            canApplyDiscount = false;
            reforgePrice = Item.buyPrice(silver: 50);
            if (Main.hardMode)
            {
                reforgePrice *= 2;
            }
            return false;
        }
    }
}
