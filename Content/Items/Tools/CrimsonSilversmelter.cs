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

namespace WeDoALittleTrolling.Content.Items.Tools
{
    internal class CrimsonSilversmelter : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;

            Item.value = Item.buyPrice(gold: 5);
            Item.maxStack = 1;

            Item.autoReuse = true;
            Item.useTime = 6;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item1;

            Item.pick = 55;

            Item.damage = 25;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 3f;
            Item.crit = 12;

            Item.rare = ItemRarityID.Yellow;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.Anvils)
              .AddIngredient(ItemID.SilverBar, 20)
              .AddIngredient(ItemID.MeteoriteBar, 5)
              .AddIngredient(ItemID.CrimtaneBar, 5)
              .AddIngredient(ItemID.Ruby, 5)
              .Register();

            CreateRecipe()
             .AddTile(TileID.Anvils)
             .AddIngredient(ItemID.TungstenBar, 20)
             .AddIngredient(ItemID.MeteoriteBar, 5)
             .AddIngredient(ItemID.CrimtaneBar, 5)
             .AddIngredient(ItemID.Ruby, 5)
             .Register();
        }
    }
}