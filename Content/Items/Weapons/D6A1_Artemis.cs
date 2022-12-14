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
    internal class D6A1_Artemis : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a Laser powerful enough to eliminate most enemies");
            DisplayName.SetDefault("D6A1 - Artemis");
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
            Item.UseSound = SoundID.Item84;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 1600;
            Item.knockBack = 5f;
            Item.noMelee = true;
            Item.crit = 0;
            Item.shoot = ModContent.ProjectileType<Beamlaser1>();
            Item.shootSpeed = 1.0f;
            Item.autoReuse = true;
       

        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 shootDirection = velocity;
            shootDirection.Normalize();
            float xOffset = shootDirection.X * 84.0f;
            float yOffset = shootDirection.Y * 84.0f;
            position = new Vector2(position.X + xOffset, position.Y + yOffset - 10.0f);
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-36.5f, -5.0f);
        }

    }
}
