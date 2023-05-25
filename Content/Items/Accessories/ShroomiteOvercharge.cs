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
    internal class ShroomiteOvercharge : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 58;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 12);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Ranged) += 0.30f;
            player.GetCritChance(DamageClass.Ranged) += 15.0f;
            player.GetAttackSpeed(DamageClass.Ranged) += -0.15f;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                equippedItem.type == ModContent.ItemType<ShroomiteOverdrive>() ||
                incomingItem.type == ModContent.ItemType<ShroomiteOverdrive>()
            )
            {
                return false;
            }
            else
            {
                return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.TinkerersWorkbench)
              .AddIngredient(ItemID.ShroomiteBar, 12)
              .AddIngredient(ItemID.RifleScope, 1)
              .AddIngredient(ItemID.RangerEmblem, 1)
              .Register();
        }
    }
}
