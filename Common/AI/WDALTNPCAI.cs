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

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Common.AI
{
    internal static class WDALTNPCAI
    {
        private static UnifiedRandom random = new UnifiedRandom();

        public static void AI_007_GlobalNPC(NPC npc)
        {
            if
            (
                npc.type == NPCID.EaterofWorldsHead ||
                npc.type == NPCID.EaterofWorldsBody ||
                npc.type == NPCID.EaterofWorldsTail
            )
            {
                //Replicate vanilla behavior as good as possible.
                if(random.NextBool(300) && Main.expertMode && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.TargetClosest();
                    if(!Collision.CanHitLine(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                    {
                        NPC.NewNPC(new EntitySource_Parent(npc), (int)(npc.position.X + (float)(npc.width / 2) + npc.velocity.X), (int)(npc.position.Y + (float)(npc.height / 2) + npc.velocity.Y), NPCID.VileSpitEaterOfWorlds, 0, 0f, 1f);
                    }
                }
            }
            npc.GetGlobalNPC<WDALTNPCUtil>().VileSpitTimeLeft--;
            if(npc.type == NPCID.TheDestroyerBody)
            {
                //Replicate vanilla behavior as good as possible.
                if(random.NextBool(900) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.TargetClosest();
                    if(!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    {
                        Vector2 posWithOffset = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)(npc.height / 2));
                        float randomMultiplierX = Main.player[npc.target].position.X + ((float)Main.player[npc.target].width * 0.5f) + (float)random.Next(-16, 17) - posWithOffset.X;
                        float randomMultiplierY = Main.player[npc.target].position.Y + ((float)Main.player[npc.target].height * 0.5f) + (float)random.Next(-16, 17) - posWithOffset.Y;
                        float randomMultiplierLengh = 8f / (new Vector2(randomMultiplierX, randomMultiplierY).Length());
                        randomMultiplierX = (randomMultiplierX*randomMultiplierLengh) + ((float)random.Next(-16, 17) * 0.04f);
                        randomMultiplierY = (randomMultiplierY*randomMultiplierLengh) + ((float)random.Next(-16, 17) * 0.04f);
                        posWithOffset.X += randomMultiplierX * 4f;
                        posWithOffset.Y += randomMultiplierY * 4f;
                        int damage = npc.GetAttackDamage_ForProjectiles(22f, 18f);
                        int i = Projectile.NewProjectile(npc.GetSource_FromThis(), posWithOffset.X, posWithOffset.Y, randomMultiplierX, randomMultiplierY, ProjectileID.DeathLaser, damage, 0f, Main.myPlayer);
                        Main.projectile[i].timeLeft = 300;
                        npc.netUpdate = true;
                    }
                }
            }
            if(npc.type == NPCID.Plantera)
            {
                long timeSinceLastShot = (npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive - npc.GetGlobalNPC<WDALTNPCUtil>().lastActionTick);
                int shotDelay = 120;
                float dmg1 = 32f;
                float dmg2 = 24f;
                if(npc.life < (npc.lifeMax / 4))
                {
                    shotDelay = 90;
                }
                if (!Main.player[npc.target].ZoneJungle || (double)Main.player[npc.target].position.Y < Main.worldSurface * 16.0 || Main.player[npc.target].position.Y > (double)(Main.UnderworldLayer * 16.0))
                {
                    dmg1 *= 2;
                    dmg2 *= 2;
                    shotDelay = 30;
                }
                if(timeSinceLastShot >= shotDelay && Main.expertMode && Main.netMode != NetmodeID.MultiplayerClient && (npc.life < (npc.lifeMax / 2)))
                {
                    npc.GetGlobalNPC<WDALTNPCUtil>().lastActionTick = npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive;
                    npc.TargetClosest();
                    if(Main.player[npc.target].active && !Main.player[npc.target].dead)
                    {
                        int damage = npc.GetAttackDamage_ForProjectiles(dmg1, dmg2);
                        int amount = random.Next(4, 7);
                        float sprayIntensity = 16.0f; //Max Spraying in Tiles
                        for (int j = 0; j < amount; j++)
                        {
                            float randomModifierX = (random.NextFloat() - 0.5f);
                            float randomModifierY = (random.NextFloat() - 0.5f);
                            randomModifierX *= (sprayIntensity * 16.0f);
                            randomModifierY *= (sprayIntensity * 16.0f);
                            Vector2 vectorToTarget = new Vector2((Main.player[npc.target].Center.X + randomModifierX) - npc.Center.X, (Main.player[npc.target].Center.Y + randomModifierY) - npc.Center.Y);
                            vectorToTarget.Normalize();
                            Projectile proj = Projectile.NewProjectileDirect(npc.GetSource_FromThis(), npc.Center, vectorToTarget, ProjectileID.PoisonSeedPlantera, damage, 0f, Main.myPlayer);
                            proj.timeLeft = 300;
                            proj.extraUpdates = 1;
                            proj.GetGlobalProjectile<WDALTProjectileUtil>().speedyPlanteraPoisonSeed = true;
                        }
                    }
                }
            }
        }
    }
}
