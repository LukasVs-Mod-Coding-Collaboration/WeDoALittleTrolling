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
    internal class ShroomiteOverdrive : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 42;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 12);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Ranged) += 0.30f;
            player.GetArmorPenetration(DamageClass.Ranged) += 30.0f;
            player.GetDamage(DamageClass.Ranged) += -0.30f;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                equippedItem.type == ModContent.ItemType<ShroomiteOvercharge>() ||
                incomingItem.type == ModContent.ItemType<ShroomiteOvercharge>()
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
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.ShroomiteBar, 12)
              .AddIngredient(ItemID.SWATHelmet, 1)
              .AddIngredient(ItemID.RangerEmblem, 1)
              .Register();
        }
    }
}
