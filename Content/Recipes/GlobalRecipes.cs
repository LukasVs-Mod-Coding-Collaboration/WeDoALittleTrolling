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

namespace WeDoALittleTrolling.Content.Recipes
{
    internal static class GlobalRecipes
    {
        public static void AddRecipes()
        {
            AddComplexCraftingTreeItemDuplicationRecipes();

            Condition ShimmerCondition = new Condition("Shimmer", WDALTConditionFunctions.GetFalse);
            
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

            Recipe WDALT_Soulconvert_V1 = Recipe.Create(ItemID.SoulofFright, 3);
            WDALT_Soulconvert_V1.AddIngredient(ItemID.SoulofFright, 1);
            WDALT_Soulconvert_V1.AddIngredient(ItemID.SoulofMight, 1);
            WDALT_Soulconvert_V1.AddIngredient(ItemID.SoulofSight, 1);
            List<Item> shimmerResult_WDALT_Soulconvert_V1 = new List<Item>();
            Item shimmerResultItem_WDALT_Soulconvert_V1 = new Item();
            shimmerResultItem_WDALT_Soulconvert_V1.type = ItemID.SoulofFright;
            shimmerResultItem_WDALT_Soulconvert_V1.stack = 3;
            shimmerResult_WDALT_Soulconvert_V1.Add(shimmerResultItem_WDALT_Soulconvert_V1);
            WDALT_Soulconvert_V1.customShimmerResults = shimmerResult_WDALT_Soulconvert_V1;
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
        }

