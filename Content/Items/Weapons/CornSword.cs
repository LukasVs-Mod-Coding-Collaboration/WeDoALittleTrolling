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
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Content.Projectiles;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    public class CornSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<ThrownCorncob>();
            Item.shootSpeed = 10f;
            Item.shootsEveryUse = true;
            Item.useTime = 20;
            Item.useAnimation = 20;
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
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Corncob>(), 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
