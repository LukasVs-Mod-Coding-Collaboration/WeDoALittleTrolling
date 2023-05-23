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

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 10;
            Item.knockBack = 0.5f;
            Item.noMelee = true;
            Item.crit = 0;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 10.0f;
            Item.autoReuse = true;


        }

        /*
          
         
        public override bool AltFunctionUse(Player player)
        {
            return false;
        }


        */


        // public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) { }


        /*
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-36.5f, -5.0f);
        }
        */

    }
}