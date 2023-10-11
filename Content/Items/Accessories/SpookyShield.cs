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
using WeDoALittleTrolling.Common.ModPlayers;
using System;
using System.Collections.Generic;

namespace WeDoALittleTrolling.Content.Items.Accessories
{

    [AutoloadEquip(EquipType.Shield)]
    public class SpookyShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 8);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine spookyBonus0 = new TooltipLine(Mod, "SpookyBonus0", "Current reduced damage taken bonus: "+Main.player[Main.myPlayer].GetModPlayer<WDALTPlayer>().spookyBonus3X+"%");
            TooltipLine spookyBonus1 = new TooltipLine(Mod, "SpookyBonus1", "Current defense bonus: "+Main.player[Main.myPlayer].GetModPlayer<WDALTPlayer>().spookyBonus3X);
            tooltips.Add(spookyBonus0);
            tooltips.Add(spookyBonus1);
            base.ModifyTooltips(tooltips);
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.dashType = 1;
            player.maxMinions += 2;
            player.aggro += 400;
            player.statDefense += player.GetModPlayer<WDALTPlayer>().spookyBonus3X;
            player.endurance += (((float)player.GetModPlayer<WDALTPlayer>().spookyBonus3X) * 0.01f);
            base.UpdateAccessory(player, hideVisual);
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                equippedItem.type == ModContent.ItemType<SpookyEmblem>() ||
                incomingItem.type == ModContent.ItemType<SpookyEmblem>()
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
              .AddIngredient(ItemID.Tabi, 1)
              .Register();
        }
    }
}
