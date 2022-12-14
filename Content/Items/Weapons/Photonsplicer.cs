/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022 LukasV-Coding

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
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class Photonsplicer : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Photonsplicer");
            Tooltip.SetDefault("Double blades, sharp enough to cut even a photon in half");
        }


        public override void SetDefaults()
        {
            Item.width = 200;
            Item.height = 200;

            Item.consumable = false;

            Item.value = Item.buyPrice(platinum: 1);
            Item.maxStack = 1;

            Item.autoReuse = true;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Thrust; // Temporary Replacement Attack Style Because I am Too lazy to code a real spear today, also this is a size test
            Item.UseSound = SoundID.Item1;



            Item.damage = 1200;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 8f;
            Item.crit = 16;

            Item.rare = ItemRarityID.Red;
            Item.scale= 1.2f;
        }


        /*
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(target.position.X, target.position.Y - 700), new Vector2(rnd.Next(-6, -3), 15f), ProjectileID.Typhoon, damage = 250, knockBack = 6f, Main.myPlayer);
            Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(target.position.X, target.position.Y - 640), new Vector2(rnd.Next(-3, 0), 15f), ProjectileID.Typhoon, damage = 250, knockBack = 6f, Main.myPlayer);
            Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(target.position.X, target.position.Y - 580), new Vector2(0, 30f), ProjectileID.Electrosphere, damage = 750, knockBack = 6f, Main.myPlayer);
            Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(target.position.X, target.position.Y - 640), new Vector2(rnd.Next(0, 3), 15f), ProjectileID.Typhoon, damage = 250, knockBack = 6f, Main.myPlayer);
            Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(target.position.X, target.position.Y - 700), new Vector2(rnd.Next(3, 6), 15f), ProjectileID.Typhoon, damage = 250, knockBack = 6f, Main.myPlayer);
        }
        */

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.Nanites, 100)
              .AddIngredient(ItemID.ChlorophyteBar, 15)
              .AddIngredient(ItemID.MushroomSpear, 1)
              .AddIngredient(ItemID.Ectoplasm, 5 )
              .AddIngredient(ItemID.FragmentStardust, 15)
              .AddIngredient(ItemID.FragmentVortex, 5)
              .Register();
        }
    }
}
