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

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Items.Accessories;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Content.Recipes
{
    internal static class ProgressionRecipes
    {

        public static void AddRecipes()
        {
            Condition HolyCharmCondition = new Condition("Holy Charm in Inventory", WDALTConditionFunctions.HasHolyCharmInInventory);
            Recipe WDALT_HolyCharm = Recipe.Create(ModContent.ItemType<HolyCharm>(), 1);
            WDALT_HolyCharm.AddIngredient(ItemID.PlatinumCoin, 1);
            WDALT_HolyCharm.AddTile(TileID.MythrilAnvil);
            WDALT_HolyCharm.AddCondition(HolyCharmCondition);
            WDALT_HolyCharm.AddCustomShimmerResult(ItemID.PlatinumCoin, 1);
            WDALT_HolyCharm.Register();
        }

        public static void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

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
