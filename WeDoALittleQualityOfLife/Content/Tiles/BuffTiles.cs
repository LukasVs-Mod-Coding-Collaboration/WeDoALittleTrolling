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
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace WeDoALittleQualityOfLife.Content.Tiles
{
    internal class BuffTiles : GlobalTile
    {
        public static readonly int[] BuffTilesItemIDs =
        {
            ItemID.BewitchingTable,
            ItemID.WarTable,
            ItemID.SharpeningStation,
            ItemID.CrystalBall,
            ItemID.AmmoBox,
            ItemID.SliceOfCake
        };

        public override void PlaceInWorld(int i, int j, int type, Item item)
        {
            if (BuffTilesItemIDs.Contains(item.type))
            {
                ReduceStack(item);
            }
            base.PlaceInWorld(i, j, type, item);
        }

        public static void ReduceStack(Item item)
        {
            if (item.stack > 1)
            {
                item.stack--;
            }
            else
            {
                item.type = ItemID.None;
                item.stack = 0;
                item.active = false;
            }
        }
    }
}

