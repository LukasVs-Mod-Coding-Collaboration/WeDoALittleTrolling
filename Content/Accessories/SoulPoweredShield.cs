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
    internal class SoulPoweredShield : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul-Powered Shield");
            Tooltip.SetDefault("A shield forged from the souls of a powerful mechanical creature\nGrants knockback immunity\n7 Defense\nReduces 25% of damage taken\nGrants immunity to fire and frost effects of any kind\nAlso grants immunity to Ichor and Electrified");
        }


        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 50);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Expert; //Expert Mode Item

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.25f; //Damage Reduction
            player.statDefense += 7;
            player.noKnockback = true;
            player.lavaImmune = true; //Immunity to Lava and Fire blocks
            player.fireWalk = true;
            player.buffImmune[44] = true; //Debuff Immunities against fire & frost debuffs
            player.buffImmune[39] = true;
            player.buffImmune[24] = true;
            player.buffImmune[47] = true;
            player.buffImmune[46] = true;
            player.buffImmune[67] = true;
            player.buffImmune[69] = true;
            player.buffImmune[144] = true;
            player.buffImmune[153] = true;

        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if(equippedItem.type == ItemID.WormScarf || incomingItem.type == ItemID.WormScarf)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.WormScarf, 1)
              .AddIngredient(ItemID.SoulofMight, 5)
              .AddIngredient(ItemID.HallowedBar, 10)
              .AddIngredient(ItemID.CobaltShield, 1)
              .AddIngredient(ItemID.Lens, 1)
              .Register();
        }
    }
}
