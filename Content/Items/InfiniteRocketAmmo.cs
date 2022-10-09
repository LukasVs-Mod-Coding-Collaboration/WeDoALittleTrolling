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

        Random rnd = new Random();
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinite Rocket I");
            Tooltip.SetDefault("It seems to multiply as you load it into your weapon...");
        }


        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 50;

            Item.value = Item.buyPrice(copper: 1);
            Item.maxStack = 1;

            Item.damage = 40;
            Item.knockBack = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ItemRarityID.Red;
            Item.ammo = AmmoID.Rocket;
            Item.shoot = ProjectileID.RocketI;
        }

        //Set allowed weapons
        public override bool? CanBeChosenAsAmmo(Item weapon, Player player)
        {
            switch(weapon.type)
            {
                case ItemID.Celeb2:
                    return true;
                case ItemID.SnowmanCannon:
                    return true;
                case ItemID.FireworksLauncher:
                    return true;
                case ItemID.ElectrosphereLauncher:
                    return true;
                case ItemID.GrenadeLauncher:
                    return true;
                case ItemID.ProximityMineLauncher:
                    return true;
                case ItemID.RocketLauncher:
                    return true;
                default:
                    return false;
            }
        }
        
        //Handle ammo conversions
        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
        {
            switch(weapon.type)
            {
                case ItemID.Celeb2:
                    type = ProjectileID.Celeb2Rocket;
                    break;
                case ItemID.SnowmanCannon:
                    type = ProjectileID.RocketSnowmanI;
                    break;
                case ItemID.FireworksLauncher:
                    switch(rnd.Next(0, 4))
                    {
                        case 0:
                            type = ProjectileID.RocketFireworkBlue;
                            break;
                        case 1:
                            type = ProjectileID.RocketFireworkGreen;
                            break;
                        case 2:
                            type = ProjectileID.RocketFireworkRed;
                            break;
                        case 3:
                            type = ProjectileID.RocketFireworkYellow;
                            break;
                        default:
                            type = ProjectileID.RocketFireworkBlue;
                            break;
                    }
                    break;
                case ItemID.ElectrosphereLauncher:
                    type = ProjectileID.ElectrosphereMissile;
                    break;
                case ItemID.GrenadeLauncher:
                    type = ProjectileID.GrenadeI;
                    break;
                case ItemID.ProximityMineLauncher:
                    type = ProjectileID.ProximityMineI;
                    break;
                case ItemID.RocketLauncher:
                    type = ProjectileID.RocketI;
                    break;
                default:
                    break;
            }
            base.PickAmmo(weapon, player, ref type, ref speed, ref damage, ref knockback);
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
