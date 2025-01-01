/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2025 LukasV-Coding

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
using Terraria.Utilities;

namespace WeDoALittleTrolling.Common.ModSystems
{
    public class WDALTWorldGenSystem : ModSystem
    {
        public static LocalizedText GeneralCustomCavesMessage { get; private set; }
        public static LocalizedText IceBiomeCustomCavesMessage { get; private set; }
        public static LocalizedText IceBiomeGloomMessage { get; private set; }

        public override void SetStaticDefaults()
        {
            GeneralCustomCavesMessage = Language.GetOrRegister(Mod.GetLocalizationKey($"WorldGen.{nameof(GeneralCustomCavesMessage)}"));
            IceBiomeCustomCavesMessage = Language.GetOrRegister(Mod.GetLocalizationKey($"WorldGen.{nameof(IceBiomeCustomCavesMessage)}"));
            IceBiomeGloomMessage = Language.GetOrRegister(Mod.GetLocalizationKey($"WorldGen.{nameof(IceBiomeGloomMessage)}"));
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int CavesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Wavy Caves"));
            if (CavesIndex != -1)
            {
                tasks.Insert(CavesIndex + 1, new GeneralCustomCaves("Generate Additional Caves", 100f));
            }
            int IceBiomeIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Generate Ice Biome"));
            if (IceBiomeIndex != -1)
            {
                tasks.Insert(IceBiomeIndex + 1, new IceBiomeCustomCaves("Generate Additional Ice Biome Caves", 100f));
            }
            int GloomIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            if (GloomIndex != -1) {
                tasks.Insert((GloomIndex + 1), new IceBiomeGloom("Generate Additional Ice Biome Gloom", 100f));
            }
        }
    }

    public class IceBiomeGloom : GenPass
    {
        public static WeightedRandom<int> wRandom = new WeightedRandom<int>
        (
            Tuple.Create((int)ItemID.IceBoomerang, 1.0),
            Tuple.Create((int)ItemID.IceBlade, 1.0),
            Tuple.Create((int)ItemID.IceSkates, 1.0),
            Tuple.Create((int)ItemID.BlizzardinaBottle, 2.0),
            Tuple.Create((int)ItemID.FlurryBoots, 1.0),
            Tuple.Create((int)ItemID.IceMirror, 1.0),
            Tuple.Create((int)ItemID.IceMachine, 1.0),
            Tuple.Create((int)ItemID.Fish, 1.0),
            Tuple.Create((int)ItemID.SnowballCannon, 1.0)
        );
        
        public IceBiomeGloom(string name, float loadWeight) : base(name, loadWeight)
        {
        }

        public static void PlaceWallSupportDownwards(ref int posX, ref int posY, int y, int wallID)
        {
            while (!WorldGen.SolidTile(posX, (posY + 1)))
            {
                if (posY == y + 1)
                {
                    WorldGen.PlaceWall(posX, posY, wallID, mute: true);
                }
                Main.tile[posX, (posY + 1)].ClearEverything();
                WorldGen.PlaceWall(posX, (posY + 1), wallID, mute: true);
                if (WorldGen.SolidTile(posX, (posY + 2)))
                {
                    WorldGen.PlaceWall(posX, (posY + 2), wallID, mute: true);
                }
                posY++;
            }
        }

