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

using Microsoft.Xna.Framework;
using System.Threading;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Content.Tiles
{
    public class EmberfluxOre : ModTile
    {
        public override void SetStaticDefaults() {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
            Main.tileOreFinderPriority[Type] = 410; // Metal Detector value, see https://terraria.wiki.gg/wiki/Metal_Detector
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileShine[Type] = 975; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(152, 171, 198), name);

            DustType = 84;
            HitSound = SoundID.Tink;
            MineResist = 4f;
            MinPick = 200;
        }
    }

    public class EmberfluxOreSystem : ModSystem
    {
        public static LocalizedText BlessedWithEmberfluxOreMessage { get; private set; }
        public const float worldSizeDenominator = 4200f;
        public const int minDistanceFromWorldBorder = 100;
        public const int oreVeinsPerBlessing = 100;
        public const int minStrength = 4;
        public const int maxStrength = 8;
        public const int minSteps = 4;
        public const int maxSteps = 8;

        public override void SetStaticDefaults() {
            BlessedWithEmberfluxOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithEmberfluxOreMessage)}");
        }

        public void BlessWorldWithEmberfluxOre() {
            if (Main.netMode == NetmodeID.MultiplayerClient) {
                return;
            }

            ThreadPool.QueueUserWorkItem(_ => {
                // Broadcast a message to notify the user.
                if (Main.netMode == NetmodeID.SinglePlayer) {
                    Main.NewText(BlessedWithEmberfluxOreMessage.Value, 50, 255, 130);
                }
                else if (Main.netMode == NetmodeID.Server) {
                    ChatHelper.BroadcastChatMessage(BlessedWithEmberfluxOreMessage.ToNetworkText(), new Color(50, 255, 130));
                }

                int splotches = (int)(oreVeinsPerBlessing * (Main.maxTilesX / worldSizeDenominator));
                int highestY = (int)Utils.Lerp(Main.rockLayer, Main.UnderworldLayer, 0.5);
                for (int iteration = 0; iteration < splotches; iteration++) {
                    // Find a point in the lower half of the rock layer but above the underworld depth.
                    int i = WorldGen.genRand.Next(minDistanceFromWorldBorder, Main.maxTilesX - minDistanceFromWorldBorder);
                    int j = WorldGen.genRand.Next(highestY, Main.UnderworldLayer);

                    WorldGen.OreRunner(i, j, WorldGen.genRand.Next(minStrength, (maxStrength + 1)), WorldGen.genRand.Next(minSteps, (maxSteps + 1)), (ushort)ModContent.TileType<EmberfluxOre>());
                }
            });
        }
    }
}
