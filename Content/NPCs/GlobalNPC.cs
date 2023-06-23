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
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Content.Items.Accessories;
using Microsoft.Xna.Framework;

namespace WeDoALittleTrolling.Content.NPCs
{
    internal class GlobalNPCs : GlobalNPC
    {
        public override bool InstancePerEntity => false;
        public static int[] NerfGroup25Percent =
        {
            NPCID.Derpling,
            NPCID.Antlion,
            NPCID.WalkingAntlion,
            NPCID.GiantWalkingAntlion,
            NPCID.TombCrawlerHead,
            NPCID.JungleCreeper,
            NPCID.JungleCreeperWall,
            NPCID.GiantMossHornet,
            NPCID.BigMossHornet,
            NPCID.MossHornet,
            NPCID.LittleMossHornet,
            NPCID.TinyMossHornet
        };
        public static int[] NerfGroup35Percent =
        {
            NPCID.GiantTortoise,
            NPCID.IceTortoise
        };
        public static int[] NerfGroup50Percent =
        {
            NPCID.RedDevil
        };
        public static int[] KnockbackResistanceGroup =
        {
            NPCID.AngryTrapper
        };
        public static int[] InflictVenomDebuff1In1Group =
        {
            NPCID.AngryTrapper,
            NPCID.Moth
        };
        public static int[] InflictPoisonDebuff1In1Group =
        {
            NPCID.Snatcher,
            NPCID.ManEater
        };
        public static int[] InflictBleedingDebuff1In1Group =
        {
            NPCID.Shark,
            NPCID.SandShark
        };
        public static int[] InflictBleedingDebuff1In8Group =
        {
            NPCID.Herpling,
            NPCID.Wolf,
            NPCID.PirateCorsair,
            NPCID.PirateGhost
        };

        public override void SetDefaults(NPC npc)
        {
            if(NerfGroup25Percent.Contains(npc.type))
            {
                npc.damage = (int)Math.Round(npc.damage * 0.75);
            }
            if(NerfGroup35Percent.Contains(npc.type))
            {
                npc.damage = (int)Math.Round(npc.damage * 0.65);
            }
            if(KnockbackResistanceGroup.Contains(npc.type))
            {
                npc.knockBackResist = 0f;
            }
            base.SetDefaults(npc);
        }

        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if(npc.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                drawColor.R = 255;
                drawColor.G = 191;
                drawColor.B = 0;
                Random rnd = new Random();
                int xOffset = rnd.Next(-(npc.width/2), (npc.width/2));
                int yOffset = rnd.Next(-(npc.height/2), (npc.height/2));
                Vector2 dustPosition = npc.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                int dustType = rnd.Next(0, 5);
                switch(dustType)
                {
                    case 0:
                        Dust.NewDust(dustPosition, 10, 10, DustID.SolarFlare);
                        break;
                    case 1:
                        Dust.NewDust(dustPosition, 10, 10, DustID.Ash);
                        break;
                    default:
                        break;
                }
                
            }
            base.DrawEffects(npc, ref drawColor);
        }
        
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if(npc.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                modifiers.SourceDamage *= SearingInferno.damageNerfMultiplier;
            }
            base.ModifyHitPlayer(npc, target, ref modifiers);
        }

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            Random random = new Random();
            if(InflictVenomDebuff1In1Group.Contains(npc.type))
            {
                if(random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Venom, 240, false); //4s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictPoisonDebuff1In1Group.Contains(npc.type))
            {
                if(random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Poisoned, 240, false); //4s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictBleedingDebuff1In1Group.Contains(npc.type))
            {
                if(random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Bleeding, 960, false); //16s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictBleedingDebuff1In8Group.Contains(npc.type))
            {
                if(random.Next(0, 8) == 0) //1 in 8 Chance
                {
                    target.AddBuff(BuffID.Bleeding, 480, false); //8s, X2 in Expert, X2.5 in Master
                }
            }
            base.OnHitPlayer(npc, target, hurtInfo);
        }
        
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.ChaosElemental)
            {
                int chanceNumerator = 1; // 1% chance
                int chanceDenominator = 100;
                TryModifyRodOfDiscordDropChance(npcLoot, chanceNumerator, chanceDenominator);
            }
            if
            (
                npc.type == NPCID.Bee ||
                npc.type == NPCID.BeeSmall
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 1; // 1 in
                int chanceDenominator = 3; // 3 chance
                int itemID = ModContent.ItemType<Consumablebee>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }

            //Icy fossil drops

            if
            (
                npc.type == NPCID.PigronCorruption ||
                npc.type == NPCID.PigronCrimson ||
                npc.type == NPCID.PigronHallow
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 20; // 20% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<IcyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.ArmoredViking ||
                npc.type == NPCID.IceTortoise ||
                npc.type == NPCID.IceElemental ||
                npc.type == NPCID.IcyMerman
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 10; // 10% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<IcyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.IceMimic ||
                npc.type == NPCID.IceGolem
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 40; // 40% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<IcyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }

            //Dusty fossil drops

            if
            (
                npc.type == NPCID.DesertGhoul ||
                npc.type == NPCID.DesertGhoulCorruption ||
                npc.type == NPCID.DesertGhoulCrimson ||
                npc.type == NPCID.DesertGhoulHallow
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 20; // 20% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<DustyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.DesertBeast || //Basilisk
                npc.type == NPCID.DesertScorpionWalk || //Sand Poacher
                npc.type == NPCID.DesertScorpionWall || //Sand Poacher
                npc.type == NPCID.DesertLamiaDark ||
                npc.type == NPCID.DesertLamiaLight ||
                npc.type == NPCID.DuneSplicerHead
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 10; // 10% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<DustyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.DesertDjinn || //Desert Spirit
                npc.type == NPCID.SandElemental
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 40; // 40% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<DustyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }

        //Modify Rod of Discord drop chance. Are you kidding me, Re-Logic???!!!
        
        public static void TryModifyRodOfDiscordDropChance(NPCLoot npcLoot, int newChanceNumerator, int newChanceDenominator)
        {
            try
            {
                foreach (IItemDropRule rule in npcLoot.Get())
                {
                    if (rule is LeadingConditionRule leadingConditionRule)
                    {
                        try
                        {
                            foreach (IItemDropRuleChainAttempt chainedRuleAttempt in leadingConditionRule.ChainedRules)
                            {
                                try
                                {
                                    IItemDropRule chainedRule = chainedRuleAttempt.RuleToChain;
                                    if (chainedRule is CommonDrop drop)
                                    {
                                        if (drop.itemId == ItemID.RodofDiscord)
                                        {
                                            drop.chanceNumerator = newChanceNumerator;
                                            drop.chanceDenominator = newChanceDenominator;
                                        }
                                    }
                                    if (chainedRule is DropBasedOnExpertMode expertDropRule)
                                    {
                                        if (expertDropRule.ruleForNormalMode is CommonDrop normalDrop)
                                        {
                                            if (normalDrop.itemId == ItemID.RodofDiscord)
                                            {
                                                normalDrop.chanceNumerator = newChanceNumerator;
                                                normalDrop.chanceDenominator = newChanceDenominator;
                                            }
                                        }
                                        if (expertDropRule.ruleForExpertMode is CommonDrop expertDrop)
                                        {
                                            if (expertDrop.itemId == ItemID.RodofDiscord)
                                            {
                                                expertDrop.chanceNumerator = newChanceNumerator;
                                                expertDrop.chanceDenominator = newChanceDenominator;
                                            }
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }
    }
}
