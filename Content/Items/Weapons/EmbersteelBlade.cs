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
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using System;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Content.Items.Placeable;
using WeDoALittleTrolling.Common.ModSystems;
using System.Linq;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class EmbersteelBlade : ModItem
    {
        public const int maxCharges = 4;
        public static UnifiedRandom rnd = new UnifiedRandom();
        public int charges = 0;
        public bool isExploding = false;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 52;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 25);
            Item.maxStack = 1;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.scale = 2.5f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = false;
            Item.UseSound = SoundID.Item1;

            Item.damage = 75;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 8f;

            Item.rare = ItemRarityID.Orange;
        }

        public override bool AltFunctionUse(Player player) // Woohoo Tornado Launch! (not yet implemented)
        {
            if (charges >= maxCharges && !isExploding)
            {
                Projectile.NewProjectileDirect
                (
                    new EntitySource_ItemUse(player, Item),
                    player.Center,
                    Vector2.Zero,
                    ModContent.ProjectileType<EmbersteelExplosion>(),
                    Item.damage,
                    Item.knockBack
                );
                charges = 60;
                isExploding = true;
            }
            return false;
        }

        public override void UpdateInventory(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (isExploding && charges > 0)
                {
                    if (charges == 54)
                    {
                        Vector2 pos = new Vector2(player.Center.X + 45f, player.Center.Y);
                        for (int i = 0; i < 8; i++)
                        {
                            Projectile.NewProjectileDirect
                            (
                                new EntitySource_ItemUse(player, Item),
                                pos.RotatedBy((double)i * Math.PI / 4.0, player.Center),
                                Vector2.Zero,
                                ModContent.ProjectileType<EmbersteelExplosion>(),
                                Item.damage,
                                Item.knockBack
                            );
                        }
                    }
                    if (charges == 48)
                    {
                        Vector2 pos = new Vector2(player.Center.X + 90f, player.Center.Y);
                        for (int i = 0; i < 8; i++)
                        {
                            Projectile.NewProjectileDirect
                            (
                                new EntitySource_ItemUse(player, Item),
                                pos.RotatedBy((double)i * Math.PI / 4.0, player.Center),
                                Vector2.Zero,
                                ModContent.ProjectileType<EmbersteelExplosion>(),
                                Item.damage,
                                Item.knockBack
                            );
                        }
                    }
                    if (charges == 42)
                    {
                        Vector2 pos = new Vector2(player.Center.X + 135f, player.Center.Y);
                        for (int i = 0; i < 8; i++)
                        {
                            Projectile.NewProjectileDirect
                            (
                                new EntitySource_ItemUse(player, Item),
                                pos.RotatedBy((double)i * Math.PI / 4.0, player.Center),
                                Vector2.Zero,
                                ModContent.ProjectileType<EmbersteelExplosion>(),
                                Item.damage,
                                Item.knockBack
                            );
                        }
                    }
                    if (charges == 36)
                    {
                        Vector2 pos = new Vector2(player.Center.X + 180f, player.Center.Y);
                        for (int i = 0; i < 16; i++)
                        {
                            Projectile.NewProjectileDirect
                            (
                                new EntitySource_ItemUse(player, Item),
                                pos.RotatedBy((double)i * Math.PI / 8.0, player.Center),
                                Vector2.Zero,
                                ModContent.ProjectileType<EmbersteelExplosion>(),
                                Item.damage,
                                Item.knockBack
                            );
                        }
                    }
                    if (charges == 30)
                    {
                        Vector2 pos = new Vector2(player.Center.X + 225f, player.Center.Y);
                        for (int i = 0; i < 16; i++)
                        {
                            Projectile.NewProjectileDirect
                            (
                                new EntitySource_ItemUse(player, Item),
                                pos.RotatedBy((double)i * Math.PI / 8.0, player.Center),
                                Vector2.Zero,
                                ModContent.ProjectileType<EmbersteelExplosion>(),
                                Item.damage,
                                Item.knockBack
                            );
                        }
                    }
                    if (charges == 24)
                    {
                        Vector2 pos = new Vector2(player.Center.X + 270f, player.Center.Y);
                        for (int i = 0; i < 16; i++)
                        {
                            Projectile.NewProjectileDirect
                            (
                                new EntitySource_ItemUse(player, Item),
                                pos.RotatedBy((double)i * Math.PI / 8.0, player.Center),
                                Vector2.Zero,
                                ModContent.ProjectileType<EmbersteelExplosion>(),
                                Item.damage,
                                Item.knockBack
                            );
                        }
                    }
                    if (charges == 18)
                    {
                        Vector2 pos = new Vector2(player.Center.X + 315f, player.Center.Y);
                        for (int i = 0; i < 32; i++)
                        {
                            Projectile.NewProjectileDirect
                            (
                                new EntitySource_ItemUse(player, Item),
                                pos.RotatedBy((double)i * Math.PI / 16.0, player.Center),
                                Vector2.Zero,
                                ModContent.ProjectileType<EmbersteelExplosion>(),
                                Item.damage,
                                Item.knockBack
                            );
                        }
                    }
                    if (charges == 12)
                    {
                        Vector2 pos = new Vector2(player.Center.X + 360f, player.Center.Y);
                        for (int i = 0; i < 32; i++)
                        {
                            Projectile.NewProjectileDirect
                            (
                                new EntitySource_ItemUse(player, Item),
                                pos.RotatedBy((double)i * Math.PI / 16.0, player.Center),
                                Vector2.Zero,
                                ModContent.ProjectileType<EmbersteelExplosion>(),
                                Item.damage,
                                Item.knockBack
                            );
                        }
                    }
                    if (charges == 6)
                    {
                        Vector2 pos = new Vector2(player.Center.X + 405f, player.Center.Y);
                        for (int i = 0; i < 32; i++)
                        {
                            Projectile.NewProjectileDirect
                            (
                                new EntitySource_ItemUse(player, Item),
                                pos.RotatedBy((double)i * Math.PI / 16.0, player.Center),
                                Vector2.Zero,
                                ModContent.ProjectileType<EmbersteelExplosion>(),
                                Item.damage,
                                Item.knockBack
                            );
                        }
                    }
                    charges--;
                }
                if (isExploding && charges <= 0)
                {
                    isExploding = false;
                    charges = 0;
                }
            }
            base.UpdateInventory(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (charges < maxCharges && !isExploding && (!target.active || target.CanBeChasedBy()))
            {
                charges++;
                if (target.boss || WDALTImmunitySystem.BossNPCIDWhitelist.Contains(target.type))
                {
                    charges++;
                }
                if (charges >= maxCharges)
                {
                    for (int i = 0; i < 120; i++)
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
                        dustVelocity = dustVelocity.SafeNormalize(Vector2.Zero);
                        dustVelocity *= 15f;
                        Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Lava, dustVelocity, 0, default);
                        newDust.noGravity = true;
                    }
                    SoundEngine.PlaySound(SoundID.Item74, player.Center);
                }
            }
            base.OnHitNPC(player, target, hit, damageDone);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.TitaniumSword, 1)
              .AddIngredient(ModContent.ItemType<EmbersteelBarItem>(), 12)
              .Register();

            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.AdamantiteSword, 1)
              .AddIngredient(ModContent.ItemType<EmbersteelBarItem>(), 12)
              .Register();
        }
    }
}
