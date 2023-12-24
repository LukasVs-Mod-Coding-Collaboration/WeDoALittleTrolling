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
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Items.Accessories;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Common.Utilities;
using System.Linq;

namespace WeDoALittleTrolling.Content.Recipes
{
    internal static class GlobalRecipes
    {
        public static void AddRecipes()
        {
            AddComplexCraftingTreeItemDuplicationRecipes();
            AddShimmeringBetweenMimicItems_MeleeWeapons();
            AddShimmeringBetweenMimicItems_RangedWeapons();
            AddShimmeringBetweenMimicItems_MagicWeapons();
            AddShimmeringBetweenMimicItems_Accessories();
            AddShimmeringBetweenMimicItems_Hooks();
            AddShimmeringBetweenMimicItems_NormalMimics();
            AddShimmeringBetweenMimicItems_IceMimics();

            Condition ShimmerCondition = new Condition("Shimmer", WDALTConditionFunctions.GetFalse);

            Recipe.Create(ItemID.VampireKnives, 1)
            .AddIngredient(ItemID.ScourgeoftheCorruptor, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.ScourgeoftheCorruptor, 1)
            .AddIngredient(ItemID.VampireKnives, 1)
            .AddCondition(ShimmerCondition)
            .Register();

            AddBiomeKeyDuplicationRecipes();

            Recipe.Create(ItemID.ObsidianRose, 1)
            .AddIngredient(ItemID.Obsidian, 50)
            .AddIngredient(ItemID.JungleRose, 1)
            .AddIngredient(ItemID.Hellstone, 10)
            .AddTile(TileID.Hellforge)
            .Register();

            Recipe.Create(ItemID.JungleGrassSeeds, 5)
            .AddIngredient(ItemID.GrassSeeds, 5)
            .AddIngredient(ItemID.JungleSpores, 1)
            .AddTile(TileID.ShimmerMonolith)
            .Register();

            Recipe.Create(ItemID.BloodMoonMonolith, 1)
            .AddIngredient(ItemID.AngelStatue, 1)
            .AddIngredient(ItemID.BloodMoonStarter, 1)
            .AddTile(TileID.ShimmerMonolith)
            .Register();

            Recipe.Create(ItemID.GravityGlobe, 1)
            .AddIngredient(ItemID.GravitationPotion, 10)
            .AddIngredient(ItemID.GelBalloon, 10)
            .AddTile(TileID.AlchemyTable)
            .Register();

            Recipe.Create(ItemID.RodofDiscord, 1)
            .AddIngredient(ItemID.ChaosElementalBanner, 2)
            .AddTile(TileID.HeavyWorkBench)
            .Register();

            Recipe.Create(ItemID.BandofRegeneration, 1)
            .AddIngredient(ItemID.CrimtaneBar, 4)
            .AddIngredient(ItemID.LifeCrystal, 1)
            .AddTile(TileID.HeavyWorkBench)
            .Register();

            Recipe.Create(ItemID.BandofRegeneration, 1)
            .AddIngredient(ItemID.DemoniteBar, 4)
            .AddIngredient(ItemID.LifeCrystal, 1)
            .AddTile(TileID.HeavyWorkBench)
            .Register();

            Recipe.Create(ItemID.AngelStatue, 1)
            .AddIngredient(ItemID.StoneBlock, 50)
            .AddIngredient(ItemID.ShimmerTorch, 25)
            .AddTile(TileID.ShimmerMonolith)
            .Register();

            Recipe.Create(ItemID.TsunamiInABottle, 1)
            .AddIngredient(ItemID.CloudinaBottle, 1)
            .AddCondition(ShimmerCondition)
            .Register();

            Recipe.Create(ItemID.CloudinaBottle, 1)
            .AddIngredient(ItemID.TsunamiInABottle, 1)
            .AddCondition(ShimmerCondition)
            .Register();

            Recipe WDALT_WormScarf_BoC_V1 = Recipe.Create(ItemID.WormScarf, 1);
            WDALT_WormScarf_BoC_V1.AddCondition(ShimmerCondition);
            WDALT_WormScarf_BoC_V1.AddIngredient(ItemID.BrainOfConfusion, 1);
            WDALT_WormScarf_BoC_V1.Register();

            Recipe WDALT_WormScarf_BoC_V2 = Recipe.Create(ItemID.BrainOfConfusion, 1);
            WDALT_WormScarf_BoC_V2.AddCondition(ShimmerCondition);
            WDALT_WormScarf_BoC_V2.AddIngredient(ItemID.WormScarf, 1);
            WDALT_WormScarf_BoC_V2.Register();

            Recipe WDALT_Wrath_Rage_V1 = Recipe.Create(ItemID.WrathPotion, 1);
            WDALT_Wrath_Rage_V1.AddCondition(ShimmerCondition);
            WDALT_Wrath_Rage_V1.AddIngredient(ItemID.RagePotion, 1);
            WDALT_Wrath_Rage_V1.Register();

            Recipe WDALT_Wrath_Rage_V2 = Recipe.Create(ItemID.RagePotion, 1);
            WDALT_Wrath_Rage_V2.AddCondition(ShimmerCondition);
            WDALT_Wrath_Rage_V2.AddIngredient(ItemID.WrathPotion, 1);
            WDALT_Wrath_Rage_V2.Register();

            Recipe WDALT_WoodBoomerang = Recipe.Create(ItemID.WoodenBoomerang, 1);
            WDALT_WoodBoomerang.AddTile(TileID.WorkBenches);
            WDALT_WoodBoomerang.AddIngredient(ItemID.Wood, 50);
            WDALT_WoodBoomerang.Register();

            Recipe WDALT_Yelets = Recipe.Create(ItemID.Yelets, 1);
            WDALT_Yelets.AddTile(TileID.MythrilAnvil);
            WDALT_Yelets.AddIngredient(ItemID.JungleYoyo, 1);
            WDALT_Yelets.AddIngredient(ItemID.HallowedBar, 5);
            WDALT_Yelets.AddIngredient(ItemID.Vine, 2);
            WDALT_Yelets.AddIngredient(ItemID.TurtleShell, 1);
            WDALT_Yelets.Register();

            Recipe WDALT_GHealPot = Recipe.Create(ItemID.GreaterHealingPotion, 60);
            WDALT_GHealPot.AddTile(TileID.AlchemyTable);
            WDALT_GHealPot.AddIngredient(ItemID.HealingPotion, 60);
            WDALT_GHealPot.AddIngredient(ItemID.SoulofMight, 1);
            WDALT_GHealPot.AddIngredient(ItemID.SoulofSight, 1);
            WDALT_GHealPot.AddIngredient(ItemID.SoulofFright, 1);
            WDALT_GHealPot.AddCustomShimmerResult(ItemID.GreaterHealingPotion, 60);
            WDALT_GHealPot.Register();

            Recipe WDALT_SManaPot = Recipe.Create(ItemID.SuperManaPotion, 60);
            WDALT_SManaPot.AddTile(TileID.AlchemyTable);
            WDALT_SManaPot.AddIngredient(ItemID.GreaterManaPotion, 60);
            WDALT_SManaPot.AddIngredient(ItemID.SoulofMight, 1);
            WDALT_SManaPot.AddIngredient(ItemID.SoulofSight, 1);
            WDALT_SManaPot.AddIngredient(ItemID.SoulofFright, 1);
            WDALT_SManaPot.AddCustomShimmerResult(ItemID.SuperManaPotion, 60);
            WDALT_SManaPot.Register();

            Recipe WDALT_HorseShoe_V1 = Recipe.Create(ItemID.LuckyHorseshoe, 1);
            WDALT_HorseShoe_V1.AddTile(TileID.Anvils);
            WDALT_HorseShoe_V1.AddIngredient(ItemID.GoldBar, 10);
            WDALT_HorseShoe_V1.AddIngredient(ItemID.Cloud, 10);
            WDALT_HorseShoe_V1.Register();

            Recipe WDALT_HorseShoe_V2 = Recipe.Create(ItemID.LuckyHorseshoe, 1);
            WDALT_HorseShoe_V2.AddTile(TileID.Anvils);
            WDALT_HorseShoe_V2.AddIngredient(ItemID.PlatinumBar, 10);
            WDALT_HorseShoe_V2.AddIngredient(ItemID.Cloud, 10);
            WDALT_HorseShoe_V2.Register();

            Recipe.Create(ItemID.Spear, 1)
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.GoldBar, 12)
            .Register();

