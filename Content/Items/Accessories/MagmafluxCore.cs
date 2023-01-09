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
    internal class MagmafluxCore : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magmaflux Core");
            Tooltip.SetDefault("Grants Immunity to Lava\nMelee attacks inflict \'On fire!\'\n3 Defense\nGrants a minor increase to\nlife regeneration");
        }


        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 42;

            Item.value = Item.buyPrice(gold: 1);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lavaImmune = true;
            player.magmaStone = true;
            player.lifeRegen += 3;
            player.fireWalk= true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.Furnaces)
              .AddIngredient(ItemID.Obsidian, 25)
              .AddIngredient(ItemID.LavaBucket, 3)
              .AddIngredient(ItemID.LifeCrystal, 1)
              .AddIngredient(ItemID.BandofRegeneration, 1)
              .AddIngredient(ItemID.ObsidianSkinPotion, 1)
              .AddIngredient(ItemID.LeadBar, 10)
              .Register();

            CreateRecipe()
              .AddTile(TileID.Furnaces)
              .AddIngredient(ItemID.Obsidian, 25)
              .AddIngredient(ItemID.LavaBucket, 3)
              .AddIngredient(ItemID.LifeCrystal, 1)
              .AddIngredient(ItemID.BandofRegeneration, 1)
              .AddIngredient(ItemID.ObsidianSkinPotion, 1)
              .AddIngredient(ItemID.IronBar, 10)
              .Register();
        }
    }
}