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

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if(damageSource.SourceNPCIndex > -1 && damageSource.SourceNPCIndex < Main.npc.Length) //Check if PlayerDeathEvent was triggered by a NPC
            {
               

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

                // First Generation
                player.name + " went to Brazil.",
                player.name + " died. Must have been the wind.",
                player.name + " mysteriously vanished.",
                player.name + " accepted candy from the man in the white van.",
                player.name + " noclipped into the backrooms.",
                player.name + " is gone, reduced to atoms.",
                player.name + " was not forgiven for their sins.",
                player.name + " died of natural causes.",
                player.name + " forgot the uhmmm, the umm, uhhhhh",
                player.name + " died peacefully in their sleep.",
                player.name + " died of nothing in particular.",
                player.name + " was finally arrested for their war crimes.",
                player.name + " experienced the consequences of banana overdose.",
                player.name + " didn't know what happened on July Second.",
                player.name + " decided to wear a paladin's shield during the daylight Empress of Light fight.",
                player.name + " ate the tasty uranium-235.",
                player.name + " tried to run Path of Exile on hitpower's PC.",
                player.name + " took an arrow to the knee.",
                player.name + " was unfortunate enough to fall victim to our clever little trap of social teasing.",
                player.name + " is probably breaking their keyboard right now.",
                player.name + " forgot about timezones.",
                player.name + " told the truth to the psychiatrist.",
                player.name + " made a severe and continuous lapse in their judgement.",
                player.name + "'s honest reaction:",
                player.name + " is trolled.",
                player.name + "'s free trial of life expired.",
                player.name + " was- actually, nevermind.",
                player.name + " got cancelled on the internet.",
                player.name + " got ratioed.",
                player.name + " went to find their absent father.",
                "According to all known laws of aviation, " + player.name + " is not able to fly.",
                "The voices in " + player.name + "'s head got too loud.",
                "Monday left " + player.name + " broken.",               
                // First Generation Third Party Suggestions
                player.name + " was morbed to death.",
                player.name + " was not that guy.",
                player.name + " didn't pay their taxes and was caught by the IRS.",
                player.name + " entered the void.",
                player.name + " turned out to be a low intelligence specimen.",
                // Second Generation
                player.name + " was unable to operate a calculator.",
                player.name + " couldn't endure the harshest punishment.",
                player.name + " had never seen such bullshit before.",
                player.name + " discovered the fire in the hole.",
                player.name + " did not follow the 57 precepts.",
                player.name + " was heinously assassinated by the toaster hiding in their bathtub.",
                player.name + " fell into a very convenient hole.",
                player.name + " did not ecologise.",
                player.name + "'s this was stolen.",
                player.name + " spontaneously combusted.",
                player.name + " spontaneously imploded.",
                player.name + " couldn't imagine Sisyphus happy.",
                player.name + " attempted to witness secrets sealed.",
                player.name + " was lost in the fire.",
                player.name + " will never financially recover from this.",
                player.name + " just got incredibly lucky! Unfortunately, they're dead.",
                player.name + " drank the funny juice under the sink.",
                player.name + " found out about the truth.",
                player.name + " googled en passant.",
                player.name + " was in fact not too young to die.",
                player.name + " forgot to bring cheese to the mines.",
                player.name + " can neither confirm nor deny this information.",
                player.name + " left the stove on.",
                player.name + " wanted to become the #1 potato farmer in Skyblock.",
                player.name + " goes on vacation, never comes back.",
                player.name + " took the heartfelt advice of the man with the lightning.",
                player.name + " was informed about the sponsor of " + Main.worldName + ", RAID: Shadow Legends.",
                player.name + " was ejected. " + player.name + " was not an impostor.",
                player.name + " was ejected. " + player.name + " was an impostor.",
                player.name + " has been tricked, " + player.name + " has been backstabbed and quite possibly bamboozled.",
                "It was at that moment " + player.name + " knew they screwed up.",
                "The corn has started to grow on " + player.name + ".",
                "Is there a lore reason for why " + player.name + " died? Are they stupid?",
                "All of " + player.name + "'s towers were pickled.",
                "Spoiler: " + player.name + " dies in Endgame.",
                "Well well well, " + player.name + " appears to have fallen into a well.",
                "What was " + player.name + " thinking?!",
                // Third Generation Or Random
                player.name + " rolled a stone. It returned upon them.",
                player.name + " dug a pit and fell therein.",
                player.name + " is now the human equivalent of a participation award.",
                player.name + " couldn't have just walked away...",
                player.name + " was too devious, perhaps too mischievous.",
                player.name + "'s foolish ambitions were put to rest.",
                player.name + " tried to get online therapy.",
                "Ding dong, the wicked " + player.name + " is dead.",
                "Oh how the turns have tabled on " + player.name + ".",
                player.name + " is trolled, they should probably just fold.",
                player.name + " is an individual of lesser talent.",
                "That's right, " + player.name + " goes into the square hole.",
                player.name + " found a cost too great.",
                player.name + " participated in a Magic the Noah gameshow.",
                player.name + " blundered all their health points.",
                "How did " + player.name + " die to THAT of all things?",
                "There was a zombie on " + player.name + "'s lawn.",
                player.name + " got too close to the truth.", // Schmiddi!
                player.name + " is projecting.",
                player.name + " is dreaming of wells now.",
                player.name + "'s only winning move is not to play.",
                player.name + " got taken to a nice farm upstate.", // Schmiddi
                player.name + " was forced to play League of Legends.",
                "To solve this situation I simply kill " + player.name + ".",
                player.name + " switched the babies at the hospital around.",
                player.name + " died of death.",
                player.name + " fell victim to the Pharaoh's curse.",
                player.name + " should have aspired to the strength and certainty of steel.",
                "The harvest of " + player.name + "'s deaths is very bountiful this year.",
                "Well, " + player.name + " is not of passing skill.",
                "For some reason " + player.name + " did not have legs in the proximity of their home.",
                player.name + " made a minor spelling mistake. I win.",
                player.name + " got Elden Ring-style backstabbed.",
                player.name + " has been foiled again.",
                player.name + " lagged, trust me!",
                "Surely there was nothing " + player.name + " could have done to avoid that.",
                "The allegations against " + player.name + " turned out to be true.",
                "Why did " + player.name + " cross the river Styx? Weed eater.",
                player.name + " was ignorant to the Mind Goblin.",
                player.name + " did not receive vital information from the Sugondese.",
                player.name + "'s AC broke down in the middle of summer.",
                player.name + " got cancelled out of Waterfowl Dance.",
                player.name + " went to the store to get milk.",
                "The curse seeped to " + player.name + "'s very soul.",
                "Okay but... " + player.name + " thought it would be funny!",
                "It's a shame " + player.name + " didn't die 87 years ago.",
                player.name + " fell off the lobter copter.",
                "Thank you, " + player.name + ", for participating in this Aperture Science computer-aided Enrichment Center activity. Goodbye",
                "Hippity hoppity " + player.name + "'s soul is now my property."


            };

            if (damageSource.SourceNPCIndex > -1 && damageSource.SourceNPCIndex < Main.npc.Length) //Check if PlayerDeathEvent was triggered by a NPC
            {

                string[] AbsurdDeathsByNPC =
{
                    player.name + " was reminded of the incident by " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    player.name + " was informed of their skill issue by " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    player.name + " discovered that they were simply lacking the talent to overcome " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    player.name + " made a minor grammatical error, " + Main.npc[damageSource.SourceNPCIndex].FullName + " wins.",
                    player.name + "'s head was smashed in with a rock by " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    player.name + " died after hearing the horrendous joke made by " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    player.name + " rolled a Nat 1 on their saving throw against " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    player.name + " didn't expect the " + Main.npc[damageSource.SourceNPCIndex].FullName + " inquisition.",
                    Main.npc[damageSource.SourceNPCIndex].FullName + " did not take " + player.name + " winning at Jenga lightly.",
                    player.name + " ate the candy given to them by " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    Main.npc[damageSource.SourceNPCIndex].FullName + " did not accept " + player.name + "'s takeback.",
                    player.name + " believed " + Main.npc[damageSource.SourceNPCIndex].FullName + "'s claim about gullible being written on the ceiling.",
                    player.name + " pulled out the ukulele after being called out by " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    player.name + " fell victim to " + Main.npc[damageSource.SourceNPCIndex].FullName + "'s YouTube prank.",
                    player.name + " was eliminated from a Mr. Beast challenge by " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    Main.npc[damageSource.SourceNPCIndex].FullName + " just wanted to talk to " + player.name + ".",
                    Main.npc[damageSource.SourceNPCIndex].FullName + " was the second worst thing to ever happen to " + player.name + ".",
                    player.name + " had it writ upon their meagre grave: Felled by" + Main.npc[damageSource.SourceNPCIndex].FullName + "!",
                    player.name + " has been hit by, " + player.name + " has been struck by " + Main.npc[damageSource.SourceNPCIndex].FullName + "!",
                    player.name + " was defeated by " + Main.npc[damageSource.SourceNPCIndex].FullName + ", blade of Moon Lord.",
                    player.name + " is" + Main.npc[damageSource.SourceNPCIndex].FullName + "'s and " + Main.npc[damageSource.SourceNPCIndex].FullName + "'s alone!",
                    player.name + " died. Or did they? Hey VSauce, " + Main.npc[damageSource.SourceNPCIndex].FullName + " here!",
                    "Jingle jongie, it's time for " + Main.npc[damageSource.SourceNPCIndex].FullName + "! " + player.name + " explodes violently.",
                    "Who would win? " + player.name + " or one lowly little " + Main.npc[damageSource.SourceNPCIndex].FullName + "? That's right, it's the " + Main.npc[damageSource.SourceNPCIndex].FullName + ".",
                    "Curse you," + Main.npc[damageSource.SourceNPCIndex].FullName + "! " + player.name + " hereby vows: You will rue this day!",
                    Main.npc[damageSource.SourceNPCIndex].FullName + " can see it as clear as day; The death of " + player.name + "!",
                    Main.npc[damageSource.SourceNPCIndex].FullName + " found " + player.name + "'s joke pretty funny. " + player.name + " dies \"accidentally\".",
                    Main.npc[damageSource.SourceNPCIndex].FullName + " slid to the left, onto " + player.name + "'s location. Cha cha real smooth."
                };

                if (random.NextBool(15))
                {
                    if(random.NextBool(3))
                    {
                        damageSource.SourceCustomReason = AbsurdDeathsByNPC[random.Next(0, AbsurdDeathsByNPC.Length)];
                    }
                    else
                    {
                        damageSource.SourceCustomReason = AbsurdDeathsUnspecific[random.Next(0, AbsurdDeathsUnspecific.Length)];
                    }
                }
            }
            else if (random.NextBool(15))
            {
                damageSource.SourceCustomReason = AbsurdDeathsUnspecific[random.Next(0, AbsurdDeathsUnspecific.Length)];
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
    }
}