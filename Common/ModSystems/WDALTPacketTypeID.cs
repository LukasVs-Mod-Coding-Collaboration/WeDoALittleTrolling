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

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal static class WDALTPacketTypeID
    {
        public const short moondial                  = 0;
        public const short sundial                   = 1;
        public const short weatherVane               = 2;
        public const short djinnLamp                 = 3;
        public const short skyMill                   = 4;
        public const short updateWindSpeedTarget     = 5;
        public const short soundPlayRainOfDecay      = 6;
        public const short soundBroadcastRainOfDecay = 7;
        public const short spawnCrateItem            = 8;
        public const short syncNetFinalDamage        = 9;
        public const short broadcastNetFinalDamage   = 10;
        public const short syncBrightness            = 11;
    }
}
