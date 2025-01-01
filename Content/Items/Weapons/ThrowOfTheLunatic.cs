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
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Projectiles;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    public class ThrowOfTheLunatic : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 21;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;

            Item.damage = 115;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 5f;
            Item.channel = true;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(gold: 10);

            Item.shoot = ModContent.ProjectileType<ThrowOfTheLunaticProjectile>();
            Item.shootSpeed = 16f;       
        }
    }
}
