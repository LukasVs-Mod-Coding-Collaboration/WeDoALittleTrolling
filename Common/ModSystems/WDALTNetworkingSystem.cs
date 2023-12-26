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

using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal class WDALTNetworkingSystem
    {
        public void HandlePacket(BinaryReader reader, int whoAmI, Mod mod)
        {
            short type = reader.ReadInt16();
            float value = 0f;
            Vector2 RODCsoundPos = new Vector2(0f, 0f);
            short itemType = 0;
            int playerWidth = 0;
            int playerHeight = 0;
            int dropAmount = 0;
            int netLifePvP = 100;
            int netLifePvPPlayerIndex = 255;
            Vector2 itemSpawnPos = new Vector2(0f, 0f);
            if(type == WDALTPacketTypeID.updateWindSpeedTarget)
            {
                value = reader.ReadSingle();
            }
            if(type == WDALTPacketTypeID.soundBroadcastRainOfDecay || type == WDALTPacketTypeID.soundPlayRainOfDecay)
            {
                RODCsoundPos = reader.ReadVector2();
            }
            if(type == WDALTPacketTypeID.spawnCrateItem)
            {
                itemType = reader.ReadInt16();
                playerWidth = reader.ReadInt32();
                playerHeight = reader.ReadInt32();
                dropAmount = reader.ReadInt32();
                itemSpawnPos = reader.ReadVector2();
            }
            if(type == WDALTPacketTypeID.syncNetFinalDamage)
            {
                netLifePvP = reader.ReadInt32();
                netLifePvPPlayerIndex = reader.ReadInt32();
            }
            if(type == WDALTPacketTypeID.broadcastNetFinalDamage)
            {
                netLifePvP = reader.ReadInt32();
                netLifePvPPlayerIndex = reader.ReadInt32();
            }
            if(Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (type == WDALTPacketTypeID.updateWindSpeedTarget)
                {
                    Main.windSpeedTarget = value;
                }
                if (type == WDALTPacketTypeID.soundBroadcastRainOfDecay)
                {
                    SoundEngine.PlaySound(SoundID.Item5, RODCsoundPos);
                }
                //SmartPVP(TM) Technology: Display actual pvp damage to clients on non-lethal hits and sync health
                //Other relevent code is found in WDALTPlayerUtil
                if (type == WDALTPacketTypeID.broadcastNetFinalDamage)
                {
                    if(netLifePvPPlayerIndex != Main.myPlayer)
                    {
                        CombatText.NewText
                        (
                            new Rectangle
                            (
                                (int)Main.player[netLifePvPPlayerIndex].position.X,
                                (int)Main.player[netLifePvPPlayerIndex].position.Y,
                                Main.player[netLifePvPPlayerIndex].width,
                                Main.player[netLifePvPPlayerIndex].height
                            ),
                            new Color(255, 255, 0),
                            ((Main.player[netLifePvPPlayerIndex].statLife - netLifePvP) + 1)
                        );
                        Main.player[netLifePvPPlayerIndex].statLife = netLifePvP;
                    }
                }
            }
            if(Main.netMode == NetmodeID.Server)
            {
                if(type == WDALTPacketTypeID.syncNetFinalDamage)
                {
                    ModPacket broadcastNetFinalDamagePacket = mod.GetPacket();
                    broadcastNetFinalDamagePacket.Write(WDALTPacketTypeID.broadcastNetFinalDamage);
                    broadcastNetFinalDamagePacket.Write(netLifePvP);
                    broadcastNetFinalDamagePacket.Write(netLifePvPPlayerIndex);
                    broadcastNetFinalDamagePacket.Send();
                }
                if(type == WDALTPacketTypeID.spawnCrateItem)
                {
                    Item.NewItem(new EntitySource_SpawnNPC(), (int)itemSpawnPos.X, (int)itemSpawnPos.Y, playerWidth, playerHeight, itemType, dropAmount);
                }
                if (type == WDALTPacketTypeID.soundPlayRainOfDecay)
                {
                    ModPacket soundBroadcastRainOfDecayPacket = mod.GetPacket();
                    soundBroadcastRainOfDecayPacket.Write(WDALTPacketTypeID.soundBroadcastRainOfDecay);
                    soundBroadcastRainOfDecayPacket.WriteVector2(RODCsoundPos);
                    soundBroadcastRainOfDecayPacket.Send();
                }
                if(type == WDALTPacketTypeID.moondial)
                {
                    if (Main.moondialCooldown > 2)
                    {
                        Main.moondialCooldown = 2;
                    }
                }
                if(type == WDALTPacketTypeID.sundial)
                {
                    if(!Main.dontStarveWorld && Main.sundialCooldown > 2)
                    {
                        Main.sundialCooldown = 2;
                    }
                }
                if(type == WDALTPacketTypeID.weatherVane)
                {
                    if (Main.IsItRaining)
                    {
                        if (Main.maxRaining < 0.2f)
                        {
                            Main.maxRaining = 0.4f;
                        }
                        else if (Main.maxRaining < 0.6f)
                        {
                            Main.maxRaining = 0.8f;
                        }
                        else if (!Main.dontStarveWorld)
                        {
                            Main.StopRain();
                        }
                    }
                    else
                    {
                        Main.StartRain();
                        Main.maxRaining = 0.1f;
                    }
                }
                if(type == WDALTPacketTypeID.djinnLamp)
                {
                    float windSpeedPerMph = ((1.0f)/(50.0f));
                    float windSign = 1.0f;
                    if (Main.windSpeedTarget >= 0)
                    {
                        windSign = 1;
                    }
                    else
                    {
                        windSign = -1;
                    }
                    if (Sandstorm.Happening)
                    {
                        Sandstorm.StopSandstorm();
                    }
                    else
                    {
                        Sandstorm.StartSandstorm();
                        if (Math.Abs(Main.windSpeedTarget) < windSpeedPerMph * 30.0f)
                        {
                            if (Main.windSpeedTarget > 0)
                            {
                                Main.windSpeedTarget = windSign * windSpeedPerMph * 35.0f;

                            }
                            else
                            {
                                Main.windSpeedTarget = windSign * windSpeedPerMph * 35.0f;
                            }
                        }
                    }
                    ModPacket updateWindSpeedTargetPacket = mod.GetPacket();
                    updateWindSpeedTargetPacket.Write(WDALTPacketTypeID.updateWindSpeedTarget);
                    updateWindSpeedTargetPacket.Write((float)Main.windSpeedTarget);
                    updateWindSpeedTargetPacket.Send();
                }
                if(type == WDALTPacketTypeID.skyMill)
                {
                    float windSpeedPerMph = ((1.0f)/(50.0f));
                    float windSign = 1.0f;
                    if (Main.windSpeedTarget >= 0)
                    {
                        windSign = 1;
                    }
                    else
                    {
                        windSign = -1;
                    }
                    if (Math.Abs(Main.windSpeedTarget) < windSpeedPerMph * 39.0f)
                    {
                        Main.windSpeedTarget += windSign * windSpeedPerMph * 10.0f;
                    }
                    else if (Math.Abs(Main.windSpeedTarget) >= windSpeedPerMph * 39.0f)
                    {
                        Main.windSpeedTarget = (-windSign) * windSpeedPerMph * 5.0f;
                    }
                    ModPacket updateWindSpeedTargetPacket = mod.GetPacket();
                    updateWindSpeedTargetPacket.Write(WDALTPacketTypeID.updateWindSpeedTarget);
                    updateWindSpeedTargetPacket.Write((float)Main.windSpeedTarget);
                    updateWindSpeedTargetPacket.Send();
                }
            }
        }
    }
}
