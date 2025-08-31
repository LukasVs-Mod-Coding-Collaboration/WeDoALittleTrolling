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
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Content.Tiles;

namespace WeDoALittleTrolling.Content.Items.Placeable
{
    public class WormCandle : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ShadowCandle);
            Item.width = 12;
            Item.height = 24;
            Item.createTile = ModContent.TileType<WormCandleTile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddTile(TileID.ShimmerMonolith)
            .AddIngredient(ItemID.Candle, 1)
            .AddIngredient(ItemID.Worm, 10)
            .AddCustomShimmerResult(ItemID.Candle, 1)
            .Register();

            CreateRecipe(1)
            .AddTile(TileID.ShimmerMonolith)
            .AddIngredient(ItemID.PlatinumCandle, 1)
            .AddIngredient(ItemID.Worm, 10)
            .AddCustomShimmerResult(ItemID.PlatinumCandle, 1)
            .Register();
        }
    }
}
