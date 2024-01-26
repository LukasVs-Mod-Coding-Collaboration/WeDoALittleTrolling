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

using WeDoALittleTrolling.Content.Projectiles;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    public class Photonsplicer : ModItem
    {
        public override void SetStaticDefaults() {
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true;
            ItemID.Sets.Spears[Item.type] = true;
        }

        public override void SetDefaults() {
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(gold: 5);

            Item.width = 200;
            Item.height = 200;
            Item.scale = (float)1.0;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.UseSound = SoundID.Item45;
            Item.autoReuse = true;

            Item.damage = 80;
            Item.knockBack = (float)10.0;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;

            Item.shootSpeed = (float)3.7;
            Item.shoot = ModContent.ProjectileType<PhotonsplicerProjectile>();
        }

        public override bool CanUseItem(Player player) {
            return player.ownedProjectileCounts[Item.shoot] < 1;
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
