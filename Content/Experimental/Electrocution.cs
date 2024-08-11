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
/*

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;

namespace WeDoALittleTrolling.Content.Experimental
{
    internal class Electrocution : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 14;
            Item.scale = 1;
            Item.rare = ItemRarityID.Expert;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item92;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 1;
            Item.knockBack = 0f;
            Item.noMelee = true;
            Item.crit = 0;
            Item.shoot = ProjectileID.Electrosphere;
            Item.shootSpeed = 1.0f;
            Item.autoReuse = true;


        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 ShootDirection = new Vector2(Main.MouseWorld.X - player.position.X, Main.MouseWorld.Y - player.position.Y);
            ShootDirection = ShootDirection.SafeNormalize(Vector2.Zero);
            ShootDirection *= 3;

            Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(player.position.X, player.position.Y), ShootDirection, ProjectileID.Electrosphere, damage = 100, knockback = 6f, Main.myPlayer);
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1.0f, 1.0f);
        }

        //We really don't need a holdout offset right now.

        //public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ProjectileID.Electrosphere] <= 0;
        //Would technically limit to one projectile once we have a projectile with custom AI that always moves towards the cursor

    }
}
*/