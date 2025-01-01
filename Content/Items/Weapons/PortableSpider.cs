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
    internal class PortableSpider : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 44;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;

            Item.value = Item.sellPrice(gold: 3);
            Item.maxStack = 1;

            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.mana = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.NPCHit29;

            Item.DamageType = DamageClass.Magic;
            Item.damage = 24;
            Item.knockBack = 8f;
            Item.crit = 8;
            Item.scale = 1f;
            Item.shoot = ModContent.ProjectileType<PortableSpiderBullet>();
            Item.shootSpeed = 12f;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.HeavyWorkBench)
                .AddIngredient(ModContent.ItemType<WallCreeperFang>(), 16)
                .AddRecipeGroup(RecipeGroupID.Wood, 4)
                .Register();
        }

    }
}
