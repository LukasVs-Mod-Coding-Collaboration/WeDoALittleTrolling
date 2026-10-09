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
    public class SearingHelmet : ModItem
    {
        public static void AddArmorSetBonus()
        {
            /* TAPI_1.4.5.8: Use new ArmorSetBonuses engine */
            ArmorSetBonuses.Add
            (
                SearingBonus,
                "Sears attackers, greatly reducing damage\nGrants immunity to Searing Inferno",
                ModContent.ItemType<SearingHelmet>(),
                ModContent.ItemType<SearingBreastplate>(),
                ModContent.ItemType<SearingLeggings>()
            );
        }

        public static void RemoveArmorSetBonus()
        {
            ArmorSetBonuses.All.RemoveAll((ArmorSetBonus b) => {
                return b != null &&
                b.Head == ModContent.ItemType<SearingHelmet>() &&
                b.Body == ModContent.ItemType<SearingBreastplate>() &&
                b.Legs ==  ModContent.ItemType<SearingLeggings>();
            });
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<WDALTPlayer>().searingValue += 4;
            player.lifeRegen += 6;
        }

        public static void SearingBonus(Player player)
        {
            player.GetModPlayer<WDALTPlayer>().searingSetBonus = true;
        }

        /* TAPI_1.4.5.8: DEPRECATED
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<SearingHelmet>() && body.type == ModContent.ItemType<SearingBreastplate>() && legs.type == ModContent.ItemType<SearingLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<WDALTPlayer>().searingSetBonus = true;
            player.setBonus = "Sears attackers, greatly reducing damage\nGrants immunity to Searing Inferno";
        }*/

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
