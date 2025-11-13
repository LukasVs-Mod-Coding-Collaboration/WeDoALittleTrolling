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
using Terraria.Utilities;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Items;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Common.Configs;

namespace WeDoALittleTrolling.Content.Items
{
    internal class BalancingItemList : GlobalItem
    {
        public override bool InstancePerEntity => false;

        public static UnifiedRandom random = new UnifiedRandom();

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

        public static void RegisterHooks()
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRodOfHarmonyCooldown)
            {
                On_Player.ItemCheck_UseTeleportRod += On_Player_ItemCheck_UseTeleportRod;
            }
        }

        public static void UnregisterHooks()
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRodOfHarmonyCooldown)
            {
                On_Player.ItemCheck_UseTeleportRod -= On_Player_ItemCheck_UseTeleportRod;
            }
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

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if
                (
                    item.type == ItemID.LeadAxe ||
                    item.type == ItemID.LeadBroadsword ||
                    item.type == ItemID.LeadHammer ||
                    item.type == ItemID.LeadPickaxe
                )
                {
                    target.AddBuff(BuffID.Poisoned, 600, false);
                }
            }
            base.OnHitNPC(item, player, target, hit, damageDone);
        }

        //Revert damage reduction from Spectre Hood
        public override void UpdateEquip(Item item, Player player)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
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
            }
            base.UpdateEquip(item, player);
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (item.type == ItemID.PygmyNecklace)
                {
                    player.maxMinions += 1;
                }
                if (player.hasPaladinShield)
                {
                    player.DefenseEffectiveness *= 1.125f;
                }
            }
            base.UpdateAccessory(item, player, hideVisual);
        }

        public static void ModifySetBonus(Player player)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
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
        }

        //Adjust Tooltips accordingly
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (item.type == ItemID.SpectreHood)
                {
                    TooltipLine extraManaLine = new TooltipLine(Mod, "PrefixAccMaxMana", "Increases maximum mana by 60");
                    tooltips.Add(extraManaLine);
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
                    item.type == ItemID.LunarFlareBook
                )
                {
                    TooltipLine extraCritChanceLine = new TooltipLine(Mod, "ProjectileHomingDescription", "Projectiles move towards the closest target");
                    tooltips.Add(extraCritChanceLine);
                }
                if
                (
                    item.type == ItemID.ThunderStaff ||
                    item.type == ItemID.DiamondStaff ||
                    item.type == ItemID.RubyStaff ||
                    item.type == ItemID.AmberStaff
                )
                {
                    TooltipLine extraCritChanceLine = new TooltipLine(Mod, "ProjectileHomingDescription", "Projectiles move towards the closest target until they pierce");
                    tooltips.Add(extraCritChanceLine);
                }
                if
                (
                    item.type == ItemID.PygmyNecklace
                )
                {
                    List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip0") && (t.Mod == "Terraria"));
                    infoLine.ForEach(t => t.Text = t.Text.Replace("1", "2"));
                }
                if
                (
                    item.type == ItemID.PaladinsShield ||
                    item.type == ItemID.FrozenShield ||
                    item.type == ItemID.HeroShield
                )
                {
                    List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip0") && (t.Mod == "Terraria"));
                    infoLine.ForEach(t => t.Text = t.Text + "\n12.5% increased defense effectiveness");
                }
                if
                (
                    item.type == ItemID.RodOfHarmony
                )
                {
                    TooltipLine chaosStateLine = new TooltipLine(Mod, "ChaosState", "Causes the chaos state");
                    tooltips.Add(chaosStateLine);
                }
            }
            base.ModifyTooltips(item, tooltips);
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
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
            }
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override void SetStaticDefaults()
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                PrefixLegacy.ItemSets.GunsBows[ItemID.FlareGun] = true;
            }
        }

        public override void SetDefaults(Item item)
        {
            if (WDALTModSystem.isCalamityModPresent && !ModContent.GetInstance<WDALTServerConfig>().DisableCalamityCompatibilityMode)
            {
                base.SetDefaults(item);
                return;
            }
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
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
                if (item.type == ItemID.LandMine)
                {
                    item.value = Item.buyPrice(silver: 50);
                }
                if (item.type == ItemID.CactusBreastplate)
                {
                    item.defense += 2;
                }
                if (item.type == ItemID.BoneSword)
                {
                    item.shootsEveryUse = true;
                    item.shoot = ProjectileID.BoneGloveProj;
                    item.shootSpeed = 10f;
                }
                if (item.type == ItemID.EnchantedSword)
                {
                    item.shootsEveryUse = true;
                    item.knockBack = 5.25f;
                }
                if (item.type == ItemID.TheRottedFork)
                {
                    item.damage = 20;
                    item.useTime = 28;
                    item.useAnimation = 28;
                    item.shootsEveryUse = true;
                }
                if (item.type == ItemID.InfernoFork)
                {
                    item.damage = 60;
                    item.mana = 20;
                }
                if (item.type == ItemID.FlamingMace)
                {
                    item.damage = (int)Math.Round(20.0 / 2.0); //Terraria.ID.ItemID.Sets.ToolTipDamageMultiplier == 2.0
                }
                if (item.type == ItemID.BlueMoon)
                {
                    item.damage = (int)Math.Round(60.0 / 2.0); //Terraria.ID.ItemID.Sets.ToolTipDamageMultiplier == 2.0
                }
                if (item.type == ItemID.Sunfury)
                {
                    item.damage = (int)Math.Round(70.0 / 2.0); //Terraria.ID.ItemID.Sets.ToolTipDamageMultiplier == 2.0
                }
                if (item.type == ItemID.DarkLance)
                {
                    item.damage = 40;
                }
                if (item.type == ItemID.Gungnir)
                {
                    item.crit = 2;
                }
                if (item.type == ItemID.MonkStaffT1)
                {
                    item.damage = 55;
                }
                if (item.type == ItemID.MonkStaffT2)
                {
                    item.damage = 50;
                }
                if (item.type == ItemID.MonkStaffT3)
                {
                    item.damage = 150;
                }
                if (item.type == ItemID.Terragrim)
                {
                    item.damage = 20;
                }
                if (item.type == ItemID.EmpressBlade)
                {
                    item.damage = 100;
                }
                if (item.type == ItemID.Arkhalis)
                {
                    item.damage = 35;
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
                    item.damage = 55;
                    item.crit = 1;
                    item.useTime = 30;
                    item.useAnimation = 15;
                    item.attackSpeedOnlyAffectsWeaponAnimation = false;
                    item.GetGlobalItem<WDALTItemUtil>().attackSpeedRoundingErrorProtection = true;
                }
                if (item.type == ItemID.ClockworkAssaultRifle)
                {
                    item.damage = 20;
                }
                if (item.type == ItemID.Marrow)
                {
                    item.damage = 60;
                    item.useTime = 18;
                    item.useAnimation = 18;
                    item.shootsEveryUse = true;
                    item.crit = 8;
                }
                if (item.type == ItemID.FlintlockPistol)
                {
                    item.damage = 18;
                }
                if (item.type == ItemID.FlareGun)
                {
                    item.damage = 8;
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
                    item.useTime = 20;
                    item.useAnimation = 20;
                    item.shootsEveryUse = true;
                }
                if (item.type == ItemID.BatScepter)
                {
                    item.damage = 50;
                }
                if (item.type == ItemID.CandyCornRifle)
                {
                    item.ArmorPenetration = 48;
                    item.damage = 48;
                    item.useTime = 8;
                    item.useAnimation = 8;
                    item.shootsEveryUse = true;
                }
                if (item.type == ItemID.JackOLanternLauncher)
                {
                    item.damage = 70;
                    item.crit = 11;
                }
                if (item.type == ItemID.NailGun)
                {
                    item.damage = 90;
                }
                if (item.type == ItemID.MagnetSphere)
                {
                    item.damage = 55;
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
                if (item.type == ItemID.ClingerStaff)
                {
                    item.damage = 60;
                    item.knockBack = 10f;
                }
                if (item.type == ItemID.BubbleGun)
                {
                    item.damage = 64;
                    item.useTime = 8;
                    item.useAnimation = 8;
                    item.shootsEveryUse = true;
                    item.ArmorPenetration = 16;
                    item.mana = 4;
                    if (item.Variant == ItemVariants.WeakerVariant)
                    {
                        item.damage = 8;
                    }
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
                if (item.type == ItemID.PaladinsHammer)
                {
                    item.damage = 100;
                    item.shootSpeed = 24f;
                }
                if (item.type == ItemID.IllegalGunParts)
                {
                    item.shopCustomPrice = 80000;
                }
                if (item.type == ItemID.AntlionClaw) //Mandible Blade
                {
                    item.damage = 20;
                }
                if (item.type == ItemID.ChainKnife)
                {
                    item.damage = 16;
                    if (item.Variant == ItemVariants.StrongerVariant)
                    {
                        item.damage = 75;
                    }
                }
                if (item.type == ItemID.ThunderSpear)
                {
                    item.damage = 16;
                }
                if (item.type == ItemID.ThunderStaff)
                {
                    item.damage = 22;
                    item.mana -= 2;
                    item.crit = 2;
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
                if (item.type == ItemID.CrystalVileShard)
                {
                    item.damage = 30;
                }
                if (item.type == ItemID.Hammush)
                {
                    item.damage = 60;
                    item.useTime = 25;
                    item.useAnimation = 25;
                }
                if (item.type == ItemID.MushroomSpear)
                {
                    item.crit = 8;
                    item.useTime = 35;
                    item.useAnimation = 35;
                }
                if (item.type == ItemID.ChlorophytePartisan)
                {
                    item.damage = 50;
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
                    item.damage = 24;
                    item.useTime = 18;
                    item.useAnimation = 18;
                    item.shootsEveryUse = true;
                }
                if (item.type == ItemID.Flamarang)
                {
                    item.damage = 55;
                }
                if (item.type == ItemID.DeathSickle)
                {
                    item.damage = 60;
                    item.crit = 2;
                }
                if (item.type == ItemID.TitaniumSword)
                {
                    item.damage = 65;
                }
                if (item.type == ItemID.TitaniumTrident)
                {
                    item.damage = 50;
                }
                if (item.type == ItemID.TitaniumWaraxe)
                {
                    item.damage = 80;
                }
                if (item.type == ItemID.TitaniumRepeater)
                {
                    item.useTime = 16;
                    item.useAnimation = 16;
                }
                if (item.type == ItemID.AdamantiteSword)
                {
                    item.damage = 65;
                }
                if (item.type == ItemID.AdamantiteGlaive)
                {
                    item.damage = 50;
                }
                if (item.type == ItemID.AdamantiteWaraxe)
                {
                    item.damage = 80;
                }
                if (item.type == ItemID.AdamantiteRepeater)
                {
                    item.useTime = 17;
                    item.useAnimation = 17;
                }
                if (item.type == ItemID.HallowedRepeater)
                {
                    item.useTime = 16;
                    item.useAnimation = 16;
                }
                if (item.type == ItemID.FlowerofFire)
                {
                    item.mana = 8;
                    item.autoReuse = true;
                    if (item.Variant == ItemVariants.StrongerVariant)
                    {
                        item.mana = 10;
                    }
                }
                if (item.type == ItemID.UnholyTrident)
                {
                    item.damage = 44;
                    item.mana = 20;
                    item.autoReuse = true;
                    if (item.Variant == ItemVariants.WeakerVariant)
                    {
                        item.damage = 22;
                        item.mana = 10;
                    }
                }
                if
                (
                    item.type == ItemID.PoisonStaff ||
                    item.type == ItemID.VenomStaff
                )
                {
                    item.damage = 36;
                }
                if
                (
                    item.type == ItemID.SpaceGun
                )
                {
                    item.damage = 28;
                }
                if (item.type == ItemID.BlizzardStaff)
                {
                    item.damage = 48;
                    item.UseSound = SoundID.Item9;
                }
                if (item.type == ItemID.NebulaBlaze)
                {
                    item.mana = 8;
                }
                if (item.type == ItemID.NebulaArcanum)
                {
                    item.damage = 80;
                    item.mana = 20;
                }

                // Buff Ice-Biome related stuff
                // Pre-Hardmode
                if (item.type == ItemID.IceSickle)
                {
                    item.damage = 55;
                    item.crit = 1;
                    item.autoReuse = true;
                }
                if (item.type == ItemID.FrostStaff)
                {
                    item.damage = 44;
                    item.autoReuse = true;
                    item.crit = 1;
                    item.mana = 8;
                }
                if (item.type == ItemID.IceBoomerang)
                {
                    item.autoReuse = true;
                    item.shootsEveryUse = true;
                }
                if (item.type == ItemID.IceBlade)
                {
                    item.damage = 18;
                    item.autoReuse = true;
                }
                if (item.type == ItemID.SnowballCannon)
                {
                    item.damage = 12;
                    item.autoReuse = true;
                    item.useTime = 16;
                    item.useAnimation = 16;
                    item.shootsEveryUse = true;
                    if (item.Variant == ItemVariants.StrongerVariant)
                    {
                        item.damage = 24;
                        item.useTime = 6;
                        item.useAnimation = 6;
                    }
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
                    item.damage = 42;
                    if (item.Variant == ItemVariants.WeakerVariant)
                    {
                        item.damage = 10;
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
                    item.damage = 40;
                }
                if (item.type == ItemID.HoundiusShootius)
                {
                    item.damage = 32;
                }
                if (item.type == ItemID.SkyFracture)
                {
                    item.damage = 32;
                }
                if (item.type == ItemID.MeteorStaff)
                {
                    item.damage = 40;
                }
                if (item.type == ItemID.BookStaff)
                {
                    item.damage = 32;
                    item.mana = 16;
                }
                if (item.type == ItemID.Bananarang)
                {
                    item.damage = 50;
                }
                if (item.type == ItemID.Uzi)
                {
                    item.damage = 32;
                    item.useTime = 8;
                    item.useAnimation = 8;
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
                if
                (
                    item.type == ItemID.ElectrosphereLauncher
                )
                {
                    item.autoReuse = true;
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
                    item.useTime = 12;
                    item.useAnimation = 12;
                    item.shootsEveryUse = true;
                }
                if (item.type == ItemID.MoltenFury)
                {
                    item.damage = 32;
                    item.shootSpeed = 12f;
                }

                if (item.type == ItemID.Celeb2)
                {
                    item.damage = 65;
                }
                if (item.type == ItemID.FireworksLauncher)
                {
                    item.damage = 35;
                }
                if
                (
                    item.type == ItemID.AmethystStaff ||
                    item.type == ItemID.TopazStaff ||
                    item.type == ItemID.SapphireStaff ||
                    item.type == ItemID.EmeraldStaff ||
                    item.type == ItemID.RubyStaff ||
                    item.type == ItemID.DiamondStaff ||
                    item.type == ItemID.AmberStaff
                )
                {
                    item.damage += 2;
                    item.autoReuse = true;
                    item.mana -= 2;
                }
                if
                (
                    item.type == ItemID.LeadAxe ||
                    item.type == ItemID.LeadBroadsword ||
                    item.type == ItemID.LeadHammer ||
                    item.type == ItemID.LeadPickaxe
                )
                {
                    item.useTime = (int)Math.Round((double)item.useTime * 1.3);
                    item.useAnimation = (int)Math.Round((double)item.useAnimation * 1.3);
                    item.knockBack += 4f;
                }

                //Buff Shortswords
                int[] shortSwordsToBuff =
                {
                    ItemID.CopperShortsword,
                    ItemID.TinShortsword,
                    ItemID.IronShortsword,
                    ItemID.SilverShortsword,
                    ItemID.TungstenShortsword,
                    ItemID.GoldShortsword,
                    ItemID.PlatinumShortsword,
                    ItemID.Gladius
                };
                if (shortSwordsToBuff.Contains(item.type))
                {
                    item.damage += 2;
                    item.knockBack += 1.5f;
                }
                if (item.type == ItemID.LeadShortsword)
                {
                    item.knockBack += 4f;
                }
            }
        }
    }
}
