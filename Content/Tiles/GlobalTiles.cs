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
using Terraria.ID;
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Content.Tiles
{
    internal class GlobalTiles : GlobalTile
    {
        public override void RightClick(int i, int j, int type)
        {
            if(type == TileID.Moondial)
            {
                Main.moondialCooldown = 0;
            }
            else if(type == TileID.Sundial)
            {
                Main.sundialCooldown = 0;
            }
            base.RightClick(i, j, type);
        }
    }
}

