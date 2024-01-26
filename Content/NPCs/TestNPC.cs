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

namespace WeDoALittleTrolling.Content.NPCs
{
    public class TestNPC : ModNPC
    {
        private const float detectionRange = 1920f + 1f; //One Screen Wide
        private Player target;
        private float distanceToTarget;
        //private int targetAggro;
        //private bool hasTarget;
        //Usable later but brick the NPC apparently

        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 18;
            NPC.damage = 10;
            NPC.defense = 10;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit53;
            NPC.DeathSound = SoundID.NPCDeath56;
            NPC.value = 10000000f;
            NPC.knockBackResist = 1.0f;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }

        public override void AI()
        {
            TestNPCAI();
        }

        private void TestNPCAI()
        {
            distanceToTarget = detectionRange;
            for (int i = 0 ; i < Main.player.Length ; i++)
            {
                if
                (
                    Main.player[i] == null ||
                    Main.player[i].dead ||
                    !Main.player[i].active
                )
                {
                    continue;
                }
                float TargetDistanceControl = Vector2.Distance(NPC.Center, Main.player[i].Center);
                if (TargetDistanceControl < distanceToTarget)
                {
                    target = Main.player[i];
                    distanceToTarget = TargetDistanceControl;
                }
            }

            if (target != null && !target.dead && target.active)
            {
                    NPC.velocity = new Vector2(target.Center.X - NPC.Center.X, target.Center.Y - NPC.Center.Y);
                    NPC.velocity.Normalize();
                    NPC.velocity *= 4f;
            }
            else
            {
                NPC.EncourageDespawn(10);
            }
        }
    }
}

