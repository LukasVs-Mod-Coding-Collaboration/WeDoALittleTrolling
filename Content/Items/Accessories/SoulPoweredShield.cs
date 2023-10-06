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
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class SoulPoweredShield : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 25);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Expert; //Expert Mode Item

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<WDALTPlayerUtil>().soulPoweredShield = true;
            player.endurance += 0.17f; //Damage Reduction (originally 0.25)
            player.statDefense += 5; // (originally 6)
            player.noKnockback = true;
            player.lavaImmune = true; //Immunity to Lava and Fire blocks
            player.fireWalk = true;
            player.buffImmune[BuffID.Frostburn] = true; //Debuff Immunities against fire & frost debuffs
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.OnFire3] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.ShadowFlame] = true;
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.Lovestruck] = true;
            player.buffImmune[BuffID.Stinky] = true;
            player.buffImmune[BuffID.Suffocation] = true;
            player.buffImmune[BuffID.Rabies] = true;
            player.buffImmune[BuffID.Obstructed] = true;
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            if(NPC.downedPlantBoss)
            {
                player.buffImmune[BuffID.Venom] = true;
            }
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                (
                    equippedItem.type == ItemID.WormScarf ||
                    incomingItem.type == ItemID.WormScarf ||
                    equippedItem.type == ModContent.ItemType<CrimsonAssassinGear>() ||
                    incomingItem.type == ModContent.ItemType<CrimsonAssassinGear>() ||
                    equippedItem.type == ItemID.BrainOfConfusion ||
                    incomingItem.type == ItemID.BrainOfConfusion
                )
            )
            {
                return false;
            }
            else
            {
                return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem,player);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.WormScarf, 1)
              .AddIngredient(ItemID.CobaltShield, 1)
              .AddIngredient(ItemID.SoulofMight, 5)
              .AddIngredient(ItemID.HallowedBar, 10)
              .AddIngredient(ItemID.TitaniumBar, 10)
              .AddIngredient(ItemID.Lens, 1)
              .Register();

            CreateRecipe()
             .AddTile(TileID.MythrilAnvil)
             .AddIngredient(ItemID.WormScarf, 1)
             .AddIngredient(ItemID.CobaltShield, 1)
             .AddIngredient(ItemID.SoulofMight, 5)
             .AddIngredient(ItemID.HallowedBar, 10)
             .AddIngredient(ItemID.AdamantiteBar, 10)
             .AddIngredient(ItemID.Lens, 1)
             .Register();
        }
    }
}
