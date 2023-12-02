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
using WeDoALittleTrolling.Content.Items.Material;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class ElementalStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;

            Item.consumable = false;

            Item.value = Item.sellPrice(gold: 20);
            Item.maxStack = 1;

            Item.autoReuse = true;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item44;
            Item.scale = 1f;

            Item.buffType = ModContent.BuffType<ElementalStaffBuff>();
            Item.shoot = ModContent.ProjectileType<ElementalStaffProjectile>();

            Item.damage = 36;
            Item.mana = 12;
            Item.ArmorPenetration = 12;
            Item.DamageType = DamageClass.Summon; //Item damage type
            Item.knockBack = 2f;

            Item.rare = ItemRarityID.LightPurple;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.maxMinions < ItemID.Sets.StaffMinionSlotsRequired[Type])
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
            velocity *= ElementalStaffProjectile.idleMoveSpeed;
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 3600, true);
            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            projectile.originalDamage = Item.damage;
            projectile.netUpdate = true;
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.ShimmerMonolith)
              .AddIngredient(ModContent.ItemType<FrozenFossil>(), 8)
              .AddIngredient(ItemID.HallowedBar, 8)
              .Register();
        }
    }
}
