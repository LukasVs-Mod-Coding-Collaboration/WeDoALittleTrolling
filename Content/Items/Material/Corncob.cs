/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2023 LukasV-Coding

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

namespace WeDoALittleTrolling.Content.Items.Material
{
    internal class Corncob : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;

            Item.material = true;
            Item.consumable = true;
            Item.noUseGraphic = false;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = (60 * 60 * 8);

            Item.value = Item.sellPrice(copper: 25);
            Item.maxStack = Item.CommonMaxStack;

            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(ItemID.Sunflower, 5)
            .AddIngredient(ItemID.Pumpkin, 10)
            .AddTile(TileID.ShimmerMonolith)
            .Register();
        }
    }
}
