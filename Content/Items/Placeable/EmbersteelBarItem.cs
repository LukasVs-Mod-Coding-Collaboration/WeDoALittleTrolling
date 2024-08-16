using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Tiles;

namespace WeDoALittleTrolling.Content.Items.Placeable
{
    public class EmbersteelBarItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 61;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<EmbersteelBar>());
            Item.width = 30;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 4);
            Item.material = true;
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EmberfluxOreItem>(), 4)
                .AddIngredient(ItemID.Meteorite, 4)
                .AddTile(TileID.AdamantiteForge)
                .Register();
        }
    }
}
