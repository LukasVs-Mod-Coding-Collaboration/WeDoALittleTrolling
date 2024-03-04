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
        public const int chargeTicksMax = 120;
        public const float chargeTicksScalingFactor = 0.01f;
        public static UnifiedRandom rnd = new UnifiedRandom(); //rnd
        public int chargeTicks;
        public bool isCharging;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 54;

            Item.consumable = false;

            Item.value = Item.sellPrice(gold: 5);
            Item.maxStack = 1;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item60;

            Item.damage = 28;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 5.5f;

            Item.rare = ItemRarityID.Orange;
        }

        public override bool AltFunctionUse(Player player) // Woohoo Tornado Launch! (not yet implemented)
        {
            if (!isCharging)
            {
                chargeTicks = 0;
                isCharging = true;
            }
            if (chargeTicks < chargeTicksMax)
            {
                chargeTicks++;
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
                Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Sandnado, dustVelocity, 0, default);
                newDust.noGravity = true;
                if (chargeTicks == 1 || chargeTicks % 45 == 0)
                {
                    SoundEngine.PlaySound(SoundID.DD2_BetsyWindAttack, player.Center);
                }
                if (player.itemAnimation <= (Item.useAnimation / 4))
                {
                    player.SetItemAnimation(Item.useAnimation);
                    player.itemAnimation = (Item.useAnimation / 4);
                }
            }
            return false;
        }

        public override bool? CanHitNPC(Player player, NPC target)
        {
            if(isCharging)
            {
                return false;
            }
            return base.CanHitNPC(player, target);
        }

        public override bool CanHitPvp(Player player, Player target)
        {
            if(isCharging)
            {
                return false;
            }
            return base.CanHitPvp(player, target);
        }

        public override void UseAnimation(Player player)
        {
            if (isCharging)
            {
                isCharging = false;
                if (player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectileDirect(new EntitySource_ItemUse(player, Item), Main.MouseWorld, Vector2.Zero, ProjectileID.SandnadoFriendly, (int)Math.Round(Item.damage * chargeTicksScalingFactor * (float)chargeTicks), (2f * chargeTicksScalingFactor * (float)chargeTicks));
                }
            }
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
