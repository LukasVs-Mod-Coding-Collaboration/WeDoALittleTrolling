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

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Generation;
using System.Collections.Generic;
using Terraria.WorldBuilding;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader.Default;
using Microsoft.Xna.Framework;
using System;

namespace WeDoALittleTrolling.Common.ModSystems
{
    public class WDALTWorldGenSystem : ModSystem
    {
        public static LocalizedText IceBiomeCustomCavesMessage { get; private set; }
        public static LocalizedText IceBiomeGloomMessage { get; private set; }

        public override void SetStaticDefaults()
        {
            IceBiomeCustomCavesMessage = Language.GetOrRegister(Mod.GetLocalizationKey($"WorldGen.{nameof(IceBiomeCustomCavesMessage)}"));
            IceBiomeGloomMessage = Language.GetOrRegister(Mod.GetLocalizationKey($"WorldGen.{nameof(IceBiomeGloomMessage)}"));
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int IceBiomeIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Generate Ice Biome"));
            if (IceBiomeIndex != -1)
            {
                tasks.Insert(IceBiomeIndex + 1, new IceBiomeCustomCaves("Generate Additional Ice Biome Caves", 100f));
            }
            int PotsIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Pots"));
            if (PotsIndex != -1) {
                tasks.Insert(PotsIndex, new IceBiomeGloom("Generate Additional Ice Biome Gloom", 100f));
            }
        }
    }

