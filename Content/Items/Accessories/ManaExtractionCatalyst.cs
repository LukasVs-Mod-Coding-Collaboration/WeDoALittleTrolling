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
using System;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class ManaExtractionCatalyst : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 34;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 5);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.LightPurple;

            Item.accessory = true; //Item is an accessory
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += -100; //Weird but working way to set Max down (originally 125)
            player.buffImmune[BuffID.ManaSickness] = true; //Immunity to Mana Sickness
            player.manaFlower = true;

        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                equippedItem.type == ModContent.ItemType<SorcerousMirror>() ||
                incomingItem.type == ModContent.ItemType<SorcerousMirror>()
            )
            {
                return false;
            }
            else
            {
                return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem,player);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe() //Recipe
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.ManaCrystal, 5)
              .AddIngredient(ItemID.ManaFlower, 1)
              .AddIngredient(ItemID.SoulofMight, 5)
              .AddIngredient(ItemID.SoulofNight, 10)
              .AddIngredient(ItemID.Obsidian, 15)
              .Register();
        }
    }
}
