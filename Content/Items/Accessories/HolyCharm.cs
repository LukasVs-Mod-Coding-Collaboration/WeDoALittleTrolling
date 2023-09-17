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
using WeDoALittleTrolling.Content.Items.Material;
using System.Collections.Generic;

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
              .AddIngredient(ItemID.HolyWater, 12)
              .AddIngredient(ModContent.ItemType<HellishFossil>(), 12)
              .AddIngredient(ModContent.ItemType<MushroomFossil>(), 12)
              .AddIngredient(ItemID.SpookyWood, 24)
              .AddIngredient(ModContent.ItemType<FrostCrystal>(), 24)
              .AddIngredient(ItemID.BrokenHeroSword, 1)
              .AddCustomShimmerResult(ItemID.PlatinumCoin, 1)
              .Register();
        }
    }
}
