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
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using WeDoALittleTrolling.Content.Items.Material;
using System.Collections.Generic;
using Humanizer;
using System;
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class MirrorOfRecollection : ModItem
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

            Item.value = 200000;
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Cyan;
        }

        public override void UseAnimation(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                player.GetModPlayer<WDALTPlayer>().mirrorOfRecollectionTicks = (Item.useAnimation / 2) + 1;
                base.UseAnimation(player);
            }
        }

        public static void TeleportHome(Player player)
        {
            /*player.RemoveAllGrapplingHooks();
            player.StopVanityActions(multiplayerBroadcast: false);
            player.Spawn(PlayerSpawnContext.RecallFromItem);*/
            player.DoPotionOfReturnTeleportationAndSetTheComebackPoint();
        }

        public override bool? UseItem(Player player)
        {
            AnimateMirrorOfRecollection(player);
            return base.UseItem(player);
        }

        public static void RegisterHooks()
        {
            On_Player.HasUnityPotion += On_Player_HasUnityPotion;
            On_Player.TakeUnityPotion += On_Player_TakeUnityPotion;
        }

        public static void UnregisterHooks()
        {
            On_Player.HasUnityPotion -= On_Player_HasUnityPotion;
            On_Player.TakeUnityPotion -= On_Player_TakeUnityPotion;
        }

        public static bool On_Player_HasUnityPotion(On_Player.orig_HasUnityPotion orig, Player player)
        {
            if(player.HasItem(ModContent.ItemType<MirrorOfRecollection>()))
            {
                return true;
            }
            return orig.Invoke(player);
        }

        public static void On_Player_TakeUnityPotion(On_Player.orig_TakeUnityPotion orig, Player player)
        {
            if(player.HasItem(ModContent.ItemType<MirrorOfRecollection>()))
            {
                return;
            }
            orig.Invoke(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddTile(TileID.ShimmerMonolith)
                .AddIngredient(ModContent.ItemType<UnionMirror>(), 1)
                .AddIngredient(ItemID.PotionOfReturn, 4)
                .AddIngredient(ItemID.Ectoplasm, 4)
                .Register();
        }

        public static void AnimateMirrorOfRecollection(Player player)
        {
            for (int i = 0; i < 3; i++)
            {
                int rMax = (int)player.width * 4;
                double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                double angle = Main.rand.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = player.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                Vector2 dustVelocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                dustVelocity = dustVelocity.SafeNormalize(Vector2.Zero);
                dustVelocity *= 3f;
                Dust newDust = Dust.NewDustPerfect(dustPosition, 181, dustVelocity, 0, default);
                newDust.noGravity = true;
            }
        }
    }
}
