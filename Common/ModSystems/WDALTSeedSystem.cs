/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2025 LukasV-Coding

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
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.States;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Configs;
using WeDoALittleTrolling.Content.Buffs;

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal static class WDALTSeedSystem
    {
        public static bool TestyWorld = false;

        public static void InitWorldVariables()
        {
            string seed = Main.ActiveWorldFileData.SeedText;
            if (seed == "testy")
            {
                TestyWorld = true;
            }
            else
            {
                TestyWorld = false;
            }
        }

        public static void ResetWorldVariables()
        {
            TestyWorld = false;
        }

        public static void RegisterHooks()
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableCustomTheConstant)
            {
                On_DontStarveDarknessDamageDealer.Update += On_DontStarveDarknessDamageDealer_Update;
            }
            On_Player.VanillaBaseDefenseEffectiveness += On_Player_VanillaBaseDefenseEffectiveness;
            On_UIWorldCreation.ProcessSpecialWorldSeeds += On_UIWorldCreation_ProcessSpecialWorldSeeds;
            IL_WorldGen.GenerateWorld += IL_WorldGen_GenerateWorld;
        }

        public static void UnregisterHooks()
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableCustomTheConstant)
            {
                On_DontStarveDarknessDamageDealer.Update -= On_DontStarveDarknessDamageDealer_Update;
            }
            On_Player.VanillaBaseDefenseEffectiveness -= On_Player_VanillaBaseDefenseEffectiveness;
            On_UIWorldCreation.ProcessSpecialWorldSeeds -= On_UIWorldCreation_ProcessSpecialWorldSeeds;
            IL_WorldGen.GenerateWorld -= IL_WorldGen_GenerateWorld;
        }

        public static void On_DontStarveDarknessDamageDealer_Update(On_DontStarveDarknessDamageDealer.orig_Update orig, Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<Haunted>()))
            {
                DontStarveDarknessDamageDealer.Reset();
            }
            orig.Invoke(player);
        }

        public static float On_Player_VanillaBaseDefenseEffectiveness(On_Player.orig_VanillaBaseDefenseEffectiveness orig)
        {
            if (Main.getGoodWorld && Main.GameModeInfo.IsMasterMode)
            {
                return 1.25f;
            }
            return orig.Invoke();
        }

        public static void On_UIWorldCreation_ProcessSpecialWorldSeeds(Terraria.GameContent.UI.States.On_UIWorldCreation.orig_ProcessSpecialWorldSeeds orig, string processedSeed)
        {
            orig.Invoke(processedSeed);
            if (processedSeed.Contains("WDALTMixup") || processedSeed.Contains("WDALTDrunkFTW"))
            {
                WorldGen.getGoodWorldGen = true;
                if (processedSeed.Contains("WDALTMixup"))
                {
                    WorldGen.noTrapsWorldGen = true;
                }
            }
            else if (processedSeed.Contains("WDALTDrunkBees"))
            {
                WorldGen.notTheBees = true;
            }
        }

        public static void IL_WorldGen_GenerateWorld(ILContext intermediateLanguageContext)
        {
            bool successInjectDrunkSeedHook = true;
            try
            {
                ILCursor cursor = new ILCursor(intermediateLanguageContext);
                cursor.GotoNext(i => i.MatchLdsfld<WorldGen>(nameof(WorldGen.everythingWorldGen))); //Go to the position where the Get Fixed Boi check is for the drunk world
                cursor.Index++; //Go behind it now
                cursor.Emit(OpCodes.Pop); //Pop the original value of "WorldGen.everythingWorldGen" off the stack
                cursor.EmitDelegate<Func<bool>>
                (
                    () =>
                    {
                        bool flag = WorldGen.everythingWorldGen;
                        if (WorldGen.currentWorldSeed.Contains("WDALTMixup") || WorldGen.currentWorldSeed.Contains("WDALTDrunkBees") || WorldGen.currentWorldSeed.Contains("WDALTDrunkFTW"))
                        {
                            flag = true;
                        }
                        return flag;
                    }
                ); //Instead, push the original value of "WorldGen.everythingWorldGen" onto the stack but also push "true" onto the stack when our special seed is detected.
            }
            catch
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<WeDoALittleTrolling>(), intermediateLanguageContext);
                WeDoALittleTrolling.logger.Fatal("WDALT: Failed to inject Drunk Seed Hook. Broken IL Code has been dumped to tModLoader-Logs/ILDumps/WeDoALittleTrolling.");
                successInjectDrunkSeedHook = false;
            }
            if(successInjectDrunkSeedHook)
            {
                WeDoALittleTrolling.logger.Debug("WDALT: Successfully injected Drunk Seed Hook via IL Editing.");
            }
        }
    }
}

