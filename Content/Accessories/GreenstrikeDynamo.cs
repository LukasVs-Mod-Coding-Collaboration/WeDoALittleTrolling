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

namespace WeDoALittleTrolling.Content.Accessories
{
    internal class GreenstrikeDynamo : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greenstrike Dynamo");
            Tooltip.SetDefault("Give yourself a boost with the power of Nostalgia\nIn memory of bad Piggies\nNegates fall damage and Increases movement speed\nGrants MASSIVE buffs to life regeneration\n+5 Defense and prolonged immmunity frames");
        }


        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 34;

            Item.consumable = false;

            Item.value = Item.buyPrice(platinum: 1);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Red;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 1.5f;
            player.noFallDmg = true;
            player.accRunSpeed += 0.4f;
            player.lifeRegen += 25;
            player.statDefense += 5;
            player.longInvince = true;
            player.wingTime += 360f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.LifeFruit, 5)
              .AddIngredient(ItemID.ChlorophyteBar, 5)
              .AddIngredient(ItemID.Ectoplasm, 5)
              .AddIngredient(ItemID.Wire, 50)
              .AddIngredient(ItemID.CrossNecklace, 1)
              .AddIngredient(ItemID.MartianConduitPlating, 5)
              .Register();
        }
    }
}