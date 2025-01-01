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
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using Microsoft.CodeAnalysis;
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class Boulderspell : ModItem
    {
        public static UnifiedRandom rnd = new UnifiedRandom(); //Introduce random Values
        int damageModifier;

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;

            Item.consumable = false;

            Item.value = Item.sellPrice(gold: 3);
            Item.maxStack = 1;

            Item.autoReuse = false;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.mana = 50;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTurn = false;
            Item.UseSound = SoundID.Item69;

            Item.damage = 140;
            Item.DamageType = DamageClass.Default;
            Item.knockBack = 8f;

            Item.rare = ItemRarityID.Red;
            Item.scale = 1.0f;

            Item.shoot = ProjectileID.Boulder;
            Item.shootSpeed = 8f;
            Item.noMelee = true;
        }

        public override bool AltFunctionUse(Player player) // Boulder Rain
        {
            if (player.statMana > 300)
            {
                if (!Main.expertMode)
                {
                    damageModifier = 1;
                }
                if (Main.expertMode)
                {
                    damageModifier = 2;
                }
                if (Main.masterMode)
                {
                    damageModifier = 3;
                }

                while (player.statMana > 50)
                {
                    player.statMana -= 50;
                    Vector2 boulderRainRndLocation = new Vector2(Main.MouseWorld.X + rnd.Next(321) - 160, Main.MouseWorld.Y + rnd.Next(321) - 160);
                    Vector2 boulderRainMomentumDown= new Vector2(0f, 12.0f);
                    Projectile.NewProjectile(player.GetSource_FromThis(), boulderRainRndLocation, boulderRainMomentumDown, Item.shoot, Item.damage * damageModifier, Item.knockBack, player.whoAmI);
                    for (int j = 0; j < 24; j++)
                    {
                        Vector2 dustPosition = boulderRainRndLocation;
                        Vector2 dustVelocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                        dustVelocity = dustVelocity.SafeNormalize(Vector2.Zero);
                        dustVelocity *= 4f;
                        Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Stone, dustVelocity, 0, default);
                        newDust.noGravity = true;
                    }

                }
            }
            return false;
        }


        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (Main.expertMode)
            {
                damage *= 2;
            }
            if (Main.expertMode)
            {
                damage *= 1.5f;
            }
            base.ModifyWeaponDamage(player, ref damage);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 spawnpos = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y);
            Vector2 momentumdown = new Vector2(0f, 16.3f);
            if (!Main.expertMode)
            {
                damageModifier = 1;
            }
            if (Main.expertMode)
            {
                damageModifier = 2;
            }
            if (Main.masterMode)
            {
                damageModifier = 3;
            }

            position = spawnpos;
            velocity = momentumdown;
            Projectile.NewProjectile(source, position, velocity, Item.shoot, Item.damage * damageModifier, Item.knockBack, player.whoAmI);
            for (int k = 0; k < 20; k++)
            {
                Vector2 dustPosition = Main.MouseWorld;
                Vector2 dustVelocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                dustVelocity = dustVelocity.SafeNormalize(Vector2.Zero);
                dustVelocity *= 4f;
                Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Stone, dustVelocity, 0, default);
                newDust.noGravity = true;
            }
            return false;
        }

        public override void AddRecipes()
        {            
            CreateRecipe()
              .AddTile(TileID.CrystalBall)
              .AddIngredient(ItemID.SpellTome, 1)
              .AddIngredient(ItemID.Boulder, 9999)
              .AddIngredient(ItemID.SoulofLight, 10)
              .AddIngredient(ItemID.SoulofNight, 10)
              .AddIngredient(ItemID.LunarBar, 5)
              .Register();            
        }
    }
}
