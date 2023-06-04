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
    internal class HeartOfDespair : ModItem
    {


        float heartOfDespairDamageBonus;


        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 18;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 8);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
           heartOfDespairDamageBonus = (player.statLifeMax - player.statLife) / 4;
           player.GetDamage(DamageClass.Magic) += (0.01f + 0.01f * (int)heartOfDespairDamageBonus);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.DemonAltar)
              .AddIngredient(ItemID.DemoniteBar, 5)
              .AddIngredient(ItemID.ShadowScale, 1)
              .AddIngredient(ItemID.HellstoneBar, 3)
              .AddIngredient(ItemID.VilePowder, 3)
              .Register();

            CreateRecipe()
              .AddTile(TileID.DemonAltar)
              .AddIngredient(ItemID.CrimtaneBar, 5)
              .AddIngredient(ItemID.TissueSample, 1)
              .AddIngredient(ItemID.HellstoneBar, 3)
              .AddIngredient(ItemID.VilePowder, 3)
              .Register();
        }
    }
}
