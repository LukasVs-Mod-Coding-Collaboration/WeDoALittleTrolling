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
using System;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class ShroomiteGenesis : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 26;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 12);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<WDALTPlayer>().shroomiteGenesis = true;
        }

        /*
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                equippedItem.type == ModContent.ItemType<ShroomiteOvercharge>() ||
                incomingItem.type == ModContent.ItemType<ShroomiteOvercharge>()
            )
            {
                return false;
            }
            else
            {
                return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
            }
        }
        */
        //Nope, Compatible with the other two. The big drawback to this (Like your weapons overheating or something) will be added later. Have fun for now, lmao

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.TinkerersWorkbench)
              .AddTile(TileID.CrystalBall)
              .AddIngredient(ItemID.ShroomiteBar, 12)
              .AddIngredient(ItemID.AmmoBox, 1)
              .AddIngredient(ItemID.RangerEmblem, 1)
              .AddIngredient(ItemID.EndlessMusketPouch, 1)
              .AddIngredient(ItemID.EndlessQuiver, 1)
              .AddIngredient(ItemID.EndlessQuiver, 1)
              .AddIngredient(ModContent.ItemType<InfiniteRocketAmmo>(), 1)
              .Register();
        }
    }
}
