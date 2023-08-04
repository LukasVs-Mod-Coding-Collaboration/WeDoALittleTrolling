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
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using System.Linq;
using Terraria.Utilities;

namespace WeDoALittleTrolling.Content.Items
{
    internal class InfiniteRocketAmmo : ModItem
    {

        public static UnifiedRandom FireworksColor = new UnifiedRandom();
        public static readonly int[] CompatibleWeaponIDs = 
        {
            ItemID.Celeb2,
            ItemID.SnowmanCannon,
            ItemID.FireworksLauncher,
            ItemID.ElectrosphereLauncher,
            ItemID.GrenadeLauncher,
            ItemID.ProximityMineLauncher,
            ItemID.RocketLauncher
        };

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 50;

            Item.value = Item.buyPrice(copper: 1);
            Item.maxStack = 1;

            Item.damage = 40;
            Item.knockBack = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ItemRarityID.Red;
            Item.ammo = AmmoID.Rocket;
            Item.shoot = ProjectileID.RocketI;
        }

        //Set allowed weapons
        public override bool? CanBeChosenAsAmmo(Item weapon, Player player)
        {
            if(CompatibleWeaponIDs.Contains(weapon.type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        //Handle ammo conversions
        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
        {
            switch(weapon.type)
            {
                case ItemID.Celeb2:
                    type = ProjectileID.Celeb2Rocket;
                    break;
                case ItemID.SnowmanCannon:
                    type = ProjectileID.RocketSnowmanI;
                    break;
                case ItemID.FireworksLauncher:
                    switch(FireworksColor.Next(0, 4))
                    {
                        case 0:
                            type = ProjectileID.RocketFireworkBlue;
                            break;
                        case 1:
                            type = ProjectileID.RocketFireworkGreen;
                            break;
                        case 2:
                            type = ProjectileID.RocketFireworkRed;
                            break;
                        case 3:
                            type = ProjectileID.RocketFireworkYellow;
                            break;
                        default:
                            type = ProjectileID.RocketFireworkBlue;
                            break;
                    }
                    break;
                case ItemID.ElectrosphereLauncher:
                    type = ProjectileID.ElectrosphereMissile;
                    break;
                case ItemID.GrenadeLauncher:
                    type = ProjectileID.GrenadeI;
                    break;
                case ItemID.ProximityMineLauncher:
                    type = ProjectileID.ProximityMineI;
                    break;
                case ItemID.RocketLauncher:
                    type = ProjectileID.RocketI;
                    break;
                default:
                    break;
            }
            base.PickAmmo(weapon, player, ref type, ref speed, ref damage, ref knockback);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.CrystalBall)
              .AddIngredient(ItemID.RocketI, 3000)
              .AddIngredient(ItemID.Ectoplasm, 15)
              .Register();
        }
    }
}
