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
using WeDoALittleTrolling.Content.Projectiles;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class D6A2_Apollon : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a Laser powerful enough to vaporise a spaceship");
            DisplayName.SetDefault("D6A2 - Apollon");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 144;
            Item.height = 57;
            Item.scale = 0.75f;
            Item.rare = ItemRarityID.Expert;

            Item.useTime = 120;
            Item.useAnimation = 120;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = false;

            Item.UseSound = SoundID.Item1;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 16000;
            Item.knockBack = 5f;
            Item.noMelee = true;
            Item.crit = -300;

            Item.shoot = ProjectileID.Bullet; // ModContent.ProjectileType<Beamlaser1>();
            Item.shootSpeed = 30f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-36.5f, -5f);
        }





        //Dropped Code from Previous Versions of the Weapon that did not help my goal.

        /*
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            new Vector2(position.X, position.Y - 120);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        */

        /*

     public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
     {
         Vector2 muzzleOffset = Vector2.Normalize(velocity) * 50f;
         if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
         {
             position += muzzleOffset;
         }
     }
     */
    }
}
