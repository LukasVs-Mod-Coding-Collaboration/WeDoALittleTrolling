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
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
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
            On_DontStarveDarknessDamageDealer.Update += On_DontStarveDarknessDamageDealer_Update;
            On_Main.UpdateTime_StartNight += On_Main_UpdateTime_StartNight;
        }

        public static void UnregisterHooks()
        {
            On_DontStarveDarknessDamageDealer.Update -= On_DontStarveDarknessDamageDealer_Update;
            On_Main.UpdateTime_StartNight -= On_Main_UpdateTime_StartNight;
        }

        public static void On_DontStarveDarknessDamageDealer_Update(On_DontStarveDarknessDamageDealer.orig_Update orig, Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<Haunted>()))
            {
                DontStarveDarknessDamageDealer.Reset();
            }
            orig.Invoke(player);
        }

        public static void On_Main_UpdateTime_StartNight(On_Main.orig_UpdateTime_StartNight orig, ref bool stopEvents)
        {
            orig.Invoke(ref stopEvents);
            if(Main.dontStarveWorld && Main.netMode != NetmodeID.MultiplayerClient && !Main.IsItRaining && Main.rand.NextBool(5))
            {
                Main.StartRain();
                Main.SyncRain();
            }
        }
    }
}

