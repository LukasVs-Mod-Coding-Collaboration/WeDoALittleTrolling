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

using WeDoALittleTrolling.Content.Projectiles;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Prefixes;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    public class GloriousDemiseInventoryItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true;
            ItemID.Sets.Spears[Item.type] = true;
            PrefixLegacy.ItemSets.SpearsMacesChainsawsDrillsPunchCannon[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(gold: 5);

            Item.width = 50;
            Item.height = 50;
            Item.scale = 1f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.UseSound = SoundID.Item45;
            Item.autoReuse = true;

            Item.damage = 4;
            Item.knockBack = 10f;
            Item.ArmorPenetration = 333;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Generic;
            Item.noMelee = true;

            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<GloriousDemiseProjectile>();
        }

        public override bool? UseItem(Player player)
		{
            SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            return null;
        }

        public override float UseSpeedMultiplier(Player player)
        {
            return (1f / player.GetAttackSpeed(DamageClass.Generic));
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            int additionalDamage = 0;
            if (NPC.downedSlimeKing)
            {
                additionalDamage += 1; // 3
            }
            if (NPC.downedBoss1)
            {
                additionalDamage += 1; // 4
            }
            if (NPC.downedBoss2)
            {
                additionalDamage += 2; // 6
            }
            if (NPC.downedQueenBee)
            {
                additionalDamage += 1; // 7
            }
            if (NPC.downedDeerclops)
            {
                additionalDamage += 1; // 8
            }
            if (NPC.downedBoss3)
            {
                additionalDamage += 2; // 10
            }
            if (player.downedDD2EventAnyDifficulty)
            {
                additionalDamage += 1; // 11
            }
            if (Main.hardMode)
            {
                additionalDamage += 2; // 13
            }
            if (NPC.downedMechBoss1)
            {
                additionalDamage += 1; // 15
            }
            if (NPC.downedMechBoss2)
            {
                additionalDamage += 2; // 17
            }
            if (NPC.downedMechBoss3)
            {
                additionalDamage += 2; // 19
            }
            if (NPC.downedQueenSlime)
            {
                additionalDamage += 1; // 20
            }
            if (NPC.downedPlantBoss)
            {
                additionalDamage += 3; // 23
            }
            if (NPC.downedHalloweenKing)
            {
                additionalDamage += 1; // 24
            }
            if (NPC.downedHalloweenTree)
            {
                additionalDamage += 1; // 25
            }
            if (NPC.downedChristmasIceQueen)
            {
                additionalDamage += 1; // 26
            }
            if (NPC.downedChristmasSantank)
            {
                additionalDamage += 1; // 27
            }
            if (NPC.downedChristmasTree)
            {
                additionalDamage += 1; // 28
            }
            if (NPC.downedPirates)
            {
                additionalDamage += 1; // 29
            }
            if (NPC.downedGoblins)
            {
                additionalDamage += 1; // 30
            }
            if (NPC.downedGolemBoss)
            {
                additionalDamage += 3; // 33
            }
            if (NPC.downedMartians)
            {
                additionalDamage += 2; // 35
            }
            if (NPC.downedEmpressOfLight)
            {
                additionalDamage += 3; // 38
            }
            if (NPC.downedAncientCultist)
            {
                additionalDamage += 2; // 40
            }
            if (NPC.downedTowerNebula)
            {
                additionalDamage += 1; // 41
            }
            if (NPC.downedTowerVortex)
            {
                additionalDamage += 1; // 42
            }
            if (NPC.downedTowerSolar)
            {
                additionalDamage += 1; // 43
            }
            if (NPC.downedTowerStardust)
            {
                additionalDamage += 1; // 44
            }
            if (NPC.downedMoonlord)
            {
                additionalDamage += 6; // 50
            }
            damage.Flat += (additionalDamage - 2);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.ShimmerMonolith)
                .AddIngredient(ItemID.Spear)
                .AddIngredient(ItemID.Ruby, 5)
                .AddIngredient(ItemID.Amber, 5)
                .AddIngredient(ItemID.Topaz, 5)
                .AddIngredient(ItemID.Emerald, 5)
                .AddIngredient(ItemID.Diamond, 5)
                .AddIngredient(ItemID.Sapphire, 5)
                .AddIngredient(ItemID.Amethyst, 5)
                .Register();
        }
    }
}
