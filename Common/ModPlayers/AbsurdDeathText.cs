/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2026 LukasV-Coding

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
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace WeDoALittleTrolling.Common.ModPlayers
{
    internal class AbsurdDeathText : ModPlayer
    {
        private Player player;
        private static UnifiedRandom random = new UnifiedRandom();
        private static char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
        public int deathMessageChance1inX = 8;

        //Initializing´these in case for some reason they aren't set when they should be
        string playername = "playername";
        string enemyname = "enemyname";
        string worldname = "worldname";
        string aAnPlayer = "a";
        string aAnEnemy = "a";
        string pvpWasWere = "s were";

        string[] deathMessagesUnspecific;
        string[] deathMessagesSpecific;
        string[] finalDeathMessagePool;

        public override void Initialize()
        {
            player = this.Player;
        }


        // On kill logic method
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (random.NextBool(deathMessageChance1inX))
            {
                // Run logic for any death
                bool isSpecific = false;
                worldname = Main.worldName;
                playername = player.name;
                aAnPlayer = determineAAnPlayer(playername);
                buildUnspecificDeathMessages();

                // Run logic in case of specific deaths
                if (damageSource.SourceNPCIndex > -1 && damageSource.SourceNPCIndex < Main.npc.Length)
                {
                    enemyname = Main.npc[damageSource.SourceNPCIndex].FullName;
                    aAnEnemy = determineAAnEnemy(enemyname);
                    buildSpecificDeathMessages();
                    isSpecific = true;
                }

                if (damageSource.SourcePlayerIndex > -1 && damageSource.SourcePlayerIndex < Main.player.Length)
                {
                    enemyname = Main.player[damageSource.SourcePlayerIndex].name;
                    aAnEnemy = determineAAnEnemy(enemyname);
                    pvpWasWere = determinePvpWasWere(pvp);
                    buildSpecificDeathMessages();
                    isSpecific = true;
                }

                // Determine necessary length of final message pool and add messages
                if (!isSpecific)
                {
                    finalDeathMessagePool = new string[deathMessagesUnspecific.Length];
                    deathMessagesUnspecific.CopyTo(finalDeathMessagePool, 0);
                }
                else
                {
                    finalDeathMessagePool = new string[deathMessagesUnspecific.Length + deathMessagesSpecific.Length];
                    deathMessagesUnspecific.CopyTo(finalDeathMessagePool, 0);
                    deathMessagesSpecific.CopyTo(finalDeathMessagePool, deathMessagesUnspecific.Length);
                }

                // Set the actual death message from the final pool as the death text
                damageSource.CustomReason = NetworkText.FromLiteral(finalDeathMessagePool[random.Next(0, finalDeathMessagePool.Length)]);

                // Empty the arrays
                deathMessagesSpecific = Array.Empty<string>();
                deathMessagesUnspecific = Array.Empty<string>();
                finalDeathMessagePool = Array.Empty<string>();
            }

            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }





        // Method to determine whether a or an should be used for the player
        public string determineAAnPlayer(string nameToCheck)
        {
            string correctAAn = "an ";

            for (int i = 0; i < vowels.Length; i++)
            {
                if (nameToCheck.ToLower().StartsWith(vowels[i]))
                {
                    correctAAn = "an ";
                }
                else
                {
                    correctAAn = "a ";
                }
            }

            if (nameToCheck.ToLower().StartsWith("a ") || nameToCheck.ToLower().StartsWith("an "))
            {
                correctAAn = string.Empty;
            }

            return correctAAn;
        }

        // Method to determine whether a or an should be used for the player
        public string determineAAnEnemy(string nameToCheck)
        {
            string correctAAn = "an ";

            for (int i = 0; i < vowels.Length; i++)
            {
                if (nameToCheck.ToLower().StartsWith(vowels[i]))
                {
                    correctAAn = "an ";
                }
                else
                {
                    correctAAn = "a ";
                }
            }

            if (nameToCheck.ToLower().StartsWith("a ") || nameToCheck.ToLower().StartsWith("an "))
            {
                correctAAn = string.Empty;
            }

            return correctAAn;
        }

        // Method to determine whether was or were should be used in a death message in specific situations.
        // "Jesus christ was in the forest looking to see the trees, but Satan was there <-> but Red Devils were there." 
        public string determinePvpWasWere(bool isPvpDeath)
        {
            string correctWasWere = "s were";

            if (isPvpDeath)
            {
                correctWasWere = " was";
            }

            return correctWasWere;
        }


        //This is the part where the death message strings are built and the class thus becomes unoverlookable because there's just so many

        // Method to build unspecifc death message strings
        public void buildUnspecificDeathMessages()
        {
            deathMessagesUnspecific = new string[] 
            { 
            
                playername + " went to Brazil.",
                playername + " died. Must have been the wind.",
                playername + " mysteriously vanished.",
                playername + " accepted candy from the man in the white van.",
                playername + " is gone, reduced to atoms.",
                playername + " was not forgiven for their sins.",
                playername + " died of natural causes.",
                playername + " forgot the uhmmm, the umm, uhhhhh",
                playername + " died peacefully in their sleep.",
                playername + " died of nothing in particular.",
                playername + " was finally arrested for their war crimes.",
                playername + " experienced the consequences of banana overdose.",
                playername + " didn't know what happened on July Second.",
                playername + " ate the tasty uranium-235.",
                playername + " tried to run Path of Exile on hitpower's PC.",
                playername + " took an arrow to the knee.",
                playername + " was unfortunate enough to fall victim to our clever little trap of social teasing.",
                playername + " is probably breaking their keyboard right now.",
                playername + " told the truth to the psychiatrist.",
                playername + " made a severe and continuous lapse in their judgement.",
                playername + "'s honest reaction:",
                playername + " is trolled.",
                playername + "'s free trial of life expired.",
                playername + " was- actually, nevermind.",
                playername + " went to find their absent father.",
                "According to all known laws of aviation, " + playername + " is not able to fly.",
                "The voices in " + playername + "'s head got too loud.",             
                playername + " didn't pay their taxes and was caught by the IRS.",
                playername + " turned out to be a low intelligence specimen.",
                playername + " attempted to witness secrets sealed.",
                playername + " had never seen such bullshit before.",
                playername + " discovered the fire in the hole.",
                playername + " did not follow the 57 precepts.",
                playername + " was heinously assassinated by the toaster hiding in their bathtub.",
                playername + " fell into a very convenient hole.",
                playername + " did not ecologise.",
                playername + "'s this was stolen.",
                playername + " spontaneously combusted.",
                playername + " spontaneously imploded.",
                playername + " couldn't imagine Sisyphus happy.",
                playername + " will never financially recover from this.",
                playername + " just got incredibly lucky! Unfortunately, they're dead.",
                playername + " drank the funny juice under the sink.",
                playername + " googled en passant.",
                playername + " was in fact not too young to die.",
                playername + " forgot to bring cheese to the mines.",
                playername + " can neither confirm nor deny this information.",
                playername + " left the stove on.",
                playername + " wanted to become the #1 potato farmer in Skyblock.",
                playername + " goes on vacation, never comes back.",
                playername + " took the heartfelt advice of the man with the lightning.",
                playername + " was informed about the sponsor of " + worldname + ", RAID: Shadow Legends.",
                playername + " was ejected. " + playername + " was not an impostor.",
                playername + " was ejected. " + playername + " was an impostor.",
                playername + " has been tricked, " + playername + " has been backstabbed and quite possibly bamboozled.",
                "It was at that moment " + playername + " knew they screwed up.",
                "The corn has started to grow on " + playername + ".",
                "Is there a lore reason for why " + playername + " died? Are they stupid?",
                "All of " + playername + "'s towers were pickled.",
                "Spoiler: " + playername + " dies in Endgame.",
                "Well well well, " + playername + " appears to have fallen into a well.",
                "What was " + playername + " thinking?!",
                playername + " rolled a stone. It returned upon them.",
                playername + " dug a pit and fell therein.",
                playername + " is now the human equivalent of a participation award.",
                playername + " couldn't have just walked away...",
                playername + " was too devious, perhaps too mischievous.",
                playername + "'s foolish ambitions were put to rest.",
                playername + " tried to get online therapy.",
                "Ding dong, the wicked " + playername + " is dead.",
                "Oh how the turns have tabled on " + playername + ".",
                playername + " is trolled, they should probably just fold.",
                playername + " is an individual of lesser talent.",
                "That's right, " + playername + " goes into the square hole.",
                playername + " found a cost too great.",
                playername + " participated in a Magic the Noah gameshow.",
                playername + " blundered all their health points.",
                "How did " + playername + " die to THAT of all things?",
                "There was a zombie on " + playername + "'s lawn.",
                playername + " got too close to the truth.",
                playername + " is projecting.",
                playername + " is dreaming of wells now.",
                playername + "'s only winning move is not to play.",
                playername + " was forced to play League of Legends.",
                "To solve this situation I simply kill " + playername + ".",
                playername + " switched the babies at the hospital around.",
                playername + " died of death.",
                playername + " fell victim to the Pharaoh's curse.",
                playername + " should have aspired to the strength and certainty of steel.",
                "The harvest of " + playername + "'s deaths is very bountiful this year.",
                "Well, " + playername + " is not of passing skill.",
                "For some reason " + playername + " did not have legs in the proximity of their home.",
                playername + " made a minor spelling mistake. I win.",
                playername + " got Elden Ring-style backstabbed.",
                playername + " has been foiled again.",
                "Surely there was nothing " + playername + " could have done to avoid that.",
                "The allegations against " + playername + " turned out to be true.",
                "Why did " + playername + " cross the river Styx? Weed eater.",
                playername + " was ignorant to the Mind Goblin.",
                playername + " did not receive vital information from the Sugondese.",
                playername + "'s AC broke down in the middle of summer.",
                playername + " got cancelled out of Waterfowl Dance.",
                playername + " went to the store to get milk.",
                "The curse seeped to " + playername + "'s very soul.",
                playername + "'s part in this shall not be forgiven.",
                "Okay but... " + playername + " thought it would be funny!",
                "It's a shame " + playername + " didn't die 87 years ago.",
                playername + " fell off the lobter copter.",
                "Thank you, " + playername + ", for participating in this Aperture Science computer-aided Enrichment Center activity. Goodbye",
                "Hippity hoppity " + playername + "'s soul is now my property.",
                "And then the " + playername + " says:",
                "Is " + playername + " serious right meow :3",
                "Solid of scale you might be, foul " + playername + ", but riddled with holes is your rotten hide!",
                playername + " alerted the frog /!\\",
                "Where didst thou flee, my " + playername + "? Come out from whence you hide!",
                "Joker, It's " + aAnPlayer + playername + ", Joker, you can't!",
                "THERE'S NO LAWS AGAINST THE " + playername + ", BATMAN!",
                "Noo, " + playername + ", don’t leave me here!",
                "Please no, dear me! I haven't a clue, no " + playername + "s lie with me, not a one!",
                playername + " clearly doesn't own an air fryer.",
                playername + " has left the game.",
                playername + " will remember that.",
                playername + " accepted the definition of a word as some letters surrounded by a gap.",
                playername + " isn't the sharpest tool in the shed.",
                "Holy mother of bananas, " + playername + " has died!",
                playername + " accidentaly sent themselves to the fungal wastes.",
                "[" + playername + "] MISSED THEIR CHANCE TO BE A [Big Shot].",
                "Hello? Is anybody there? Someone who might be interested in rescuing the great " + playername + "?",
                "[" + playername + "]!!! YOU [Little Sponge]! I KNEW YOU'D COME HERE [[On A Saturday Night]]!",
                playername + " was enlightened to the lies of Big Oxygen.",
                "I caught a little "+ playername + ", Batman, I caught a little " + playername + "!",
                playername + " has a good heart, albeit insane; Condemn them to the infirmary.",
                playername + " was paid their monthly salary of 27.6 wells.",
                playername + " must away!",
                playername + " has been living a lie, a metamorphical scheme.",
                playername + " did not resist the temptation of Michael.",
                "Bungalay, bungalow make up your " + playername + " and tell me no.",
                "Well it's nine o'clock and it's getting dark and " + playername + " is falling from the sky.",
                playername + " left the stage of time with no answers to no questions.",
                playername + " was wished a bad odding.",
                "If this mirror were clear, " + playername + " would be standing so tall.",
                playername + " is as weak as a kitten and as thick as two planks.",
                playername + " played Sudoku to restore their honor.",
                "I want to play a game, " + playername + ". In your inventory there is a pipe bomb disguised as another item. I hope you have a very good memory.",
                "The loathsome " + playername + " eater!",
                "But I was saving " + playername + " for the grand finale!",
                "Okay, cut! " + playername + ", have you even read the script? Let's take that back.",
                "Would " + playername + " please stop complaining? I'm playing a song.",
                "It feels like flying, but maybe " + playername + " is dying.",
                playername + " returned to obscure, or wherever they were before they were.",
                playername + " was taught that images on the internet are stored on the internet and not on their computer.",
                playername + " was divided by zero.",
                playername + " gained three random buffs.",
                playername + " is the worst emoji. It's horrendous and ugly. I hate it. The point of emojis is to show emotions, but what emotion does this show?",
                "Evil " + playername + "s fall from trees.",
                "This " + playername + " cannot be used for [i:4024] cooking.",
                playername + " was dissected into 10 rhombi.",
                "octopus",
                "Kill this " + playername + " when they least expect it.",
                playername + " tomorrow!",
                playername + " witnessed the unfathomable horrors of GlobalDeathReason.cs.",
                "I suppose " + playername + " is quite content in their respawn screen. All you recently deceased are the same.",
                "Is this really the hill " + playername + " wants to die on?",
                "The Caligulas claim " + playername + ".",
                "Exacration of the names of the unworthy " + playername + "s!",
                "Does " + playername + " really think?",
                "An additional electron was added to every atom in " + playername + "'s body.",
                playername + " was ionized.",
                "They're eating " + playername + "'s flesh!",
                "If you miss the train " + playername + " is on, you will know that they are gone.",
                playername + " wished for two goldfish.",
                playername + " discovered that at the end of the day, it is night."

            };

        }


        // Method to build NPC death message strings
        public void buildSpecificDeathMessages()
        {
            deathMessagesSpecific = new string[]
            {
                playername + " was reminded of the incident by " + enemyname + ".",
                playername + " was informed of their skill issue by " + enemyname + ".",
                playername + " discovered that they were simply lacking the talent to overcome " + enemyname + ".",
                playername + " made a minor grammatical error, " + enemyname + " wins.",
                playername + "'s head was smashed in with a rock by " + enemyname + ".",
                playername + " died after hearing the horrendous joke made by " + enemyname + ".",
                playername + " rolled a Nat 1 on their saving throw against " + enemyname + ".",
                playername + " didn't expect the " + enemyname + " inquisition.",
                enemyname + " did not take " + playername + " winning at Jenga lightly.",
                playername + " ate the candy given to them by " + enemyname + ".",
                enemyname + " did not accept " + playername + "'s takeback.",
                playername + " believed " + enemyname + "'s claim about gullible being written on the ceiling.",
                playername + " pulled out the ukulele after being called out by " + enemyname + ".",
                playername + " fell victim to " + enemyname + "'s YouTube prank.",
                enemyname + " just wanted to talk to " + playername + ".",
                enemyname + " was the second worst thing to ever happen to " + playername + ".",
                playername + " had it writ upon their meagre grave: Felled by " + enemyname + "!",
                playername + " has been hit by, " + playername + " has been struck by " + enemyname + "!",
                playername + " was defeated by " + enemyname + ", blade of Moon Lord.",
                playername + " is" + enemyname + "'s and " + enemyname + "'s alone!",
                playername + " died. Or did they? Hey VSauce, " + enemyname + " here!",
                "Jingle jongie, it's time for " + enemyname + "! " + playername + " explodes violently.",
                "Who would win? " + playername + " or one lowly little " + enemyname + "? That's right, it's the " + enemyname + ".",
                "Curse you, " + enemyname + "! " + playername + " hereby vows: You will rue this day!",
                enemyname + " can see it as clear as day; The death of " + playername + "!",
                enemyname + " found " + playername + "'s joke pretty funny. " + playername + " dies \"accidentally\".",
                enemyname + " slid to the left, onto " + playername + "'s location. Cha cha real smooth.",
                playername + ", watch out! " + enemyname + "!",
                playername + "us *extremely loud " + enemyname + " music*",
                "I don't mean to alarm " + playername + ", but the leading cause of death over 40 is " + enemyname + ".",
                "Hand of " + enemyname + " has struck the " + playername + ".",
                "*tips " + playername + "* M'" + enemyname + ".",
                "Would you rather have unlimited " + enemyname + ", but no more " + playername + ", or " + playername + ", UNLIMITED " + playername + ", but no more " + playername + "?",
                "Whatever. Go, my " + enemyname,
                "But the lord laughs at the " + playername + ", for he knows their " + enemyname +" is coming.",
                "Blessed is the " + enemyname + " who seizes your " + playername + " and smashes them against the rock.",
                "The " + enemyname + "s joined the battle against " + playername + ", fighting only with bare hands and dictionaries.",
                "Noo, " + enemyname + ", don't leave me with " + playername + "! " + enemyname + "... " + enemyname + " HELP ME! NOO!",
                playername + "... I remember you're " + enemyname + "s.",
                "Making the mother of all omelettes here, " + enemyname + ", can't fret over every " + playername + ".",
                enemyname + " knew they'd been had, so it shot at the " + playername + " with a gun.",
                "These " + playername + "s are pissing me off. I'm the original        " + enemyname + ".",
                playername + ", welcome! The " + enemyname + "s have aligned, the festival is nigh!",
                playername + ", pauseth? Pauseth for a second? So I haveth " + aAnEnemy + enemyname + "e.",
                playername + " inquireth: What's a mind goblin, " + enemyname + "?",
                enemyname + ", may I have some " + playername + "s? I am STARVING, " + enemyname + ".",
                "The " + enemyname + "s demand repentance! Cough up your " + playername + ", all of it!",
                "Would you rather fight 10 " + enemyname + " sized " + playername + "s or 50 " + playername + " sized " + enemyname + "s?",
                playername + " understands, mechanical " + enemyname + "s are the ruler of everything in the end.",
                "If " + playername + " wishes to defeat " + enemyname + ", they will need to train for another " + player.numberOfDeathsPVE + " years.",
                "All the " + enemyname + "s in the tree, chant a tune to let " + playername + " free.",
                playername + " danced around the flame and got to play the " + enemyname + " game.",
                "Inside " + playername + " there are two " + enemyname + "s.",
                "The " + playername + " is gone, taken for a ride. Far away from " + enemyname + ", no longer left inside.",
                "Welcome to punch the " + playername + ", this game is sponsored by " + enemyname + ".",
                playername + " has come to the conclusion that they love and the hate " + enemyname + " and they cannot change it, so they must replace it.",
                enemyname + " was the second worst thing to ever happen to " + playername + ".",
                "Like a bumbling dragon " + enemyname + " flies, scraping " + playername + " on the skies.",
                "Kill all its " + playername + "s, wonder about no things, circles and " + enemyname + "s in mind.",
                "Do you want " + aAnEnemy + enemyname + "? This " + enemyname + " for " + playername + ".",
                "The " + enemyname + " didn't start the bonfire of the vanities, but it's throwing in our " + playername + "s and our humanities.",
                playername + " has another word to sell, another story to tell, another " + enemyname + " ringing the bell.",
                "You either die " + aAnPlayer + playername + " or live long enough to see yourself become the " + enemyname + ".",
                "I can’t wait, when are they gonna open up that door? " + playername + " is going (yes they’re) going, really going to the " + enemyname + " store!",
                playername + " happens to have died next to three " + enemyname + "s in a trenchoat.",
                "What a sight, " + aAnEnemy + enemyname + " true if ever one there was! A fellow " + playername + " warrior!",
                "If special " + playername + " then only " + enemyname + ".",
                enemyname + " deniers be like: Must have been the uhhh um the uhh...",
                "One secondary " + enemyname + ", to go that extra mile, to make " + playername + " feel today, to make them go away.",
                "Without " + aAnEnemy + enemyname + " or a rhyme, " + playername + " does not banana all the time.",
                "Tomorrow morning on the plane, " + enemyname + " makes " + playername + " go insane.",
                "Of course " + enemyname + " would deal 2 masks of damage to " + playername + ".",
                "I wonder whether "+ playername + " wonders if " + enemyname + " ever wonders, ever wonders.",
                playername + " thinks their answer isn't real, it's just a picture, of " + aAnEnemy + enemyname + ".",
                "Does " + enemyname + " want " + aAnPlayer + playername + "? Peel it down and go \"Mm-mmm mm-mmm\".",
                "The " + enemyname + "s seem to know where " + playername + " needs them to go, igniting a spark in their mind, so they circle and fly.",
                "You told me to buy " + aAnEnemy + enemyname + ", but all I wanted was " + playername + ".",
                enemyname + " undercover, " + playername + "'s health points obscene.",
                playername + " practices their " + enemyname + "isms into the wall.",
                enemyname + " to " + playername + " " + ((player.numberOfDeathsPVE % 8) + 1) + " #.",
                "Having enjoyed all the " + playername + " from the " + worldname + " is perfectly equal to being " + enemyname + ".",
                "Just a little bit of " + enemyname + "s to end " + worldname + ", does it even matter to " + playername + "?",
                "The " + enemyname + " are bountiful this time of " + worldname + ".",
                "Your " + playername + " just ran into " + aAnEnemy + enemyname + " and needs to respawn. We're just collecting some info and then we'll revive it for you.",
                "Can " + playername + " feel their heart burning? Can they feel the struggle within? The fear within is beyond anything their soul can make. They cannot kill " + enemyname + " in a way that matters.",
                playername + " is done living; " + enemyname + " has eaten.",
                "POV - " + playername + "'s " + enemyname + " is a " + worldname + "ian spy:",
                "Oh how the " + playername + "s have " + enemyname + "ed.",
                "The " + enemyname + " came down that day and it drained " + playername + "'s soul away.",
                "Then the Lord gave " + playername + " the ability to speak. \"What have I done to you that deserves your beating me three times?\" it asked " + enemyname + ".",
                enemyname + "s, " + playername + ", the DNA of the soul!",
                "The " + enemyname + " knows where it is at all times. It knows this because it knows where it isn't. By subtracting where it is, from where it isn't, or where it isn't, from where it is, whichever is greater, it obtains a difference, or " + playername + ".",
                "And because " + playername + " cannot remain unarmed, they must turn to " + aAnEnemy + enemyname + " military, which is of the quality described above.",
                playername + " hates them with perfect hatred; They count them their " + enemyname + "s.",
                "Can you believe it, " + enemyname + "? " + playername + ", just a week away! Oh wow, I am so happy about this information. " + playername + ", just a week away!",
                "Precept fifteen: One " + playername + ", one " + enemyname + ". You should only use a single " + enemyname + " to defeat " + aAnPlayer + playername + ". any more is a waste.",
                "What would " + playername + " prefer, would they like to fight for civil rights or tweet " + aAnEnemy + enemyname + " slur?",
                playername + " cannot have roots and branches, so that the first adverse " + enemyname + " eliminates them.",
                "\"Where's " + playername + "?\" Suspiciously " + playername + " shaped " + enemyname + ":",
                "The offense done to " + playername + " was such that " + enemyname + " did not have to fear revenge for it.",
                "And truly it is a very natural and ordinary thing to kill " + playername + ", and always, when " + enemyname + "s do it who can, they will be praised or not blamed.",
                "Once " + enemyname + " had won the war, there was nothing left to do except celebrate its victory with grace and humility: \"Watch me dance, " + playername + ", you lose!",
                "Are you telling me " + aAnEnemy + enemyname + " fried this " + playername + "?",
                playername + " would indeed get older, if " + enemyname + " didn't roll a boulder.",
                "Did " + playername + " hear about the " + enemyname + " that escaped the zoo? No? Well that's because it was a very quiet " + enemyname + ".",
                "I don't know why " + enemyname + " even tries. " + playername + " will be back in three seconds anyways.",
                playername + " challenged tender " + enemyname + ", only to have their own heart rather artfully stolen.",
                "All I require is your greatest " + enemyname + ". Which one, you ask? The one " + playername + " hates the most.",
                "We are sorry about the issue regarding " + playername + "s delivery. " + enemyname + " Inc. does not take warranty for any potential pipe bombs packages may have been replaced with.",
                "Does " + playername + " feel like a... little giggle... when I say the name 'Sussus " + enemyname + "us'?",
                "Your " + enemyname + " tricks " + playername + " into thinking.",
                enemyname + "work ahead? " + playername + " sure hopes it doesn't!",
                "Nice opinion, " + playername + ". One small issue: " + enemyname + ".",
                playername + " can't believe the news today, oh they can't close their eyes and make " + enemyname + " go away.",
                playername + " was converted to " + enemyname + "ism.",
                "Give " + playername + " an " + enemyname + " and you kill them once, teach " + playername + " to " + enemyname + " and they'll die for a lifetime.",
                enemyname + "s can kill " + playername + ", but if they don't they make them stronger.",
                "Now, " + playername + " was a murderer who had a problem with the " + enemyname + "s and as someone who is " + aAnEnemy + enemyname + " you find the whole thing quite offensive.",
                "As you know, " + playername + ", " + enemyname + " is very homophobic.",
                playername + " remained oblivious to two " + enemyname + "s disguised as chairs.",
                playername + " " + enemyname + "ed.",
                "Even the " + enemyname + "s know " + playername + " is brewing and they're all cooing.",
                enemyname + " [i:1254] " + playername,
                "With a thousand " + enemyname + "s and a good disguise, hit " + playername + " right between the eyes.",
                playername + " was forced to extend the " + enemyname + " lasagne.",
                "I miss my " + enemyname + ", " + playername + ", I miss it a lot. I'll be back.",
                enemyname + " tried to contact " + playername + " about their car's extended warranty.",
                playername + " is still amazed by " + enemyname + ".",
                "There were over " + ((player.numberOfDeathsPVE + 1) * 72) + " mentions of " + playername + " in the " + enemyname + " files.",
                playername + ", the " + enemyname + " is here.",
                enemyname + "! Tear down " + playername + "'s wife, tear down " + playername + "'s kids!",
                "Knowing one day " + playername + " might respawn and get revenge on " + enemyname + "... It fills you with determination.",
                playername + " was finally excommunicated from the church of " +  enemyname + ".",
                "True happiness for " + enemyname + " peaked when it finally managed to kill " + playername + ".",
                "There's no need for " + playername + ", this isn't a test. Just nod or shake your head and " + enemyname + " will do the rest.",
                "Would you look at all that stuff! They've got allen wrenches, gerbil feeders, toilet seats, electric heaters, trash compactors, juice extractors, shower rods and water meters, walkie-talkies, copper wires, safety goggles, radial tires, BB pellets, rubber mallets, fans and dehumidifiers, picture hangers, paper cutters, waffle irons, window shutters, paint removers, window louvers, masking tape and plastic gutters, kitchen faucets, folding tables, weather stripping, jumper cables, hooks and tackle, grout and spackle, power foggers, spoons and ladles, pesticides for fumigation, high-performance lubrication, metal roofing, waterproofing, multi-purpose insulation, air compressors, brass connectors, wrecking chisels, smoke detectors, tire gauges, hamster cages, thermostats and bug deflectors, trailer hitch demagnetizers, automatic circumcisers, tennis rackets, angle brackets, Duracells and Energizers, soffit panels, circuit breakers, vacuum cleaners, coffee makers, calculators, generators, matching " + enemyname + " and dead " + playername + " shakers.",
                enemyname + " discovered a way to play Bad Apple on " + playername + ".",
                enemyname + " discovered a way to run Doom on " + playername + ".",
                playername + " gave in to temptation and clicked the ad about loot-filled " + enemyname + "s in their area.",
                "Coming straight from " + playername + "'s house, " + enemyname + " is groovy, but never glooby!",
                enemyname + " killed " + playername + "- Oops, other way aroun- wait, no, nevermind.",
                "Standing here " + enemyname + " realizes it was just like " + playername + ", trying to avoid the respawn screen.",
                enemyname + "'s winning move would be remembered for centuries to come as the fried " + playername + " attack.",
                playername + " coughed. The " + enemyname + " tilted its head.",
                playername + " ate the forbidden " + enemyname + " fruit.",
                "Hello? " + playername + " artist? I would like to commission one " + enemyname + "!",
                "The government of " + worldname + " found out about " + enemyname + "'s group chat with " + playername + ".",
                playername + " once, " + playername + " twice, every " + enemyname + " has its vice.",
                "You're telling me " + aAnEnemy + enemyname + " fried this " + playername + "?",
                "See " + playername + " get beheaded, get offended, see " + aAnEnemy + enemyname + ".",
                "Show us pictures of your " + enemyname + ", tell us every thought " + playername + " thinks.",
                "The square of " + playername + "'s deaths is equal to the product of its projection to the hypotenuse and its " + enemyname + ".",
                playername + " was among the one third of those who suffer from " + enemyname + "-pattern baldness.",
                enemyname + " is the " + worldname + " conditioning man, and it's keeping " + playername + " dead with its giant fan.",
                enemyname + ", quick! Grab " + playername + "! Don't let this kraken be a squidnapper!",
                "Ooh, live the dream with " + aAnEnemy + enemyname + " machine, " + playername + " has been waiting forever.",
                "Crazy? " + playername + " was crazy once. They locked " + playername + " in a room. A rubber room. A rubber room with " + enemyname + "s. And " + enemyname + "s make " + playername + " crazy.",
                "99% of " + enemyname + "s quit right before they manage to kill " + playername + ".",
                playername + "! Your " + enemyname + " has returned! It brings the destruction of " + worldname + ".",
                "Why did murderous " + enemyname + "s survive? Why did " + playername + " deserve to be revived?",
                playername + " was sent home by " + enemyname + ", Host of " + worldname + ". Returning to their world.",
                playername + " was in " + worldname + ", wearying of the " + enemyname + "s, hate me not. Wait, they forgot. Woe, oh the rot.",
                "The foul "  + enemyname + " is devouring its wretched " + playername + ".",
                "Meanwhile " + enemyname + "? It's going back to " + worldname + ", it's been eating mush all week and it's time for " + playername + "!",
                "In the night of the black " + enemyname + "s, " + playername + " was first to perish.",
                playername + "'s detested sport to play is die in narrow crevice. Here are the rules: You need a narrow crevice and " + aAnEnemy + enemyname + " and an asbestos. " + playername + " is going to go play it now.",
                playername + " calculated that the limit of their death count as it approaches " + enemyname + " is positive and boundless.",
                playername + " is dead. " + worldname + " is full. " + enemyname + "s are fuel.",
                playername + "'s " + enemyname + "s were many, and their days few.",
                "Ok jump, to end " + playername + ", let this " + enemyname + " be to break the fall.",
                playername + " acknowledged the possibility of " + enemyname + " and seizure.",
                playername + " played a simple numbers game with " + enemyname + ".",
                "Hey guys, I think " + playername + " found " + aAnEnemy + enemyname + "!",
                "Once upon a time there was light in " + playername + "'s heart, now there's only " + enemyname + " in the dark.",
                "And I think it would be totally cool if " + enemyname + " hung around " + playername + "'s apartment and enrolled in their school.",
                playername + " had " + aAnEnemy + enemyname + " they hadn't felt. Why would it hurt them, or was it real?",
                playername + " makes even the " + enemyname + "s cry!",
                "Forget all of your " + enemyname + "s and go with the flow, forget about whatever " + playername + " may never know.",
                "O lifeless " + playername + ", embrace thine " + enemyname + "s, as shall I.",
                "Even a broken " + enemyname + " kills " + playername + " twice a day.",
                playername + " foolishly allowed " + enemyname + " to triangulate their mother.",
                enemyname + " does not decide who is right, only whether " + playername + " is left.",
                "Commence " + playername + "'s mass " + enemyname + "ing!",
                "It doesn't make sense. Why would " + enemyname + " kill " + playername + "? After all, they're just the drummer.",
                "There will be " + playername + "-shed, the " + enemyname + " in the mirror nods its head.",
                playername + " was in the forest looking to see the trees, but " + enemyname + pvpWasWere + " there.",
                "I was in " + worldname + " looking to find the fountain of infinite " + enemyname + "s. " + playername + " falling, no one would hear.",
                "Hello, we are about to launch an all out attack on " + playername + ". Sincerely, the " + enemyname + "s.",
                playername + ", my love... I shall gift to you all the " + enemyname + "s that can exist, and many that cannot.",
                "O " + playername + ", now dawns thy reckoning and thy gore shall glisten before the temples of " + enemyname + ".",
                "Before " + enemyname + " tears down the cities and crushes the armies of " + worldname + "... " + playername + " shall do as an appetizer.",
                "So concludes the life and times of " + playername + ". A fitting end to an existence defined by futile " + enemyname + "s.",
                "Help for the " + worldname + " game: When the " + enemyname + "s show up, just sit there and don't do anything. You win the game when the " + enemyname + "s get to " + playername + ".",
                "Might there even be... " + aAnEnemy + enemyname + " within " + playername + "'s walls?",
                "Before " + enemyname + " " + enemyname + " " + enemyname + ", " + enemyname + " " + enemyname + " " + playername + ".",
                enemyname  + " clearly owns " + aAnPlayer + playername + " fryer.",
                "The exit shown to " + playername + " by " + enemyname + " led to the disintegration loop."
            };
        }
    }
}