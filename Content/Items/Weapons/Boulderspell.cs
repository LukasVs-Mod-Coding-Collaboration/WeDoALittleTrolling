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
        }

        public override bool AltFunctionUse(Player player) // Boulder Rain
        {
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
            Vector2 testspawnpos = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y);
            //this works

            Vector2 testmomentumdown = new Vector2(0f, 16.3f);
            //This does not work, the boulder just explodes.

            position = testspawnpos;
            velocity = testmomentumdown;
            Projectile.NewProjectile(source, position, velocity, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.CrystalBall)
              .AddIngredient(ItemID.SpellTome, 1)
              .AddIngredient(ItemID.Boulder, 50)
              .AddIngredient(ItemID.SoulofLight, 10)
              .AddIngredient(ItemID.SoulofNight, 10)
              .Register();
        }
    }
}