        public static void GenMineshafts(int x, int y)
        {
            int limitY = -7;
            Main.tile[(x), (y + 1)].ClearEverything();
            for (int i = -1; i >= limitY; i--)
            {
                Main.tile[(x), (y + i)].ClearEverything();
            }
            for (int i = 0; i >= limitY + 1; i--)
            {
                int random1 = WorldGen.genRand.Next(10);
                int wallType1 = WallID.IceBrick;
                if (random1 <= 2)
                {
                    wallType1 = WallID.SnowBrick;
                }
                if (WorldGen.genRand.Next(5) < 3)
                {
                    WorldGen.PlaceWall((x), (y + i), wallType1, mute: true);
                }
            }
            int random2 = WorldGen.genRand.Next(10);
            int tileType1 = TileID.IceBrick;
            if (random2 <= 2)
            {
                tileType1 = TileID.SnowBrick;
            }
            WorldGen.PlaceTile((x), (y + 1), tileType1, mute: true);
            WorldGen.PlaceTile((x), (y + limitY), tileType1, mute: true);
            if (Main.tile[x + 1, y].TileType != TileID.MinecartTrack && Main.tile[x - 1, y].TileType != TileID.MinecartTrack)
            {
                WorldGen.PlaceTile((x), (y + 2), tileType1, mute: true);
                WorldGen.PlaceTile((x), (y + limitY - 1), tileType1, mute: true);
                if (Main.tile[x + 1, y + 1].TileType == TileID.MinecartTrack && Main.tile[x - 1, y - 1].TileType == TileID.MinecartTrack)
                {
                    SlopeType slopeType = SlopeType.SlopeDownLeft;
                    SlopeType slopeTypeInverse = SlopeType.SlopeUpRight;
                    WorldGen.SlopeTile((x), (y + 1), (int)slopeType, noEffects: true);
                    WorldGen.SlopeTile((x), (y + limitY - 1), (int)slopeType, noEffects: true);
                    WorldGen.SlopeTile((x), (y + 2), (int)slopeTypeInverse, noEffects: true);
                    WorldGen.SlopeTile((x), (y + limitY), (int)slopeTypeInverse, noEffects: true);
                }
                else if (Main.tile[x + 1, y - 1].TileType == TileID.MinecartTrack && Main.tile[x - 1, y + 1].TileType == TileID.MinecartTrack)
                {
                    SlopeType slopeType = SlopeType.SlopeDownRight;
                    SlopeType slopeTypeInverse = SlopeType.SlopeUpLeft;;
                    WorldGen.SlopeTile((x), (y + 1), (int)slopeType, noEffects: true);
                    WorldGen.SlopeTile((x), (y + limitY - 1), (int)slopeType, noEffects: true);
                    WorldGen.SlopeTile((x), (y + 2), (int)slopeTypeInverse, noEffects: true);
                    WorldGen.SlopeTile((x), (y + limitY), (int)slopeTypeInverse, noEffects: true);
                }
            }
            else if (Main.tile[x + 1, y].TileType != TileID.MinecartTrack && Main.tile[x - 1, y].TileType == TileID.MinecartTrack)
            {
                if (Main.tile[x + 1, y - 1].TileType == TileID.MinecartTrack)
                {
                    WorldGen.PlaceTile((x), (y + limitY - 1), tileType1, mute: true);
                    WorldGen.SlopeTile((x), (y + limitY - 1), (int)SlopeType.SlopeDownRight, noEffects: true);
                    WorldGen.SlopeTile((x), (y + limitY), (int)SlopeType.SlopeUpLeft, noEffects: true);
                }
                else if (Main.tile[x + 1, y + 1].TileType == TileID.MinecartTrack)
                {
                    WorldGen.PlaceTile((x), (y + 2), tileType1, mute: true);
                    WorldGen.SlopeTile((x), (y + 2), (int)SlopeType.SlopeUpRight, noEffects: true);
                    WorldGen.SlopeTile((x), (y + 1), (int)SlopeType.SlopeDownLeft, noEffects: true);
                }
            }
            else if (Main.tile[x + 1, y].TileType == TileID.MinecartTrack && Main.tile[x - 1, y].TileType != TileID.MinecartTrack)
            {
                if (Main.tile[x - 1, y - 1].TileType == TileID.MinecartTrack)
                {
                    WorldGen.PlaceTile((x), (y + limitY - 1), tileType1, mute: true);
                    WorldGen.SlopeTile((x), (y + limitY - 1), (int)SlopeType.SlopeDownLeft, noEffects: true);
                    WorldGen.SlopeTile((x), (y + limitY), (int)SlopeType.SlopeUpRight, noEffects: true);
                }
                else if (Main.tile[x - 1, y + 1].TileType == TileID.MinecartTrack)
                {
                    WorldGen.PlaceTile((x), (y + 2), tileType1, mute: true);
                    WorldGen.SlopeTile((x), (y + 2), (int)SlopeType.SlopeUpLeft, noEffects: true);
                    WorldGen.SlopeTile((x), (y + 1), (int)SlopeType.SlopeDownRight, noEffects: true);
                }
            }
            if (x % 64 == 0)
            {
                WorldGen.Place1x2Top((x), (y + limitY + 1), TileID.HangingLanterns, 3);
            }
            else
            {
                for (int i = -1; i >= limitY + 1; i--)
                {
                    if (WorldGen.genRand.NextBool(5))
                    {
                        WorldGen.PlaceTile((x), (y + i), TileID.Cobweb, mute: true);
                    }
                }
            }
        }

