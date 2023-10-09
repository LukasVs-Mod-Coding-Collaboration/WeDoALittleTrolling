using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Projectiles;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    public class ThrowOfTheLunatic : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 21;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;

            Item.damage = 185;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 5f;
            Item.crit = 5;
            Item.channel = true;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(gold: 10);

            Item.shoot = ModContent.ProjectileType<ThrowOfTheLunaticProjectile>();
            Item.shootSpeed = 16f;       
        }
    }
}
