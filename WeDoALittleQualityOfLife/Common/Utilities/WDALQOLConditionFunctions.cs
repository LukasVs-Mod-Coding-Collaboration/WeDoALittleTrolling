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

namespace WeDoALittleQualityOfLife.Common.Utilities
{
    internal static class WDALQOLConditionFunctions
    {
        public static bool GetFalse()
        {
            return false;
        }

        public static bool HasAnkhShieldInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.AnkhShield))
            {
                return true;
            }
            return false;
        }

        public static bool HasCellPhoneInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.CellPhone))
            {
                return true;
            }
            return false;
        }

        public static bool HasTerrasparkBootsInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.TerrasparkBoots))
            {
                return true;
            }
            return false;
        }

        public static bool HasArcticDivingGearInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.ArcticDivingGear))
            {
                return true;
            }
            return false;
        }

        public static bool HasCelestialCuffsInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.CelestialCuffs))
            {
                return true;
            }
            return false;
        }

        public static bool HasCelestialEmblemInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.CelestialEmblem))
            {
                return true;
            }
            return false;
        }

        public static bool HasCelestialShellInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.CelestialShell))
            {
                return true;
            }
            return false;
        }

        public static bool HasFireGauntletInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.FireGauntlet))
            {
                return true;
            }
            return false;
        }

        public static bool HasFrogGearInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.FrogGear))
            {
                return true;
            }
            return false;
        }

        public static bool HasLavaproofTackleBagInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.LavaproofTackleBag))
            {
                return true;
            }
            return false;
        }

        public static bool HasSniperScopeInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.SniperScope))
            {
                return true;
            }
            return false;
        }

        public static bool HasMasterNinjaGearInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.MasterNinjaGear))
            {
                return true;
            }
            return false;
        }

        public static bool HasPiranhaGunInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.PiranhaGun))
            {
                return true;
            }
            return false;
        }

        public static bool HasScourgeoftheCorruptorInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.ScourgeoftheCorruptor))
            {
                return true;
            }
            return false;
        }

        public static bool HasVampireKnivesInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.VampireKnives))
            {
                return true;
            }
            return false;
        }

        public static bool HasRainbowGunInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.RainbowGun))
            {
                return true;
            }
            return false;
        }

        public static bool HasStaffoftheFrostHydraInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.StaffoftheFrostHydra))
            {
                return true;
            }
            return false;
        }

        public static bool HasStormTigerStaffInInventory()
        {
            if (Main.player[Main.myPlayer].HasItem(ItemID.StormTigerStaff))
            {
                return true;
            }
            return false;
        }
    }
}