            Recipe.Create(ItemID.Spear, 1)
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.PlatinumBar, 12)
            .Register();

            Recipe WDALT_CobaltShield_V1 = Recipe.Create(ItemID.CobaltShield, 1);
            WDALT_CobaltShield_V1.AddTile(TileID.MythrilAnvil);
            WDALT_CobaltShield_V1.AddIngredient(ItemID.CobaltBar, 8);
            WDALT_CobaltShield_V1.Register();

            Recipe WDALT_CobaltShield_V2 = Recipe.Create(ItemID.CobaltShield, 1);
            WDALT_CobaltShield_V2.AddTile(TileID.MythrilAnvil);
            WDALT_CobaltShield_V2.AddIngredient(ItemID.PalladiumBar, 8);
            WDALT_CobaltShield_V2.Register();

            Recipe WDALT_SanguineStaff_V1 = Recipe.Create(ItemID.SanguineStaff, 1);
            WDALT_SanguineStaff_V1.AddTile(TileID.HeavyWorkBench);
            WDALT_SanguineStaff_V1.AddIngredient(ItemID.BloodMoonStarter, 1);
            WDALT_SanguineStaff_V1.AddIngredient(ItemID.BatBat, 1);
            WDALT_SanguineStaff_V1.AddIngredient(ItemID.PalladiumBar, 12);
            WDALT_SanguineStaff_V1.Register();

            Recipe WDALT_SanguineStaff_V2 = Recipe.Create(ItemID.SanguineStaff, 1);
            WDALT_SanguineStaff_V2.AddTile(TileID.HeavyWorkBench);
            WDALT_SanguineStaff_V2.AddIngredient(ItemID.BloodMoonStarter, 1);
            WDALT_SanguineStaff_V2.AddIngredient(ItemID.BatBat, 1);
            WDALT_SanguineStaff_V2.AddIngredient(ItemID.CobaltBar, 12);
            WDALT_SanguineStaff_V2.Register();

            Recipe WDALT_Terraprisma = Recipe.Create(ItemID.EmpressBlade, 1);
            WDALT_Terraprisma.AddTile(TileID.LunarCraftingStation);
            WDALT_Terraprisma.AddIngredient(ItemID.RainbowWhip, 1);
            WDALT_Terraprisma.AddIngredient(ItemID.RainbowCrystalStaff, 1);
            WDALT_Terraprisma.Register();

            Recipe WDALT_HardySaddleToHeartCrystal = Recipe.Create(ItemID.LifeCrystal, 1);
            WDALT_HardySaddleToHeartCrystal.AddIngredient(ItemID.HardySaddle, 1);
            WDALT_HardySaddleToHeartCrystal.Register();

