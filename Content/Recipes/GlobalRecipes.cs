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

                //Platinum Armor

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

                //Gold Armor

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

                //Iron Armor

                if(recipe.TryGetResult(ItemID.IronHelmet, out Item IronHelmet))
                {
                    recipe.RemoveIngredient(ItemID.IronBar);
                    recipe.AddIngredient(ItemID.IronBar, 10);
                }
                if(recipe.TryGetResult(ItemID.IronChainmail, out Item IronChainmail))
                {
                    recipe.RemoveIngredient(ItemID.IronBar);
                    recipe.AddIngredient(ItemID.IronBar, 20);
                }
                if(recipe.TryGetResult(ItemID.IronGreaves, out Item IronGreaves))
                {
                    recipe.RemoveIngredient(ItemID.IronBar);
                    recipe.AddIngredient(ItemID.IronBar, 10);
                }

                //Lead Armor

                if(recipe.TryGetResult(ItemID.LeadHelmet, out Item LeadHelmet))
                {
                    recipe.RemoveIngredient(ItemID.LeadBar);
                    recipe.AddIngredient(ItemID.LeadBar, 10);
                }
                if(recipe.TryGetResult(ItemID.LeadChainmail, out Item LeadChainmail))
                {
                    recipe.RemoveIngredient(ItemID.LeadBar);
                    recipe.AddIngredient(ItemID.LeadBar, 20);
                }
                if(recipe.TryGetResult(ItemID.LeadGreaves, out Item LeadGreaves))
                {
                    recipe.RemoveIngredient(ItemID.LeadBar);
                    recipe.AddIngredient(ItemID.LeadBar, 10);
                }

                //Silver Armor

                if(recipe.TryGetResult(ItemID.SilverHelmet, out Item SilverHelmet))
                {
                    recipe.RemoveIngredient(ItemID.SilverBar);
                    recipe.AddIngredient(ItemID.SilverBar, 10);
                }
                if(recipe.TryGetResult(ItemID.SilverChainmail, out Item SilverChainmail))
                {
                    recipe.RemoveIngredient(ItemID.SilverBar);
                    recipe.AddIngredient(ItemID.SilverBar, 20);
                }
                if(recipe.TryGetResult(ItemID.SilverGreaves, out Item SilverGreaves))
                {
                    recipe.RemoveIngredient(ItemID.SilverBar);
                    recipe.AddIngredient(ItemID.SilverBar, 10);
                }

                //Tungsten Armor

                if(recipe.TryGetResult(ItemID.TungstenHelmet, out Item TungstenHelmet))
                {
                    recipe.RemoveIngredient(ItemID.TungstenBar);
                    recipe.AddIngredient(ItemID.TungstenBar, 10);
                }
                if(recipe.TryGetResult(ItemID.TungstenChainmail, out Item TungstenChainmail))
                {
                    recipe.RemoveIngredient(ItemID.TungstenBar);
                    recipe.AddIngredient(ItemID.TungstenBar, 20);
                }
                if(recipe.TryGetResult(ItemID.TungstenGreaves, out Item TungstenGreaves))
                {
                    recipe.RemoveIngredient(ItemID.TungstenBar);
                    recipe.AddIngredient(ItemID.TungstenBar, 10);
                }

                //Copper Armor

                if(recipe.TryGetResult(ItemID.CopperHelmet, out Item CopperHelmet))
                {
                    recipe.RemoveIngredient(ItemID.CopperBar);
                    recipe.AddIngredient(ItemID.CopperBar, 8);
                }
                if(recipe.TryGetResult(ItemID.CopperChainmail, out Item CopperChainmail))
                {
                    recipe.RemoveIngredient(ItemID.CopperBar);
                    recipe.AddIngredient(ItemID.CopperBar, 14);
                }
                if(recipe.TryGetResult(ItemID.CopperGreaves, out Item CopperGreaves))
                {
                    recipe.RemoveIngredient(ItemID.CopperBar);
                    recipe.AddIngredient(ItemID.CopperBar, 8);
                }

                //Tin Armor

                if(recipe.TryGetResult(ItemID.TinHelmet, out Item TinHelmet))
                {
                    recipe.RemoveIngredient(ItemID.TinBar);
                    recipe.AddIngredient(ItemID.TinBar, 8);
                }
                if(recipe.TryGetResult(ItemID.TinChainmail, out Item TinChainmail))
                {
                    recipe.RemoveIngredient(ItemID.TinBar);
                    recipe.AddIngredient(ItemID.TinBar, 14);
                }
                if(recipe.TryGetResult(ItemID.TinGreaves, out Item TinGreaves))
                {
                    recipe.RemoveIngredient(ItemID.TinBar);
                    recipe.AddIngredient(ItemID.TinBar, 8);
                }
            }
        }
    }
}

