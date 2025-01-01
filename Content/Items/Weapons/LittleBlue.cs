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
using System;
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class LittleBlue : ModItem
    {
        public static UnifiedRandom rnd = new UnifiedRandom(); //Introduce random Values
        public long lastDashTick;

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 60;

            Item.consumable = false;

            Item.value = Item.sellPrice(platinum: 1);
            Item.maxStack = 1;

            Item.autoReuse = true;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item1;

            Item.damage = 600;
            Item.DamageType = DamageClass.Melee; //Item damage type
            Item.knockBack = 8f;
            Item.crit = 16;

            Item.rare = ItemRarityID.Red;
            Item.scale = 1.0f;
        }

        public override bool AltFunctionUse(Player player) // Woohoo invincibility charge!
        {
            if(Math.Abs(player.GetModPlayer<WDALTPlayer>().currentTick - lastDashTick) >= 120)
            {
                player.SetImmuneTimeForAllTypes(player.longInvince ? 90 : 60);
                Vector2 chargeDirection = new Vector2(Main.MouseWorld.X - player.position.X, Main.MouseWorld.Y - player.position.Y);
                chargeDirection = chargeDirection.SafeNormalize(Vector2.Zero);
                chargeDirection *= 24f;
                player.velocity = chargeDirection;
                player.GetModPlayer<WDALTPlayer>().chargeAccelerationTicks += 25;
                for (int i = 0; i < 60; i++)
                {
                    int rMax = (int)player.width;
                    double r = rMax * Math.Sqrt(rnd.NextDouble());
                    double angle = rnd.NextDouble() * 2 * Math.PI;
                    int xOffset = (int)Math.Round(r * Math.Cos(angle));
                    int yOffset = (int)Math.Round(r * Math.Sin(angle));
                    Vector2 dustPosition = player.Center;
                    dustPosition.X += xOffset;
                    dustPosition.Y += yOffset;
                    Vector2 dustVelocity = new Vector2((rnd.NextFloat() - 0.5f), (rnd.NextFloat() - 0.5f));
                    dustVelocity = dustVelocity.SafeNormalize(Vector2.Zero);
                    dustVelocity *= 8f;
                    Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Electric, dustVelocity, 0, default);
                    newDust.noGravity = true;
                }
                SoundEngine.PlaySound(SoundID.Item117, player.Center);
                lastDashTick = player.GetModPlayer<WDALTPlayer>().currentTick;
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int littleBlueProjectileCount = 5;
            int damage = damageDone;
            float knockBack = hit.Knockback;
            for (int i = 0; i < littleBlueProjectileCount; i++)
            {
                Vector2 littleBlueBulletSpawnPos = new Vector2(target.position.X + rnd.Next(-240, 240), target.position.Y + rnd.Next(-240, 240));
                Vector2 littleBlueBulletVelocity = new Vector2(target.Center.X - littleBlueBulletSpawnPos.X, target.Center.Y - littleBlueBulletSpawnPos.Y);
                littleBlueBulletVelocity = littleBlueBulletVelocity.SafeNormalize(Vector2.Zero);
                littleBlueBulletVelocity *= 6f;
                Projectile.NewProjectile(player.GetSource_FromThis(), littleBlueBulletSpawnPos, littleBlueBulletVelocity, ProjectileID.MagicMissile, (int)Math.Round(damageDone * 0.75), knockBack = 6f, player.whoAmI);
                for (int j = 0; j < 20; j++)
                {
                    Vector2 dustPosition = littleBlueBulletSpawnPos;
                    Vector2 dustVelocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                    dustVelocity = dustVelocity.SafeNormalize(Vector2.Zero);
                    dustVelocity *= 4f;
                    Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.HallowSpray, dustVelocity, 0, default);
                    newDust.noGravity = true;
                }
            }

            /*
            Projectile.NewProjectileDirect(player.GetSource_FromThis(), new Vector2(target.position.X, target.position.Y - 700), new Vector2(rnd.Next(-6, -3), 15f), ProjectileID.Typhoon, damage = 625, knockBack = 6f, player.whoAmI);
            Projectile.NewProjectileDirect(player.GetSource_FromThis(), new Vector2(target.position.X, target.position.Y - 640), new Vector2(rnd.Next(-3, 0), 15f), ProjectileID.Typhoon, damage = 625, knockBack = 6f, player.whoAmI);
            Projectile.NewProjectileDirect(player.GetSource_FromThis(), new Vector2(target.position.X, target.position.Y - 580), new Vector2(0, 30f), ProjectileID.Electrosphere, damage = 1875, knockBack = 6f, player.whoAmI);
            Projectile.NewProjectileDirect(player.GetSource_FromThis(), new Vector2(target.position.X, target.position.Y - 640), new Vector2(rnd.Next(0, 3), 15f), ProjectileID.Typhoon, damage = 625, knockBack = 6f, player.whoAmI);
            Projectile.NewProjectileDirect(player.GetSource_FromThis(), new Vector2(target.position.X, target.position.Y - 700), new Vector2(rnd.Next(3, 6), 15f), ProjectileID.Typhoon, damage = 625, knockBack = 6f, player.whoAmI);
            */
            base.OnHitNPC(player, target, hit, damageDone);    
        } //Projectile spawn location calculator, summons projectiles on hit


        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.LunarCraftingStation)
              .AddIngredient(ItemID.CobaltSword, 1)
              .AddIngredient(ItemID.MythrilSword,1)
              .AddIngredient(ItemID.Diamond, 10)
              .AddIngredient(ItemID.Sapphire, 10)
              .AddIngredient(ItemID.Emerald, 10)
              .AddIngredient(ItemID.LunarBar, 5)
              .AddIngredient(ItemID.FragmentStardust, 15)
              .AddIngredient(ItemID.FragmentVortex, 5)
              .Register();
        }
    }
}