            Recipe WDALT_ShimmerBackRodOfDiscord = Recipe.Create(ItemID.RodOfHarmony, 1);
            WDALT_ShimmerBackRodOfDiscord.AddCondition(ShimmerCondition);
            WDALT_ShimmerBackRodOfDiscord.AddIngredient(ItemID.RodofDiscord, 1);
            WDALT_ShimmerBackRodOfDiscord.Register();

            Recipe WDALT_MoneyTrough = Recipe.Create(ItemID.MoneyTrough, 1);
            WDALT_MoneyTrough.AddIngredient(ItemID.PiggyBank, 3);
            WDALT_MoneyTrough.AddTile(TileID.Anvils);
            WDALT_MoneyTrough.Register();

            Recipe WDALT_MythrilAnvil_Duplicate = Recipe.Create(ItemID.MythrilAnvil, 1);
            WDALT_MythrilAnvil_Duplicate.AddIngredient(ItemID.MythrilBar, 10);
            WDALT_MythrilAnvil_Duplicate.AddTile(TileID.MythrilAnvil);
            WDALT_MythrilAnvil_Duplicate.Register();
            Recipe WDALT_OrichalcumAnvil_Duplicate = Recipe.Create(ItemID.OrichalcumAnvil, 1);
            WDALT_OrichalcumAnvil_Duplicate.AddIngredient(ItemID.OrichalcumBar, 12);
            WDALT_OrichalcumAnvil_Duplicate.AddTile(TileID.MythrilAnvil);
            WDALT_OrichalcumAnvil_Duplicate.Register();

            Recipe WDALT_MythrilAnvil_Alternate = Recipe.Create(ItemID.MythrilAnvil, 1);
            WDALT_MythrilAnvil_Alternate.AddIngredient(ItemID.MythrilBar, 10);
            WDALT_MythrilAnvil_Alternate.AddIngredient(ItemID.FrostCore, 5);
            WDALT_MythrilAnvil_Alternate.AddIngredient(ItemID.AncientBattleArmorMaterial, 5);
            WDALT_MythrilAnvil_Alternate.AddTile(TileID.Anvils);
            WDALT_MythrilAnvil_Alternate.Register();
            Recipe WDALT_OrichalcumAnvil_Alternate = Recipe.Create(ItemID.OrichalcumAnvil, 1);
            WDALT_OrichalcumAnvil_Alternate.AddIngredient(ItemID.OrichalcumBar, 12);
            WDALT_OrichalcumAnvil_Alternate.AddIngredient(ItemID.FrostCore, 5);
            WDALT_OrichalcumAnvil_Alternate.AddIngredient(ItemID.AncientBattleArmorMaterial, 5);
            WDALT_OrichalcumAnvil_Alternate.AddTile(TileID.Anvils);
            WDALT_OrichalcumAnvil_Alternate.Register();

            Recipe WDALT_Soulconvert_V1 = Recipe.Create(ItemID.SoulofFright, 3);
            WDALT_Soulconvert_V1.AddIngredient(ItemID.SoulofFright, 1);
            WDALT_Soulconvert_V1.AddIngredient(ItemID.SoulofMight, 1);
            WDALT_Soulconvert_V1.AddIngredient(ItemID.SoulofSight, 1);
            WDALT_Soulconvert_V1.AddCustomShimmerResult(ItemID.SoulofFright, 3);
            WDALT_Soulconvert_V1.AddTile(TileID.MythrilAnvil);
            WDALT_Soulconvert_V1.Register();
            Recipe WDALT_Soulconvert_V2 = Recipe.Create(ItemID.SoulofLight, 1);
            WDALT_Soulconvert_V2.AddIngredient(ItemID.SoulofNight, 1);
            WDALT_Soulconvert_V2.AddCondition(ShimmerCondition);
            WDALT_Soulconvert_V2.Register();
            Recipe WDALT_Soulconvert_V3 = Recipe.Create(ItemID.SoulofNight, 1);
            WDALT_Soulconvert_V3.AddIngredient(ItemID.SoulofLight, 1);
            WDALT_Soulconvert_V3.AddCondition(ShimmerCondition);
            WDALT_Soulconvert_V3.Register();

            Recipe WDALT_AvengerEmblem = Recipe.Create(ItemID.AvengerEmblem, 1);
            WDALT_AvengerEmblem.AddTile(TileID.MythrilAnvil);
            WDALT_AvengerEmblem.AddIngredient(ItemID.HallowedBar, 12);
            WDALT_AvengerEmblem.AddIngredient(ItemID.SoulofNight, 12);
            WDALT_AvengerEmblem.Register();

