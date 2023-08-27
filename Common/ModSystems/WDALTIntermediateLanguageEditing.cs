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
using MonoMod.Cil;
using Mono.Cecil.Cil;
using Terraria.ModLoader;
using Mono.Cecil;
using log4net.Repository.Hierarchy;
using log4net.Core;

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
                cursor.Index++; //Go in front of it now!
                cursor.Emit(OpCodes.Ldc_I4_0); //set "false" as the parameter to write!!!
                cursor.Emit(OpCodes.Stsfld, typeof(WorldGen).GetField(nameof(WorldGen.AllowedToSpreadInfections))); //Make sTuPEED compOOOOOter set the variable to false instead :saltroll: !1!!!1!!!!!!
                cursor.GotoNext(i => i.MatchStsfld<WorldGen>(nameof(WorldGen.AllowedToSpreadInfections))); //Do the same if StopBiomeSpreadPower is enabled
                cursor.Index++;
                cursor.Emit(OpCodes.Ldc_I4_0);
                cursor.Emit(OpCodes.Stsfld, typeof(WorldGen).GetField(nameof(WorldGen.AllowedToSpreadInfections))); //hAhAHaHA gET r4KT sTuPEED cORrUPtIoN, cRiMSoN or hALloWEd1!!1!!!!!1
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
    }
}
