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
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using WeDoALittleTrolling.Content.Items.Material;
using System.Collections.Generic;
using Humanizer;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class UnionMirror : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;

            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 90;
            Item.useAnimation = 90;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item6;

            Item.value = 50000;
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Blue;
        }

        public override void UseAnimation(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                player.RemoveAllGrapplingHooks();
                player.StopVanityActions(multiplayerBroadcast: false);
                player.Spawn(PlayerSpawnContext.RecallFromItem);
                base.UseAnimation(player);
            }
        }

        public static void RegisterHooks()
        {
            On_Player.HasUnityPotion += On_Player_HasUnityPotion;
            On_Player.TakeUnityPotion += On_Player_TakeUnityPotion;
        }

        public static bool On_Player_HasUnityPotion(On_Player.orig_HasUnityPotion orig, Player player)
        {
            if(player.HasItem(ModContent.ItemType<UnionMirror>()))
            {
                return true;
            }
            return orig.Invoke(player);
        }

        public static void On_Player_TakeUnityPotion(On_Player.orig_TakeUnityPotion orig, Player player)
        {
            if(player.HasItem(ModContent.ItemType<UnionMirror>()))
            {
                return;
            }
            orig.Invoke(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddTile(TileID.ShimmerMonolith)
                .AddIngredient(ItemID.MagicMirror, 1)
                .AddIngredient(ItemID.WormholePotion, 4)
                .Register();

            CreateRecipe(1)
                .AddTile(TileID.ShimmerMonolith)
                .AddIngredient(ItemID.IceMirror, 1)
                .AddIngredient(ItemID.WormholePotion, 4)
                .Register();
        }
    }
}
