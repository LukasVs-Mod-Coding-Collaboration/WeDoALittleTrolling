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
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class MinerEmblem : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 1);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Blue;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.fireWalk = true;
            float pickSpeedModifier = 0.20f;
            player.pickSpeed = player.pickSpeed - (player.pickSpeed * pickSpeedModifier);

        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.Anvils)
              .AddIngredient(ItemID.GoldPickaxe, 1)
              .AddIngredient(ItemID.StoneBlock, 75)
              .AddRecipeGroup(RecipeGroupID.IronBar, 15)
              .AddIngredient(ItemID.FallenStar, 3)
              .Register();

            CreateRecipe()
             .AddTile(TileID.Anvils)
             .AddIngredient(ItemID.PlatinumPickaxe, 1)
             .AddIngredient(ItemID.StoneBlock, 75)
             .AddRecipeGroup(RecipeGroupID.IronBar, 15)
             .AddIngredient(ItemID.FallenStar, 3)
             .Register();
        }
    }
}
