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
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Content.Items.Accessories;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class CrimsonAssassinGear : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 10);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Red;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noFallDmg = true;
            player.dashType = 1;
            player.longInvince = true;
            player.panic = true;
            player.spikedBoots = 2;
            player.blackBelt = true;
            player.brainOfConfusionItem = Item;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.SoulofFright, 5)
              .AddIngredient(ItemID.CrimtaneBar, 5)
              .AddIngredient(ItemID.MasterNinjaGear, 1)
              .AddIngredient(ItemID.BrainOfConfusion, 1)
              .AddIngredient(ItemID.CrossNecklace, 1)
              .AddIngredient(ItemID.PanicNecklace, 1)
              .Register();
        }
    }
}