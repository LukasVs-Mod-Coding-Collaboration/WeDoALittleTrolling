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
/*

using System;
using System.Linq;
using ReLogic.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Content.AccessorySlots
{
    internal class MaskSlot : ModAccessorySlot
    {
        public override bool DrawVanitySlot => true;
        public override string FunctionalBackgroundTexture => "Terraria/Images/Inventory_Back11";
        public override string FunctionalTexture => "Terraria/Images/Item_" + ItemID.TikiMask;
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            if(checkItem.GetGlobalItem<WDALTPowerMaskItemUtil>().isPowerMask)
            {
                return true;
            }
            return false;
        }

        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            if(item.GetGlobalItem<WDALTPowerMaskItemUtil>().isPowerMask)
            {
                return true;
            }
            return false;
        }

        public override void OnMouseHover(AccessorySlotType context) {
            switch (context) {
                case AccessorySlotType.FunctionalSlot:
                    Main.hoverItemName = "Power Mask";
                    break;
                case AccessorySlotType.VanitySlot:
                    Main.hoverItemName = "Vanity Power Mask";
                    break;
            }
        }
    }
}
*/