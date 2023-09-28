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
using WeDoALittleTrolling.Content.AccessorySlots;
using WeDoALittleTrolling.Content.Buffs.PowerMasks;

namespace WeDoALittleTrolling.Content.Items.Accessories.PowerMasks
{
    internal class EternalMask : ModItem
    {
        public const float MaskRange = 2048f;
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 38;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 8);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
            Item.GetGlobalItem<WDALTPowerMaskItemUtil>().isPowerMask = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if(modded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<WDALTPowerMaskUtil>().EternalMask = true;
            if (player.whoAmI != Main.myPlayer && player.miscCounter % 30 == 0)
            {
                Player MyPlayer = Main.player[Main.myPlayer];
                if (MyPlayer.team == player.team && player.team != 0 && Vector2.Distance(player.Center, MyPlayer.Center) <= MaskRange)
                {
                    MyPlayer.AddBuff(ModContent.BuffType<EternalPower>(), 60);
                }
            }
            base.UpdateAccessory(player, hideVisual);
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem,player);
        }
    }
}
