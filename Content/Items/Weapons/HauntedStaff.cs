/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2024 LukasV-Coding

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
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using Microsoft.CodeAnalysis;
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class HauntedStaff : ModItem
    {
        public static UnifiedRandom random = new UnifiedRandom();

        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 46;

            Item.consumable = false;

            Item.value = Item.sellPrice(gold: 1);
            Item.maxStack = 1;

            Item.autoReuse = true;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.mana = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item45;

            Item.damage = 24;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 8f;

            Item.rare = ItemRarityID.Orange;
            Item.scale = 1f;

            Item.shoot = ProjectileID.FlamingJack;
            Item.shootSpeed = 4f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.HeavyWorkBench)
              .AddIngredient(ItemID.Pumpkin, 64)
              .AddIngredient(ItemID.DemoniteBar, 8)
              .AddIngredient(ItemID.ShadowScale, 4)
              .AddIngredient(ItemID.Amber, 4)
              .Register();

            CreateRecipe()
              .AddTile(TileID.HeavyWorkBench)
              .AddIngredient(ItemID.Pumpkin, 64)
              .AddIngredient(ItemID.CrimtaneBar, 8)
              .AddIngredient(ItemID.TissueSample, 4)
              .AddIngredient(ItemID.Amber, 4)
              .Register();
        }
    }
}
