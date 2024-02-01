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
            int PotsIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Pots"));
            if (IceBiomeIndex != -1 && PotsIndex != -1) {
                tasks.Insert(IceBiomeIndex + 1, new IceBiomeCustomCaves("Generate Additional Ice Biome Caves", 100f));
                tasks.Insert(PotsIndex - 1, new IceBiomeGloom("Generate Additional Ice Biome Gloom", 100f));
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
                bool cond = (
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
                bool cond = (
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
        }
    }
}
