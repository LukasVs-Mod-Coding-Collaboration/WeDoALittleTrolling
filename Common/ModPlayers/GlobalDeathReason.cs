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
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Utilities;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Items.Accessories;
using SteelSeries.GameSense;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.NPCs;

namespace WeDoALittleTrolling.Common.ModPlayers
{
    internal class GlobalDeathReason : ModPlayer
    {
        public Player player;
        public static UnifiedRandom random = new UnifiedRandom();

        public override void Initialize()
        {
            player = this.Player;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if(damageSource.SourceNPCIndex > -1 && damageSource.SourceNPCIndex < Main.npc.Length) //Check if PlayerDeathEvent was triggered by a NPC
            {
                int[] LeFisheIDs =
                {
                    NPCID.FungoFish,
                    NPCID.Piranha,
                    NPCID.AnglerFish,
                    NPCID.DukeFishron,
                    NPCID.FlyingFish,
                    NPCID.CorruptGoldfish,
                    NPCID.CrimsonGoldfish,
                    NPCID.Arapaima
                };

                int[] BeeIDs =
                {
                    NPCID.Bee,
                    NPCID.BeeSmall,
                    NPCID.QueenBee
                };

                int[] BoCIDs =
                {
                    NPCID.BrainofCthulhu,
                    NPCID.Creeper
                };

                int[] BlubbyIDs =
                {
                    NPCID.Plantera,
                    NPCID.PlanterasHook,
                    NPCID.PlanterasTentacle
                };

                int[] SansIDs =
                {
                    NPCID.SkeletronHead,
                    NPCID.SkeletronHand,
                    NPCID.Skeleton,
                    NPCID.PrimeSaw,
                    NPCID.PrimeVice
                };

                if (LeFisheIDs.Contains(Main.npc[damageSource.SourceNPCIndex].type) && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " was tragically slain by le fishe.";
                }
                if (Main.npc[damageSource.SourceNPCIndex].type == NPCID.TheDestroyer && random.NextBool(4))
                {
                    damageSource.SourceCustomReason = player.name + " was eated.";
                }
                if (BeeIDs.Contains(Main.npc[damageSource.SourceNPCIndex].type) && random.NextBool(100))
                {
                    damageSource.SourceCustomReason = player.name + " does not prefer their bees roasted.";
                }
                if (Main.npc[damageSource.SourceNPCIndex].type == NPCID.Gnome && random.NextBool(3))
                {
                    damageSource.SourceCustomReason = player.name + " was gnomed.";
                }
                if (Main.npc[damageSource.SourceNPCIndex].type == NPCID.SkeletonSniper && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " discovered the meaning of stream sniping.";
                }
                if (BoCIDs.Contains(Main.npc[damageSource.SourceNPCIndex].type) && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " had a seizure and passed away.";
                }
                if (BlubbyIDs.Contains(Main.npc[damageSource.SourceNPCIndex].type) && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " tried to touch grass.";
                }
                if (SansIDs.Contains(Main.npc[damageSource.SourceNPCIndex].type) && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " had a bad time.";
                }
                if (Main.npc[damageSource.SourceNPCIndex].type == NPCID.SkeletronPrime && random.NextBool(5))
                {
                    damageSource.SourceCustomReason = player.name + " was forced to work at amazon.";
                }
            }
            if (damageSource.SourceProjectileType == ProjectileID.SniperBullet && random.NextBool(5))
            {
                damageSource.SourceCustomReason = player.name + " discovered the meaning of stream sniping.";
            }
            if (damageSource.SourceProjectileType == ProjectileID.Boulder && random.NextBool(5))
            {
                damageSource.SourceCustomReason = player.name + " was mistaken for a bowling pin by a Boulder.";
            }

            //very rare, absurd death reasons
            string[] AbsurdDeathsUnspecific =
            {
                player.name + " went to Brazil.",
                player.name + " died. Must have been the wind.",
                player.name + " mysteriously vanished.",
                player.name + " accepted candy from the man in the white van.",
                player.name + " noclipped into the backrooms.",
                player.name + " is gone, reduced to atoms.",
                "According to all known laws of aviation, " + player.name + " is not able to fly.",
                player.name + " was not forgiven for their sins.",
                player.name + " died of natural causes.",
                player.name + " forgot the uhmmm, the umm, uhhhhh",
                player.name + " died peacefully in their sleep.",
                player.name + " died of nothing in particular.",
                player.name + " was finally arrested for their war crimes.",
                player.name + " experienced the consequences of banana overdose.",
                player.name + " didn’t know what happened on July Second.",
                player.name + " decided to wear a paladin's shield during the daylight Empress of Light fight.",
                player.name + " ate the tasty uranium-235.",
                player.name + " tried to run Path of Exile on hitpower‘s PC.",
                player.name + " took an arrow to the knee.",
                player.name + " was unfortunate enough to fall victim to our clever little trap of social teasing.",
                player.name + " is probably breaking their keyboard right now.",
                player.name + " forgot about timezones.",
                player.name + " told the truth to the psychiatrist.",
                player.name + " made a severe and continuous lapse in their judgement.",
                player.name + "'s honest reaction:",
                player.name + " is trolled.",
                player.name + " got morbed to death.",
                player.name + " was not that guy.",
                "The voices in " + player.name + "'s head got too loud.",
                player.name + " was- actually, nevermind.",
                player.name + " didn’t pay their taxes and was caught by the IRC.",
                player.name + "'s free trial of life expired.",
                "Monday left " + player.name + " broken.",
                player.name + " got cancelled on the internet.",
                player.name + " got ratioed.",
                player.name + " entered the void.",
                player.name + " turned out to be a low intelligence specimen.",
                player.name + " went to find their absent father."
            };
            if(damageSource.SourceNPCIndex > -1 && damageSource.SourceNPCIndex < Main.npc.Length) //Check if PlayerDeathEvent was triggered by a NPC
            {
                string[] AbsurdDeathsByNPC =
                {
                    player.name + " was reminded of the incident by " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    player.name + " was informed of their skill issue by " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    player.name + " discovered that they were simply lacking the talent to overcome " + Main.npc[damageSource.SourceNPCIndex].FullName + "."
                };
                if (random.NextBool(1))
                {
                    damageSource.SourceCustomReason = AbsurdDeathsByNPC[random.Next(0, AbsurdDeathsByNPC.Length)];
                }
            }
            else if (random.NextBool(1))
            {
                damageSource.SourceCustomReason = AbsurdDeathsUnspecific[random.Next(0, AbsurdDeathsUnspecific.Length)];
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
    }
}