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
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using System.IO;
using WeDoALittleTrolling.Common.ModPlayers;
using System.Collections.Generic;
using Terraria.ID;
using WeDoALittleTrolling.Content.Projectiles;
using Humanizer;

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal class WDALTHitFreezeSystemPlayer : ModPlayer
    {
        public bool __AI_IS_FROZEN = false;
        public int __AI_FROZEN_TICKS = 0;

        public static void On_Player_Update(Terraria.On_Player.orig_Update orig, Terraria.Player self, int i)
        {
            if (self.TryGetModPlayer<WDALTHitFreezeSystemPlayer>(out WDALTHitFreezeSystemPlayer sys) && sys.__AI_IS_FROZEN)
            {
                if (sys.__AI_FROZEN_TICKS > 0)
                {
                    sys.__AI_FROZEN_TICKS--;
                }
                else
                {
                    __AI_SYNC_UNSET_FROZEN(self);
                }
                return;
            }
            orig.Invoke(self, i);
        }

        public static void RegisterHooks()
        {
            On_Player.Update += On_Player_Update;
        }

        public static void UnregisterHooks()
        {
            On_Player.Update -= On_Player_Update;
        }

        public static void FreezePlayerForTicks(Player player, int ticks)
        {
            if (player.dead || !player.active)
            {
                return;
            }
            if (player.GetModPlayer<WDALTHitFreezeSystemPlayer>().__AI_FROZEN_TICKS <= 0)
            {
                player.GetModPlayer<WDALTHitFreezeSystemPlayer>().__AI_FROZEN_TICKS = ticks;
            }
            __AI_SYNC_SET_FROZEN(player, ticks);
        }

        private static void __AI_SYNC_SET_FROZEN(Player player, int ticks)
        {
            player.GetModPlayer<WDALTHitFreezeSystemPlayer>().__AI_IS_FROZEN = true;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket syncHitFreezePacket = WeDoALittleTrolling.instance.GetPacket();
                syncHitFreezePacket.Write(WDALTPacketTypeID.syncHitFreeze);
                syncHitFreezePacket.Write((bool)true);
                syncHitFreezePacket.Write((int)player.whoAmI);
                syncHitFreezePacket.Write((int)ticks);
                syncHitFreezePacket.Send();
            }
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket syncHitFreezePacket = WeDoALittleTrolling.instance.GetPacket();
                syncHitFreezePacket.Write(WDALTPacketTypeID.broadcastHitFreeze);
                syncHitFreezePacket.Write((bool)true);
                syncHitFreezePacket.Write((int)player.whoAmI);
                syncHitFreezePacket.Write((int)ticks);
                syncHitFreezePacket.Send();
            }
        }

        private static void __AI_SYNC_UNSET_FROZEN(Player player)
        {
            player.GetModPlayer<WDALTHitFreezeSystemPlayer>().__AI_IS_FROZEN = false;
        }
    }
}