            Recipe WDALT_SlimeStaff = Recipe.Create(ItemID.SlimeStaff, 1);
            WDALT_SlimeStaff.AddTile(TileID.WorkBenches);
            WDALT_SlimeStaff.AddIngredient(ItemID.Gel, 99);
            WDALT_SlimeStaff.AddRecipeGroup(RecipeGroupID.Wood, 33);
            WDALT_SlimeStaff.Register();
        }

        public static void AddShimmeringBetweenMimicItems_MeleeWeapons()
        {
            Condition ShimmerCondition = new Condition("Shimmer", WDALTConditionFunctions.GetFalse);
            Recipe.Create(ItemID.ChainGuillotines, 1)
            .AddIngredient(ItemID.FetidBaghnakhs, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.FetidBaghnakhs, 1)
            .AddIngredient(ItemID.FlyingKnife, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.FlyingKnife, 1)
            .AddIngredient(ItemID.ChainGuillotines, 1)
            .AddCondition(ShimmerCondition)
            .Register();
        }

        public static void AddShimmeringBetweenMimicItems_RangedWeapons()
        {
            Condition ShimmerCondition = new Condition("Shimmer", WDALTConditionFunctions.GetFalse);
            Recipe.Create(ItemID.DartRifle, 1)
            .AddIngredient(ItemID.DartPistol, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.DartPistol, 1)
            .AddIngredient(ItemID.DaedalusStormbow, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.DaedalusStormbow, 1)
            .AddIngredient(ItemID.DartRifle, 1)
            .AddCondition(ShimmerCondition)
            .Register();
        }

        public static void AddShimmeringBetweenMimicItems_MagicWeapons()
        {
            Condition ShimmerCondition = new Condition("Shimmer", WDALTConditionFunctions.GetFalse);
            Recipe.Create(ItemID.ClingerStaff, 1)
            .AddIngredient(ItemID.SoulDrain, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.SoulDrain, 1)
            .AddIngredient(ItemID.CrystalVileShard, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.CrystalVileShard, 1)
            .AddIngredient(ItemID.ClingerStaff, 1)
            .AddCondition(ShimmerCondition)
            .Register();
        }

        public static void AddShimmeringBetweenMimicItems_Accessories()
        {
            Condition ShimmerCondition = new Condition("Shimmer", WDALTConditionFunctions.GetFalse);
            Recipe.Create(ItemID.PutridScent, 1)
            .AddIngredient(ItemID.FleshKnuckles, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.FleshKnuckles, 1)
            .AddIngredient(ItemID.PutridScent, 1)
            .AddCondition(ShimmerCondition)
            .Register();
        }

        public static void AddShimmeringBetweenMimicItems_Hooks()
        {
            Condition ShimmerCondition = new Condition("Shimmer", WDALTConditionFunctions.GetFalse);
            Recipe.Create(ItemID.WormHook, 1)
            .AddIngredient(ItemID.TendonHook, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.TendonHook, 1)
            .AddIngredient(ItemID.IlluminantHook, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.IlluminantHook, 1)
            .AddIngredient(ItemID.WormHook, 1)
            .AddCondition(ShimmerCondition)
            .Register();
        }

        public static void AddShimmeringBetweenMimicItems_NormalMimics()
        {
            Condition ShimmerCondition = new Condition("Shimmer", WDALTConditionFunctions.GetFalse);
            Recipe.Create(ItemID.DualHook, 1)
            .AddIngredient(ItemID.MagicDagger, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.MagicDagger, 1)
            .AddIngredient(ItemID.TitanGlove, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.TitanGlove, 1)
            .AddIngredient(ItemID.PhilosophersStone, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.PhilosophersStone, 1)
            .AddIngredient(ItemID.CrossNecklace, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.CrossNecklace, 1)
            .AddIngredient(ItemID.DualHook, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            //Requires manual intervention 
            Recipe.Create(ItemID.StarCloak, 1)
            .AddIngredient(ItemID.CrossNecklace, 1)
            .AddTile(TileID.ShimmerMonolith)
            .Register();
            Recipe.Create(ItemID.CrossNecklace, 1)
            .AddIngredient(ItemID.StarCloak, 1)
            .AddTile(TileID.ShimmerMonolith)
            .Register();
        }

        public static void AddShimmeringBetweenMimicItems_IceMimics()
        {
            Condition ShimmerCondition = new Condition("Shimmer", WDALTConditionFunctions.GetFalse);
            Recipe.Create(ItemID.Frostbrand, 1)
            .AddIngredient(ItemID.IceBow, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.IceBow, 1)
            .AddIngredient(ItemID.FlowerofFrost, 1)
            .AddCondition(ShimmerCondition)
            .Register();
            Recipe.Create(ItemID.FlowerofFrost, 1)
            .AddIngredient(ItemID.Frostbrand, 1)
            .AddCondition(ShimmerCondition)
            .Register();
        }

        public static void AddComplexCraftingTreeItemDuplicationRecipes()
        {
            Condition AnkhShieldCondition = new Condition("Ankh Shield in Inventory", WDALTConditionFunctions.HasAnkhShieldInInventory);
            Recipe WDALT_AnkhShield = Recipe.Create(ItemID.AnkhShield, 1);
            WDALT_AnkhShield.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_AnkhShield.AddTile(TileID.TinkerersWorkbench);
            WDALT_AnkhShield.AddCondition(AnkhShieldCondition);
            WDALT_AnkhShield.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_AnkhShield.Register();

            Condition CellPhoneCondition = new Condition("Cell Phone in Inventory", WDALTConditionFunctions.HasCellPhoneInInventory);
            Recipe WDALT_CellPhone = Recipe.Create(ItemID.CellPhone, 1);
            WDALT_CellPhone.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_CellPhone.AddTile(TileID.TinkerersWorkbench);
            WDALT_CellPhone.AddCondition(CellPhoneCondition);
            WDALT_CellPhone.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_CellPhone.Register();

            Condition TerrasparkBootsCondition = new Condition("Terraspark Boots in Inventory", WDALTConditionFunctions.HasTerrasparkBootsInInventory);
            Recipe WDALT_TerrasparkBoots = Recipe.Create(ItemID.TerrasparkBoots, 1);
            WDALT_TerrasparkBoots.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_TerrasparkBoots.AddTile(TileID.TinkerersWorkbench);
            WDALT_TerrasparkBoots.AddCondition(TerrasparkBootsCondition);
            WDALT_TerrasparkBoots.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_TerrasparkBoots.Register();

            Condition ArcticDivingGearCondition = new Condition("Arctic Diving Gear in Inventory", WDALTConditionFunctions.HasArcticDivingGearInInventory);
            Recipe WDALT_ArcticDivingGear = Recipe.Create(ItemID.ArcticDivingGear, 1);
            WDALT_ArcticDivingGear.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_ArcticDivingGear.AddTile(TileID.TinkerersWorkbench);
            WDALT_ArcticDivingGear.AddCondition(ArcticDivingGearCondition);
            WDALT_ArcticDivingGear.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_ArcticDivingGear.Register();

            Condition CelestialCuffsCondition = new Condition("Celestial Cuffs in Inventory", WDALTConditionFunctions.HasCelestialCuffsInInventory);
            Recipe WDALT_CelestialCuffs = Recipe.Create(ItemID.CelestialCuffs, 1);
            WDALT_CelestialCuffs.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_CelestialCuffs.AddTile(TileID.TinkerersWorkbench);
            WDALT_CelestialCuffs.AddCondition(CelestialCuffsCondition);
            WDALT_CelestialCuffs.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_CelestialCuffs.Register();

            Condition CelestialEmblemCondition = new Condition("Celestial Emblem in Inventory", WDALTConditionFunctions.HasCelestialEmblemInInventory);
            Recipe WDALT_CelestialEmblem = Recipe.Create(ItemID.CelestialEmblem, 1);
            WDALT_CelestialEmblem.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_CelestialEmblem.AddTile(TileID.TinkerersWorkbench);
            WDALT_CelestialEmblem.AddCondition(CelestialEmblemCondition);
            WDALT_CelestialEmblem.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_CelestialEmblem.Register();

            Condition CelestialShellCondition = new Condition("Celestial Shell in Inventory", WDALTConditionFunctions.HasCelestialShellInInventory);
            Recipe WDALT_CelestialShell = Recipe.Create(ItemID.CelestialShell, 1);
            WDALT_CelestialShell.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_CelestialShell.AddTile(TileID.TinkerersWorkbench);
            WDALT_CelestialShell.AddCondition(CelestialShellCondition);
            WDALT_CelestialShell.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_CelestialShell.Register();

            Condition FireGauntletCondition = new Condition("Fire Gauntlet in Inventory", WDALTConditionFunctions.HasFireGauntletInInventory);
            Recipe WDALT_FireGauntlet = Recipe.Create(ItemID.FireGauntlet, 1);
            WDALT_FireGauntlet.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_FireGauntlet.AddTile(TileID.TinkerersWorkbench);
            WDALT_FireGauntlet.AddCondition(FireGauntletCondition);
            WDALT_FireGauntlet.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_FireGauntlet.Register();

            Condition FrogGearCondition = new Condition("Frog Gear in Inventory", WDALTConditionFunctions.HasFrogGearInInventory);
            Recipe WDALT_FrogGear = Recipe.Create(ItemID.FrogGear, 1);
            WDALT_FrogGear.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_FrogGear.AddTile(TileID.TinkerersWorkbench);
            WDALT_FrogGear.AddCondition(FrogGearCondition);
            WDALT_FrogGear.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_FrogGear.Register();

            Condition LavaproofTackleBagCondition = new Condition("Lavaproof Tackle Bag in Inventory", WDALTConditionFunctions.HasLavaproofTackleBagInInventory);
            Recipe WDALT_LavaproofTackleBag = Recipe.Create(ItemID.LavaproofTackleBag, 1);
            WDALT_LavaproofTackleBag.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_LavaproofTackleBag.AddTile(TileID.TinkerersWorkbench);
            WDALT_LavaproofTackleBag.AddCondition(LavaproofTackleBagCondition);
            WDALT_LavaproofTackleBag.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_LavaproofTackleBag.Register();

            Condition MasterNinjaGearCondition = new Condition("Master Ninja Gear in Inventory", WDALTConditionFunctions.HasMasterNinjaGearInInventory);
            Recipe WDALT_MasterNinjaGear = Recipe.Create(ItemID.MasterNinjaGear, 1);
            WDALT_MasterNinjaGear.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_MasterNinjaGear.AddTile(TileID.TinkerersWorkbench);
            WDALT_MasterNinjaGear.AddCondition(MasterNinjaGearCondition);
            WDALT_MasterNinjaGear.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_MasterNinjaGear.Register();

            Condition SniperScopeCondition = new Condition("Sniper Scope in Inventory", WDALTConditionFunctions.HasSniperScopeInInventory);
            Recipe WDALT_SniperScope = Recipe.Create(ItemID.SniperScope, 1);
            WDALT_SniperScope.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_SniperScope.AddTile(TileID.TinkerersWorkbench);
            WDALT_SniperScope.AddCondition(SniperScopeCondition);
            WDALT_SniperScope.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_SniperScope.Register();

            Condition HolyCharmCondition = new Condition("Holy Charm in Inventory", WDALTConditionFunctions.HasHolyCharmInInventory);
            Recipe WDALT_HolyCharm = Recipe.Create(ModContent.ItemType<HolyCharm>(), 1);
            WDALT_HolyCharm.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_HolyCharm.AddTile(TileID.MythrilAnvil);
            WDALT_HolyCharm.AddCondition(HolyCharmCondition);
            WDALT_HolyCharm.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_HolyCharm.Register();
        }

        public static void AddBiomeKeyDuplicationRecipes()
        {
            Recipe.Create(ItemID.PiranhaGun, 1)
            .AddIngredient(ItemID.JungleKey, 1)
            .AddCondition(new Condition("Piranha Gun in Inventory", WDALTConditionFunctions.HasPiranhaGunInInventory))
            .AddCustomShimmerResult(ItemID.PiranhaGun, 1)
            .Register();

            Recipe.Create(ItemID.ScourgeoftheCorruptor, 1)
            .AddIngredient(ItemID.CorruptionKey, 1)
            .AddCondition(new Condition("Scourge of the Corruptor in Inventory", WDALTConditionFunctions.HasScourgeoftheCorruptorInInventory))
            .AddCustomShimmerResult(ItemID.ScourgeoftheCorruptor, 1)
            .Register();

            Recipe.Create(ItemID.VampireKnives, 1)
            .AddIngredient(ItemID.CrimsonKey, 1)
            .AddCondition(new Condition("Vampire Knives in Inventory", WDALTConditionFunctions.HasVampireKnivesInInventory))
            .AddCustomShimmerResult(ItemID.VampireKnives, 1)
            .Register();

            Recipe.Create(ItemID.RainbowGun, 1)
            .AddIngredient(ItemID.HallowedKey, 1)
            .AddCondition(new Condition("Rainbow Gun in Inventory", WDALTConditionFunctions.HasRainbowGunInInventory))
            .AddCustomShimmerResult(ItemID.RainbowGun, 1)
            .Register();

            Recipe.Create(ItemID.StaffoftheFrostHydra, 1)
            .AddIngredient(ItemID.FrozenKey, 1)
            .AddCondition(new Condition("Staff of the Frost Hydra in Inventory", WDALTConditionFunctions.HasStaffoftheFrostHydraInInventory))
            .AddCustomShimmerResult(ItemID.StaffoftheFrostHydra, 1)
            .Register();

            Recipe.Create(ItemID.StormTigerStaff, 1)
            .AddIngredient(ItemID.DungeonDesertKey, 1)
            .AddCondition(new Condition("Desert Tiger Staff in Inventory", WDALTConditionFunctions.HasStormTigerStaffInInventory))
            .AddCustomShimmerResult(ItemID.StormTigerStaff, 1)
            .Register();
        }

        public static void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(ItemID.PumpkinMoonMedallion, out Item PumpkinMoonMedallion))
                {
                    recipe.AddIngredient(ItemID.BeetleHusk, 5);
                }

                if (recipe.TryGetResult(ItemID.PotionOfReturn, out Item PotionOfReturn))
                {
                    PotionOfReturn.stack = 3;
                    if (recipe.TryGetIngredient(ItemID.RecallPotion, out Item RecallPotion1))
                    {
                        RecallPotion1.stack = 3;
                    }
                }

                if (recipe.TryGetResult(ItemID.RecallPotion, out Item RecallPotion2))
                {
                    RecallPotion2.stack = 3;
                    if (recipe.TryGetIngredient(ItemID.BottledWater, out Item BottledWater1))
                    {
                        BottledWater1.stack = 3;
                    }
                }
                
                //Platinum Armor

                if (recipe.TryGetResult(ItemID.PlatinumHelmet, out Item PlatinumHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.PlatinumBar, out Item ingredient))
                    {
                        ingredient.stack = 12;
                    }
                }
                if (recipe.TryGetResult(ItemID.PlatinumChainmail, out Item PlatinumChainmail))
                {
                    if (recipe.TryGetIngredient(ItemID.PlatinumBar, out Item ingredient))
                    {
                        ingredient.stack = 22;
                    }
                }
                if (recipe.TryGetResult(ItemID.PlatinumGreaves, out Item PlatinumGreaves))
                {
                    if (recipe.TryGetIngredient(ItemID.PlatinumBar, out Item ingredient))
                    {
                        ingredient.stack = 16;
                    }
                }

                //Gold Armor

                if (recipe.TryGetResult(ItemID.GoldHelmet, out Item GoldHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.GoldBar, out Item ingredient))
                    {
                        ingredient.stack = 12;
                    }
                }
                if (recipe.TryGetResult(ItemID.GoldChainmail, out Item GoldChainmail))
                {
                    if (recipe.TryGetIngredient(ItemID.GoldBar, out Item ingredient))
                    {
                        ingredient.stack = 22;
                    }
                }
                if (recipe.TryGetResult(ItemID.GoldGreaves, out Item GoldGreaves))
                {
                    if (recipe.TryGetIngredient(ItemID.GoldBar, out Item ingredient))
                    {
                        ingredient.stack = 16;
                    }
                }

                //Iron Armor

                if (recipe.TryGetResult(ItemID.IronHelmet, out Item IronHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.IronBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.IronChainmail, out Item IronChainmail))
                {
                    if (recipe.TryGetIngredient(ItemID.IronBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if (recipe.TryGetResult(ItemID.IronGreaves, out Item IronGreaves))
                {
                    if (recipe.TryGetIngredient(ItemID.IronBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Lead Armor

                if (recipe.TryGetResult(ItemID.LeadHelmet, out Item LeadHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.LeadBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.LeadChainmail, out Item LeadChainmail))
                {
                    if (recipe.TryGetIngredient(ItemID.LeadBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if (recipe.TryGetResult(ItemID.LeadGreaves, out Item LeadGreaves))
                {
                    if (recipe.TryGetIngredient(ItemID.LeadBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Silver Armor

                if (recipe.TryGetResult(ItemID.SilverHelmet, out Item SilverHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.SilverBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.SilverChainmail, out Item SilverChainmail))
                {
                    if (recipe.TryGetIngredient(ItemID.SilverBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if (recipe.TryGetResult(ItemID.SilverGreaves, out Item SilverGreaves))
                {
                    if (recipe.TryGetIngredient(ItemID.SilverBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Tungsten Armor

                if (recipe.TryGetResult(ItemID.TungstenHelmet, out Item TungstenHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.TungstenBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.TungstenChainmail, out Item TungstenChainmail))
                {
                    if (recipe.TryGetIngredient(ItemID.TungstenBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if (recipe.TryGetResult(ItemID.TungstenGreaves, out Item TungstenGreaves))
                {
                    if (recipe.TryGetIngredient(ItemID.TungstenBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Copper Armor

                if (recipe.TryGetResult(ItemID.CopperHelmet, out Item CopperHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.CopperBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }
                if (recipe.TryGetResult(ItemID.CopperChainmail, out Item CopperChainmail))
                {
                    if (recipe.TryGetIngredient(ItemID.CopperBar, out Item ingredient))
                    {
                        ingredient.stack = 14;
                    }
                }
                if (recipe.TryGetResult(ItemID.CopperGreaves, out Item CopperGreaves))
                {
                    if (recipe.TryGetIngredient(ItemID.CopperBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }

                //Tin Armor

                if (recipe.TryGetResult(ItemID.TinHelmet, out Item TinHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.TinBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }
                if (recipe.TryGetResult(ItemID.TinChainmail, out Item TinChainmail))
                {
                    if (recipe.TryGetIngredient(ItemID.TinBar, out Item ingredient))
                    {
                        ingredient.stack = 14;
                    }
                }
                if (recipe.TryGetResult(ItemID.TinGreaves, out Item TinGreaves))
                {
                    if (recipe.TryGetIngredient(ItemID.TinBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }

                //Cobalt Armor

                if (recipe.TryGetResult(ItemID.CobaltHat, out Item CobaltHat))
                {
                    if (recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.CobaltHelmet, out Item CobaltHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.CobaltMask, out Item CobaltMask))
                {
                    if (recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.CobaltBreastplate, out Item CobaltBreastplate))
                {
                    if (recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if (recipe.TryGetResult(ItemID.CobaltLeggings, out Item CobaltLeggings))
                {
                    if (recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Palladium Armor

                if (recipe.TryGetResult(ItemID.PalladiumHelmet, out Item PalladiumHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.PalladiumHeadgear, out Item PalladiumHeadgear))
                {
                    if (recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.PalladiumMask, out Item PalladiumMask))
                {
                    if (recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.PalladiumBreastplate, out Item PalladiumBreastplate))
                {
                    if (recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if (recipe.TryGetResult(ItemID.PalladiumLeggings, out Item PalladiumLeggings))
                {
                    if (recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Mythril Armor

                if (recipe.TryGetResult(ItemID.MythrilHat, out Item MythrilHat))
                {
                    if (recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.MythrilHelmet, out Item MythrilHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.MythrilHood, out Item MythrilMask))
                {
                    if (recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.MythrilChainmail, out Item MythrilChainmail))
                {
                    if (recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if (recipe.TryGetResult(ItemID.MythrilGreaves, out Item MythrilGreaves))
                {
                    if (recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Orichalcum Armor

                if (recipe.TryGetResult(ItemID.OrichalcumHelmet, out Item OrichalcumHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.OrichalcumHeadgear, out Item OrichalcumHeadgear))
                {
                    if (recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.OrichalcumMask, out Item OrichalcumMask))
                {
                    if (recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.OrichalcumBreastplate, out Item OrichalcumBreastplate))
                {
                    if (recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if (recipe.TryGetResult(ItemID.OrichalcumLeggings, out Item OrichalcumLeggings))
                {
                    if (recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Adamantite Armor

                if (recipe.TryGetResult(ItemID.AdamantiteHelmet, out Item AdamantiteHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.AdamantiteHeadgear, out Item AdamantiteHeadgear))
                {
                    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.AdamantiteMask, out Item AdamantiteMask))
                {
                    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.AdamantiteBreastplate, out Item AdamantiteBreastplate))
                {
                    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if (recipe.TryGetResult(ItemID.AdamantiteLeggings, out Item AdamantiteLeggings))
                {
                    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Titanium Armor

                if (recipe.TryGetResult(ItemID.TitaniumHelmet, out Item TitaniumHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if (recipe.TryGetResult(ItemID.TitaniumHeadgear, out Item TitaniumHeadgear))
                {
                    if (recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if (recipe.TryGetResult(ItemID.TitaniumMask, out Item TitaniumMask))
                {
                    if (recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if (recipe.TryGetResult(ItemID.TitaniumBreastplate, out Item TitaniumBreastplate))
                {
                    if (recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if (recipe.TryGetResult(ItemID.TitaniumLeggings, out Item TitaniumLeggings))
                {
                    if (recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Frost Armor

                if (recipe.TryGetResult(ItemID.FrostHelmet, out Item FrostHelmet))
                {
                    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostHelmet))
                    {
                        AdamantiteBar_FrostHelmet.stack = 10;
                    }
                    if (recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostHelmet))
                    {
                        TitaniumBar_FrostHelmet.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.FrostBreastplate, out Item FrostBreastplate))
                {
                    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostBreastplate))
                    {
                        AdamantiteBar_FrostBreastplate.stack = 15;
                    }
                    if (recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostBreastplate))
                    {
                        TitaniumBar_FrostBreastplate.stack = 15;
                    }
                }
                if (recipe.TryGetResult(ItemID.FrostLeggings, out Item FrostLeggings))
                {
                    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostLeggings))
                    {
                        AdamantiteBar_FrostLeggings.stack = 10;
                    }
                    if (recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostLeggings))
                    {
                        TitaniumBar_FrostLeggings.stack = 10;
                    }
                }

                //Forbidden Armor

                if (recipe.TryGetResult(ItemID.AncientBattleArmorHat, out Item AncientBattleArmorHat))
                {
                    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorHat))
                    {
                        AdamantiteBar_AncientBattleArmorHat.stack = 10;
                    }
                    if (recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorHat))
                    {
                        TitaniumBar_AncientBattleArmorHat.stack = 10;
                    }
                }
                if (recipe.TryGetResult(ItemID.AncientBattleArmorShirt, out Item AncientBattleArmorShirt))
                {
                    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorShirt))
                    {
                        AdamantiteBar_AncientBattleArmorShirt.stack = 15;
                    }
                    if (recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorShirt))
                    {
                        TitaniumBar_AncientBattleArmorShirt.stack = 15;
                    }
                }
                if (recipe.TryGetResult(ItemID.AncientBattleArmorPants, out Item AncientBattleArmorPants))
                {
                    if (recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorPants))
                    {
                        AdamantiteBar_AncientBattleArmorPants.stack = 10;
                    }
                    if (recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorPants))
                    {
                        TitaniumBar_AncientBattleArmorPants.stack = 10;
                    }
                }

                //Chlorophyte Bar

                if (recipe.TryGetResult(ItemID.ChlorophyteBar, out Item ChlorophyteBar))
                {
                    if (recipe.TryGetIngredient(ItemID.ChlorophyteOre, out Item ChlorophyteOre))
                    {
                        ChlorophyteOre.stack = 3;
                    }
                }

                //Chlorophyte Bullet

                if (recipe.TryGetResult(ItemID.ChlorophyteBullet, out Item ChlorophyteBullet))
                {
                    if (recipe.TryGetIngredient(ItemID.MusketBall, out Item MusketBall))
                    {
                        MusketBall.stack = 150;
                    }
                    ChlorophyteBullet.stack = 150;
                }

                //Spooky Armor
                if (recipe.TryGetResult(ItemID.SpookyHelmet, out Item SpookyHelmet))
                {
                    recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
                }
                if (recipe.TryGetResult(ItemID.SpookyBreastplate, out Item SpookyBreastplate))
                {
                    recipe.AddIngredient(ItemID.ChlorophyteBar, 18);
                }
                if (recipe.TryGetResult(ItemID.SpookyLeggings, out Item SpookyLeggings))
                {
                    recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
                }

                //Progression Changes
                if (recipe.TryGetResult(ItemID.NightmarePickaxe, out Item NightmarePickaxe))
                {
                    recipe.AddIngredient(ItemID.FossilOre, 6);
                    recipe.AddIngredient(ItemID.FlinxFur, 6);
                }
                if (recipe.TryGetResult(ItemID.DeathbringerPickaxe, out Item DeathbringerPickaxe))
                {
                    recipe.AddIngredient(ItemID.FossilOre, 6);
                    recipe.AddIngredient(ItemID.FlinxFur, 6);
                }
                if (recipe.TryGetResult(ItemID.MythrilAnvil, out Item MythrilAnvil))
                {
                    if (!recipe.HasTile(TileID.MythrilAnvil) && !recipe.HasIngredient(ItemID.FrostCore))
                    {
                        recipe.AddIngredient(ModContent.ItemType<IcyFossil>(), 10);
                        recipe.AddIngredient(ModContent.ItemType<DustyFossil>(), 10);
                        recipe.AddCustomShimmerResult(ItemID.MythrilBar, 10);
                    }
                }
                if (recipe.TryGetResult(ItemID.OrichalcumAnvil, out Item OrichalcumAnvil))
                {
                    if (!recipe.HasTile(TileID.MythrilAnvil) && !recipe.HasIngredient(ItemID.FrostCore))
                    {
                        recipe.AddIngredient(ModContent.ItemType<IcyFossil>(), 10);
                        recipe.AddIngredient(ModContent.ItemType<DustyFossil>(), 10);
                        recipe.AddCustomShimmerResult(ItemID.OrichalcumBar, 12);
                    }
                }

                //Gem Hooks now require one Geode (disabled)
                /*
                if(recipe.TryGetResult(ItemID.AmethystHook, out Item AmethystHook))
                {
                    recipe.AddIngredient(ItemID.Geode, 1);
                }
                if(recipe.TryGetResult(ItemID.DiamondHook, out Item DiamondHook))
                {
                    recipe.AddIngredient(ItemID.Geode, 1);
                }
                if(recipe.TryGetResult(ItemID.TopazHook, out Item TopazHook))
                {
                    recipe.AddIngredient(ItemID.Geode, 1);
                }
                if(recipe.TryGetResult(ItemID.RubyHook, out Item RubyHook))
                {
                    recipe.AddIngredient(ItemID.Geode, 1);
                }
                if(recipe.TryGetResult(ItemID.EmeraldHook, out Item EmeraldHook))
                {
                    recipe.AddIngredient(ItemID.Geode, 1);
                }
                if(recipe.TryGetResult(ItemID.SapphireHook, out Item SapphireHook))
                {
                    recipe.AddIngredient(ItemID.Geode, 1);
                }
                if(recipe.TryGetResult(ItemID.AmberHook, out Item AmberHook))
                {
                    recipe.AddIngredient(ItemID.Geode, 1);
                }
                */
            }
        }
    }
}
