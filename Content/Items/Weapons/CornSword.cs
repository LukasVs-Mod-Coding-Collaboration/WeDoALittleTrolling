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

using System;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Content.Projectiles;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    public class CornSword : ModItem
    {
        public static UnifiedRandom rnd = new UnifiedRandom();
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.knockBack = 5.25f;
            Item.scale = 1.25f;
            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 2);
            Item.maxStack = 1;
            Item.useTurn = true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int kernelCount = 3;
            for (int i = 0; i < kernelCount; i++)
            {
                Vector2 kernelVelocity = new Vector2((rnd.NextFloat() - 0.5f), rnd.NextFloat());
                kernelVelocity = kernelVelocity.SafeNormalize(Vector2.Zero);
                kernelVelocity.X *= 3 + rnd.Next(0, 3);
                kernelVelocity.Y *= -5 + rnd.Next(-2, 1);
                Projectile.NewProjectileDirect(player.GetSource_OnHit(target), target.Center, kernelVelocity, ModContent.ProjectileType<Kernel>(), (int)(damageDone * 0.2f), 4f, player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.ShimmerMonolith)
                .AddIngredient(ItemID.WoodenSword)
                .AddIngredient(ItemID.Sunflower, 5)
                .AddIngredient(ItemID.Pumpkin, 10)
                .Register();
        }
    }
}
