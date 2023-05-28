using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;

namespace WeDoALittleTrolling.Content.Recipes
{
    internal class GlobalRecipes : ModSystem
    {
        
        public override void AddRecipes()
        {
            /*
            Recipe WDALT_SharkToothNecklace = Recipe.Create(ItemID.SharkToothNecklace, 1);
            WDALT_SharkToothNecklace.AddTile(TileID.TinkerersWorkbench);
            WDALT_SharkToothNecklace.AddIngredient(ItemID.Shackle, 1);
            WDALT_SharkToothNecklace.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            WDALT_SharkToothNecklace.Register();
            */
        }

        public override void PostAddRecipes()
        {
            for(int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if(recipe.TryGetResult(ItemID.PlatinumHelmet, out Item PlatinumHelmet))
                {
                    recipe.RemoveIngredient(ItemID.PlatinumBar);
                    recipe.AddIngredient(ItemID.PlatinumBar, 12);
                }
                if(recipe.TryGetResult(ItemID.PlatinumChainmail, out Item PlatinumChainmail))
                {
                    recipe.RemoveIngredient(ItemID.PlatinumBar);
                    recipe.AddIngredient(ItemID.PlatinumBar, 22);
                }
                if(recipe.TryGetResult(ItemID.PlatinumGreaves, out Item PlatinumGreaves))
                {
                    recipe.RemoveIngredient(ItemID.PlatinumBar);
                    recipe.AddIngredient(ItemID.PlatinumBar, 16);
                }
                if(recipe.TryGetResult(ItemID.GoldHelmet, out Item GoldHelmet))
                {
                    recipe.RemoveIngredient(ItemID.GoldBar);
                    recipe.AddIngredient(ItemID.GoldBar, 12);
                }
                if(recipe.TryGetResult(ItemID.GoldChainmail, out Item GoldChainmail))
                {
                    recipe.RemoveIngredient(ItemID.GoldBar);
                    recipe.AddIngredient(ItemID.GoldBar, 22);
                }
                if(recipe.TryGetResult(ItemID.GoldGreaves, out Item GoldGreaves))
                {
                    recipe.RemoveIngredient(ItemID.GoldBar);
                    recipe.AddIngredient(ItemID.GoldBar, 16);
                }
            }
        }
    }
}

