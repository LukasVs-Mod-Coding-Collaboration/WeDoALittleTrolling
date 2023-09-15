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
using System.Collections.Generic;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class HeartOfDespair : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 8);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonus = 0;
            if(Main.player[Main.myPlayer].GetModPlayer<WDALTPlayerUtil>().heartOfDespairDamageBonus > 0)
            {
                bonus = Main.player[Main.myPlayer].GetModPlayer<WDALTPlayerUtil>().heartOfDespairDamageBonus;
            }
            TooltipLine despairBonus0 = new TooltipLine(Mod, "DespairBonus0", "Current damage bonus: "+bonus+"%");
            tooltips.Add(despairBonus0);
            /*
            TooltipLine incompatible0 = new TooltipLine(Mod, "Incompatible0", "Cannot be equipped when the\nSorcerous Mirror is equipped");
            tooltips.Add(incompatible0);
            */
            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if(player.GetModPlayer<WDALTPlayerUtil>().heartOfDespairDamageBonus >= 0f)
            {
                player.GetDamage(DamageClass.Magic) += (0.01f + (0.01f * (float)player.GetModPlayer<WDALTPlayerUtil>().heartOfDespairDamageBonus));
            }
            player.GetModPlayer<WDALTPlayerUtil>().heartOfDespair = true;
        }

        /*
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if(player.GetModPlayer<WDALTPlayerUtil>().sorcerousMirror)
            {
                return false;
            }
            return base.CanEquipAccessory(player, slot, modded);
        }
        */

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.DemonAltar)
              .AddIngredient(ItemID.DemoniteBar, 5)
              .AddIngredient(ItemID.SoulofFright, 1)
              .AddIngredient(ItemID.HellstoneBar, 3)
              .AddIngredient(ItemID.VilePowder, 3)
              .AddIngredient(ItemID.AshBlock, 3)
              .Register();

            CreateRecipe()
              .AddTile(TileID.DemonAltar)
              .AddIngredient(ItemID.CrimtaneBar, 5)
              .AddIngredient(ItemID.SoulofFright, 1)
              .AddIngredient(ItemID.HellstoneBar, 3)
              .AddIngredient(ItemID.VilePowder, 3)
              .AddIngredient(ItemID.AshBlock, 3)
              .Register();
        }
    }
}
