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

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal static class WDALTSceneMetrics
    {
        public static bool HasWormCandle = false;

        public static void RegisterHooks()
        {
            On_SceneMetrics.Reset += On_SceneMetrics_Reset;
        }

        public static void UnregisterHooks()
        {
            On_SceneMetrics.Reset -= On_SceneMetrics_Reset;
        }

        public static void On_SceneMetrics_Reset(On_SceneMetrics.orig_Reset orig, SceneMetrics self)
        {
            HasWormCandle = false;
            orig.Invoke(self);
        }
    }
}

