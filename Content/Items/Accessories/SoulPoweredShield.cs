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

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class SoulPoweredShield : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 25);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Expert; //Expert Mode Item

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.25f; //Damage Reduction (originally 0.25)
            player.statDefense += 5; // (originally 7)
            player.noKnockback = true;
            player.lavaImmune = true; //Immunity to Lava and Fire blocks
            player.fireWalk = true;
            player.buffImmune[44] = true; //Debuff Immunities against fire & frost debuffs
            player.buffImmune[39] = true;
            player.buffImmune[24] = true;
            player.buffImmune[47] = true;
            player.buffImmune[46] = true;
            player.buffImmune[67] = true;
            player.buffImmune[69] = true;
            player.buffImmune[144] = true;
            player.buffImmune[153] = true;

        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                (equippedItem.type == ItemID.WormScarf || incomingItem.type == ItemID.WormScarf)
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
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.WormScarf, 1)
              .AddIngredient(ItemID.SoulofMight, 5)
              .AddIngredient(ItemID.HallowedBar, 10)
              .AddIngredient(ItemID.CobaltShield, 1)
              .AddIngredient(ItemID.Lens, 1)
              .Register();
        }
    }
}
