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
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal static class WDALTModContentID
    {
        //IDs for content from other mods to make cross-compatibility possible
        public const int ThoriumDamageClass_BardDamage = 0;
        public const int ThoriumDamageClass_HealerDamage = 1;
        public const int ThoriumDamageClass_HealerTool = 2;
        public const int ThoriumDamageClass_HealerToolDamageHybrid = 3;
        public const int ThoriumDamageClass_TrueDamage = 4;
        private static readonly string[] ThoriumDamageClassRegisterStrings =
        {
            "BardDamage",
            "HealerDamage",
            "HealerTool",
            "HealerToolDamageHybrid",
            "TrueDamage"
        };
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
        public const int ThoriumBossProjectile_BS_V1 = 0;
        public const int ThoriumBossProjectile_BS_V2 = 1;
        public const int ThoriumBossProjectile_BS_V3 = 2;
        public const int ThoriumBossProjectile_BS_V4 = 3;
        public const int ThoriumBossProjectile_FB_V1 = 4;
        public const int ThoriumBossProjectile_FB_V2 = 5;
        public const int ThoriumBossProjectile_FB_V3 = 6;
        public const int ThoriumBossProjectile_FB_V4 = 7;
        public const int ThoriumBossProjectile_FB_V5 = 8;
        public const int ThoriumBossProjectile_LI_V1 = 9;
        public const int ThoriumBossProjectile_LI_V2 = 10;
        public const int ThoriumBossProjectile_LI_V3 = 11;
        public const int ThoriumBossProjectile_LI_V4 = 12;
        public const int ThoriumBossProjectile_LI_V5 = 13;
        public const int ThoriumBossProjectile_LI_V6 = 14;
        public const int ThoriumBossProjectile_LI_V7 = 15;
        public const int ThoriumBossProjectile_LI_V8 = 16;
        public const int ThoriumBossProjectile_LI_V9 = 17;
        public const int ThoriumBossProjectile_LI_V10 = 18;
        public const int ThoriumBossProjectile_LI_V11 = 19;
        public const int ThoriumBossProjectile_LI_V12 = 20;
        public const int ThoriumBossProjectile_FO_V1 = 21;
        public const int ThoriumBossProjectile_FO_V2 = 22;
        public const int ThoriumBossProjectile_FO_V3 = 23;
        public const int ThoriumBossProjectile_FO_V4 = 24;
        public const int ThoriumBossProjectile_FO_V5 = 25;
        public const int ThoriumBossProjectile_AET_V1 = 26;
        public const int ThoriumBossProjectile_SFF_V1 = 27;
        public const int ThoriumBossProjectile_SFF_V2 = 28;
        public const int ThoriumBossProjectile_DE_V1 = 29;
        public const int ThoriumBossProjectile_OLD_V1 = 30;
        public const int ThoriumBossProjectile_OLD_V2 = 31;
        private static readonly string[] ThoriumBossProjectileRegisterStrings =
        {
            "BlizzardBoom",
            "BlizzardCascade",
            "BlizzardFang",
            "BlizzardStart",
            "BeholderBeam",
            "BeholderEyeEffect",
            "BeholderEyeEffect2",
            "BeholderLavaCascade",
            "BeholderLavaCascade1",
            "LichFlare",
            "LichFlareDeath",
            "LichFlareDeathSpawn",
            "LichFlareDeathSpawn2",
            "LichFlareDeathSpawn3",
            "LichFlareSpawn",
            "LichFlareSpawn2",
            "LichFlareSpawn3",
            "LichGaze",
            "LichPumpkinPro",
            "LichPumpkinPro2",
            "SoulRenderLich",
            "AbyssalStrike",
            "AbyssalStrike2",
            "Whirlpool",
            "ForgottenOneSpit",
            "ForgottenOneSpit2",
            "AquaSplash",
            "FlameLash",
            "FlamePulsePro",
            "LucidPulse",
            "DeathCircle",
            "DeathCircle2"
        };
        private static int[] ThoriumBossProjectileID = new int[ThoriumBossProjectileRegisterStrings.Length];
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
        private static DamageClass[] ThoriumDamageClass = new DamageClass[ThoriumDamageClassRegisterStrings.Length];
        private static int[] ThoriumItemItemID = new int[ThoriumItemRegisterStrings.Length];
        private static int[] InflictWreckedResistance1in1Group_ThoriumBoss = new int[(9 + 1)];
        private static int[] InflictWreckedResistance1in1Group_ThoriumBossProjectile = new int[(26 + 1)];
        private static int[] InflictDevastated1in1Group_ThoriumBoss = new int[(2 + 1)];
        private static int[] InflictDevastated1in1Group_ThoriumBossProjectile = new int[(4 + 1)];
        public const int ConsolariaNPC_VM = 0;
        private static readonly string[] ConsolariaNPCRegisterStrings =
        {
            "VampireMiner"
        };
        private static int[] ConsolariaNPCID = new int[ConsolariaNPCRegisterStrings.Length];

        public static int GetConsolariaNPCID(int modContentID)
        {
            int i = 0;
            try
            {
                i = ConsolariaNPCID[modContentID];
            }
            catch
            {
                WeDoALittleTrolling.logger.Fatal("WDALT: ERROR: GetConsolariaNPCID() was called with an invalid ModContentID.");
            }
            return i;
        }

        public static DamageClass GetThoriumDamageClass(int modContentID)
        {
            DamageClass i = null;
            try
            {
                i = ThoriumDamageClass[modContentID];
            }
            catch
            {
                WeDoALittleTrolling.logger.Fatal("WDALT: ERROR: GetThoriumDamageClass() was called with an invalid ModContentID.");
                return null;
            }
            return i;
        }

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
        public static int GetThoriumBossProjectileID(int modContentID)
        {
            int i = 0;
            try
            {
                i = ThoriumBossProjectileID[modContentID];
            }
            catch
            {
                WeDoALittleTrolling.logger.Fatal("WDALT: ERROR: GetThoriumBossProjectileID was called with an invalid ModContentID.");
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
        public static int[] GetThoriumBossProjectileInflictWreckedResistance1in1Group()
        {
            return InflictWreckedResistance1in1Group_ThoriumBossProjectile;
        }
        public static int[] GetThoriumBossInflictDevastated1in1Group()
        {
            return InflictDevastated1in1Group_ThoriumBoss;
        }
        public static int[] GetThoriumBossProjectileInflictDevastated1in1Group()
        {
            return InflictDevastated1in1Group_ThoriumBossProjectile;
        }

        public static bool SetContentIDs()
        {
            bool integrity = true;
            if (WDALTModSystem.TryGetConsolariaMod(out Mod consolariaMod))
            {
                for (int i = 0; i < ConsolariaNPCRegisterStrings.Length; i++)
                {
                    if (consolariaMod.TryFind<ModNPC>(ConsolariaNPCRegisterStrings[i], out ModNPC npc))
                    {
                        ConsolariaNPCID[i] = npc.Type;
                    }
                    else
                    {
                        integrity = false;
                    }
                }
            }
            if (WDALTModSystem.TryGetThoriumMod(out Mod thoriumMod))
            {
                for (int i = 0; i < ThoriumDamageClassRegisterStrings.Length; i++)
                {
                    if(thoriumMod.TryFind(ThoriumDamageClassRegisterStrings[i], out DamageClass c))
                    {
                        ThoriumDamageClass[i] = c;
                    }
                    else
                    {
                        integrity = false;
                    }
                }
                for (int i = 0; i < ThoriumBossRegisterStrings.Length; i++)
                {
                    if (thoriumMod.TryFind(ThoriumBossRegisterStrings[i], out ModNPC bossNPC))
                    {
                        ThoriumBossNPCID[i] = bossNPC.Type;
                    }
                    else
                    {
                        integrity = false;
                    }
                }
                for (int i = 0; i < ThoriumItemRegisterStrings.Length; i++)
                {
                    if (thoriumMod.TryFind(ThoriumItemRegisterStrings[i], out ModItem item))
                    {
                        ThoriumItemItemID[i] = item.Type;
                    }
                    else
                    {
                        integrity = false;
                    }
                }
                for (int i = 0; i < ThoriumBossProjectileRegisterStrings.Length; i++)
                {
                    if (thoriumMod.TryFind(ThoriumBossProjectileRegisterStrings[i], out ModProjectile proj))
                    {
                        ThoriumBossProjectileID[i] = proj.Type;
                    }
                    else
                    {
                        integrity = false;
                    }
                }
            }
            if (integrity)
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
                InflictDevastated1in1Group_ThoriumBoss[0] = GetThoriumBossNPCID(ThoriumBoss_LI_V2);
                InflictDevastated1in1Group_ThoriumBoss[1] = GetThoriumBossNPCID(ThoriumBoss_OLD);
                InflictDevastated1in1Group_ThoriumBoss[2] = GetThoriumBossNPCID(ThoriumBoss_DE);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[0] = GetThoriumBossProjectileID(ThoriumBossProjectile_BS_V1);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[1] = GetThoriumBossProjectileID(ThoriumBossProjectile_BS_V2);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[2] = GetThoriumBossProjectileID(ThoriumBossProjectile_BS_V3);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[3] = GetThoriumBossProjectileID(ThoriumBossProjectile_BS_V4);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[4] = GetThoriumBossProjectileID(ThoriumBossProjectile_FB_V1);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[5] = GetThoriumBossProjectileID(ThoriumBossProjectile_FB_V2);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[6] = GetThoriumBossProjectileID(ThoriumBossProjectile_FB_V3);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[7] = GetThoriumBossProjectileID(ThoriumBossProjectile_FB_V4);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[8] = GetThoriumBossProjectileID(ThoriumBossProjectile_FB_V5);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[9] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V1);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[10] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V2);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[11] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V3);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[12] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V4);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[13] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V5);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[14] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V6);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[15] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V7);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[16] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V8);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[17] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V9);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[18] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V10);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[19] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V11);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[20] = GetThoriumBossProjectileID(ThoriumBossProjectile_FO_V3);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[21] = GetThoriumBossProjectileID(ThoriumBossProjectile_FO_V4);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[22] = GetThoriumBossProjectileID(ThoriumBossProjectile_FO_V5);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[23] = GetThoriumBossProjectileID(ThoriumBossProjectile_AET_V1);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[24] = GetThoriumBossProjectileID(ThoriumBossProjectile_SFF_V1);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[25] = GetThoriumBossProjectileID(ThoriumBossProjectile_SFF_V2);
                InflictWreckedResistance1in1Group_ThoriumBossProjectile[26] = GetThoriumBossProjectileID(ThoriumBossProjectile_DE_V1);
                InflictDevastated1in1Group_ThoriumBossProjectile[0] = GetThoriumBossProjectileID(ThoriumBossProjectile_FO_V1);
                InflictDevastated1in1Group_ThoriumBossProjectile[1] = GetThoriumBossProjectileID(ThoriumBossProjectile_FO_V2);
                InflictDevastated1in1Group_ThoriumBossProjectile[2] = GetThoriumBossProjectileID(ThoriumBossProjectile_LI_V12);
                InflictDevastated1in1Group_ThoriumBossProjectile[3] = GetThoriumBossProjectileID(ThoriumBossProjectile_OLD_V1);
                InflictDevastated1in1Group_ThoriumBossProjectile[4] = GetThoriumBossProjectileID(ThoriumBossProjectile_OLD_V2);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
