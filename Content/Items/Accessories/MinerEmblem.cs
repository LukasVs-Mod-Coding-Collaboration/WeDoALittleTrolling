using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class MinerEmblem : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 1);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Blue;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.fireWalk = true;
            float pickSpeedModifier = (float)0.35;
            player.pickSpeed = player.pickSpeed - (player.pickSpeed * pickSpeedModifier);

        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.Anvils)
              .AddIngredient(ItemID.GoldPickaxe, 1)
              .AddIngredient(ItemID.StoneBlock, 75)
              .AddIngredient(ItemID.IronBar, 5)
              .AddIngredient(ItemID.FallenStar, 3)
              .Register();


            CreateRecipe()
             .AddTile(TileID.Anvils)
             .AddIngredient(ItemID.GoldPickaxe, 1)
             .AddIngredient(ItemID.StoneBlock, 75)
             .AddIngredient(ItemID.LeadBar, 5)
             .AddIngredient(ItemID.FallenStar, 3)
             .Register();


            CreateRecipe()
             .AddTile(TileID.Anvils)
             .AddIngredient(ItemID.PlatinumPickaxe, 1)
             .AddIngredient(ItemID.StoneBlock, 75)
             .AddIngredient(ItemID.IronBar, 5)
             .AddIngredient(ItemID.FallenStar, 3)
             .Register();

            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.PlatinumPickaxe, 1)
            .AddIngredient(ItemID.StoneBlock, 75)
            .AddIngredient(ItemID.LeadBar, 5)
            .AddIngredient(ItemID.FallenStar, 3)
            .Register();
        }
    }
}
