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

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Common.ModSystems;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class Rain_of_Decay : ModItem
    {
        
        public int attackMode = 0;
        public static UnifiedRandom rnd = new UnifiedRandom();
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 142;
            Item.rare = ItemRarityID.Expert;
            Item.value = Item.sellPrice(gold: 8);

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.ArmorPenetration = 36;
            Item.damage = 36;
            Item.knockBack = 1.25f;
            Item.noMelee = true;
            Item.crit = 0;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 13.0f;
            Item.useAmmo = AmmoID.Arrow;
            Item.autoReuse = true;

        }
        
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ProjectileID.CursedArrow;
            }
        }

        public override float UseSpeedMultiplier(Player player)
        {
            if(attackMode == 1)
            {
                return 2f;
            }
            return base.UseSpeedMultiplier(player);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if(attackMode == 1)
            {
                if (rnd.Next(0, 2) == 0)
                {
                    return false;
                }
            }
            return base.CanConsumeAmmo(ammo, player);
        }

        public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
        {
            if(attackMode == 1)
            {
                knockback *= 5f;
            }
            base.ModifyWeaponKnockback(player, ref knockback);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if(attackMode == 1)
            {
                damage *= 1.5f;
            }
            base.ModifyWeaponDamage(player, ref damage);
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            if(attackMode == 1)
            {
                crit *= 1.25f;
            }
            base.ModifyWeaponCrit(player, ref crit);
        }

        
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(attackMode != 1)
            {
                float numberProjectiles = 4;
                float rotation = MathHelper.ToRadians(5);
                position += Vector2.Normalize(velocity) * 5f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 burstArrowSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles)));
                    Projectile.NewProjectileDirect(source, position, burstArrowSpeed, type, damage, knockback, player.whoAmI);
                }
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket soundPlayRainOfDecayPacket = Mod.GetPacket();
                    soundPlayRainOfDecayPacket.Write(WDALTPacketTypeID.soundPlayRainOfDecay);
                    soundPlayRainOfDecayPacket.WriteVector2(player.position);
                    soundPlayRainOfDecayPacket.Send();
                }
                else if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    SoundEngine.PlaySound(SoundID.Item5, player.position);
                }
                return false;
            }
            else
            {
                Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket soundPlayRainOfDecayPacket = Mod.GetPacket();
                    soundPlayRainOfDecayPacket.Write(WDALTPacketTypeID.soundPlayRainOfDecay);
                    soundPlayRainOfDecayPacket.WriteVector2(player.position);
                    soundPlayRainOfDecayPacket.Send();
                }
                else if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    SoundEngine.PlaySound(SoundID.Item5, player.position);
                }
                return false;
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            if(attackMode == 0)
            {
                attackMode = 1;
                SoundEngine.PlaySound(SoundID.Item60, player.position);
            }
            else
            {
                attackMode = 0;
                SoundEngine.PlaySound(SoundID.Item74, player.position);
            }
            return false;
        }

        
        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.HeavyWorkBench)
              .AddIngredient(ItemID.DaedalusStormbow)
              .AddIngredient(ItemID.Marrow)
              .AddIngredient(ItemID.CursedFlame, 25)
              .AddIngredient(ItemID.AshWood, 20)
              .AddRecipeGroup(RecipeGroupID.IronBar, 15)
              .AddIngredient(ItemID.SoulofNight, 10)
              .AddIngredient(ItemID.SoulofSight, 5)
              .AddIngredient(ItemID.JungleSpores, 5)
              .AddIngredient(ItemID.WhiteString, 1)
              .Register();

            CreateRecipe()
             .AddTile(TileID.HeavyWorkBench)
             .AddIngredient(ItemID.DaedalusStormbow)
             .AddIngredient(ItemID.Marrow)
             .AddIngredient(ItemID.Ichor, 25)
             .AddIngredient(ItemID.AshWood, 20)
             .AddRecipeGroup(RecipeGroupID.IronBar, 15)
             .AddIngredient(ItemID.SoulofNight, 10)
             .AddIngredient(ItemID.SoulofSight, 5)
             .AddIngredient(ItemID.JungleSpores, 5)
             .AddIngredient(ItemID.WhiteString, 1)
             .Register();

        }
        

    }
}