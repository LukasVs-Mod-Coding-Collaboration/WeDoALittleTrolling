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
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Content.Tiles;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Content.Items.Placeable;

namespace WeDoALittleTrolling.Content.Items.Tools
{
    internal class BedrockBreaker : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;

            Item.value = Item.buyPrice(gold: 25);
            Item.consumable = false;
            Item.maxStack = 1;

            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item1;

            Item.damage = 240;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6.0f;
            Item.rare = ItemRarityID.Orange;

            Item.pick = int.MaxValue;
            Item.crit = 6;
            Item.tileBoost = 5;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.TitaniumBar, 15)
              .AddIngredient(ModContent.ItemType<EmbersteelBarItem>(), 10)
              .AddIngredient(ItemID.Picksaw)
              .Register();
        }
    }
}