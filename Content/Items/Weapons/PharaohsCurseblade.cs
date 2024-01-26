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
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using Microsoft.CodeAnalysis;
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class PharaohsCurseblade : ModItem
    {
        public static UnifiedRandom rnd = new UnifiedRandom(); //rnd
        public long lastDashTick;

        public override void SetDefaults()
        {
            Item.width = 68;
            Item.height = 76;

            Item.consumable = false;

            Item.value = Item.sellPrice(gold: 5);
            Item.maxStack = 1;

            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item60;

            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 5.5f;
            Item.crit = 6;

            Item.rare = ItemRarityID.Orange;
            Item.scale = 0.85f;
        }

        public override bool AltFunctionUse(Player player) // Woohoo Tornado Launch! (not yet implemented)
        {
            if(Math.Abs(player.GetModPlayer<WDALTPlayer>().currentTick - lastDashTick) >= 120)
            {
                player.SetImmuneTimeForAllTypes(player.longInvince ? 90 : 60);
                Vector2 chargeDirection = new Vector2(Main.MouseWorld.X - player.position.X, Main.MouseWorld.Y - player.position.Y);
                chargeDirection.Normalize();
                chargeDirection *= 24f;
                player.velocity = chargeDirection;
                player.GetModPlayer<WDALTPlayer>().chargeAccelerationTicks += 25;
                for (int i = 0; i < 60; i++)
                {
                    int rMax = (int)player.width;
                    double r = rMax * Math.Sqrt(rnd.NextDouble());
                    double angle = rnd.NextDouble() * 2 * Math.PI;
                    int xOffset = (int)Math.Round(r * Math.Cos(angle));
                    int yOffset = (int)Math.Round(r * Math.Sin(angle));
                    Vector2 dustPosition = player.Center;
                    dustPosition.X += xOffset;
                    dustPosition.Y += yOffset;
                    Vector2 dustVelocity = new Vector2((rnd.NextFloat() - 0.5f), (rnd.NextFloat() - 0.5f));
                    dustVelocity.Normalize();
                    dustVelocity *= 8f;
                    Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Electric, dustVelocity, 0, default);
                    newDust.noGravity = true;
                }
                SoundEngine.PlaySound(SoundID.Item117, player.Center);
                lastDashTick = player.GetModPlayer<WDALTPlayer>().currentTick;
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.CatBast)
              .AddIngredient(ItemID.Cactus, 5)
              .AddIngredient(ItemID.NightsEdge,1)
              .AddIngredient(ItemID.Amber, 10)
              .AddIngredient(ItemID.SandBlock, 10)
              .AddIngredient(ItemID.AntlionMandible, 5)
              .Register();
        }
    }
}
