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
    internal class ShroomiteOverdrive : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Overdrive");
            Tooltip.SetDefault("Can only be worn in the first acessory slot\nIncreases attack speed at the cost of damage\nIncreases ranged attack speed by 60%\nIncreases ranged armor penetration by 40\nDecreases ranged attack damage by 10%");
        }


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
            player.GetAttackSpeed(DamageClass.Ranged) += 0.60f;
            player.GetArmorPenetration(DamageClass.Ranged) += 40.0f;
            player.GetDamage(DamageClass.Ranged) += -0.10f;
        }

        //Only allow equipping to first acessory slot
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            //slot >= 13: Terraria 1.4.4+ and CalamityMod vanity slot compat
            if(slot == 3 || slot >= 13)
            {
                return true;
            }
            else
            {
                return false;
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
