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
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Content.Buffs;
using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class TungstenThornRing : ModItem
    {
        public static readonly DamageClass[] supportedDamageClasses =
        {
            DamageClass.Melee,
            DamageClass.Ranged,
            DamageClass.SummonMeleeSpeed,
            DamageClass.Throwing,
            DamageClass.Magic
        };

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 5);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.LightPurple;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<WDALTPlayer>().tungstenThornNecklace = true;
            foreach (DamageClass dc in supportedDamageClasses)
            {
                player.GetAttackSpeed(dc) -= 0.1f;
            }
            base.UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.SharkToothNecklace, 1)
              .AddIngredient(ItemID.SoulofFright, 5)
              .AddIngredient(ItemID.TungstenBar, 10)
              .AddIngredient(ItemID.TitaniumBar, 10)
              .Register();

            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.SharkToothNecklace, 1)
              .AddIngredient(ItemID.SoulofFright, 5)
              .AddIngredient(ItemID.TungstenBar, 10)
              .AddIngredient(ItemID.AdamantiteBar, 10)
              .Register();

            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.SharkToothNecklace, 1)
              .AddIngredient(ItemID.SoulofFright, 5)
              .AddIngredient(ItemID.SilverBar, 10)
              .AddIngredient(ItemID.TitaniumBar, 10)
              .Register();

            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.SharkToothNecklace, 1)
              .AddIngredient(ItemID.SoulofFright, 5)
              .AddIngredient(ItemID.SilverBar, 10)
              .AddIngredient(ItemID.AdamantiteBar, 10)
              .Register();
        }
    }
}
