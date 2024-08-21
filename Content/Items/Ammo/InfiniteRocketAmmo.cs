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
using Terraria.ModLoader;
using Terraria.ID;

namespace WeDoALittleTrolling.Content.Items.Ammo
{
    internal class InfiniteRocketAmmo : ModItem
    {

        public override void SetStaticDefaults()
        {
            AmmoID.Sets.IsSpecialist[Type] = true;

            AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.RocketLauncher].Add(Type, ProjectileID.RocketI);
            AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.ProximityMineLauncher].Add(Type, ProjectileID.ProximityMineI);
            AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.GrenadeLauncher].Add(Type, ProjectileID.GrenadeI);
            AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.SnowmanCannon].Add(Type, ProjectileID.RocketSnowmanI);
            AmmoID.Sets.SpecificLauncherAmmoProjectileMatches[ItemID.Celeb2].Add(Type, ProjectileID.Celeb2Rocket);
        }

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

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.CrystalBall)
              .AddIngredient(ItemID.RocketI, 3000)
              .AddIngredient(ItemID.Ectoplasm, 15)
              .Register();
        }

        //Define allowed weapons - deprecated - replaced by new Specialist API in SetStaticDefaults()
        /*
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
        */

        //Set allowed weapons - deprecated - replaced by new Specialist API in SetStaticDefaults()
        /*
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
        */
        
        //Handle ammo conversions - deprecated - replaced by new Specialist API in SetStaticDefaults()
        /*
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
        */
    }
}
