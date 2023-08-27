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

using System;
using Terraria;
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal static class WDALTModContentID
    {
        public const int ThoriumBoss_GTB = 0;
        public const int ThoriumBoss_QJ = 1;
        public const int ThoriumBoss_VC = 2;
        public const int ThoriumBoss_GES = 3;
        public const int ThoriumBoss_BC = 4;
        public const int ThoriumBoss_SCS = 5;
        public const int ThoriumBoss_BS_V1 = 6;
        public const int ThoriumBoss_BS_V2 = 7;
        public const int ThoriumBoss_FB_V1 = 8;
        public const int ThoriumBoss_FB_V2 = 9;
        public const int ThoriumBoss_LI_V1 = 10;
        public const int ThoriumBoss_LI_V2 = 11;
        public const int ThoriumBoss_FO_V1 = 12;
        public const int ThoriumBoss_FO_V2 = 13;
        public const int ThoriumBoss_FO_V3 = 14;
        public const int ThoriumBoss_AET = 15;
        public const int ThoriumBoss_OLD = 16;
        public const int ThoriumBoss_SFF = 17;
        public const int ThoriumBoss_DE = 18;
        private static readonly string[] ThoriumBossRegisterStrings =
        {
            "TheGrandThunderBird",
            "QueenJellyfish",
            "Viscount",
            "GraniteEnergyStorm",
            "BuriedChampion",
            "StarScouter",
            "BoreanStrider",
            "BoreanStriderPopped",
            "FallenBeholder",
            "FallenBeholder2",
            "Lich",
            "LichHeadless",
            "ForgottenOne",
            "ForgottenOneCracked",
            "ForgottenOneReleased",
            "Aquaius",
            "Omnicide",
            "SlagFury",
            "DreamEater"
        };
        private static int[] ThoriumBossNPCID = new int[ThoriumBossRegisterStrings.Length];
        public const int ThoriumItem_Essence_AET = 0;
        public const int ThoriumItem_Essence_OLD = 1;
        public const int ThoriumItem_Essence_SFF = 2;
        public const int ThoriumItem_Fragment_WD = 3;
        public const int ThoriumItem_Fragment_C = 4;
        public const int ThoriumItem_Fragment_STS = 5;
        private static readonly string[] ThoriumItemRegisterStrings =
        {
            "OceanEssence",
            "DeathEssence",
            "InfernoEssence",
            "WhiteDwarfFragment",
            "CelestialFragment",
            "ShootingStarFragment"
        };
        private static int[] ThoriumItemItemID = new int[ThoriumItemRegisterStrings.Length];
        private static int[] InflictWreckedResistance1in1Group_ThoriumBoss = new int[(10+1)];
        private static int[] InflictDevastated1in1Group_ThoriumBoss = new int[(1+1)];

        public static int GetThoriumBossNPCID(int modContentID)
        {
            int i = 0;
            try
            {
                i = ThoriumBossNPCID[modContentID];
            }
            catch
            {
                WeDoALittleTrolling.logger.Fatal("WDALT: ERROR: GetThoriumBossNPCID() was called with an invalid ModContentID.");
            }
            return i;
        }
        public static int GetThoriumItemID(int modContentID)
        {
            int i = 0;
            try
            {
                i = ThoriumItemItemID[modContentID];
            }
            catch
            {
                WeDoALittleTrolling.logger.Fatal("WDALT: ERROR: GetThoriumItemID was called with an invalid ModContentID.");
            }
            return i;
        }

        public static int[] GetThoriumBossInflictWreckedResistance1in1Group()
        {
            return InflictWreckedResistance1in1Group_ThoriumBoss;
        }
        public static int[] GetThoriumBossInflictDevastated1in1Group()
        {
            return InflictDevastated1in1Group_ThoriumBoss;
        }
        
        public static bool SetContentIDs()
        {
            bool integrity = true;
            if(WDALTModSystem.TryGetThoriumMod(out Mod thoriumMod))
            {
                for(int i = 0; i < ThoriumBossRegisterStrings.Length; i++)
                {
                    if(thoriumMod.TryFind(ThoriumBossRegisterStrings[i], out ModNPC bossNPC))
                    {
                        ThoriumBossNPCID[i] = bossNPC.Type;
                    }
                    else
                    {
                        integrity = false;
                    }
                }
                for(int i = 0; i < ThoriumItemRegisterStrings.Length; i++)
                {
                    if(thoriumMod.TryFind(ThoriumItemRegisterStrings[i], out ModItem item))
                    {
                        ThoriumItemItemID[i] = item.Type;
                    }
                    else
                    {
                        integrity = false;
                    }
                }
            }
            if(integrity)
            {
                InflictWreckedResistance1in1Group_ThoriumBoss[0] = GetThoriumBossNPCID(ThoriumBoss_BS_V1);
                InflictWreckedResistance1in1Group_ThoriumBoss[1] = GetThoriumBossNPCID(ThoriumBoss_BS_V2);
                InflictWreckedResistance1in1Group_ThoriumBoss[2] = GetThoriumBossNPCID(ThoriumBoss_FB_V1);
                InflictWreckedResistance1in1Group_ThoriumBoss[3] = GetThoriumBossNPCID(ThoriumBoss_FB_V2);
                InflictWreckedResistance1in1Group_ThoriumBoss[4] = GetThoriumBossNPCID(ThoriumBoss_LI_V1);
                InflictWreckedResistance1in1Group_ThoriumBoss[5] = GetThoriumBossNPCID(ThoriumBoss_FO_V1);
                InflictWreckedResistance1in1Group_ThoriumBoss[6] = GetThoriumBossNPCID(ThoriumBoss_FO_V2);
                InflictWreckedResistance1in1Group_ThoriumBoss[7] = GetThoriumBossNPCID(ThoriumBoss_FO_V3);
                InflictWreckedResistance1in1Group_ThoriumBoss[8] = GetThoriumBossNPCID(ThoriumBoss_AET);
                InflictWreckedResistance1in1Group_ThoriumBoss[9] = GetThoriumBossNPCID(ThoriumBoss_SFF);
                InflictWreckedResistance1in1Group_ThoriumBoss[10] = GetThoriumBossNPCID(ThoriumBoss_DE);
                InflictDevastated1in1Group_ThoriumBoss[0] = GetThoriumBossNPCID(ThoriumBoss_OLD);
                InflictDevastated1in1Group_ThoriumBoss[1] = GetThoriumBossNPCID(ThoriumBoss_LI_V2);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
