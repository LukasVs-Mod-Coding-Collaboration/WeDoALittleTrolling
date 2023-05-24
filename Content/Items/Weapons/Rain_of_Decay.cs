using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class Rain_of_Decay : ModItem
    {
        public bool autoAim = true;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 142;
            Item.rare = ItemRarityID.Expert;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 36;
            Item.knockBack = 1.25f;
            Item.noMelee = true;
            Item.crit = 0;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 13.0f;
            Item.useAmmo = AmmoID.Arrow;
            Item.autoReuse = true;


        }


        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ProjectileID.CursedArrow;
            }
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 4;
            float rotation = MathHelper.ToRadians(5);
            position += Vector2.Normalize(velocity) * 5f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 burstArrowSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles)));
                Projectile.NewProjectile(source, position, burstArrowSpeed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }


        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.HeavyWorkBench)
              .AddIngredient(ItemID.CursedFlame, 25)
              .AddIngredient(ItemID.AshWood, 20)
              .AddIngredient(ItemID.IronBar, 15)
              .AddIngredient(ItemID.SoulofNight, 10)
              .AddIngredient(ItemID.SoulofSight, 5)
              .AddIngredient(ItemID.JungleSpores, 5)
              .AddIngredient(ItemID.WhiteString, 1)
              .Register();


            CreateRecipe()
             .AddTile(TileID.HeavyWorkBench)
             .AddIngredient(ItemID.Ichor, 25)
             .AddIngredient(ItemID.AshWood, 20)
             .AddIngredient(ItemID.IronBar, 15)
             .AddIngredient(ItemID.SoulofNight, 10)
             .AddIngredient(ItemID.SoulofSight, 5)
             .AddIngredient(ItemID.JungleSpores, 5)
             .AddIngredient(ItemID.WhiteString, 1)
             .Register();
        }

    }
}