        public static void AddComplexCraftingTreeItemDuplicationRecipes()
        {
            List<Item> shimmerResult_WDALT_AnkhShieldLike = new List<Item>();
            Item shimmerResultItem_WDALT_AnkhShieldLike = new Item();
            shimmerResultItem_WDALT_AnkhShieldLike.type = ItemID.PlatinumCoin;
            shimmerResultItem_WDALT_AnkhShieldLike.stack = 1;
            shimmerResult_WDALT_AnkhShieldLike.Add(shimmerResultItem_WDALT_AnkhShieldLike);
            
            Condition AnkhShieldCondition = new Condition("Ankh Shield in Inventory", WDALTConditionFunctions.HasAnkhShieldInInventory);
            Recipe WDALT_AnkhShield = Recipe.Create(ItemID.AnkhShield, 1);
            WDALT_AnkhShield.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_AnkhShield.AddTile(TileID.TinkerersWorkbench);
            WDALT_AnkhShield.AddCondition(AnkhShieldCondition);
            WDALT_AnkhShield.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_AnkhShield.Register();

            Condition CellPhoneCondition = new Condition("Cell Phone in Inventory", WDALTConditionFunctions.HasCellPhoneInInventory);
            Recipe WDALT_CellPhone = Recipe.Create(ItemID.CellPhone, 1);
            WDALT_CellPhone.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_CellPhone.AddTile(TileID.TinkerersWorkbench);
            WDALT_CellPhone.AddCondition(CellPhoneCondition);
            WDALT_CellPhone.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_CellPhone.Register();

            Condition TerrasparkBootsCondition = new Condition("Terraspark Boots in Inventory", WDALTConditionFunctions.HasTerrasparkBootsInInventory);
            Recipe WDALT_TerrasparkBoots = Recipe.Create(ItemID.TerrasparkBoots, 1);
            WDALT_TerrasparkBoots.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_TerrasparkBoots.AddTile(TileID.TinkerersWorkbench);
            WDALT_TerrasparkBoots.AddCondition(TerrasparkBootsCondition);
            WDALT_TerrasparkBoots.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_TerrasparkBoots.Register();

            Condition ArcticDivingGearCondition = new Condition("Arctic Diving Gear in Inventory", WDALTConditionFunctions.HasArcticDivingGearInInventory);
            Recipe WDALT_ArcticDivingGear = Recipe.Create(ItemID.ArcticDivingGear, 1);
            WDALT_ArcticDivingGear.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_ArcticDivingGear.AddTile(TileID.TinkerersWorkbench);
            WDALT_ArcticDivingGear.AddCondition(ArcticDivingGearCondition);
            WDALT_ArcticDivingGear.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_ArcticDivingGear.Register();

            Condition CelestialCuffsCondition = new Condition("Celestial Cuffs in Inventory", WDALTConditionFunctions.HasCelestialCuffsInInventory);
            Recipe WDALT_CelestialCuffs = Recipe.Create(ItemID.CelestialCuffs, 1);
            WDALT_CelestialCuffs.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_CelestialCuffs.AddTile(TileID.TinkerersWorkbench);
            WDALT_CelestialCuffs.AddCondition(CelestialCuffsCondition);
            WDALT_CelestialCuffs.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_CelestialCuffs.Register();

            Condition CelestialEmblemCondition = new Condition("Celestial Emblem in Inventory", WDALTConditionFunctions.HasCelestialEmblemInInventory);
            Recipe WDALT_CelestialEmblem = Recipe.Create(ItemID.CelestialEmblem, 1);
            WDALT_CelestialEmblem.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_CelestialEmblem.AddTile(TileID.TinkerersWorkbench);
            WDALT_CelestialEmblem.AddCondition(CelestialEmblemCondition);
            WDALT_CelestialEmblem.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_CelestialEmblem.Register();

            Condition CelestialShellCondition = new Condition("Celestial Shell in Inventory", WDALTConditionFunctions.HasCelestialShellInInventory);
            Recipe WDALT_CelestialShell = Recipe.Create(ItemID.CelestialShell, 1);
            WDALT_CelestialShell.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_CelestialShell.AddTile(TileID.TinkerersWorkbench);
            WDALT_CelestialShell.AddCondition(CelestialShellCondition);
            WDALT_CelestialShell.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_CelestialShell.Register();

            Condition FireGauntletCondition = new Condition("Fire Gauntlet in Inventory", WDALTConditionFunctions.HasFireGauntletInInventory);
            Recipe WDALT_FireGauntlet = Recipe.Create(ItemID.FireGauntlet, 1);
            WDALT_FireGauntlet.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_FireGauntlet.AddTile(TileID.TinkerersWorkbench);
            WDALT_FireGauntlet.AddCondition(FireGauntletCondition);
            WDALT_FireGauntlet.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_FireGauntlet.Register();

            Condition FrogGearCondition = new Condition("Frog Gear in Inventory", WDALTConditionFunctions.HasFrogGearInInventory);
            Recipe WDALT_FrogGear = Recipe.Create(ItemID.FrogGear, 1);
            WDALT_FrogGear.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_FrogGear.AddTile(TileID.TinkerersWorkbench);
            WDALT_FrogGear.AddCondition(FrogGearCondition);
            WDALT_FrogGear.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_FrogGear.Register();

            Condition LavaproofTackleBagCondition = new Condition("Lavaproof Tackle Bag in Inventory", WDALTConditionFunctions.HasLavaproofTackleBagInInventory);
            Recipe WDALT_LavaproofTackleBag = Recipe.Create(ItemID.LavaproofTackleBag, 1);
            WDALT_LavaproofTackleBag.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_LavaproofTackleBag.AddTile(TileID.TinkerersWorkbench);
            WDALT_LavaproofTackleBag.AddCondition(LavaproofTackleBagCondition);
            WDALT_LavaproofTackleBag.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_LavaproofTackleBag.Register();

            Condition MasterNinjaGearCondition = new Condition("Master Ninja Gear in Inventory", WDALTConditionFunctions.HasMasterNinjaGearInInventory);
            Recipe WDALT_MasterNinjaGear = Recipe.Create(ItemID.MasterNinjaGear, 1);
            WDALT_MasterNinjaGear.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_MasterNinjaGear.AddTile(TileID.TinkerersWorkbench);
            WDALT_MasterNinjaGear.AddCondition(MasterNinjaGearCondition);
            WDALT_MasterNinjaGear.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_MasterNinjaGear.Register();

            Condition SniperScopeCondition = new Condition("Sniper Scope in Inventory", WDALTConditionFunctions.HasSniperScopeInInventory);
            Recipe WDALT_SniperScope = Recipe.Create(ItemID.SniperScope, 1);
            WDALT_SniperScope.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_SniperScope.AddTile(TileID.TinkerersWorkbench);
            WDALT_SniperScope.AddCondition(SniperScopeCondition);
            WDALT_SniperScope.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_SniperScope.Register();

            Condition HolyCharmCondition = new Condition("Holy Charm in Inventory", WDALTConditionFunctions.HasHolyCharmInInventory);
            Recipe WDALT_HolyCharm = Recipe.Create(ModContent.ItemType<HolyCharm>(), 1);
            WDALT_HolyCharm.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_HolyCharm.AddTile(TileID.MythrilAnvil);
            WDALT_HolyCharm.AddCondition(HolyCharmCondition);
            WDALT_HolyCharm.customShimmerResults = shimmerResult_WDALT_AnkhShieldLike;
            WDALT_HolyCharm.Register();
        }

