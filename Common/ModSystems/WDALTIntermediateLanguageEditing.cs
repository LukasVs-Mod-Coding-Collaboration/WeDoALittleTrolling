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
using WeDoALittleTrolling.Common.Configs;
using Microsoft.Xna.Framework;

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal static class WDALTIntermediateLanguageEditing
    {
        public static void RegisterILHooks()
        {
            IL_NPC.AI_069_DukeFishron += IL_NPC_AI_069_DukeFishron;
            IL_NPC.AI_037_Destroyer += IL_NPC_AI_037_Destroyer;
            IL_Player.Update_NPCCollision += IL_Player_Update_NPCCollision;
        }

        public static void UnregisterILHooks()
        {
            IL_NPC.AI_069_DukeFishron -= IL_NPC_AI_069_DukeFishron;
            IL_NPC.AI_037_Destroyer -= IL_NPC_AI_037_Destroyer;
            IL_Player.Update_NPCCollision -= IL_Player_Update_NPCCollision;
        }

        public static void IL_NPC_AI_069_DukeFishron(ILContext intermediateLanguageContext)
        {
            bool successInjectDukeFishronAIHook = true;
            if (!ModContent.GetInstance<WDALTServerConfig>().EnableDukeFishronExtraAI)
            {
                WeDoALittleTrolling.logger.Debug("WDALT: Duke Fishron AI Hook is disabled in the server configuration, skipping injection...");
                return;
            }
            try
            {
                ILCursor cursor = new ILCursor(intermediateLanguageContext);
                cursor.GotoNext(i => i.MatchLdsfld<Main>(nameof(Main.maxTilesX))); //go to the surface check
                cursor.GotoNext(i => i.MatchLdcR4(6f)); //move cursor to instruction setting the local "num6" variable.
                cursor.Index++;
                cursor.Index++; 
                intermediateLanguageContext.Instrs[cursor.Index].MatchStloc(out int fetchedIdx1); //fetch memory adress of local "num6" variable.
                byte idx1 = (byte)fetchedIdx1;
                cursor.Index++;
                cursor.Index++;
                cursor.Index++; //move cursor after the "flag7" variable setup phase.
                cursor.Emit(OpCodes.Ldarg_0); //push the NPC instance onto the stack.
                cursor.Emit(OpCodes.Ldloc_S, idx1); //push the NPC instance onto the stack.
                cursor.EmitDelegate<Func<NPC, float, float>> //code to return the desired speed value for duke fishron
                (
                    (NPC npc, float orig_speed) =>
                    {
                        float speed = orig_speed;
                        if (npc != null && npc.active && npc.lifeMax > 0 && npc.life > 0)
                        {
                            speed = speed +
                            (
                                speed * (((float)npc.lifeMax + (float)npc.life) / ((float)npc.lifeMax * 8.0f)) //Give fishron more dash speed, tapering from 25% at full life to 12.5% at 1 hp.
                            );
                        }
                        return speed;
                    }
                );
                cursor.Emit(OpCodes.Stloc_S, idx1);
                for (int i = 0; i < 3; i++) //Patch all dash code instances (3 instances currently in game)
                {
                    cursor.GotoNext(i => i.MatchLdloc(idx1)); //go to the dash vector computation code.
                    cursor.GotoNext(i => i.MatchStfld<Entity>(nameof(Entity.velocity)));
                    cursor.Emit(OpCodes.Ldarg_0); //push the NPC instance onto the stack.
                    cursor.Emit(OpCodes.Ldloc_S, idx1);
                    cursor.EmitDelegate<Func<Microsoft.Xna.Framework.Vector2, NPC, float, Microsoft.Xna.Framework.Vector2>> //code to return the desired speed value for duke fishron
                    (
                        (Microsoft.Xna.Framework.Vector2 orig_velocity, NPC npc, float dashSpeed) =>
                        {
                            Microsoft.Xna.Framework.Vector2 velocity = orig_velocity;
                            if (npc != null && npc.target >= 0 && npc.target < Main.player.Length)
                            {
                                //Recompute a more accurate dash vector
                                Player player = Main.player[npc.target];
                                float moveSpeed = velocity.Length();
                                if (player.active && !player.dead && moveSpeed > 0f)
                                {
                                    Microsoft.Xna.Framework.Vector2 predictVelocity = player.velocity * (Vector2.Distance(npc.Center, player.Center) / moveSpeed); //Roughly Predict where the target is going to be when Duke Fishron reaches it
                                    Microsoft.Xna.Framework.Vector2 dashVector = (player.Center + predictVelocity) - npc.Center;
                                    dashVector = dashVector.SafeNormalize(Microsoft.Xna.Framework.Vector2.Zero);
                                    velocity = dashVector * dashSpeed;
                                }
                            }
                            return velocity;
                        }
                    );
                    cursor.Index++;
                    cursor.Index++; //Move after the instruction to correctly switch to the next occurence of the dash code.
                }
            }
            catch
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<WeDoALittleTrolling>(), intermediateLanguageContext);
                WeDoALittleTrolling.logger.Fatal("WDALT: Failed to inject Duke Fishron AI Hook. Broken IL Code has been dumped to tModLoader-Logs/ILDumps/WeDoALittleTrolling.");
                successInjectDukeFishronAIHook = false;
            }
            if(successInjectDukeFishronAIHook)
            {
                WeDoALittleTrolling.logger.Debug("WDALT: Successfully injected Duke Fishron AI Hook via IL Editing.");
            }
        }

        public static void IL_NPC_AI_037_Destroyer(ILContext intermediateLanguageContext)
        {
            bool successInjectDestroyerAIHook = true;
            if (ModContent.GetInstance<WDALTServerConfig>().DisableTheDestroyerExtraAI)
            {
                WeDoALittleTrolling.logger.Debug("WDALT: Destroyer AI Hook is disabled in the server configuration, skipping injection...");
                return;
            }
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
            if (ModContent.GetInstance<WDALTServerConfig>().DisableBossImmunityPatch)
            {
                WeDoALittleTrolling.logger.Debug("WDALT: Boss Immunity Hook is disabled in the server configuration, skipping injection...");
                return;
            }
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
    }
}
