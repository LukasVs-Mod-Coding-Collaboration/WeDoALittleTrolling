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
using Terraria.Localization;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Content.Items.Material;

namespace WeDoALittleTrolling.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class SearingHelmet : ModItem
    {
        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            SetBonusText = LocalizedText.Literal("Sears attackers, greatly reducing damage\nGrants immunity to Searing Inferno");
            AddArmorSet<SearingHelmet, SearingBreastplate, SearingLeggings>(SetBonusText, PrimaryPart: ArmorSetBonus.PartType.None, Effect: SearingBonus);
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 15;
        }

        public override void UpdateArmorSet(Player player, ArmorSetBonus armorSetBonus)
        {
            //Can be used as an alternative to the effect function.
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
