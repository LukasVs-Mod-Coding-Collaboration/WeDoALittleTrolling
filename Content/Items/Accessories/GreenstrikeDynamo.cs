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
    internal class GreenstrikeDynamo : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greenstrike Dynamo");
            Tooltip.SetDefault("Give yourself a boost with the power of nostalgia\nIn memory of Bad Piggies\nNegates fall damage and increases movement speed\nGrants MASSIVE buffs to life regeneration\nIncreases attack speed by 25%\nProlongs immmunity frames");
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
            player.lifeRegen += 25; //HP Regen
            player.GetAttackSpeed(DamageClass.Generic) += 0.25f;
            player.longInvince = true; //Cross Necklace effect
            player.wingTimeMax += 360;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.LifeFruit, 5)
              .AddIngredient(ItemID.SpectreBar, 10)
              .AddIngredient(ItemID.ChlorophyteBar, 10)
              .AddIngredient(ItemID.Wire, 50)
              .AddIngredient(ItemID.CrossNecklace, 1)
              .AddIngredient(ItemID.MartianConduitPlating, 5)
              .Register();
        }
    }
}