        public static void GenWorkshop(int x, int y)
        {
            for (int i = 1; i >= -27; i--)
            {
                for (int j = 25; j >= -25; j--)
                {
                    if (j < 25 && j > -25)
                    {
                        Main.tile[(x + j), (y + i)].ClearEverything();
                    }
                    else
                    {
                        Main.tile[(x + j), (y + i)].ClearTile();
                    }
                    if (i <= 0 && i >= -26 && j <= 23 && j >= -23)
                    {
                        int random1 = WorldGen.genRand.Next(10);
                        int wallType1 = WallID.IceBrick;
                        if (random1 <= 6)
                        {
                            wallType1 = WallID.SnowBrick;
                        }
                        WorldGen.PlaceWall((x + j), (y + i), wallType1, mute: true);
                    }
                    if ((i == 1 || i == -6 || i == -13 || i == -20 || i == -27) || (j == 24 || j == 0 || j == -24))
                    {
                        if
                        (
                            (
                                (i == -6 || i == -13 || i == -20) &&
                                ((j <= 23 && j >= 17) || (j >= -23 && j <= -17))
                            )
                        )
                        {
                            WorldGen.PlaceTile((x + j), (y + i), TileID.Platforms, mute: true, default, default, 35);
                        }
                        else if (!(j == 0 && (i == 0 || i == -1 || i == -2 || i == -7 || i == -8 || i == -9 || i == -14 || i == -15 || i == -16 || i == -21 || i == -22 || i == -23)))
                        {
                            int random1 = WorldGen.genRand.Next(10);
                            int tileType1 = TileID.IceBrick;
                            if (random1 <= 6)
                            {
                                tileType1 = TileID.SnowBrick;
                            }
                            WorldGen.PlaceTile((x + j), (y + i), tileType1, mute: true);
                        }
                    }
                    SlopeType slopeType = (j < 0 ? SlopeType.SlopeDownLeft : SlopeType.SlopeDownRight);
                    SlopeType slopeTypeInverse = (j >= 0 ? SlopeType.SlopeDownLeft : SlopeType.SlopeDownRight);
                    if
                    (
                        !(i == 1 || i == -6 || i == -13 || i == -20 || i == -27) &&
                        (i >= -20) &&
                        (j == (-17 + (i % 7)) || j == (17 - (i % 7)))
                    )
                    {
                        WorldGen.PlaceTile((x + j), (y + i), TileID.Platforms, mute: true, default, default, 35);
                        WorldGen.SlopeTile((x + j), (y + i), (int)slopeType, noEffects: true);
                        WorldGen.SquareTileFrame((x + j), (y + i), default);
                    }
                    if ((i == -6 || i == -13 || i == -20) && (j == 23 || j == -23))
                    {
                        WorldGen.SlopeTile((x + j), (y + i), (int)slopeType, noEffects: true);
                        WorldGen.SquareTileFrame((x + j), (y + i), default);
                    }
                    if ((i == 1 || i == -6 || i == -13 || i == -20 || i == -27) && (j == 25 || j == -25))
                    {
                        WorldGen.SlopeTile((x + j), (y + i), (int)slopeTypeInverse, noEffects: true);
                        WorldGen.SquareTileFrame((x + j), (y + i), default);
                    }
                }
            }
            int posX = x - 23;
            int posY = y + 1;
            PlaceWallSupportDownwards(ref posX, ref posY, y, WallID.WroughtIronFence);
            posX = x + 23;
            posY = y + 1;
            PlaceWallSupportDownwards(ref posX, ref posY, y, WallID.WroughtIronFence);
            posX = x;
            posY = y + 1;
            PlaceWallSupportDownwards(ref posX, ref posY, y, WallID.WroughtIronFence);
            WorldGen.Place1x2Top((x + 25), (y - 5), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 25), (y - 5), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x + 25), (y - 12), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 25), (y - 12), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x + 25), (y - 19), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 25), (y - 19), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x + 25), (y - 26), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 25), (y - 26), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x + 1), (y - 5), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 1), (y - 5), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x + 1), (y - 12), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 1), (y - 12), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x + 1), (y - 19), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 1), (y - 19), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x + 1), (y - 26), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 1), (y - 26), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x + 16), (y - 5), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 16), (y - 5), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x + 16), (y - 12), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 16), (y - 12), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x + 16), (y - 19), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 16), (y - 19), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x + 16), (y - 26), TileID.HangingLanterns, 2);
            WorldGen.Place1x2Top((x - 16), (y - 26), TileID.HangingLanterns, 2);
            WorldGen.Place1xX(x, (y - 0), TileID.ClosedDoor, 27);
            WorldGen.Place1xX(x, (y - 7), TileID.ClosedDoor, 27);
            WorldGen.Place1xX(x, (y - 14), TileID.ClosedDoor, 27);
            WorldGen.Place1xX(x, (y - 21), TileID.ClosedDoor, 27);
            // Room 1
            for (int k = 3; k < 9; k++)
            {
                WorldGen.PlaceTile((x - k), (y - 23), TileID.Platforms, mute: true, default, default, 35);
            }
            for (int k = 3; k < 9; k += 2)
            {
                WorldGen.Place2x2((x - k), (y - 21), TileID.Safes, 0);
                WorldGen.Place2x2((x - k), (y - 24), TileID.Safes, 0);
            }
            int idx1 = WorldGen.PlaceChest((x - 12), (y - 21), TileID.Containers, notNearOtherChests: false, 6);
            if (idx1 >= 0 && idx1 < Main.chest.Length)
            {
                Chest chest = Main.chest[idx1];
                List<(int type, int stack)> itemsToAdd = new List<(int type, int stack)>();
                itemsToAdd.Add((ItemID.TinCan, Main.rand.Next(3, 6)));
                itemsToAdd.Add((ItemID.OldShoe, Main.rand.Next(1, 3)));
                itemsToAdd.Add((ItemID.Cobweb, Main.rand.Next(5, 11)));
                itemsToAdd.Add((ItemID.Glowstick, Main.rand.Next(5, 11)));
                itemsToAdd.Add((ItemID.DirtBlock, Main.rand.Next(3, 6)));
                itemsToAdd.Add(((Main.rand.NextBool(2) ? ItemID.PaperAirplaneA : ItemID.PaperAirplaneB), Main.rand.Next(1, 4)));
                int chestItemIndex = 0;
                foreach ((int type, int stack) itemToAdd in itemsToAdd)
                {
                    Item item = new Item();
                    item.SetDefaults(itemToAdd.type);
                    item.stack = itemToAdd.stack;
                    chest.item[chestItemIndex] = item;
                    chestItemIndex++;
                    if (chestItemIndex >= 40)
                    {
                        break;
                    }
                }
            }
            WorldGen.Place2x2((x - 13), (y - 21), TileID.Sinks, 14);
            // Room 2
            WorldGen.Place1x2((x - 3), (y - 14), TileID.Chairs, 17);
            WorldGen.Place3x2((x - 6), (y - 14), TileID.Tables, 14);
            WorldGen.Place3x3((x - 10), (y - 14), TileID.Sawmill, 0);
            WorldGen.Place3x3Wall((x - 10), (y - 18), TileID.Painting3X3, 42);
            WorldGen.Place3x2((x - 14), (y - 14), TileID.Loom, 0);
            WorldGen.Place1x1((x - 18), (y - 14), TileID.StinkbugHousingBlocker, 0);
            // Room 3
            int idx2 = WorldGen.PlaceChest((x - 9), (y - 7), TileID.Containers, notNearOtherChests: false, 5);
            int idx3 = WorldGen.PlaceChest((x - 11), (y - 7), TileID.Containers, notNearOtherChests: false, 5);
            if (idx2 >= 0 && idx2 < Main.chest.Length && idx3 >= 0 && idx3 < Main.chest.Length)
            {
                Chest chest1 = Main.chest[idx2];
                Chest chest2 = Main.chest[idx3];
                List<(int type, int stack)> itemsToAdd1 = new List<(int type, int stack)>();
                itemsToAdd1.Add((ItemID.FlaskofFire, Main.rand.Next(4, 9)));
                itemsToAdd1.Add((ItemID.FlaskofPoison, Main.rand.Next(4, 9)));
                itemsToAdd1.Add((ItemID.FlaskofGold, Main.rand.Next(4, 9)));
                itemsToAdd1.Add((ItemID.LuckPotion, Main.rand.Next(4, 9)));
                itemsToAdd1.Add((ItemID.SpelunkerPotion, Main.rand.Next(4, 9)));
                itemsToAdd1.Add((ItemID.ShinePotion, Main.rand.Next(4, 9)));
                itemsToAdd1.Add((ItemID.Blinkroot, Main.rand.Next(4, 13)));
                itemsToAdd1.Add((ItemID.Deathweed, Main.rand.Next(4, 13)));
                itemsToAdd1.Add((ItemID.Fireblossom, Main.rand.Next(4, 13)));
                List<(int type, int stack)> itemsToAdd2 = new List<(int type, int stack)>();
                itemsToAdd2.Add((ItemID.IronskinPotion, Main.rand.Next(4, 9)));
                itemsToAdd2.Add((ItemID.EndurancePotion, Main.rand.Next(4, 9)));
                itemsToAdd2.Add((ItemID.InvisibilityPotion, Main.rand.Next(4, 9)));
                itemsToAdd2.Add((ItemID.BuilderPotion, Main.rand.Next(4, 9)));
                itemsToAdd2.Add((ItemID.BiomeSightPotion, Main.rand.Next(4, 9)));
                itemsToAdd2.Add((ItemID.CalmingPotion, Main.rand.Next(4, 9)));
                itemsToAdd2.Add((ItemID.Daybloom, Main.rand.Next(4, 13)));
                itemsToAdd2.Add((ItemID.Moonglow, Main.rand.Next(4, 13)));
                itemsToAdd2.Add((ItemID.Waterleaf, Main.rand.Next(4, 13)));
                int chestItemIndex = 0;
                foreach ((int type, int stack) itemToAdd in itemsToAdd1)
                {
                    Item item = new Item();
                    item.SetDefaults(itemToAdd.type);
                    item.stack = itemToAdd.stack;
                    chest1.item[chestItemIndex] = item;
                    chestItemIndex++;
                    if (chestItemIndex >= 40)
                    {
                        break;
                    }
                }
                chestItemIndex = 0;
                foreach ((int type, int stack) itemToAdd in itemsToAdd2)
                {
                    Item item = new Item();
                    item.SetDefaults(itemToAdd.type);
                    item.stack = itemToAdd.stack;
                    chest2.item[chestItemIndex] = item;
                    chestItemIndex++;
                    if (chestItemIndex >= 40)
                    {
                        break;
                    }
                }
            }
            for (int k = 3; k < 12; k++)
            {
                WorldGen.PlaceTile((x - k), (y - 10), TileID.Platforms, mute: true, default, default, 35);
            }
            WorldGen.Place2x2Style((x - 3), (y - 7), TileID.CookingPots, 1);
            WorldGen.Place2x2((x - 6), (y - 7), TileID.Kegs, 0);
            WorldGen.Place3x3((x - 14), (y - 7), TileID.AlchemyTable, 0);
            WorldGen.PlaceOnTable1x1((x - 4), (y - 11), TileID.Bottles, 0);
            WorldGen.PlaceOnTable1x1((x - 7), (y - 11), TileID.Bottles, 2);
            WorldGen.PlaceOnTable1x1((x - 8), (y - 11), TileID.Bottles, 0);
            WorldGen.PlaceOnTable1x1((x - 10), (y - 11), TileID.Bottles, 1);
            // Room 4
            WorldGen.PlaceMan((x - 4), y, 0);
            WorldGen.PlaceWoman((x - 16), y, 1);
            WorldGen.Place2xX((x - 8), y, TileID.Statues, 0);
            WorldGen.Place2xX((x - 12), y, TileID.Statues, 0);
            WorldGen.Place3x3Wall((x - 4), (y - 4), TileID.Painting3X3, 45);
            WorldGen.Place3x3Wall((x - 8), (y - 4), TileID.Painting3X3, 44);
            WorldGen.Place3x3Wall((x - 12), (y - 4), TileID.Painting3X3, 43);
            // Room 5
            WorldGen.Place3x3((x + 4), y, TileID.GlassKiln, 0);
            WorldGen.Place3x2((x + 8), y, TileID.SharpeningStation, 0);
            WorldGen.Place2x1((x + 11), y, TileID.Anvils, 0);
            WorldGen.Place3x2((x + 14), y, TileID.Furnaces, 0);
            WorldGen.Place3x3Wall((x + 13), (y - 4), TileID.Painting3X3, 41);
            // Room 6
            WorldGen.Place1x2((x + 3), (y - 7), TileID.Chairs, 21);
            Main.tile[(x + 3), (y - 7)].TileFrameX += 18; //Change Orientation
            WorldGen.Place3x2((x + 6), (y - 7), TileID.Tables, 17);
            WorldGen.Place1x2((x + 9), (y - 7), TileID.Chairs, 21);
            WorldGen.PlaceOnTable1x1((x + 5), (y - 9), TileID.Bottles, 4);
            WorldGen.Place2x1((x + 6), (y - 9), TileID.Bowls, 2);
            WorldGen.Place3x2((x + 12), (y - 7), TileID.Benches, 1);
            WorldGen.Place3x3Wall((x + 12), (y - 10), TileID.Painting3X3, 79);
            WorldGen.PlaceChand((x + 6), (y - 12), TileID.Chandeliers, 3);
            WorldGen.Place1x1((x + 18), (y - 7), TileID.StinkbugHousingBlocker, 0);
            // Room 7
            WorldGen.Place3x3((x + 4), (y - 14), TileID.SteampunkBoiler, 0);
            WorldGen.Place3x3((x + 14), (y - 14), TileID.Solidifier, 0);
            WorldGen.Place1x2Top((x + 7), (y - 19), TileID.HangingLanterns, 14);
            WorldGen.Place1x2Top((x + 9), (y - 19), TileID.HangingLanterns, 14);
            WorldGen.Place1x2Top((x + 11), (y - 19), TileID.HangingLanterns, 14);
            for (int k = 6; k < 13; k++)
            {
                WorldGen.PlaceTile((x + k), (y - 15), TileID.ConveyorBeltRight, mute: true);
            }
            for (int k = 7; k < 13; k += 2)
            {
                WorldGen.PlaceTile((x + k), (y - 14), TileID.Chain, mute: true);
            }
            // Room 8
            WorldGen.Place3x3((x + 4), (y - 21), TileID.HeavyWorkBench, 0);
            WorldGen.Place2x1((x + 7), (y - 21), TileID.WorkBenches, 15);
            WorldGen.Place3x3((x + 11), (y - 21), TileID.DyeVat, 0);
            WorldGen.Place3x2((x + 15), (y - 21), TileID.Blendomatic, 0);
        }

        public static void GenIceHut(int x, int y)
        {
            for (int i = 1; i >= -5; i--)
            {
                for (int j = 10; j >= -10; j--)
                {
                    Main.tile[(x + j), (y + i)].ClearEverything();
                    if (i <= 0 && i >= -4 && j <= 9 && j >= -9)
                    {
                        if (!WorldGen.genRand.NextBool(5))
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
            PlaceWallSupportDownwards(ref posX, ref posY, y, WallID.BorealWoodFence);
            posX = x + 9;
            posY = y + 1;
            PlaceWallSupportDownwards(ref posX, ref posY, y, WallID.BorealWoodFence);
            WorldGen.Place1x2Top((x + 8), (y - 4), TileID.LightningBuginaBottle, 0);
            WorldGen.Place1x2Top((x - 8), (y - 4), TileID.LightningBuginaBottle, 0);
            WorldGen.Place3x2(x, y, TileID.Campfire, 3);
            int sign = (WorldGen.genRand.NextBool(2) ? -1 : 1);
            WorldGen.Place1x1(x + (sign * 4), y, TileID.GoldCoinPile, 0);
            WorldGen.Place3x2(x + (sign * 6), y, TileID.GoldCoinPile, 0);
            WorldGen.Place1x1(x + (sign * 8), y, TileID.GoldCoinPile, 0);
            int chestX = x - (sign * 6);
            if (sign < 0)
            {
                chestX--;
            }
            int idx = WorldGen.PlaceChest(chestX, y, TileID.Containers, notNearOtherChests: false, 11);
            if (idx >= 0 && idx < Main.chest.Length)
            {
                Chest chest = Main.chest[idx];
                List<(int type, int stack)> itemsToAdd = new List<(int type, int stack)>();
                wRandom.random.SetSeed(Main.rand.Next(0, 10000));
                wRandom.needsRefresh = true;
                int specialItem = wRandom.Get();
                if (specialItem != ItemID.None)
                {
                    itemsToAdd.Add((specialItem, 1));
                }

                switch (Main.rand.Next(5))
                {
                    case 0:
                    case 1:
                        itemsToAdd.Add((ItemID.FiberglassFishingPole, 1));
                        itemsToAdd.Add((ItemID.FrozenCrate, 1));
                        itemsToAdd.Add((ItemID.FishingPotion, 2));
                        itemsToAdd.Add((ItemID.CratePotion, 2));
                        itemsToAdd.Add((ItemID.SonarPotion, 2));
                        itemsToAdd.Add((ItemID.MasterBait, Main.rand.Next(8, 17)));
                        break;
                    case 2:
                        itemsToAdd.Add((ItemID.BonePickaxe, 1));
                        itemsToAdd.Add((ItemID.LeadBar, Main.rand.Next(4, 17)));
                        itemsToAdd.Add((ItemID.TungstenBar, Main.rand.Next(4, 17)));
                        itemsToAdd.Add((ItemID.PlatinumBar, Main.rand.Next(4, 17)));
                        itemsToAdd.Add((ItemID.MiningPotion, 2));
                        itemsToAdd.Add((ItemID.SpelunkerPotion, 2));
                        break;
                    case 3:
                        itemsToAdd.Add((ItemID.ManaPotion, Main.rand.Next(8, 17)));
                        itemsToAdd.Add((ItemID.ManaRegenerationPotion, 2));
                        itemsToAdd.Add((ItemID.MagicPowerPotion, 2));
                        itemsToAdd.Add((ItemID.HealingPotion, Main.rand.Next(8, 17)));
                        itemsToAdd.Add((ItemID.RegenerationPotion, 2));
                        itemsToAdd.Add((ItemID.LifeforcePotion, 2));
                        break;
                    case 4:
                        itemsToAdd.Add((ItemID.RestorationPotion, Main.rand.Next(8, 17)));
                        itemsToAdd.Add((ItemID.MagicLantern, 1));
                        itemsToAdd.Add((ItemID.GoblinBattleStandard, 1));
                        itemsToAdd.Add((ItemID.SuspiciousLookingEye, 1));
                        itemsToAdd.Add((ItemID.FrostburnArrow, Main.rand.Next(200, 401)));
                        itemsToAdd.Add((ItemID.PartyBullet, Main.rand.Next(200, 401)));
                        break;
                    default:
                        itemsToAdd.Add((ItemID.FiberglassFishingPole, 1));
                        itemsToAdd.Add((ItemID.FrozenCrate, 1));
                        itemsToAdd.Add((ItemID.FishingPotion, 2));
                        itemsToAdd.Add((ItemID.CratePotion, 2));
                        itemsToAdd.Add((ItemID.SonarPotion, 2));
                        itemsToAdd.Add((ItemID.MasterBait, Main.rand.Next(8, 17)));
                        break;
                }

                int chestItemIndex = 0;
                foreach ((int type, int stack) itemToAdd in itemsToAdd)
                {
                    Item item = new Item();
                    item.SetDefaults(itemToAdd.type);
                    item.stack = itemToAdd.stack;
                    chest.item[chestItemIndex] = item;
                    chestItemIndex++;
                    if (chestItemIndex >= 40)
                    {
                        break;
                    }
                }
            }
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

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = WDALTWorldGenSystem.IceBiomeGloomMessage.Value;
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 0.75); k++)
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop + (Main.drunkWorld ? 100 : 0), GenVars.snowBottom);
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
                     WorldGen.SolidTile(x, y + 1) &&
                    !WorldGen.SolidTile(x, y    ) &&
                    (
                        Main.tile[x, y + 1].TileType == TileID.SnowBlock ||
                        Main.tile[x, y + 1].TileType == TileID.IceBlock
                    ) &&
                    !Main.tile[x, y].HasTile &&
                    Main.tile[x, y].LiquidAmount == 0 &&
                    Main.tile[x, y + 1].HasUnactuatedTile &&
                    Main.tile[x, y + 1].Slope == SlopeType.Solid
                );
                if (cond)
                {
                    WorldGen.PlaceAlch(x, y, 6);
                    if (Main.tile[x, y].HasTile)
                    {
                        Main.tile[x, y].TileType = TileID.BloomingHerbs;
                        Main.tile[x, y].TileFrameX = 108;
                        Main.tile[x, y].TileFrameY = 0;
                    }
                }
                else
                {
                    if (k >= 0)
                    {
                        k--;
                    }
                    continue;
                }
            }
            int br = 0;
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 5.0); k++)
            {
                
                if (br > 10000) //Max. 10000 attempts
                {
                    break;
                }
                int y = WorldGen.genRand.Next(GenVars.snowTop + (Main.drunkWorld ? 100 : 0), GenVars.snowBottom);
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
                    !WorldGen.SolidTile(x, y + 1) &&
                    Main.tile[x, y - 1].TileType == TileID.IceBlock &&
                    !Main.tile[x, y].HasTile &&
                    !Main.tile[x, (y + 1)].HasTile
                );
                if (cond)
                {
                    br = 0;
                    WorldGen.PlaceUncheckedStalactite(x, y, WorldGen.genRand.NextBool(3), 0, false);
                    WorldGen.SquareTileFrame(x, y, default);
                    WorldGen.SquareTileFrame(x, (y + 1), default);
                }
                else
                {
                    br++;
                    if (k >= 0)
                    {
                        k--;
                    }
                    continue;
                }
            }
            for (int y = GenVars.snowTop + (Main.drunkWorld ? 100 : 0); y <= GenVars.snowBottom; y++)
            {
                if (y >= 0 && y < GenVars.snowMinX.Length && y < GenVars.snowMaxX.Length)
                {
                    for (int x = GenVars.snowMinX[y]; x <= GenVars.snowMaxX[y]; x++)
                    {
                        if (Main.tile[x, y].TileType == TileID.MinecartTrack)
                        {
                            GenMineshafts(x, y);
                        }
                    }
                }
                else
                {
                    for (int x = GenVars.snowMinX[0]; x <= GenVars.snowMaxX[0]; x++)
                    {
                        if (Main.tile[x, y].TileType == TileID.MinecartTrack)
                        {
                            GenMineshafts(x, y);
                        }
                    }
                }
            }
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 0.125); k++)
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop + (Main.drunkWorld ? 100 : 0), GenVars.snowBottom);
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
                    !WorldGen.SolidTile(x, y + 1) &&
                    Main.tile[x, y].TileType != TileID.Stalactite &&
                    !Main.tile[x, y].HasTile &&
                    !Main.tile[x, (y + 1)].HasTile
                );
                if (cond)
                {
                    WorldGen.Place1x2Top(x, y, TileID.LightningBuginaBottle, 0);
                }
                else
                {
                    if (k >= 0)
                    {
                        k--;
                    }
                    continue;
                }
            }
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 0.1875); k++)
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop + (Main.drunkWorld ? 100 : 0), GenVars.snowBottom);
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
                    (WorldGen.TileType (x + 1, y - 1) != TileID.Campfire) &&
                    (WorldGen.TileType (x    , y - 1) != TileID.Campfire) &&
                    (WorldGen.TileType (x - 1, y - 1) != TileID.Campfire) &&
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
                    if (k >= 0)
                    {
                        k--;
                    }
                    continue;
                }
            }
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 0.015625); k++)
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop + (Main.drunkWorld ? 100 : 0), GenVars.snowBottom);
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
                            WorldGen.TileType((x + j), (y + i)) == TileID.SnowBrick ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.Containers
                        )
                        {
                            cond = false;
                        }
                    }
                }
                if (cond)
                {
                    GenIceHut(x, y);
                }
                else
                {
                    if (k >= 0)
                    {
                        k--;
                    }
                    continue;
                }
            }
            bool success = false;
            while (!success) //Spawn exactly 1 time.
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop + (Main.drunkWorld ? 100 : 0), GenVars.snowBottom);
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
                for (int i = 1; i >= -27; i--)
                {
                    for (int j = 25; j >= -25; j--)
                    {
                        if
                        (
                            WorldGen.TileType((x + j), (y + i)) == TileID.Campfire ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.IceBrick ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.Containers ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.BorealWood ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.BlueDungeonBrick ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.PinkDungeonBrick ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.GreenDungeonBrick ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.CrackedBlueDungeonBrick ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.CrackedPinkDungeonBrick ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.CrackedGreenDungeonBrick ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.Dirt ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.Stone ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.LihzahrdAltar ||
                            WorldGen.TileType((x + j), (y + i)) == TileID.LihzahrdBrick
                        )
                        {
                            cond = false;
                        }
                    }
                }
                if (cond)
                {
                    GenWorkshop(x, y);
                    success = true;
                }
                else
                {
                    success = false;
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
            //Ice Biome Specific Caverer - Disabled because global caverer is enabled
            /*
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 0.5); k++)
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop + (Main.drunkWorld ? 100 : 0), GenVars.snowBottom);
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
            }*/
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 0.5); k++)
            {
                int y = WorldGen.genRand.Next(GenVars.snowTop + (Main.drunkWorld ? 100 : 0), GenVars.snowBottom);
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

    public class GeneralCustomCaves : GenPass
    {
        public GeneralCustomCaves(string name, float loadWeight) : base(name, loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = WDALTWorldGenSystem.GeneralCustomCavesMessage.Value;
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05 * 8.0); k++)
            {
                int y = WorldGen.genRand.Next((int)Main.worldSurface + (Main.drunkWorld ? 100 : 0), (int)Main.UnderworldLayer);
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                double dirX = (double)WorldGen.genRand.NextFloat(-1f, 1f);
                double dirY = (double)WorldGen.genRand.NextFloat(-1f, 1f);
                WorldGen.digTunnel((double)x, (double)y, dirX, dirY, 64, 3, false);
            }
        }
    }
}
