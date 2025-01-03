﻿/*
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

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class D6A1_Artemis : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 24;
            Item.scale = 1.125f;
            Item.rare = ItemRarityID.Expert;

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item84;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 1600;
            Item.knockBack = 5f;
            Item.noMelee = true;
            Item.crit = 0;
            //Item.shoot = ModContent.ProjectileType<Beamlaser1>();
            Item.shoot = ModContent.ProjectileType<ArtemissileProjectile>();
            Item.shootSpeed = 16.0f;
            Item.autoReuse = true;


        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 shootDirection = velocity;
            shootDirection = shootDirection.SafeNormalize(Vector2.Zero);
            float xOffset = shootDirection.X * 64.0f;
            float yOffset = shootDirection.Y * 64.0f;
            position = new Vector2(position.X + xOffset, position.Y + yOffset - 8.0f);
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14.0f, -4.0f);
        }

        public override void AddRecipes()
        {
            if(WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
            {
                CreateRecipe()
                    .AddTile(TileID.LunarCraftingStation)
                    .AddIngredient(ItemID.Celeb2, 1)
                    .AddIngredient(ItemID.LunarBar, 10)
                    .AddIngredient(WDALTModContentID.GetThoriumItemID(WDALTModContentID.ThoriumItem_Fragment_STS), 10)
                    .Register();
            }
        }

    }
}
