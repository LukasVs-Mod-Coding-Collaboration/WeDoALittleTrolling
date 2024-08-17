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
        private float maxSpeed = 10f;
        private bool hasTarget = false;
        private bool hover = true;
        /*
        private float distanceToTarget;
        private bool laser = false;
        private bool changedPhases = false;
        private bool charge = false;
        private bool chargeFast = false;
        private bool surpriseCharge = false;
        */

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
            if (target == null || target.dead || !target.active || Vector2.Distance(NPC.Center, target.Center) > detectionRange)
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i] != null && !Main.player[i].dead && Main.player[i].active)
                    {
                        target = Main.player[i];
                        hasTarget = true;
                        break;  
                    }   //Stop the loop as soon as we detect any viable Target
                    else
                    {
                        hasTarget = false;
                    }
                    //If the loop runs without finding a Player, we want to not do anything.
                    //If our hasTarget is false, Idle or despawn later in the AI code.
                }
            }
            //Find A Sample Player
            for (int i = 0; i < Main.player.Length; i++)
            {
                if
                (
                     (Main.player[i].aggro > target.aggro ||
                     (Main.player[i].aggro == target.aggro && Vector2.Distance(NPC.Center, Main.player[i].Center) < Vector2.Distance(NPC.Center, target.Center)))
                     && (Main.player[i] != null && !Main.player[i].dead && Main.player[i].active)
                )
                {
                    target = Main.player[i];
                    hasTarget = true;
                }
                
            }
            //Determine our Target based on aggro and distance
            TestNPCAI();
            //Actually decide what we want to do
        }

        private void TestNPCAI()
        {          
            if (hasTarget) //Act only if we have a target
            {
                if (hover)
                {
                    Vector2 hoverDirection = new Vector2(target.Center.X - NPC.Center.X, target.Center.Y - NPC.Center.Y - 192);
                    Vector2 hoverPoint = hoverDirection;
                    hoverDirection = hoverDirection.SafeNormalize(Vector2.Zero);
                    hoverDirection *= 0.45f;
                    if (hoverPoint.Length() > 32)
                    {
                        NPC.velocity.X += hoverDirection.X;
                        NPC.velocity.Y += hoverDirection.Y * 0.75f;
                        if (NPC.velocity.Length() > maxSpeed)
                        {
                            NPC.velocity = NPC.velocity.SafeNormalize(NPC.velocity) * maxSpeed;
                        }
                        if (NPC.Center.Y < target.Center.Y && NPC.velocity.Y > 0 && hoverPoint.Length() < 48)
                        {
                            NPC.velocity.Y *= 0.98f;
                        }                        
                    }
                    else
                    {
                        NPC.velocity *= 0.96f;
                        if (NPC.velocity.Length() < 0.05f)
                        {
                            NPC.velocity *= 0f;
                        }
                    }
                }
            }
            else // Despawn without a target
            {
                NPC.EncourageDespawn(10);
            }
        }
    }
}

