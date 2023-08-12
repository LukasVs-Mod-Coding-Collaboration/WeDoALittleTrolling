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

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class GreenstrikeDynamo : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 34;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 10);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Red;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 1.25f; // (originally 1.5)
            player.noFallDmg = true;
            player.accRunSpeed += 0.4f;
            if(player.lifeRegen >= 0)
            {
                player.lifeRegen += 25; //HP Regen (originally 25)
            }
            //player.GetAttackSpeed(DamageClass.Generic) += 0.25f;
            player.longInvince = true; //Cross Necklace effect
            player.wingTimeMax += 1200;
            player.GetAttackSpeed(DamageClass.Generic) += (float)0.25;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.LifeFruit, 5)
              .AddIngredient(ItemID.LunarBar, 10)
              .AddIngredient(ItemID.ChlorophyteBar, 10)
              .AddIngredient(ItemID.Wire, 50)
              .AddIngredient(ItemID.MartianConduitPlating, 10)
              .Register();
        }
    }
}