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

using WeDoALittleTrolling.Content.Projectiles;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    public class GloriousDemiseInventoryItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true;
            ItemID.Sets.Spears[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(gold: 5);

            Item.width = 66;
            Item.height = 66;
            Item.scale = 1f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.UseSound = SoundID.Item45;
            Item.autoReuse = true;

            Item.damage = 4;
            Item.knockBack = 10f;
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

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            float modifier = 1f;
            if(NPC.downedBoss1)
            {
                modifier += 0.25f;
            }
            if(NPC.downedBoss2)
            {
                modifier += 0.25f;
            }
            if(NPC.downedBoss3)
            {
                modifier += 0.5f;
            }
            if(Main.hardMode)
            {
                modifier += 1.0f;
            }
            if(NPC.downedMechBoss2)
            {
                modifier += 0.25f;
            }
            if(NPC.downedMechBoss1)
            {
                modifier += 0.25f;
            }
            if(NPC.downedMechBoss3)
            {
                modifier += 0.5f;
            }
            if(NPC.downedChristmasIceQueen)
            {
                modifier += 0.25f;
            }
            if(NPC.downedChristmasSantank)
            {
                modifier += 0.25f;
            }
            if(NPC.downedChristmasTree)
            {
                modifier += 0.25f;
            }
            if(NPC.downedHalloweenKing)
            {
                modifier += 0.5f;
            }
            if(NPC.downedHalloweenTree)
            {
                modifier += 0.25f;
            }
            if(NPC.downedPlantBoss)
            {
                modifier *= 1.25f;
            }
            if(NPC.downedGolemBoss)
            {
                modifier *= 1.25f;
            }
            if(NPC.downedAncientCultist)
            {
                modifier *= 1.25f;
            }
            if(NPC.downedEmpressOfLight)
            {
                modifier *= 1.25f;
            }
            if(NPC.downedFishron)
            {
                modifier *= 1.25f;
            }
            if (NPC.downedMoonlord)
            {
                modifier *= 3f;
            }
            if (player.shimmerImmune)
            {
                modifier *= 2f;
            }
            damage *= modifier;
            base.ModifyWeaponDamage(player, ref damage);
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
