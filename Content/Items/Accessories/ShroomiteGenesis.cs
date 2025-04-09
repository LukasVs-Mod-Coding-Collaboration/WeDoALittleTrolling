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
using WeDoALittleTrolling.Content.Items.Ammo;
using WeDoALittleTrolling.Common.ModPlayers;
using Terraria.Localization;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    public class ShroomiteGenesisSlot : ModAccessorySlot
    {
        public override bool DrawVanitySlot => false;
        public override bool DrawDyeSlot => false;
        public override string FunctionalBackgroundTexture => "Terraria/Images/Inventory_Back4";
        public override string FunctionalTexture => "WeDoALittleTrolling/Content/Items/Accessories/ShroomiteGenesis";

        public override bool IsEnabled()
        {
            if
            (
                (
                    Main.netMode == NetmodeID.SinglePlayer ||
                    Main.netMode == NetmodeID.MultiplayerClient
                ) &&
                !Main.dedServ &&
                Player.active &&
                Player.whoAmI >= 0 &&
                Player.whoAmI < Main.player.Length
            )
            {
                return
                (
                    Player.HeldItem.type == ModContent.ItemType<ShroomiteGenesis>() ||
                    Player.HasItem(ModContent.ItemType<ShroomiteGenesis>()) ||
                    FunctionalItem.type != ItemID.None
                );
            }
            return false;
        }

        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            return (item.type == ModContent.ItemType<ShroomiteGenesis>());
        }

        public override bool IsVisibleWhenNotEnabled()
        {
            return false;
        }

        public static LocalizedText GenesisText { get; private set; }

        public override void SetupContent()
        {
            GenesisText = Mod.GetLocalization($"{nameof(ShroomiteGenesisSlot)}.ShroomiteGenesisDescription");
        }

        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            return (checkItem.type == ModContent.ItemType<ShroomiteGenesis>());
        }

        public override void OnMouseHover(AccessorySlotType context) {
            switch (context)
            {
                case AccessorySlotType.FunctionalSlot:
                    Main.hoverItemName = GenesisText.Value;
                    break;
                default:
                    break;
            }
        }
    }

    internal class ShroomiteGenesis : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 26;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 12);
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Yellow;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<WDALTPlayer>().shroomiteGenesis = true;
        }

        public override bool AllowPrefix(int pre)
        {
            return false;
        }

        public override bool CanReforge()
        {
            return false;
        }

        /*
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                equippedItem.type == ModContent.ItemType<ShroomiteOvercharge>() ||
                incomingItem.type == ModContent.ItemType<ShroomiteOvercharge>()
            )
            {
                return false;
            }
            else
            {
                return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
            }
        }
        */
        //Nope, Compatible with the other two. The big drawback to this (Like your weapons overheating or something) will be added later. Have fun for now, lmao

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.TinkerersWorkbench)
              .AddTile(TileID.CrystalBall)
              .AddIngredient(ItemID.ShroomiteBar, 12)
              .AddIngredient(ItemID.AmmoBox, 1)
              .AddIngredient(ItemID.RangerEmblem, 1)
              .AddIngredient(ItemID.EndlessMusketPouch, 1)
              .AddIngredient(ItemID.EndlessQuiver, 1)
              .AddIngredient(ModContent.ItemType<InfiniteRocketAmmo>(), 1)
              .Register();
        }
    }
}
