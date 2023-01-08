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


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miner Emblem");
            Tooltip.SetDefault("Moving around the Underground\ncan be tough sometimes.\nThis item makes it easier.\n \"Miner Inconvenience\"");
        }


        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 1);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Blue;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.lavaImmune = true;
            player.fireWalk = true;
            player.pickSpeed *= -0.75f; 

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
