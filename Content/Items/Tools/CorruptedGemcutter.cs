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
/*

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

namespace WeDoALittleTrolling.Content.Items.Tools
{
    internal class CorruptedGemcutter : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;

            Item.value = Item.buyPrice(gold: 5);
            Item.maxStack = 1;

            Item.autoReuse = true;
            Item.useTime = 6;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item1;

            Item.pick = 55;

            Item.damage = 25;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 30f;
            Item.crit = 12;

            Item.rare = ItemRarityID.Yellow;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.Anvils)
              .AddIngredient(ItemID.SilverBar, 20)
              .AddIngredient(ItemID.MeteoriteBar, 5)
              .AddIngredient(ItemID.DemoniteBar, 5)
              .AddIngredient(ItemID.Sapphire, 5)
              .Register();

            CreateRecipe()
             .AddTile(TileID.Anvils)
             .AddIngredient(ItemID.TungstenBar, 20)
             .AddIngredient(ItemID.MeteoriteBar, 5)
             .AddIngredient(ItemID.DemoniteBar, 5)
             .AddIngredient(ItemID.Sapphire, 5)
             .Register();
        }
    }
}
*/