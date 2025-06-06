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
    internal class ShroomiteOvercharge : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 62;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 12);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!player.channel)
            {
                player.GetDamage(DamageClass.Ranged) *= (float)1.25;
                player.GetCritChance(DamageClass.Ranged) += (float)5.0;
                player.GetAttackSpeed(DamageClass.Ranged) *= (float)0.9;
            }
            else
            {
                player.GetDamage(DamageClass.Ranged) *= (float)1.15;
                player.GetCritChance(DamageClass.Ranged) += (float)5.0;
            }
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                equippedItem.type == ModContent.ItemType<ShroomiteOverdrive>() ||
                incomingItem.type == ModContent.ItemType<ShroomiteOverdrive>()
            )
            {
                return false;
            }
            else
            {
                return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.TinkerersWorkbench)
              .AddIngredient(ItemID.ShroomiteBar, 12)
              .AddIngredient(ItemID.RifleScope, 1)
              .AddIngredient(ItemID.RangerEmblem, 1)
              .Register();
        }
    }
}
