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
using WeDoALittleTrolling.Content.Recipes;

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal class WDALTModSystem : ModSystem
    {

        public static bool isCalamityModPresent = false;
        public static bool isThoriumModPresent = false;
        public static readonly string calamityModName = "CalamityMod";
        public static readonly string thoriumModName = "ThoriumMod";
        public static bool MCIDIntegrity;
        
        public override void OnModLoad()
        {
            if(ModLoader.HasMod(calamityModName))
            {
                isCalamityModPresent = true;
            }
            if(ModLoader.HasMod(thoriumModName))
            {
                isThoriumModPresent = true;
            }
            MCIDIntegrity = WDALTModContentID.SetContentIDs();
            IL_WorldGen.UpdateWorld_Inner += WDALTIntermediateLanguageEditing.IL_WorldGen_UpdateWorld;
            IL_NPC.AI_037_Destroyer += WDALTIntermediateLanguageEditing.IL_NPC_AI_037_Destroyer;
            IL_SceneMetrics.Reset += WDALTIntermediateLanguageEditing.IL_SceneMetrics_Reset;
            base.OnModLoad();
        }

        public static bool TryGetCalamityMod(out Mod calamityMod)
        {
            if(ModLoader.TryGetMod(calamityModName, out Mod mod))
            {
                calamityMod = mod;
                return true;
            }
            else
            {
                calamityMod = null;
                return false;
            }
        }

        public static bool TryGetThoriumMod(out Mod thoriumMod)
        {
            if(ModLoader.TryGetMod(thoriumModName, out Mod mod))
            {
                thoriumMod = mod;
                return true;
            }
            else
            {
                thoriumMod = null;
                return false;
            }
        }

        public override void AddRecipes()
        {
            GlobalRecipes.AddRecipes();
            base.AddRecipes();
        }

        public override void PostAddRecipes()
        {
            GlobalRecipes.PostAddRecipes();
            base.PostAddRecipes();
        }
    }
}

