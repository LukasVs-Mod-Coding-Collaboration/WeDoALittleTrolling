/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2026 LukasV-Coding

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
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Content.Items.Material;

namespace WeDoALittleTrolling.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class WakewoodHelmet : ModItem
    {

		public override void SetStaticDefaults() {

			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
	
		}

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 16;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 1;
            player.maxTurrets += 1;
            player.statManaMax2 += 40;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.LivingLoom)
              .AddIngredient(ItemID.Wood, 15)
              .AddIngredient(ItemID.Acorn, 3)
              .AddIngredient(ItemID.Daybloom, 2)
              .Register();
              // Leggings will be 20, 6, 3
        }
    }
}
