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

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.Prefixes;
using WeDoALittleQualityOfLife.Content.Tiles;

namespace WeDoALittleQualityOfLife.Content.Items
{
    internal class QOLItemList : GlobalItem
    {
        public override bool InstancePerEntity => false;

        public override bool ConsumeItem(Item item, Player player)
        {
            if (BuffTiles.BuffTilesItemIDs.Contains(item.type))
            {
                SoundStyle buffActivateSoundStyle = SoundID.Item4;
                bool playSound = true;
                switch (item.type)
                {
                    case ItemID.WarTable:
                        buffActivateSoundStyle = SoundID.Item4;
                        break;
                    case ItemID.BewitchingTable:
                        buffActivateSoundStyle = SoundID.Item4;
                        break;
                    case ItemID.SharpeningStation:
                        buffActivateSoundStyle = SoundID.Item37;
                        break;
                    case ItemID.CrystalBall:
                        buffActivateSoundStyle = SoundID.Item4;
                        break;
                    case ItemID.AmmoBox:
                        buffActivateSoundStyle = SoundID.Item149;
                        break;
                    case ItemID.SliceOfCake:
                        buffActivateSoundStyle = SoundID.Item2;
                        break;
                    default:
                        playSound = false;
                        break;
                }
                if (playSound)
                {
                    SoundEngine.PlaySound(buffActivateSoundStyle, player.Center);
                }
                return false;
            }
            return base.ConsumeItem(item, player);
        }

        public override void SetDefaults(Item item)
        {
            // Make boss summoning items non-consumable
            if
            (
                item.type == ItemID.SlimeCrown ||
                item.type == ItemID.SuspiciousLookingEye ||
                item.type == ItemID.WormFood ||
                item.type == ItemID.BloodySpine ||
                item.type == ItemID.Abeemination ||
                item.type == ItemID.DeerThing ||
                item.type == ItemID.MechanicalWorm ||
                item.type == ItemID.MechanicalEye ||
                item.type == ItemID.MechanicalSkull ||
                item.type == ItemID.MechdusaSummon ||
                item.type == ItemID.CelestialSigil ||
                item.type == ItemID.PumpkinMoonMedallion ||
                item.type == ItemID.NaughtyPresent ||
                item.type == ItemID.GoblinBattleStandard ||
                item.type == ItemID.PirateMap
            )
            {
                item.consumable = false;
                item.maxStack = 1;
            }
            //Make Buff Furniture give their Buffs when used
            if (BuffTiles.BuffTilesItemIDs.Contains(item.type))
            {
                item.buffTime = 108000;
                switch (item.type)
                {
                    case ItemID.WarTable:
                        item.buffType = BuffID.WarTable;
                        break;
                    case ItemID.BewitchingTable:
                        item.buffType = BuffID.Bewitched;
                        break;
                    case ItemID.SharpeningStation:
                        item.buffType = BuffID.Sharpened;
                        break;
                    case ItemID.CrystalBall:
                        item.buffType = BuffID.Clairvoyance;
                        break;
                    case ItemID.AmmoBox:
                        item.buffType = BuffID.AmmoBox;
                        break;
                    case ItemID.SliceOfCake:
                        item.buffType = BuffID.SugarRush;
                        item.buffTime = 7200;
                        break;
                    default:
                        item.buffTime = 0;
                        break;
                }
            }
        }
    }
}