    public class IceBiomeGloom : GenPass
    {
        public IceBiomeGloom(string name, float loadWeight) : base(name, loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = WDALTWorldGenSystem.IceBiomeGloomMessage.Value;
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 0.125); k++)
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop, GenVars.snowBottom);
                int x = 0;
                if (y >= 0 && y < GenVars.snowMinX.Length && y < GenVars.snowMaxX.Length)
                {
                    x = WorldGen.genRand.Next(GenVars.snowMinX[y], GenVars.snowMaxX[y]);
                }
                else
                {
                    x = WorldGen.genRand.Next(GenVars.snowMinX[0], GenVars.snowMaxX[0]);
                }
                bool cond =
                (
                     WorldGen.SolidTile(x, y - 1) &&
                    !WorldGen.SolidTile(x, y    ) &&
                    !WorldGen.SolidTile(x, y + 1)
                );
                if (cond)
                {
                    WorldGen.Place1x2Top(x, y, TileID.LightningBuginaBottle, 0);
                }
                else
                {
                    if (k > 0)
                    {
                        k--;
                    }
                    continue;
                }
            }
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 0.1875); k++)
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop, GenVars.snowBottom);
                int x = 0;
                if (y >= 0 && y < GenVars.snowMinX.Length && y < GenVars.snowMaxX.Length)
                {
                    x = WorldGen.genRand.Next(GenVars.snowMinX[y], GenVars.snowMaxX[y]);
                }
                else
                {
                    x = WorldGen.genRand.Next(GenVars.snowMinX[0], GenVars.snowMaxX[0]);
                }
                bool cond =
                (
                     WorldGen.SolidTile(x + 1, y + 1) &&
                     WorldGen.SolidTile(x    , y + 1) &&
                     WorldGen.SolidTile(x - 1, y + 1) &&
                    (WorldGen.TileType (x + 1, y + 1) != TileID.BreakableIce) &&
                    (WorldGen.TileType (x    , y + 1) != TileID.BreakableIce) &&
                    (WorldGen.TileType (x - 1, y + 1) != TileID.BreakableIce) &&
                    !WorldGen.SolidTile(x + 1, y    ) &&
                    !WorldGen.SolidTile(x    , y    ) &&
                    !WorldGen.SolidTile(x - 1, y    ) &&
                    !WorldGen.SolidTile(x + 1, y - 1) &&
                    !WorldGen.SolidTile(x    , y - 1) &&
                    !WorldGen.SolidTile(x - 1, y - 1)
                );
                if (cond)
                {
                    WorldGen.Place3x2(x, y, TileID.Campfire, 3);
                }
                else
                {
                    if (k > 0)
                    {
                        k--;
                    }
                    continue;
                }
            }
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 0.01875); k++)
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop, GenVars.snowBottom);
                int x = 0;
                if (y >= 0 && y < GenVars.snowMinX.Length && y < GenVars.snowMaxX.Length)
                {
                    x = WorldGen.genRand.Next(GenVars.snowMinX[y], GenVars.snowMaxX[y]);
                }
                else
                {
                    x = WorldGen.genRand.Next(GenVars.snowMinX[0], GenVars.snowMaxX[0]);
                }
                bool cond = true; //Prevent Houses from spawning on top of each other
                for (int i = 1; i >= -5; i--)
                {
                    for (int j = 10; j >= -10; j--)
                    {
                        if
                        (
                            WorldGen.TileType((x + j), (y + i)) == TileID.Campfire ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.IceBrick ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.Containers
                        )
                        {
                            cond = false;
                        }
                    }
                }
                if (cond)
                {
                    for (int i = 1; i >= -5; i--)
                    {
                        for (int j = 10; j >= -10; j--)
                        {
                            Main.tile[(x + j), (y + i)].ClearEverything();
                            if (i <= 0 && i >= -4 && j <= 9 && j >= -9)
                            {
                                if(!WorldGen.genRand.NextBool(5))
                                {
                                    WorldGen.PlaceWall((x + j), (y + i), WallID.IceBrick, mute: true);
                                }
                            }
                            if ((i == 1 || i == -5) || (j == 10 || j == -10))
                            {
                                if (i == -5 && j <= 1 && j >= -1)
                                {
                                    WorldGen.PlaceTile((x + j), (y + i), TileID.Platforms, mute: true, default, default, 35);
                                }
                                else
                                {
                                    WorldGen.PlaceTile((x + j), (y + i), (WorldGen.genRand.NextBool(5) ? TileID.IceBlock : TileID.IceBrick), mute: true);
                                }
                            }
                        }
                    }
                    int posX = x - 9;
                    int posY = y + 1;
                    while (!WorldGen.SolidTile(posX, (posY + 1)))
                    {
                        if (posY == y + 1)
                        {
                            WorldGen.PlaceWall(posX, posY, WallID.BorealWoodFence, mute: true);
                        }
                        Main.tile[posX, (posY + 1)].ClearEverything();
                        WorldGen.PlaceWall(posX, (posY + 1), WallID.BorealWoodFence, mute: true);
                        if (WorldGen.SolidTile(posX, (posY + 2)))
                        {
                            WorldGen.PlaceWall(posX, (posY + 2), WallID.BorealWoodFence, mute: true);
                        }
                        posY++;
                    }
                    posX = x + 9;
                    posY = y + 1;
                    while (!WorldGen.SolidTile(posX, (posY + 1)))
                    {
                        if (posY == y + 1)
                        {
                            WorldGen.PlaceWall(posX, posY, WallID.BorealWoodFence, mute: true);
                        }
                        Main.tile[posX, (posY + 1)].ClearEverything();
                        WorldGen.PlaceWall(posX, (posY + 1), WallID.BorealWoodFence, mute: true);
                        if (WorldGen.SolidTile(posX, (posY + 2)))
                        {
                            WorldGen.PlaceWall(posX, (posY + 2), WallID.BorealWoodFence, mute: true);
                        }
                        posY++;
                    }
                    WorldGen.Place1x2Top((x + 8), (y - 4), TileID.LightningBuginaBottle, 0);
                    WorldGen.Place1x2Top((x - 8), (y - 4), TileID.LightningBuginaBottle, 0);
                    WorldGen.Place3x2(x, y, TileID.Campfire, 3);
                    int sign = (WorldGen.genRand.NextBool(2) ? -1 : 1);
                    WorldGen.Place1x1(x + (sign * 4), y, TileID.GoldCoinPile, 0);
                    WorldGen.Place3x2(x + (sign * 6), y, TileID.GoldCoinPile, 0);
                    WorldGen.Place1x1(x + (sign * 8), y, TileID.GoldCoinPile, 0);
                    for (int i = 0; i >= -4; i--)
                    {
                        for (int j = 9; j >= -9; j--)
                        {
                            if
                            (
                                WorldGen.genRand.NextBool(3) &&
                                !((j == 8 || j == -8) && i <= -3) && //Prevent overwriting Lanterns
                                (i <= -2)
                            )
                            {
                                WorldGen.PlaceTile((x + j), (y + i), TileID.Cobweb, mute: true);
                            }
                        }
                    }
                }
                else
                {
                    if (k > 0)
                    {
                        k--;
                    }
                    continue;
                }
            }
        }
    }

    public class IceBiomeCustomCaves : GenPass
    {
        public IceBiomeCustomCaves(string name, float loadWeight) : base(name, loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = WDALTWorldGenSystem.IceBiomeCustomCavesMessage.Value;
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 0.5); k++)
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop, GenVars.snowBottom);
                int x = 0;
                if (y >= 0 && y < GenVars.snowMinX.Length && y < GenVars.snowMaxX.Length)
                {
                    x = WorldGen.genRand.Next(GenVars.snowMinX[y], GenVars.snowMaxX[y]);
                }
                else
                {
                    x = WorldGen.genRand.Next(GenVars.snowMinX[0], GenVars.snowMaxX[0]);
                }
                double dirX = (double)WorldGen.genRand.NextFloat(-1f, 1f);
                double dirY = (double)WorldGen.genRand.NextFloat(-1f, 1f);
                WorldGen.digTunnel((double)x, (double)y, dirX, dirY, 64, 3, false);
            }
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 0.25); k++)
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop, GenVars.snowBottom);
                int x = 0;
                if (y >= 0 && y < GenVars.snowMinX.Length && y < GenVars.snowMaxX.Length)
                {
                    x = WorldGen.genRand.Next(GenVars.snowMinX[y], GenVars.snowMaxX[y]);
                }
                else
                {
                    x = WorldGen.genRand.Next(GenVars.snowMinX[0], GenVars.snowMaxX[0]);
                }
                int steps = WorldGen.genRand.Next(8, 16);
                int centerX = x;
                int centerY = y;
                for (int s = 1; s <= steps; s++)
                {
                    int size = WorldGen.genRand.Next(6, 12);
                    int type = (WorldGen.genRand.NextBool(3) ? WallID.SnowWallUnsafe : WallID.IceUnsafe);
                    for (int i = (centerX - size); i <= (centerX + size); i++)
                    {
                        for (int j = (centerY - size); j <= (centerY + size); j++)
                        {
                            int distance = (int)Math.Round(Vector2.Distance(new Vector2(centerX, centerY), new Vector2(i, j)));
                            if (distance <= size)
                            {
                                WorldGen.PlaceWall(i, j, type, mute: true);
                            }
                        }
                    }
                    int dir = WorldGen.genRand.Next(0, 8);
                    switch (dir)
                    {
                        case 0:
                            centerX = centerX + size;
                            break;
                        case 1:
                            centerX = centerX + size;
                            centerY = centerY + size;
                            break;
                        case 2:
                            centerY = centerY + size;
                            break;
                        case 3:
                            centerX = centerX - size;
                            centerY = centerY + size;
                            break;
                        case 4:
                            centerX = centerX - size;
                            break;
                        case 5:
                            centerX = centerX - size;
                            centerY = centerY - size;
                            break;
                        case 6:
                            centerY = centerY - size;
                            break;
                        case 7:
                            centerX = centerX + size;
                            centerY = centerY - size;
                            break;
                        default:
                            centerY = centerY - size;
                            break;
                    }
                }
            }
        }
    }
}
