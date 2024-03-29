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
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Common.ModSystems;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using System.Collections.Generic;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class EmberRay : ModItem
    {
        public static UnifiedRandom rnd = new UnifiedRandom();
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 66;
            Item.rare = ItemRarityID.Expert;
            Item.value = Item.sellPrice(gold: 8);

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 35;
            Item.knockBack = 1.25f;
            Item.noMelee = true;
            Item.crit = 0;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 1.5f;
            Item.useAmmo = AmmoID.Arrow;
            Item.autoReuse = true;

        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<EmberBolt>();
            }
        }

        //Do something with Blazing Fuel Stacks here later
        /*
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if(attackMode == 1)
            {
                damage *= 1.5f;
            }
            base.ModifyWeaponDamage(player, ref damage);
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            if(attackMode == 1)
            {
                crit *= 1.25f;
            }
            base.ModifyWeaponCrit(player, ref crit);
        }
        */

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.DemonAltar)
              .AddIngredient(ItemID.HellwingBow)
              .AddIngredient(ItemID.MoltenFury)
              .AddIngredient(ItemID.BeesKnees)
              .AddIngredient(ItemID.BloodRainBow)
              .AddIngredient(ItemID.DemonBow)
              .Register();

            CreateRecipe()
             .AddTile(TileID.DemonAltar)
             .AddIngredient(ItemID.HellwingBow)
             .AddIngredient(ItemID.MoltenFury)
             .AddIngredient(ItemID.BeesKnees)
             .AddIngredient(ItemID.BloodRainBow)
             .AddIngredient(ItemID.TendonBow)
             .Register();

        }
        

    }
}