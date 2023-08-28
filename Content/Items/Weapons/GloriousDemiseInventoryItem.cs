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

using WeDoALittleTrolling.Content.Projectiles;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    public class GloriousDemiseInventoryItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true;
            ItemID.Sets.Spears[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(gold: 5);

            Item.width = 66;
            Item.height = 66;
            Item.scale = 1f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.UseSound = SoundID.Item45;
            Item.autoReuse = true;

            Item.damage = 60;
            Item.knockBack = 10f;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;

            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<GloriousDemiseProjectile>();
        }

        public override bool? UseItem(Player player)
		{
            SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            return null;
        }

        /*
        public override void AddRecipes() {
            CreateRecipe()
                .Register();
        }
        */
    }
}
