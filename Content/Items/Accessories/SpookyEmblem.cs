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
using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class SpookyEmblem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 8);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine spookyBonus0 = new TooltipLine(Mod, "SpookyBonus0", "Current critical strike chance bonus: "+Main.player[Main.myPlayer].GetModPlayer<WDALTPlayerUtil>().spookyBonus+"%");
            string spookyBonus1Text = "Current armor penetration bonus: "+Main.player[Main.myPlayer].GetModPlayer<WDALTPlayerUtil>().spookyBonusScalingWithDifficulty;
            if(Main.masterMode)
            {
                spookyBonus1Text = spookyBonus1Text+" (Master Mode: x2.0)";
            }
            else if(Main.expertMode)
            {
                spookyBonus1Text = spookyBonus1Text+" (Expert Mode: x1.5)";
            }
            TooltipLine spookyBonus1 = new TooltipLine(Mod, "SpookyBonus1", spookyBonus1Text);
            tooltips.Add(spookyBonus0);
            tooltips.Add(spookyBonus1);
            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.blackBelt = true;
            player.maxMinions += 1;
            player.aggro -= 800;
            player.GetModPlayer<WDALTPlayerUtil>().spookyEmblem = true;
            base.UpdateAccessory(player, hideVisual);
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                equippedItem.type == ModContent.ItemType<SpookyShield>() ||
                incomingItem.type == ModContent.ItemType<SpookyShield>()
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
              .AddTile(TileID.TinkerersWorkbench)
              .AddIngredient(ItemID.SpookyWood, 250)
              .AddIngredient(ItemID.NecromanticScroll, 1)
              .AddIngredient(ItemID.BlackBelt, 1)
              .Register();
        }
    }
}
