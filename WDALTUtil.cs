using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeDoALittleTrolling
{
    public class WDALTUtil
    {
        public static bool hasPlayerAcessoryEquipped(Player player, int itemID)
        {
            int offset = 3;
            int loopLimit = 5;
            loopLimit += player.extraAccessorySlots;
            if(Main.masterMode)
            {
                loopLimit++;
            }
            for(int i = offset;i < (offset + loopLimit); i++) //Search through all accessory slots
            {
                if(player.armor[i].type == itemID)
                {
                    return true;
                }
            }
            return false;
        }

        public static int getAmountOfEquippedAccessoriesWithPrefixFromPlayer(Player player, int prefixID) //Fancy name much smart, haha :P
        {
            int equippedAmount = 0;
            int offset = 3;
            int loopLimit = 5;
            loopLimit += player.extraAccessorySlots;
            if(Main.masterMode)
            {
                loopLimit++;
            }
            for(int i = offset;i < (offset + loopLimit); i++) //Search through all accessory slots
            {
                if(player.armor[i].prefix == prefixID)
                {
                    equippedAmount++;
                }
            }
            return equippedAmount;
        }

        public static bool hasPlayerHelmetEquipped(Player player, int itemID)
        {
            int offset = 0;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public static bool hasPlayerChestplateEquipped(Player player, int itemID)
        {
            int offset = 1;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public static bool hasPlayerLeggingsEquipped(Player player, int itemID)
        {
            int offset = 2;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public static bool isPlayerHoldingItemWithPrefix(Player player, int prefixID)
        {
            Item item = player.HeldItem;
            if(item.prefix == prefixID)
            {
                return true;
            }
            return false;
        }
    }
}