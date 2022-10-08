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
    internal class ShroomiteOvercharge : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Overcharge");
            Tooltip.SetDefault("Can only be worn in the first acessory slot\nIncreases damage at the cost of attack speed\nIncreases ranged attack damage by 40%\nIncreases ranged critical strike chance by 20%\nDecreases ranged attack speed by 20%");
        }


        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 64;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 12);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Ranged) += 0.40f;
            player.GetCritChance(DamageClass.Ranged) += 20.0f;
            player.GetAttackSpeed(DamageClass.Ranged) += -0.20f;
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
              .AddIngredient(ItemID.RifleScope, 1)
              .AddIngredient(ItemID.RangerEmblem, 1)
              .Register();
        }
    }
}
