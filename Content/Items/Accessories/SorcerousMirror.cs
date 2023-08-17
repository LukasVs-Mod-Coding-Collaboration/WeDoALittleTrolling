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
using WeDoALittleTrolling.Content.Buffs;
using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class SorcerousMirror : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 5);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.LightPurple;

            Item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string bonus = "No";
            if(Main.player[Main.myPlayer].HeldItem.DamageType == DamageClass.Magic)
            {
                bonus = "Yes";
            }
            if(Main.player[Main.myPlayer].HasBuff(ModContent.BuffType<Devastated>()))
            {
                bonus = "No, you are devastated!";
            }
            TooltipLine dodgeBonus0 = new TooltipLine(Mod, "DodgeBonus0", "Dodge chance active: "+bonus);
            tooltips.Add(dodgeBonus0);
            /*
            TooltipLine incompatible0 = new TooltipLine(Mod, "Incompatible0", "Cannot be equipped when the\nHeart of Despair is equipped");
            tooltips.Add(incompatible0);
            */
            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<WDALTPlayerUtil>().sorcerousMirror = true;
            base.UpdateAccessory(player, hideVisual);
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                equippedItem.type == ModContent.ItemType<ManaExtractionCatalyst>() ||
                incomingItem.type == ModContent.ItemType<ManaExtractionCatalyst>()
            )
            {
                return false;
            }
            else
            {
                return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem,player);
            }
        }

        /*
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if(player.GetModPlayer<WDALTPlayerUtil>().heartOfDespair)
            {
                return false;
            }
            return base.CanEquipAccessory(player, slot, modded);
        }
        */

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.LifeCrystal, 5)
              .AddIngredient(ItemID.SoulofMight, 5)
              .AddIngredient(ItemID.SoulofLight, 10)
              .AddIngredient(ItemID.Obsidian, 15)
              .AddIngredient(ItemID.HallowedBar, 3)
              .Register();
        }
    }
}
