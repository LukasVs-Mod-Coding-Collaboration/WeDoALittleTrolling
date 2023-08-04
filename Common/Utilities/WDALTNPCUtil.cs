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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.NPCs;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using System.IO;

namespace WeDoALittleTrolling.Common.Utilities
{
    internal class WDALTNPCUtil : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public int VileSpitTimeLeft;
        public long ticksAlive = 0;
        public long lastActionTick = 0;

        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
        }

        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            binaryWriter.Write(VileSpitTimeLeft);
            base.SendExtraAI(npc, bitWriter, binaryWriter);
        }

        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
        {
            VileSpitTimeLeft = binaryReader.ReadInt32();
            base.ReceiveExtraAI(npc, bitReader, binaryReader);
        }

        public override void PostAI(NPC npc)
        {
            ticksAlive++;
            base.PostAI(npc);
        }
    }
}
