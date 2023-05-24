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
            Recipe WDALT_SharkToothNecklace = Recipe.Create(ItemID.SharkToothNecklace, 1);
            WDALT_SharkToothNecklace.AddTile(TileID.TinkerersWorkbench);
            WDALT_SharkToothNecklace.AddIngredient(ItemID.Shackle, 1);
            WDALT_SharkToothNecklace.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            WDALT_SharkToothNecklace.Register();
        }
    }
}

