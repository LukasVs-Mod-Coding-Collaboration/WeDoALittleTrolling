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
    internal class ManaExtractionCatalyst : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 34;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 5);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.LightPurple;

            Item.accessory = true; //Item is an accessory
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += -75; //Weird but working way to set Max down (originally 125)
            player.buffImmune[94] = true; //Immunity to Mana Sickness
            player.GetDamage(DamageClass.Magic) += 0.05f;//player.GetDamage(DamageClass.Magic) += 0.1f; //Damage increase of 20% (now 10%)

        }

        public override void AddRecipes()
        {
            CreateRecipe() //Recipe
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.ManaCrystal, 5)
              .AddIngredient(ItemID.SoulofSight, 5)
              .AddIngredient(ItemID.SoulofNight, 10)
              .AddIngredient(ItemID.Obsidian, 15)
              .AddIngredient(ItemID.HallowedBar, 3)
              .Register();
        }
    }
}
