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
                ILCursor cursor = new ILCursor(intermediateLanguageContext);
                cursor.GotoNext(i => i.MatchLdsfld<Main>(nameof(Main.maxTilesY)));
                cursor.GotoNext(i => i.MatchLdcI4(0));
                cursor.Index++;
                intermediateLanguageContext.Instrs[cursor.Index].MatchStloc(out int fetchedIdx); //fetch memory adress of local "flag2" variable.
                byte idx = (byte)fetchedIdx;
                cursor.Index++;
                cursor.Emit(OpCodes.Ldarg_0); //push the NPC instance onto the stack.
                cursor.EmitDelegate<Func<NPC, bool>>
                (
                    (
                        npc
                    ) =>
                    {
                        bool gravity = true;
                        if(npc.life < (int)Math.Round(npc.lifeMax * 0.25))
                        {
                            gravity = false; //Disable gravity code.
                        }
                        return !gravity;
                    }
                );
                cursor.Emit(OpCodes.Stloc_S, idx); //write "true" into the local "flag2" variable.
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
    }
}
