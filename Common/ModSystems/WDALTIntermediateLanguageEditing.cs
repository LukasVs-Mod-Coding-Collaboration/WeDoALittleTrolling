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
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System;

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal static class WDALTIntermediateLanguageEditing
    {
        public static void IL_WorldGen_UpdateWorld(ILContext intermediateLanguageContext)
        {
            bool successInjectInfectionSpreadHook = true;
            try
            {
                ILCursor cursor = new ILCursor(intermediateLanguageContext);
                cursor.GotoNext(i => i.MatchStsfld<WorldGen>(nameof(WorldGen.AllowedToSpreadInfections))); //Go to the place right after the "AllowedToSpreadInfections" variable is set.
                cursor.Index++; //Go in front of it now.
                cursor.Emit(OpCodes.Ldc_I4_0); //set "false" as the parameter to write.
                cursor.Emit(OpCodes.Stsfld, typeof(WorldGen).GetField(nameof(WorldGen.AllowedToSpreadInfections))); //Write "false" into the "AllowedToSpreadInfections" variable.
                cursor.GotoNext(i => i.MatchStsfld<WorldGen>(nameof(WorldGen.AllowedToSpreadInfections))); //Do the same if StopBiomeSpreadPower is enabled.
                cursor.Index++;
                cursor.Emit(OpCodes.Ldc_I4_0);
                cursor.Emit(OpCodes.Stsfld, typeof(WorldGen).GetField(nameof(WorldGen.AllowedToSpreadInfections)));
            }
            catch
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<WeDoALittleTrolling>(), intermediateLanguageContext);
                WeDoALittleTrolling.logger.Fatal("WDALT: Failed to inject Infection Spread Hook. Broken IL Code has been dumped to tModLoader-Logs/ILDumps/WeDoALittleTrolling.");
                successInjectInfectionSpreadHook = false;
            }
            if(successInjectInfectionSpreadHook)
            {
                WeDoALittleTrolling.logger.Info("WDALT: Successfully injected Infection Spread Hook via IL Editing.");
            }
        }

        public static void IL_NPC_AI_037_Destroyer(ILContext intermediateLanguageContext)
        {
            bool successInjectDestroyerAIHook = true;
            try
            {
                double liftoffLimit = 0.5;
                double accelerationLimit = 0.25;
                ILCursor cursor = new ILCursor(intermediateLanguageContext);
                cursor.GotoNext(i => i.MatchLdsfld<Main>(nameof(Main.maxTilesY)));
                cursor.GotoNext(i => i.MatchLdcI4(0));
                cursor.Index++;
                intermediateLanguageContext.Instrs[cursor.Index].MatchStloc(out int fetchedIdx1); //fetch memory adress of local "flag2" variable.
                byte idx1 = (byte)fetchedIdx1;
                cursor.Index++;
                cursor.Emit(OpCodes.Ldarg_0); //push the NPC instance onto the stack.
                cursor.EmitDelegate<Func<NPC, bool>>
                (
                    (npc) =>
                    {
                        bool gravity = true;
                        if(npc.life < (int)Math.Round(npc.lifeMax * liftoffLimit))
                        {
                            gravity = false; //Disable gravity code.
                        }
                        return !gravity;
                    }
                );
                cursor.Emit(OpCodes.Stloc_S, idx1); //write "true" into the local "flag2" variable.
                cursor.GotoNext(i => i.MatchCall<Main>(nameof(Main.IsItDay)));
                cursor.Index--;
                intermediateLanguageContext.Instrs[cursor.Index].MatchStloc(out int fetchedIdx2); //fetch memory adress of local "num18" variable.
                byte idx2 = (byte)fetchedIdx2;
                cursor.Index++;
                cursor.Emit(OpCodes.Ldarg_0); //push the NPC instance onto the stack.
                cursor.EmitDelegate<Func<NPC, float>>
                (
                    (npc) =>
                    {
                        float maxSpeed = 16f;
                        if(npc.life < (int)Math.Round(npc.lifeMax * accelerationLimit))
                        {
                            maxSpeed = 24f;
                        }
                        return maxSpeed;
                    }
                );
                cursor.Emit(OpCodes.Stloc_S, idx2); //write "true" into the local "num18" variable.
                cursor.GotoNext(i => i.MatchStfld<Entity>(nameof(Entity.active)));
                cursor.GotoNext(i => i.MatchLdcR4(out float dummy1));
                cursor.Index++;
                intermediateLanguageContext.Instrs[cursor.Index].MatchStloc(out int fetchedIdx3); //fetch memory adress of local "num19" variable.
                byte idx3 = (byte)fetchedIdx3;
                cursor.Index++;
                cursor.Emit(OpCodes.Ldarg_0); //push the NPC instance onto the stack.
                cursor.EmitDelegate<Func<NPC, float>>
                (
                    (npc) =>
                    {
                        float maxAcc1 = 0.1f;
                        if(npc.life < (int)Math.Round(npc.lifeMax * accelerationLimit))
                        {
                            maxAcc1 = 0.2f;
                        }
                        return maxAcc1;
                    }
                );
                cursor.Emit(OpCodes.Stloc_S, idx3); //write "true" into the local "num19" variable.
                cursor.Index++;
                intermediateLanguageContext.Instrs[cursor.Index].MatchStloc(out int fetchedIdx4); //fetch memory adress of local "num20" variable.
                byte idx4 = (byte)fetchedIdx4;
                cursor.Index++;
                cursor.Emit(OpCodes.Ldarg_0); //push the NPC instance onto the stack.
                cursor.EmitDelegate<Func<NPC, float>>
                (
                    (npc) =>
                    {
                        float maxAcc2 = 0.15f;
                        if(npc.life < (int)Math.Round(npc.lifeMax * accelerationLimit))
                        {
                            maxAcc2 = 0.3f;
                        }
                        return maxAcc2;
                    }
                );
                cursor.Emit(OpCodes.Stloc_S, idx4); //write "true" into the local "num20" variable.
            }
            catch
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<WeDoALittleTrolling>(), intermediateLanguageContext);
                WeDoALittleTrolling.logger.Fatal("WDALT: Failed to inject Destroyer AI Hook. Broken IL Code has been dumped to tModLoader-Logs/ILDumps/WeDoALittleTrolling.");
                successInjectDestroyerAIHook = false;
            }
            if(successInjectDestroyerAIHook)
            {
                WeDoALittleTrolling.logger.Info("WDALT: Successfully injected Destroyer AI Hook via IL Editing.");
            }
        }

        public static void IL_SceneMetrics_Reset(ILContext intermediateLanguageContext)
        {
            bool successInjectSceneMetricsHook = true;
            try
            {
                ILCursor cursor = new ILCursor(intermediateLanguageContext);
                cursor.GotoNext(i => i.MatchLdarg0());
                cursor.EmitDelegate
                (
                    () =>
                    {
                        WDALTSceneMetrics.Reset();
                    }
                );
            }
            catch
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<WeDoALittleTrolling>(), intermediateLanguageContext);
                WeDoALittleTrolling.logger.Fatal("WDALT: Failed to inject Scene Metrics Hook. Broken IL Code has been dumped to tModLoader-Logs/ILDumps/WeDoALittleTrolling.");
                successInjectSceneMetricsHook = false;
            }
            if(successInjectSceneMetricsHook)
            {
                WeDoALittleTrolling.logger.Info("WDALT: Successfully injected Scene Metrics Hook via IL Editing.");
            }
        }
    }
}
