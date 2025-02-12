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

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Prefixes;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;
using Terraria.Utilities;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;
using WeDoALittleTrolling.Content.Tiles;
using Terraria.GameContent.Items;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Content.Items.Tools;

namespace WeDoALittleTrolling.Content.Items
{
    internal class GlobalItemList : GlobalItem
    {
        public override bool InstancePerEntity => false;

        public static UnifiedRandom random = new UnifiedRandom();
        public static readonly int[] biomeCrateTypes =
        {
            ItemID.JungleFishingCrate,
            ItemID.JungleFishingCrateHard,
            ItemID.FloatingIslandFishingCrate,
            ItemID.FloatingIslandFishingCrateHard,
            ItemID.CorruptFishingCrate,
            ItemID.CorruptFishingCrateHard,
            ItemID.CrimsonFishingCrate,
            ItemID.CrimsonFishingCrateHard,
            ItemID.HallowedFishingCrate,
            ItemID.HallowedFishingCrateHard,
            ItemID.DungeonFishingCrate,
            ItemID.DungeonFishingCrateHard,
            ItemID.FrozenCrate,
            ItemID.FrozenCrateHard,
            ItemID.OasisCrate,
            ItemID.OasisCrateHard,
            ItemID.LavaCrate,
            ItemID.LavaCrateHard,
            ItemID.OceanCrate,
            ItemID.OceanCrateHard
        };

        public static readonly int[] fishronWeapons =
        {
            ItemID.Tsunami,
            ItemID.TempestStaff,
            ItemID.RazorbladeTyphoon,
            ItemID.Flairon,
            ItemID.BubbleGun
        };

        public static readonly int[] empressWeapons =
        {
            ItemID.FairyQueenMagicItem,
            ItemID.PiercingStarlight,
            ItemID.RainbowWhip,
            ItemID.FairyQueenRangedItem,
            ItemID.SparkleGuitar,
            ItemID.EmpressBlade
        };

        public override bool CanUseItem(Item item, Player player)
        {
            // Anti-Poo-Block-Mechanism
            if
            (
                item.type == ItemID.PoopBlock ||
                item.type == ItemID.PoopWall
            )
            {
                player.chatOverhead.NewMessage(player.name + " tried to uglify the world.", 180);
                return false;
            }
            //I'm sorry, hit
            /*
            else if
            (
                item.type == ItemID.LandMine
            )
            {
                player.chatOverhead.NewMessage("You may not use this item.", 180);
                return false;
            }
            */
            return base.CanUseItem(item, player);
        }

        public static void RegisterHooks()
        {
            On_Player.ItemCheck_UseTeleportRod += On_Player_ItemCheck_UseTeleportRod;
            On_Player.ItemCheck_CheckCanUse += toolUseBypass;
        }



        public static void UnregisterHooks()
        {
            On_Player.ItemCheck_UseTeleportRod -= On_Player_ItemCheck_UseTeleportRod;
            On_Player.ItemCheck_CheckCanUse -= toolUseBypass;
        }

        public static void On_Player_ItemCheck_UseTeleportRod(On_Player.orig_ItemCheck_UseTeleportRod orig, Player self, Item sItem)
        {
            bool harmony = (sItem.type == ItemID.RodOfHarmony);
            if (harmony)
            {
                Item imanginaryRodOfDiscord = new Item(ItemID.RodofDiscord, 1, 0);
                orig.Invoke(self, imanginaryRodOfDiscord);
                if (self.itemAnimation > 0 && self.chaosState)
                {
                    int idx = self.FindBuffIndex(BuffID.ChaosState);
                    if (idx >= 0 && idx < self.buffTime.Length)
                    {
                        self.buffTime[idx] = 180;
                    }
                }
            }
            orig.Invoke(self, sItem);
        }

        private static bool toolUseBypass(On_Player.orig_ItemCheck_CheckCanUse orig, Player self, Item sItem)
        {
            bool canUse = orig(self, sItem);
            if (self.HeldItem.type == ModContent.ItemType<BedrockBreaker>())
            {
                return true;
            }
            else
            {
                return canUse;
            }
        }

