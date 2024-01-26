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
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using WeDoALittleTrolling.Common.ModSystems;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class D6A2_Apollon : ModItem
    {
        public bool autoAim = true;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 96;
            Item.height = 38;
            Item.scale = 0.875f;
            Item.rare = ItemRarityID.Expert;

            Item.useTime = 120;
            Item.useAnimation = 120;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item84;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 4000;
            Item.knockBack = 5f;
            Item.noMelee = true;
            Item.crit = 0;
            Item.shoot = ModContent.ProjectileType<Beamlaser2>();
            Item.shootSpeed = 2.0f;
            Item.autoReuse = true;
       

        }

        public override bool AltFunctionUse(Player player)
        {
            if(this.autoAim)
            {
                this.autoAim = false;
                SoundEngine.PlaySound(SoundID.Item67, player.position);
                player.chatOverhead.NewMessage("Aim-Assist: Disabled", 60);
            }
            else
            {
                this.autoAim = true;
                SoundEngine.PlaySound(SoundID.Item67, player.position);
                player.chatOverhead.NewMessage("Aim-Assist: Enabled", 60);
            }
            return false;
        }
        
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 shootDirection = velocity;
            shootDirection.Normalize();
            float xOffset = shootDirection.X * 72.0f;
            float yOffset = shootDirection.Y * 72.0f;
            position = new Vector2(position.X + xOffset, position.Y + yOffset - 10.0f);
            if(this.autoAim)
            {
                type = ModContent.ProjectileType<Beamlaser2_AutoAim>();
            }
            else
            {
                type = ModContent.ProjectileType<Beamlaser2>();
            }
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-18.5f, -6.0f);
        }

        public override void AddRecipes()
        {
            if(WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
            {
                CreateRecipe()
                    .AddTile(TileID.LunarCraftingStation)
                    .AddIngredient(ModContent.ItemType<D6A1_Artemis>(), 1)
                    .AddIngredient(WDALTModContentID.GetThoriumItemID(WDALTModContentID.ThoriumItem_Essence_SFF), 3)
                    .AddIngredient(WDALTModContentID.GetThoriumItemID(WDALTModContentID.ThoriumItem_Essence_OLD), 3)
                    .AddIngredient(WDALTModContentID.GetThoriumItemID(WDALTModContentID.ThoriumItem_Essence_AET), 3)
                    .AddIngredient(ItemID.LunarBar, 20)
                    .AddIngredient(WDALTModContentID.GetThoriumItemID(WDALTModContentID.ThoriumItem_Fragment_WD), 20)
                    .AddIngredient(ItemID.FragmentStardust, 20)
                    .AddIngredient(ItemID.FragmentVortex, 20)
                    .AddIngredient(ItemID.FragmentNebula, 20)
                    .AddIngredient(ItemID.FragmentSolar, 20)
                    .Register();
            }
        }

    }
}
