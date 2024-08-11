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
using Microsoft.CodeAnalysis;
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class HallowedDisintegrationBlade : ModItem
    {
        public static UnifiedRandom rnd = new UnifiedRandom(); //Introduce random Values
        public long lastDashTick;

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.consumable = false;

            Item.value = Item.sellPrice(gold: 8);
            Item.maxStack = 1;

            Item.autoReuse = true;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item1;

            Item.damage = 75;
            Item.DamageType = DamageClass.Melee; //Item damage type
            Item.knockBack = 5f;
            Item.scale = 1.5f;

            Item.rare = ItemRarityID.LightPurple;
        }

        public override bool AltFunctionUse(Player player) // Woohoo invincibility charge!
        {
            
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(Math.Abs(player.GetModPlayer<WDALTPlayer>().currentTick - lastDashTick) >= (player.itemAnimationMax * 2))
            {
                player.SetImmuneTimeForAllTypes(player.longInvince ? ((int)Math.Round(player.itemAnimationMax * 1.0)) : ((int)Math.Round(player.itemAnimationMax * 0.75)));
                lastDashTick = player.GetModPlayer<WDALTPlayer>().currentTick;
            }
            for (int i = 0; i < 5; i++)
            {
                int rMax = (int)player.width;
                double r = rMax * Math.Sqrt(rnd.NextDouble());
                double angle = rnd.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 projPosition = player.Center;
                projPosition.X += xOffset;
                projPosition.Y += yOffset;
                Vector2 projVelocity = new Vector2((rnd.NextFloat() - 0.5f), (rnd.NextFloat() - 0.5f));
                projVelocity = projVelocity.SafeNormalize(Vector2.Zero);
                projVelocity *= 6f;
                int dmg = (int)Math.Round(Item.damage * 0.5);
                Projectile.NewProjectileDirect(player.GetSource_OnHit(target), projPosition, projVelocity, ProjectileID.CrystalStorm, dmg, Item.knockBack, player.whoAmI);
            }
            base.OnHitNPC(player, target, hit, damageDone);    
        }


        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.HallowedBar, 4)
              .AddIngredient(ItemID.OrichalcumBar, 12)
              .AddIngredient(ItemID.SoulofLight, 8)
              .AddIngredient(ItemID.CrystalShard, 8)
              .Register();
        }
    }
}
