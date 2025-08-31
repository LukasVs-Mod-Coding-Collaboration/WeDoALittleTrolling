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

namespace WeDoALittleTrolling.Content.Items
{
    internal class Consumablebee : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;

            Item.consumable = true; //Makes item Consumable

            Item.value = Item.buyPrice(silver: 10);
            Item.maxStack = 50;

            Item.autoReuse = false;
            Item.useTime = 25;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;

            Item.buffType = BuffID.WellFed2; //Buff type on use
            Item.buffTime = 10800; //60 value = 1 Second

            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.CookingPots)
              .AddIngredient(ItemID.BeeWax, 5)
              .Register();





        }



    }
}
