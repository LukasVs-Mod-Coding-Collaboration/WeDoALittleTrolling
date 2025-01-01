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

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Utilities;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Buffs;
using Terraria.Audio;
using WeDoALittleTrolling.Common.Utilities;
using Terraria.DataStructures;
using WeDoALittleTrolling.Common.ModPlayers;
using System.IO;


namespace WeDoALittleTrolling.Content.NPCs
{
    public class BlazingShieldNPC : ModNPC
    {
        public const int rotationsPerSecond = 1; // Change to adjust spinning speed
        public int currentDegree = 0;
        public const int baseDegreeMultiplier = 6;
        public const int distanceFromPlayer = 64;
        Player shieldOwner;


        public override void SetDefaults()
        {
            NPC.width = 10;
            NPC.height = 10;
            NPC.friendly = true;
            NPC.timeLeft = 1200;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCHit4;
            NPC.value = 0f;
            NPC.lifeMax = 1;
            NPC.damage = 1;
            NPC.defense = 0;
            NPC.scale = 1f;
            NPC.knockBackResist = 0.0f;
        }

        public override void AI()
        {
            AI_022_BlazingShield();
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((int)currentDegree);
            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            currentDegree = reader.ReadInt32();
            base.ReceiveExtraAI(reader);
        }

        private void AI_022_BlazingShield()
        {
            int idx = NPC.GetGlobalNPC<WDALTNPCUtil>().BlazingShieldOwnerIndex;
            if (idx >= 0 && idx < Main.player.Length)
            {
                shieldOwner = Main.player[idx];
                Vector2 direction = new Vector2
                (
                    (float)Math.Cos(MathHelper.ToRadians(currentDegree)),
                    (float)Math.Sin(MathHelper.ToRadians(currentDegree))
                );
                direction = direction.SafeNormalize(Vector2.Zero);
                Vector2 position = shieldOwner.Center + (direction * distanceFromPlayer);
                NPC.Center = position;
                currentDegree = currentDegree + (rotationsPerSecond * baseDegreeMultiplier);
                if (currentDegree >= 360)
                {
                    currentDegree = currentDegree % 360;
                }
            }
            if (NPC.GetGlobalNPC<WDALTNPCUtil>().ticksAlive % 60 == 0) //Sync position once every second.
            {
                NPC.netUpdate = true;
            }
        }
    }
}
