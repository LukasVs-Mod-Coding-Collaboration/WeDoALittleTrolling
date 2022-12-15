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
       

        }



        public override bool? UseItem(Player player)
        {
            Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(player.position.X, player.position.Y), new Vector2(0, 1), ProjectileID.Typhoon, 250, 6f, Main.myPlayer);
            return true;
        }


        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-36.5f, -5f);
        }


    }
}
