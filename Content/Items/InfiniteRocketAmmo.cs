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


namespace WeDoALittleTrolling.Content.Items
{
    internal class InfiniteRocketAmmo : ModItem
    {



        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinite Rocket I");
            Tooltip.SetDefault("It seems to multiply as you load it into your weapon...\nCan not be used with Celebration Mk2");
        }


        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 50;

            Item.value = Item.buyPrice(copper: 1);
            Item.maxStack = 1;

            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;

            Item.rare = ItemRarityID.Red;
            Item.shoot = ProjectileID.RocketI - 134; //ProjectileID - 134 = Rocket Projectile ID, THE WHAT TERRARIA DEVS
            Item.ammo = AmmoID.Rocket;

        }

        //Disable usage with Celebration MK 2 for now as it is bugged
        public override bool? CanBeChosenAsAmmo(Item weapon, Player player)
        {
            if(weapon.type == ItemID.Celeb2)
            {
                return false;
            }
            else
            {
                return base.CanBeChosenAsAmmo(weapon, player);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.CrystalBall)
              .AddIngredient(ItemID.RocketI, 3000)
              .AddIngredient(ItemID.Ectoplasm, 15)
              .Register();
        }
    }
}
