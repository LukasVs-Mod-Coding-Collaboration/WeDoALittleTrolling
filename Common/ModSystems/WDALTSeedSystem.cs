/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2026 LukasV-Coding

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
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI.States;
using Terraria.ID;
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
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableForTheWorthyDefenseEffectivenessIncrease)
            {
                On_Player.VanillaBaseDefenseEffectiveness += On_Player_VanillaBaseDefenseEffectiveness;
            }
        }

        public static void UnregisterHooks()
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableCustomTheConstant)
            {
                On_DontStarveDarknessDamageDealer.Update -= On_DontStarveDarknessDamageDealer_Update;
            }
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableForTheWorthyDefenseEffectivenessIncrease)
            {
                On_Player.VanillaBaseDefenseEffectiveness -= On_Player_VanillaBaseDefenseEffectiveness;
            }
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
            if (Main.Difficulty >= GameDifficultyLevel.Legendary)
            {
                return 1.25f;
            }
            return orig.Invoke();
        }
    }
}