        public static void PostAddRecipes()
        {
            for(int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                //Platinum Armor

                if(recipe.TryGetResult(ItemID.PlatinumHelmet, out Item PlatinumHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.PlatinumBar, out Item ingredient))
                    {
                        ingredient.stack = 12;
                    }
                }
                if(recipe.TryGetResult(ItemID.PlatinumChainmail, out Item PlatinumChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.PlatinumBar, out Item ingredient))
                    {
                        ingredient.stack = 22;
                    }
                }
                if(recipe.TryGetResult(ItemID.PlatinumGreaves, out Item PlatinumGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.PlatinumBar, out Item ingredient))
                    {
                        ingredient.stack = 16;
                    }
                }

                //Gold Armor

                if(recipe.TryGetResult(ItemID.GoldHelmet, out Item GoldHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.GoldBar, out Item ingredient))
                    {
                        ingredient.stack = 12;
                    }
                }
                if(recipe.TryGetResult(ItemID.GoldChainmail, out Item GoldChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.GoldBar, out Item ingredient))
                    {
                        ingredient.stack = 22;
                    }
                }
                if(recipe.TryGetResult(ItemID.GoldGreaves, out Item GoldGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.GoldBar, out Item ingredient))
                    {
                        ingredient.stack = 16;
                    }
                }

                //Iron Armor

                if(recipe.TryGetResult(ItemID.IronHelmet, out Item IronHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.IronBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.IronChainmail, out Item IronChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.IronBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if(recipe.TryGetResult(ItemID.IronGreaves, out Item IronGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.IronBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Lead Armor

                if(recipe.TryGetResult(ItemID.LeadHelmet, out Item LeadHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.LeadBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.LeadChainmail, out Item LeadChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.LeadBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if(recipe.TryGetResult(ItemID.LeadGreaves, out Item LeadGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.LeadBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Silver Armor

                if(recipe.TryGetResult(ItemID.SilverHelmet, out Item SilverHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.SilverBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.SilverChainmail, out Item SilverChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.SilverBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if(recipe.TryGetResult(ItemID.SilverGreaves, out Item SilverGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.SilverBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Tungsten Armor

                if(recipe.TryGetResult(ItemID.TungstenHelmet, out Item TungstenHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.TungstenBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.TungstenChainmail, out Item TungstenChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.TungstenBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if(recipe.TryGetResult(ItemID.TungstenGreaves, out Item TungstenGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.TungstenBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Copper Armor

                if(recipe.TryGetResult(ItemID.CopperHelmet, out Item CopperHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.CopperBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }
                if(recipe.TryGetResult(ItemID.CopperChainmail, out Item CopperChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.CopperBar, out Item ingredient))
                    {
                        ingredient.stack = 14;
                    }
                }
                if(recipe.TryGetResult(ItemID.CopperGreaves, out Item CopperGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.CopperBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }

                //Tin Armor

                if(recipe.TryGetResult(ItemID.TinHelmet, out Item TinHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.TinBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }
                if(recipe.TryGetResult(ItemID.TinChainmail, out Item TinChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.TinBar, out Item ingredient))
                    {
                        ingredient.stack = 14;
                    }
                }
                if(recipe.TryGetResult(ItemID.TinGreaves, out Item TinGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.TinBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }

                //Cobalt Armor

                if(recipe.TryGetResult(ItemID.CobaltHat, out Item CobaltHat))
                {
                    if(recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.CobaltHelmet, out Item CobaltHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.CobaltMask, out Item CobaltMask))
                {
                    if(recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.CobaltBreastplate, out Item CobaltBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.CobaltLeggings, out Item CobaltLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Palladium Armor

                if(recipe.TryGetResult(ItemID.PalladiumHelmet, out Item PalladiumHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.PalladiumHeadgear, out Item PalladiumHeadgear))
                {
                    if(recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.PalladiumMask, out Item PalladiumMask))
                {
                    if(recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.PalladiumBreastplate, out Item PalladiumBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.PalladiumLeggings, out Item PalladiumLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Mythril Armor

                if(recipe.TryGetResult(ItemID.MythrilHat, out Item MythrilHat))
                {
                    if(recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.MythrilHelmet, out Item MythrilHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.MythrilHood, out Item MythrilMask))
                {
                    if(recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.MythrilChainmail, out Item MythrilChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.MythrilGreaves, out Item MythrilGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Orichalcum Armor

                if(recipe.TryGetResult(ItemID.OrichalcumHelmet, out Item OrichalcumHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.OrichalcumHeadgear, out Item OrichalcumHeadgear))
                {
                    if(recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.OrichalcumMask, out Item OrichalcumMask))
                {
                    if(recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.OrichalcumBreastplate, out Item OrichalcumBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.OrichalcumLeggings, out Item OrichalcumLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Adamantite Armor

                if(recipe.TryGetResult(ItemID.AdamantiteHelmet, out Item AdamantiteHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.AdamantiteHeadgear, out Item AdamantiteHeadgear))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.AdamantiteMask, out Item AdamantiteMask))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.AdamantiteBreastplate, out Item AdamantiteBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.AdamantiteLeggings, out Item AdamantiteLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Titanium Armor

                if(recipe.TryGetResult(ItemID.TitaniumHelmet, out Item TitaniumHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.TitaniumHeadgear, out Item TitaniumHeadgear))
                {
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.TitaniumMask, out Item TitaniumMask))
                {
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.TitaniumBreastplate, out Item TitaniumBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.TitaniumLeggings, out Item TitaniumLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Frost Armor

                if(recipe.TryGetResult(ItemID.FrostHelmet, out Item FrostHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostHelmet))
                    {
                        AdamantiteBar_FrostHelmet.stack = 10;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostHelmet))
                    {
                        TitaniumBar_FrostHelmet.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.FrostBreastplate, out Item FrostBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostBreastplate))
                    {
                        AdamantiteBar_FrostBreastplate.stack = 15;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostBreastplate))
                    {
                        TitaniumBar_FrostBreastplate.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.FrostLeggings, out Item FrostLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostLeggings))
                    {
                        AdamantiteBar_FrostLeggings.stack = 10;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostLeggings))
                    {
                        TitaniumBar_FrostLeggings.stack = 10;
                    }
                }

                //Forbidden Armor

                if(recipe.TryGetResult(ItemID.AncientBattleArmorHat, out Item AncientBattleArmorHat))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorHat))
                    {
                        AdamantiteBar_AncientBattleArmorHat.stack = 10;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorHat))
                    {
                        TitaniumBar_AncientBattleArmorHat.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.AncientBattleArmorShirt, out Item AncientBattleArmorShirt))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorShirt))
                    {
                        AdamantiteBar_AncientBattleArmorShirt.stack = 15;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorShirt))
                    {
                        TitaniumBar_AncientBattleArmorShirt.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.AncientBattleArmorPants, out Item AncientBattleArmorPants))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorPants))
                    {
                        AdamantiteBar_AncientBattleArmorPants.stack = 10;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorPants))
                    {
                        TitaniumBar_AncientBattleArmorPants.stack = 10;
                    }
                }

                //Chlorophyte Bar

                if(recipe.TryGetResult(ItemID.ChlorophyteBar, out Item ChlorophyteBar))
                {
                    if(recipe.TryGetIngredient(ItemID.ChlorophyteOre, out Item ChlorophyteOre))
                    {
                        ChlorophyteOre.stack = 3;
                    }
                }

                //Chlorophyte Bullet

                if(recipe.TryGetResult(ItemID.ChlorophyteBullet, out Item ChlorophyteBullet))
                {
                    if(recipe.TryGetIngredient(ItemID.MusketBall, out Item MusketBall))
                    {
                        MusketBall.stack = 150;
                    }
                    ChlorophyteBullet.stack = 150;
                }

                //Spooky Armor
                if(recipe.TryGetResult(ItemID.SpookyHelmet, out Item SpookyHelmet))
                {
                    recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
                }
                if(recipe.TryGetResult(ItemID.SpookyBreastplate, out Item SpookyBreastplate))
                {
                    recipe.AddIngredient(ItemID.ChlorophyteBar, 18);
                }
                if(recipe.TryGetResult(ItemID.SpookyLeggings, out Item SpookyLeggings))
                {
                    recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
                }

                //Progression Changes
                if(recipe.TryGetResult(ItemID.NightmarePickaxe, out Item NightmarePickaxe))
                {
                    recipe.AddIngredient(ItemID.FossilOre, 6);
                    recipe.AddIngredient(ItemID.FlinxFur, 6);
                }
                if(recipe.TryGetResult(ItemID.DeathbringerPickaxe, out Item DeathbringerPickaxe))
                {
                    recipe.AddIngredient(ItemID.FossilOre, 6);
                    recipe.AddIngredient(ItemID.FlinxFur, 6);
                }
                if(recipe.TryGetResult(ItemID.MythrilAnvil, out Item MythrilAnvil))
                {
                    if(!recipe.HasTile(TileID.MythrilAnvil))
                    {
                        recipe.AddIngredient(ModContent.ItemType<IcyFossil>(), 10);
                        recipe.AddIngredient(ModContent.ItemType<DustyFossil>(), 10);
                        List<Item> shimmerResult_WDALT_MythrilAnvil = new List<Item>();
                        Item shimmerResultItem_WDALT_MythrilAnvil = new Item();
                        shimmerResultItem_WDALT_MythrilAnvil.type = ItemID.MythrilBar;
                        shimmerResultItem_WDALT_MythrilAnvil.stack = 10;
                        shimmerResult_WDALT_MythrilAnvil.Add(shimmerResultItem_WDALT_MythrilAnvil);
                        recipe.customShimmerResults = shimmerResult_WDALT_MythrilAnvil;
                    }
                }
                if(recipe.TryGetResult(ItemID.OrichalcumAnvil, out Item OrichalcumAnvil))
                {
                    if(!recipe.HasTile(TileID.MythrilAnvil))
                    {
                        recipe.AddIngredient(ModContent.ItemType<IcyFossil>(), 10);
                        recipe.AddIngredient(ModContent.ItemType<DustyFossil>(), 10);
                        List<Item> shimmerResult_WDALT_OrichalcumAnvil = new List<Item>();
                        Item shimmerResultItem_WDALT_OrichalcumAnvil = new Item();
                        shimmerResultItem_WDALT_OrichalcumAnvil.type = ItemID.OrichalcumBar;
                        shimmerResultItem_WDALT_OrichalcumAnvil.stack = 12;
                        shimmerResult_WDALT_OrichalcumAnvil.Add(shimmerResultItem_WDALT_OrichalcumAnvil);
                        recipe.customShimmerResults = shimmerResult_WDALT_OrichalcumAnvil;
                    }
                }

                //Gem Hooks now require one Geode
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
            }
        }
    }
}