        /* No Wings Challenge Code
        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
        {
            if(item.wingSlot > -1 && item.wingSlot < ArmorIDs.Wing.Sets.Stats.Length)
            {
                if(ArmorIDs.Wing.Sets.Stats[item.wingSlot].FlyTime > 0)
                {
                    return false;
                }
            }
            return base.CanEquipAccessory(item, player, slot, modded);
        }
        */

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                (
                    equippedItem.type == ItemID.BrainOfConfusion ||
                    equippedItem.type == ModContent.ItemType<CrimsonAssassinGear>()
                ) &&
                (
                    incomingItem.type == ItemID.BrainOfConfusion ||
                    incomingItem.type == ModContent.ItemType<CrimsonAssassinGear>()
                )
            )
            {
                return false;
            }
            return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
        }

        //Adjust Tooltips accordingly
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.Terragrim || item.type == ItemID.Arkhalis)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "WeaponHitFreezeDescription", "Briefly freezes both yourself and your target upon impact");
                tooltips.Add(extraManaLine);
            }
            if (item.type == ItemID.BetsyWings)
            {
                TooltipLine acceleratedBoostLine = new TooltipLine(Mod, "AcceleratedBoostDescription", "Scales 100% better with the 'Accelerated' reforge");
                tooltips.Add(acceleratedBoostLine);
            }
            base.ModifyTooltips(item, tooltips);
        }

        public override void RightClick(Item item, Player player)
        {
            if
            (
                item.type == ItemID.IronCrate ||
                item.type == ItemID.IronCrateHard
            )
            {
                if (random.Next(0, 8) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((int)ItemID.Sundial);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.Sundial, 1);
                    }
                }
                if (random.Next(0, 8) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((int)ItemID.LifeCrystal);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.LifeCrystal, 1);
                    }
                }
            }
            if
            (
                item.type == ItemID.GoldenCrate ||
                item.type == ItemID.GoldenCrateHard
            )
            {
                if (random.Next(0, 4) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((int)ItemID.Sundial);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.Sundial, 1);
                    }
                }
                if
                (random.Next(0, 3) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((int)ItemID.LifeCrystal);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.LifeCrystal, 1);
                    }
                }
            }
            if
            (
                item.type == ItemID.OceanCrate ||
                item.type == ItemID.OceanCrateHard
            )
            {
                if (random.Next(0, 5) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((int)ItemID.WaterWalkingBoots);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.WaterWalkingBoots, 1);
                    }
                }
            }
            if
            (
                item.type == ItemID.OasisCrate ||
                item.type == ItemID.OasisCrateHard
            )
            {
                if (random.Next(0, 5) == 0)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket spawnCrateItemPacket = Mod.GetPacket();
                        spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                        spawnCrateItemPacket.Write((int)ItemID.SandstorminaBottle);
                        spawnCrateItemPacket.Write((int)player.width);
                        spawnCrateItemPacket.Write((int)player.height);
                        spawnCrateItemPacket.Write((int)1); //Drop amount
                        spawnCrateItemPacket.WriteVector2(player.position);
                        spawnCrateItemPacket.Send();
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.SandstorminaBottle, 1);
                    }
                }
            }
            int amountGold = 0;
            int goldItemID = ItemID.GoldCoin;
            if (item.type == ItemID.WoodenCrate || item.type == ItemID.WoodenCrateHard)
            {
                amountGold = 5;
            }
            if (item.type == ItemID.IronCrate || item.type == ItemID.IronCrateHard)
            {
                amountGold = 10;
            }
            if
            (
                biomeCrateTypes.Contains(item.type) ||
                (
                    WDALTModSystem.isThoriumModPresent &&
                    WDALTModSystem.MCIDIntegrity &&
                    WDALTModContentID.GetThoriumFishingCrateTypes().Contains(item.type)
                )
            )
            {
                amountGold = 12;
            }
            if (item.type == ItemID.GoldenCrate || item.type == ItemID.GoldenCrateHard)
            {
                amountGold = 20;
            }
            if (Main.hardMode)
            {
                amountGold *= 2;
            }
            if (item.type == ItemID.OceanCrateHard && NPC.downedFishron && player.anglerQuestsFinished >= 100)
            {
                amountGold = 2;
                goldItemID = ItemID.PlatinumCoin;
            }
            if
            (
                item.type == ItemID.WoodenCrate ||
                item.type == ItemID.WoodenCrateHard ||
                item.type == ItemID.IronCrate ||
                item.type == ItemID.IronCrateHard ||
                item.type == ItemID.GoldenCrate ||
                item.type == ItemID.GoldenCrateHard ||
                biomeCrateTypes.Contains(item.type) ||
                (
                    WDALTModSystem.isThoriumModPresent &&
                    WDALTModSystem.MCIDIntegrity &&
                    WDALTModContentID.GetThoriumFishingCrateTypes().Contains(item.type)
                )
            )
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket spawnCrateItemPacket = Mod.GetPacket();
                    spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                    spawnCrateItemPacket.Write((int)goldItemID);
                    spawnCrateItemPacket.Write((int)player.width);
                    spawnCrateItemPacket.Write((int)player.height);
                    spawnCrateItemPacket.Write((int)amountGold); //Drop amount
                    spawnCrateItemPacket.WriteVector2(player.position);
                    spawnCrateItemPacket.Send();
                }
                else if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, goldItemID, amountGold);
                }
            }
            if (item.type == ItemID.JungleFishingCrateHard && NPC.downedMechBossAny)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket spawnCrateItemPacket = Mod.GetPacket();
                    spawnCrateItemPacket.Write(WDALTPacketTypeID.spawnCrateItem);
                    spawnCrateItemPacket.Write((int)ItemID.LifeFruit);
                    spawnCrateItemPacket.Write((int)player.width);
                    spawnCrateItemPacket.Write((int)player.height);
                    spawnCrateItemPacket.Write((int)1); //Drop amount
                    spawnCrateItemPacket.WriteVector2(player.position);
                    spawnCrateItemPacket.Send();
                }
                else if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Item.NewItem(player.GetSource_OpenItem(item.type), (int)player.position.X, (int)player.position.Y, player.width, player.height, ItemID.LifeFruit, 1);
                }
            }
        }

        /*
            You MUST NOT override an item's name while it's being
            rendered as an entity in a world!
            Doing so anyways will result in CATASTROPHIC multiplayer bugs
            such as random items being renamed to your custom name
            despite not even having the correct item id!
            Due to this, we need override item names when the player
            picks up an item and/or the item is
            rendered in a player's inventory.
            TL;DR:
            Calling Item.SetNameOverride() inside SetDefaults()
            and/or PreDrawInWorld() is STRICTLY PROHIBITED!
            Use OnPickup() and/or PreDrawInInventory() instead.
        */

        public override bool OnPickup(Item item, Player player)
        {
            if
            (
                (
                    Main.netMode == NetmodeID.SinglePlayer ||
                    Main.netMode == NetmodeID.MultiplayerClient
                ) &&
                player.whoAmI == Main.myPlayer
            )
            {
                AdjustItemName(item);
            }
            return base.OnPickup(item, player);
        }

        public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if
            (
                Main.netMode == NetmodeID.SinglePlayer ||
                Main.netMode == NetmodeID.MultiplayerClient
            )
            {
                AdjustItemName(item);
            }
            return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        /*
            Calling @function AdjustItemName() from SetDefaults() and/or PreDrawInWorld() is STRICTLY PROHIBITED!
        */
        public static void AdjustItemName(Item item)
        {
            if
            (
                item.type == ItemID.AncientBattleArmorHat//Forbidden Mask
            )
            {
                item.SetNameOverride("Ancient Battle Mask");
            }
            if
            (
                item.type == ItemID.AncientBattleArmorShirt
            )
            {
                item.SetNameOverride("Ancient Battle Robes");
            }
            if
            (
                item.type == ItemID.AncientBattleArmorPants //Forbidden Treads
            )
            {
                item.SetNameOverride("Ancient Battle Treads");
            }
        }

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.FishingPotion)
            {
                item.shopCustomPrice = Item.buyPrice(gold: 20);
            }
            if (item.type == ItemID.CratePotion)
            {
                item.shopCustomPrice = Item.buyPrice(gold: 40);
            }
            if (item.type == ItemID.SonarPotion)
            {
                item.shopCustomPrice = Item.buyPrice(gold: 60);
            }
        }
    }
}
