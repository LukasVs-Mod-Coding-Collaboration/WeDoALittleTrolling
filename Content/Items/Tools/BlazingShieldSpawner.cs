using Terraria;
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Content.Projectiles;

namespace WeDoALittleTrolling.Content.Items.Tools
{
    internal class BlazingShieldSpawner : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 18;

            Item.consumable = false;

            Item.value = Item.sellPrice(platinum: 1);
            Item.maxStack = 1;

            Item.autoReuse = false;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTurn = false;
            Item.UseSound = SoundID.Item1;

            Item.rare = ItemRarityID.Red;
            Item.scale = 1.0f;

            Item.shoot = ModContent.ProjectileType<BlazingShield>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<BlazingShield>(), damage, knockback, player.whoAmI);
        }
    }
}