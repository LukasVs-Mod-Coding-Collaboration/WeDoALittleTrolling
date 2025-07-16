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

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Content.Items.Weapons;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class HolyCharm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;

            Item.consumable = false;
            Item.noUseGraphic = true;

            Item.value = Item.sellPrice(gold: 8);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.HallowedBar, 12)
              .AddIngredient(ItemID.HolyWater, 10)
              .AddIngredient(ItemID.Ectoplasm, 10)
              .AddIngredient(ModContent.ItemType<FrozenEssence>(), 3)
              .AddCustomShimmerResult(ItemID.PlatinumCoin, 1)
              .Register();
        }
    }
}
