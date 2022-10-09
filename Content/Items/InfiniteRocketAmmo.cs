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


        //Fix wrong Projectile IDs
        public int FixProjectileID(int projectile_id, int ammo_id)
        {
            if (ammo_id == AmmoID.Rocket)
            {
                return projectile_id - 134; //ProjectileID - 134 = Rocket ProjectileID, THE WHAT TERRARIA DEVS
            }
            else
            {
                return projectile_id;
            }
        }
        
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
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ItemRarityID.Red;
            Item.ammo = AmmoID.Rocket;
            Item.shoot = FixProjectileID(ProjectileID.RocketI, Item.ammo);
        }

        //Celeb2 is stupid and only fires vanilla projectiles...
        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
        {
            if(weapon.type == ItemID.Celeb2)
            {
                type = ProjectileID.Celeb2Rocket;
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
