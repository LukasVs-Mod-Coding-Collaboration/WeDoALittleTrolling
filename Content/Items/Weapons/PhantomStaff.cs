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
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Projectiles;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class PhantomStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
            ItemID.Sets.StaffMinionSlotsRequired[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;

            Item.consumable = false;

            Item.value = Item.sellPrice(platinum: 1);
            Item.maxStack = 1;

            Item.autoReuse = true;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item44;

            Item.buffType = ModContent.BuffType<PhantomStaffBuff>();
            Item.shoot = ModContent.ProjectileType<PhantomStaffProjectile>();

            Item.damage = 120;
            Item.mana = 24;
            Item.DamageType = DamageClass.Summon; //Item damage type
            Item.knockBack = 2f;

            Item.rare = ItemRarityID.Red;
        }

        public override bool CanUseItem(Player player)
        {
            if(player.maxMinions < ItemID.Sets.StaffMinionSlotsRequired[Type])
            {
                return false;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
            velocity.X = (Main.rand.NextFloat() - 0.5f);
            velocity.Y = (Main.rand.NextFloat() - 0.5f);
            velocity.Normalize();
            velocity *= PhantomStaffProjectile.idleMoveSpeed;
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 10);
            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            projectile.originalDamage = Item.damage;
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.LunarCraftingStation)
              .AddIngredient(ItemID.LunarBar, 16)
              .AddIngredient(ItemID.FragmentSolar, 4)
              .AddIngredient(ItemID.FragmentVortex, 4)
              .AddIngredient(ItemID.FragmentNebula, 4)
              .AddIngredient(ItemID.FragmentStardust, 4)
              .Register();
        }
    }
}
