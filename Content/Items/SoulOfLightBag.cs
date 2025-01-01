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
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Common.ModSystems;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using WeDoALittleTrolling.Content.Items.Material;
using Terraria.Utilities;
using System.Collections.Generic;

namespace WeDoALittleTrolling.Content.Items
{
    internal class TableDropAmountsSoLBag : SortedDictionary<int, Tuple<int, int>>
    {
        public TableDropAmountsSoLBag()
        {
            this.Add(ItemID.SoulofLight, Tuple.Create(40, 60));
            this.Add(ItemID.CrystalBullet, Tuple.Create(1000, 3000));
            this.Add(ItemID.HolyArrow, Tuple.Create(1000, 3000));
            this.Add(ItemID.JestersArrow, Tuple.Create(1000, 3000));
            this.Add(ItemID.HallowedKey, Tuple.Create(1, 1));
            this.Add(ItemID.BlueSolution, Tuple.Create(500, 1500));
            this.Add(ItemID.SuperManaPotion, Tuple.Create(250, 750));
            this.Add(ItemID.SuperHealingPotion, Tuple.Create(25, 75));
            this.Add(ItemID.FastClock, Tuple.Create(1, 1));
            this.Add(ItemID.Megaphone, Tuple.Create(1, 1));
            this.Add(ItemID.PirateMap, Tuple.Create(1, 1));
            this.Add(ItemID.SnowGlobe, Tuple.Create(1, 3));
        }
    }
    internal class SoulOfLightBag : ModItem
    {
        public static TableDropAmountsSoLBag lootTableItemAmounts = new TableDropAmountsSoLBag();
        
        public static WeightedRandom<int> lootTable = new WeightedRandom<int>
        (
            Tuple.Create((int)ItemID.SoulofLight, 1.0),
            Tuple.Create((int)ItemID.HolyArrow, 1.0),
            Tuple.Create((int)ItemID.CrystalBullet, 1.0),
            Tuple.Create((int)ItemID.JestersArrow, 0.1),
            Tuple.Create((int)ItemID.HallowedKey, 0.1),
            Tuple.Create((int)ItemID.BlueSolution, 1.0),
            Tuple.Create((int)ItemID.SuperManaPotion, 0.5),
            Tuple.Create((int)ItemID.SuperHealingPotion, 0.25),
            Tuple.Create((int)ItemID.FastClock, 0.1),
            Tuple.Create((int)ItemID.Megaphone, 0.1),
            Tuple.Create((int)ItemID.PirateMap, 0.1),
            Tuple.Create((int)ItemID.SnowGlobe, 0.1)
        );

        public static int GetItemIDFromLootTable()
        {
            lootTable.random.SetSeed(Main.rand.Next(0, 10000));
            lootTable.needsRefresh = true;
            return lootTable.Get();
        }

        public static int GetItemStackForItemID(int itemID)
        {
            if (lootTableItemAmounts.TryGetValue(itemID, out Tuple<int, int> amount))
            {
                return Main.rand.Next(amount.Item1, (amount.Item2 + 1));
            }
            else
            {
                return 0;
            }
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Orange;
            Item.consumable = false;

            Item.value = Item.sellPrice(gold: 1);
            Item.maxStack = 99;
            Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.ShimmerMonolith)
                .AddIngredient(ItemID.SoulofLight, 150)
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            return false;
        }

        public override void RightClick(Player player)
        {
            for (int i = 0; i < 3; i++)
            {
                int itemToDrop = GetItemIDFromLootTable();
                int amountToDrop = GetItemStackForItemID(itemToDrop);
                if (amountToDrop < 1)
                {
                    continue;
                }
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Item.NewItem(player.GetSource_OpenItem(itemToDrop), (int)player.position.X, (int)player.position.Y, player.width, player.height, itemToDrop, amountToDrop);
                }
                else if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket spawnCrateItemPacket = Mod.GetPacket();
                    spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                    spawnCrateItemPacket.Write((int)itemToDrop);
                    spawnCrateItemPacket.Write((int)player.width);
                    spawnCrateItemPacket.Write((int)player.height);
                    spawnCrateItemPacket.Write((int)amountToDrop);
                    spawnCrateItemPacket.WriteVector2(player.position);
                    spawnCrateItemPacket.Send();
                }
            }
            base.RightClick(player);
        }
    }
}
