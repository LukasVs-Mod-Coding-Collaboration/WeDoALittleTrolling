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

using Terraria;
using Terraria.ModLoader;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System;
using WeDoALittleTrolling.Common.Utilities;
using Terraria.ID;
using System.Linq;

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal static class WDALTIntermediateLanguageEditing
    {
        public static void RegisterILHooks()
        {
            IL_NPC.AI_037_Destroyer += IL_NPC_AI_037_Destroyer;
            IL_Player.Update_NPCCollision += IL_Player_Update_NPCCollision;
            IL_WorldGen.UpdateWorld_OvergroundTile += IL_WorldGen_UpdateWorld_OvergroundTile;
            IL_WorldGen.UpdateWorld_UndergroundTile += IL_WorldGen_UpdateWorld_UndergroundTile;
        }

        public static void UnregisterILHooks()
        {
            IL_NPC.AI_037_Destroyer -= IL_NPC_AI_037_Destroyer;
            IL_Player.Update_NPCCollision -= IL_Player_Update_NPCCollision;
            IL_WorldGen.UpdateWorld_OvergroundTile -= IL_WorldGen_UpdateWorld_OvergroundTile;
            IL_WorldGen.UpdateWorld_UndergroundTile -= IL_WorldGen_UpdateWorld_UndergroundTile;
        }

        public static void IL_NPC_AI_037_Destroyer(ILContext intermediateLanguageContext)
        {
            bool successInjectDestroyerAIHook = true;
            try
            {
                ILCursor cursor = new ILCursor(intermediateLanguageContext);
                cursor.GotoNext(i => i.MatchLdsfld<Main>(nameof(Main.maxTilesY)));
                cursor.GotoNext(i => i.MatchLdcI4(0));
                cursor.Index++; //move cursor to the local "flag2" variable.
                intermediateLanguageContext.Instrs[cursor.Index].MatchStloc(out int fetchedIdx1); //fetch memory adress of local "flag2" variable.
                byte idx1 = (byte)fetchedIdx1;
                cursor.Index++;
                cursor.Emit(OpCodes.Ldarg_0); //push the NPC instance onto the stack.
                cursor.EmitDelegate<Func<NPC, bool>>
                (
                    (npc) =>
                    {
                        bool gravity = true;
                        if(npc.life < (int)Math.Round(npc.lifeMax * WDALTBossAIUtil.destroyerLiftoffLimit))
                        {
                            gravity = false; //Disable gravity code.
                        }
                        return !gravity;
                    }
                );
                cursor.Emit(OpCodes.Stloc_S, idx1); //write !gravity into the local "flag2" variable.
                cursor.GotoNext(i => i.MatchCall<Main>(nameof(Main.IsItDay)));
                cursor.Index--; //move cursor to the local "num18" variable.
                intermediateLanguageContext.Instrs[cursor.Index].MatchStloc(out int fetchedIdx2); //fetch memory adress of local "num18" variable.
                byte idx2 = (byte)fetchedIdx2;
                cursor.Index++;
                cursor.Emit(OpCodes.Ldarg_0); //push the NPC instance onto the stack.
                cursor.EmitDelegate<Func<NPC, float>>
                (
                    (npc) =>
                    {
                        float maxSpeed = 16f;
                        if(npc.life < (int)Math.Round(npc.lifeMax * WDALTBossAIUtil.destroyerAccelerationLimit))
                        {
                            maxSpeed = 20f;
                        }
                        return maxSpeed;
                    }
                );
                cursor.Emit(OpCodes.Stloc_S, idx2); //write maxSpeed into the local "num18" variable.
                cursor.GotoNext(i => i.MatchStfld<Entity>(nameof(Entity.active)));
                cursor.GotoNext(i => i.MatchLdcR4(out float dummy1));
                cursor.Index++; //move cursor to the local "num19" variable.
                intermediateLanguageContext.Instrs[cursor.Index].MatchStloc(out int fetchedIdx3); //fetch memory adress of local "num19" variable.
                byte idx3 = (byte)fetchedIdx3;
                cursor.Index++;
                cursor.Emit(OpCodes.Ldarg_0); //push the NPC instance onto the stack.
                cursor.EmitDelegate<Func<NPC, float>>
                (
                    (npc) =>
                    {
                        float maxAcc1 = 0.1f;
                        if(npc.life < (int)Math.Round(npc.lifeMax * WDALTBossAIUtil.destroyerAccelerationLimit))
                        {
                            maxAcc1 = 0.15f;
                        }
                        return maxAcc1;
                    }
                );
                cursor.Emit(OpCodes.Stloc_S, idx3); //write maxAcc1 into the local "num19" variable.
                cursor.Index++; //move cursor to the local "num20" variable.
                intermediateLanguageContext.Instrs[cursor.Index].MatchStloc(out int fetchedIdx4); //fetch memory adress of local "num20" variable.
                byte idx4 = (byte)fetchedIdx4;
                cursor.Index++;
                cursor.Emit(OpCodes.Ldarg_0); //push the NPC instance onto the stack.
                cursor.EmitDelegate<Func<NPC, float>>
                (
                    (npc) =>
                    {
                        float maxAcc2 = 0.15f;
                        if(npc.life < (int)Math.Round(npc.lifeMax * WDALTBossAIUtil.destroyerAccelerationLimit))
                        {
                            maxAcc2 = 0.225f;
                        }
                        return maxAcc2;
                    }
                );
                cursor.Emit(OpCodes.Stloc_S, idx4); //write maxAcc2 into the local "num20" variable.
            }
            catch
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<WeDoALittleTrolling>(), intermediateLanguageContext);
                WeDoALittleTrolling.logger.Fatal("WDALT: Failed to inject Destroyer AI Hook. Broken IL Code has been dumped to tModLoader-Logs/ILDumps/WeDoALittleTrolling.");
                successInjectDestroyerAIHook = false;
            }
            if(successInjectDestroyerAIHook)
            {
                WeDoALittleTrolling.logger.Debug("WDALT: Successfully injected Destroyer AI Hook via IL Editing.");
            }
        }

        public static void IL_Player_Update_NPCCollision(ILContext intermediateLanguageContext)
        {
            bool successInjectBossImmunityHook = true;
            try
            {
                ILCursor cursor = new ILCursor(intermediateLanguageContext);
                cursor.GotoNext(i => i.Match(OpCodes.Ldc_I4_M1)); //Move to the position where "specialHitSetter" is set to -1.
                cursor.Index++; //Move beween the instruction that pushes -1 onto the stack and the instruction that write the value from stack into "specialHitSetter".
                cursor.Emit(OpCodes.Pop); //Pop -1 off the stack.
                cursor.Emit(OpCodes.Ldloc_1); //Push the current NPC index (variable i) onto the stack.
                cursor.EmitDelegate<Func<int, int>> //Emit a function that accepts the current NPC index and returns the new value for "specialHitSetter".
                (
                    (index) =>
                    {
                        int specialHitSetter = -1; //-1 means normal enemy
                        if (index >= 0 && index < Main.npc.Length)
                        {
                            if
                            (
                                Main.npc[index].boss ||
                                WDALTImmunitySystem.BossNPCIDWhitelist.Contains(Main.npc[index].type)
                            )
                            {
                                specialHitSetter = 1; //1 means moon lord / empress of light
                            }
                        }
                        return specialHitSetter;
                    }
                );
            }
            catch
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<WeDoALittleTrolling>(), intermediateLanguageContext);
                WeDoALittleTrolling.logger.Fatal("WDALT: Failed to inject Boss Immunity Hook. Broken IL Code has been dumped to tModLoader-Logs/ILDumps/WeDoALittleTrolling.");
                successInjectBossImmunityHook = false;
            }
            if(successInjectBossImmunityHook)
            {
                WeDoALittleTrolling.logger.Debug("WDALT: Successfully injected Boss Immunity Hook via IL Editing.");
            }
        }

        public static void IL_WorldGen_UpdateWorld_OvergroundTile(ILContext intermediateLanguageContext)
        {
            bool successInjectPlantOvergroundHook = true;
            try
            {
                ILCursor cursor = new ILCursor(intermediateLanguageContext);
                cursor.GotoNext(i => i.MatchLdcI4(3000));
                cursor.Index++;
                cursor.GotoNext(i => i.MatchLdcI4(3000)); //Move to the position where the number 3000 is pushed onto the stack for RNG.
                cursor.Index++; //Move after it now.
                cursor.Emit(OpCodes.Pop); //Pop Terrarias RNG chance denominator 15000 off the stack.
                int rngDenominator1 = 2; //Set 2 as the denominator for RNG. 1 in 2 chance = 50%
                cursor.Emit(OpCodes.Ldc_I4, rngDenominator1); //Finally, push our denominator onto the stack instead.
                cursor.GotoNext(i => i.MatchLdcI4(15000)); //Move to the position where the number 15000 is pushed onto the stack for RNG.
                cursor.Index++; //Move after it now.
                cursor.Emit(OpCodes.Pop); //Pop Terrarias RNG chance denominator 15000 off the stack.
                int rngDenominator2 = 1; //Set 1 as the denominator for RNG. 1 in 1 chance = 100%
                cursor.Emit(OpCodes.Ldc_I4, rngDenominator2); //Finally, push our denominator onto the stack instead.
            }
            catch
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<WeDoALittleTrolling>(), intermediateLanguageContext);
                WeDoALittleTrolling.logger.Fatal("WDALT: Failed to inject Strange Plant Overground Hook. Broken IL Code has been dumped to tModLoader-Logs/ILDumps/WeDoALittleTrolling.");
                successInjectPlantOvergroundHook = false;
            }
            if(successInjectPlantOvergroundHook)
            {
                WeDoALittleTrolling.logger.Debug("WDALT: Successfully injected Strange Plant Overground Hook via IL Editing.");
            }
        }

        public static void IL_WorldGen_UpdateWorld_UndergroundTile(ILContext intermediateLanguageContext)
        {
            bool successInjectPlantUndergroundHook = true;
            try
            {
                ILCursor cursor = new ILCursor(intermediateLanguageContext);
                cursor.GotoNext(i => i.MatchLdcI4(2500)); //Move to the position where the number 2500 is pushed onto the stack for RNG.
                cursor.Index++; //Move after it now.
                cursor.Emit(OpCodes.Pop); //Pop Terrarias RNG chance denominator 15000 off the stack.
                int rngDenominator1 = 2; //Set 2 as the denominator for RNG. 1 in 2 chance = 50%
                cursor.Emit(OpCodes.Ldc_I4, rngDenominator1); //Finally, push our denominator onto the stack instead.
                cursor.GotoNext(i => i.MatchLdcI4(10000)); //Move to the position where the number 10000 is pushed onto the stack for RNG.
                cursor.Index++; //Move after it now.
                cursor.Emit(OpCodes.Pop); //Pop Terrarias RNG chance denominator 15000 off the stack.
                int rngDenominator2 = 1; //Set 1 as the denominator for RNG. 1 in 1 chance = 100%
                cursor.Emit(OpCodes.Ldc_I4, rngDenominator2); //Finally, push our denominator onto the stack instead.
            }
            catch
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<WeDoALittleTrolling>(), intermediateLanguageContext);
                WeDoALittleTrolling.logger.Fatal("WDALT: Failed to inject Strange Plant Underground Hook. Broken IL Code has been dumped to tModLoader-Logs/ILDumps/WeDoALittleTrolling.");
                successInjectPlantUndergroundHook = false;
            }
            if(successInjectPlantUndergroundHook)
            {
                WeDoALittleTrolling.logger.Debug("WDALT: Successfully injected Strange Plant Underground Hook via IL Editing.");
            }
        }
    }
}
