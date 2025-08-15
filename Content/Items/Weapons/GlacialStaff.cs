/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2025 LukasV-Coding

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Common.ModSystems;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using WeDoALittleTrolling.Content.Items.Material;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class GlacialStaff : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;

            Item.consumable = false;

            Item.value = Item.sellPrice(gold: 20);
            Item.maxStack = 1;

            Item.autoReuse = true;

            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.mana = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;

            Item.DamageType = DamageClass.Magic;
            Item.damage = 64;
            Item.knockBack = 8f;
            Item.crit = 4;
            Item.scale = 1f;
            Item.shoot = ModContent.ProjectileType<GlacialTear>();
            Item.shootSpeed = 16f;
            Item.autoReuse = true;

            Item.rare = ItemRarityID.LightPurple;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 4; i++)
            {
                float rotation = -3f + (i * 2f);
                Projectile.NewProjectileDirect
                (
                    player.GetSource_ItemUse(Item),
                    position,
                    velocity.RotatedBy(MathHelper.ToRadians(rotation)),
                    ModContent.ProjectileType<GlacialTear>(),
                    damage,
                    knockback,
                    player.whoAmI
                );
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.ShimmerMonolith)
              .AddIngredient(ModContent.ItemType<FrozenEssence>(), 8)
              .AddIngredient(ItemID.HallowedBar, 8)
              .Register();
        }

    }
}
