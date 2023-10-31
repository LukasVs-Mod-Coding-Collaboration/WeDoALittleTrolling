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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using WeDoALittleTrolling.Common.ModSystems;

namespace WeDoALittleTrolling.Content.Tiles
{
    internal class WormCandleTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileLavaDeath[Type] = true;
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
            AddToArray(ref TileID.Sets.DrawFlipMode);
            TileID.Sets.InteractibleByNPCs[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.ShadowCandle, 0));
            TileObjectData.addTile(Type);
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            WDALTSceneMetrics.HasWormCandle = true;
        }
    }
}
