/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2024 LukasV-Coding

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
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Content.Items.Material;

namespace WeDoALittleTrolling.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class SearingHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 16;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SearingBreastplate>() && legs.type == ModContent.ItemType<SearingLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<WDALTPlayer>().searingSetBonus = true;
            player.setBonus = "Attackers deal 30% reduced damage and lose life\nGrants immunity to Searing Inferno\nIncreases defense effectiveness and attack damage\nby 1% for every 4 defense you have\nCurrent bonus: "+player.GetModPlayer<WDALTPlayer>().searingSetBonusValue+"%";
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.TurtleHelmet, 1)
              .AddIngredient(ModContent.ItemType<SearingPlate>(), 2)
              .Register();
        }
    }
}
