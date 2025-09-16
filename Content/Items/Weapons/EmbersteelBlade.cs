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
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class EmbersteelBlade : ModItem
    {
        public const int maxCharges = 5;
        public static UnifiedRandom rnd = new UnifiedRandom();

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

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;

            Item.damage = 75;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 8f;
            Item.shoot = ModContent.ProjectileType<EmbersteelShockwave>();
            Item.noMelee = true;
            Item.shootsEveryUse = true;
            Item.autoReuse = true;

            Item.rare = ItemRarityID.LightPurple;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI);

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override bool AltFunctionUse(Player player) // Woohoo Tornado Launch! (not yet implemented)
        {
            WDALTPlayer p = player.GetModPlayer<WDALTPlayer>();
            if (p.embersteelBladeCharges >= maxCharges && !p.isEmbersteelExplosionActive)
            {
                p.embersteelBladeCharges = 0;
                p.embersteelExplosionSourceItem = Item;
                p.embersteelExplosionDamage = Item.damage;
                p.embersteelExplosionKnockback = Item.knockBack;
                p.embersteelExplosionTicks = 60;
                p.isEmbersteelExplosionActive = true;
            }
            return false;
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
