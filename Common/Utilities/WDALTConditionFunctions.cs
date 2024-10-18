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

using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Items.Accessories;

namespace WeDoALittleTrolling.Common.Utilities
{
    internal static class WDALTConditionFunctions
    {
        public static bool HasHolyCharmInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ModContent.ItemType<HolyCharm>()))
            {
                return true;
            }
            return false;
        }

        public static bool HasTier1FishingQuests()
        {
            if(Main.player[Main.myPlayer].anglerQuestsFinished >= 5 && Main.player[Main.myPlayer].ZoneBeach)
            {
                return true;
            }
            return false;
        }

        public static bool HasTier2FishingQuests()
        {
            if(Main.player[Main.myPlayer].anglerQuestsFinished >= 10 && Main.player[Main.myPlayer].ZoneBeach)
            {
                return true;
            }
            return false;
        }

        public static bool HasTier3FishingQuests()
        {
            if(Main.player[Main.myPlayer].anglerQuestsFinished >= 15 && Main.player[Main.myPlayer].ZoneBeach)
            {
                return true;
            }
            return false;
        }
    }
}
