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
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            TileID.Sets.OreMergesWithMud[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;
            TileID.Sets.HellSpecial[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileShine2[Type] = false;
            Main.tileShine[Type] = 512;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(235, 111, 0), name);

            DustType = DustID.Lava;
            HitSound = SoundID.Tink;
            MineResist = 4f;
            MinPick = 180;
        }
    }

    public class EmberfluxOreSystem : ModSystem
    {
        public static LocalizedText BlessedWithEmberfluxOreMessage { get; private set; }
        public const float worldSizeDenominator = 4200f;
        public const int minDistanceFromWorldBorder = 100;
        public const int oreVeinsPerBlessing = 512;
        public const int minStrength = 4;
        public const int maxStrength = 8;
        public const int minSteps = 4;
        public const int maxSteps = 8;

        public override void SetStaticDefaults()
        {
            BlessedWithEmberfluxOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithEmberfluxOreMessage)}");
        }

        public void BlessWorldWithEmberfluxOre()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(_ =>
            {
                // Broadcast a message to notify the user.
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(BlessedWithEmberfluxOreMessage.Value, 50, 255, 130);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(BlessedWithEmberfluxOreMessage.ToNetworkText(), new Color(50, 255, 130));
                }

                int splotches = (int)(oreVeinsPerBlessing * (Main.maxTilesX / worldSizeDenominator));
                int highestY = (int)Utils.Lerp(Main.UnderworldLayer, Main.maxTilesY, 0.25);
                for (int iteration = 0; iteration < splotches; iteration++)
                {
                    // Find a point in the upper two thirds of the underworld depth.
                    int i = WorldGen.genRand.Next(minDistanceFromWorldBorder, Main.maxTilesX - minDistanceFromWorldBorder);
                    int j = WorldGen.genRand.Next(highestY, Main.maxTilesY);

                    WorldGen.OreRunner(i, j, WorldGen.genRand.Next(minStrength, (maxStrength + 1)), WorldGen.genRand.Next(minSteps, (maxSteps + 1)), (ushort)ModContent.TileType<EmberfluxOre>());
                }
            });
        }
    }
}
