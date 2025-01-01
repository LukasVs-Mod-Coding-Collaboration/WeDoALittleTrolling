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
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class GloriousDemocracy : ModItem
    {
        
        public int attackMode = 0;
        public static UnifiedRandom rnd = new UnifiedRandom();
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 7));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 72;
            Item.height = 28;
            Item.rare = ItemRarityID.Expert;
            Item.value = Item.sellPrice(gold: 8);

            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.UseSound = SoundID.Item40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.ArmorPenetration = 999;
            Item.damage = 1;
            Item.knockBack = 8.0f;
            Item.noMelee = true;
            Item.crit = 6;
            Item.shoot = ModContent.ProjectileType<FreedomRound>();
            Item.shootSpeed = 13.0f;
            Item.autoReuse = true;

        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 shootDirection = velocity;
            shootDirection = shootDirection.SafeNormalize(Vector2.Zero);
            float xOffset = shootDirection.X * 32.0f;
            float yOffset = shootDirection.Y * 32.0f;
            position = new Vector2(position.X + xOffset, position.Y + yOffset - 8.0f);
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14.0f, -4.0f);
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.ShimmerMonolith)
                .AddIngredient(ItemID.Musket)
                .AddIngredient(ItemID.Ruby, 5)
                .AddIngredient(ItemID.Amber, 5)
                .AddIngredient(ItemID.Topaz, 5)
                .AddIngredient(ItemID.Emerald, 5)
                .AddIngredient(ItemID.Diamond, 5)
                .AddIngredient(ItemID.Sapphire, 5)
                .AddIngredient(ItemID.Amethyst, 5)
                .Register();

            CreateRecipe()
               .AddTile(TileID.ShimmerMonolith)
               .AddIngredient(ItemID.TheUndertaker)
               .AddIngredient(ItemID.Ruby, 5)
               .AddIngredient(ItemID.Amber, 5)
               .AddIngredient(ItemID.Topaz, 5)
               .AddIngredient(ItemID.Emerald, 5)
               .AddIngredient(ItemID.Diamond, 5)
               .AddIngredient(ItemID.Sapphire, 5)
               .AddIngredient(ItemID.Amethyst, 5)
               .Register();
        }
    }
}