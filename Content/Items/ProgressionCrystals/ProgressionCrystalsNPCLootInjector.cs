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

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;

namespace WeDoALittleTrolling.Content.Items.ProgressionCrystals
{
    internal class ProgressionCrystalsNPCLootInjector : GlobalNPC
    {
        public override bool InstancePerEntity => false;
        
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            int itemID = ItemID.None;
            bool addLoot = false;
            switch (npc.type)
            {
                case NPCID.KingSlime:
                    itemID = ModContent.ItemType<BlueCrystal>();
                    addLoot = true;
                    break;
                case NPCID.EyeofCthulhu:
                    itemID = ModContent.ItemType<LightRedCrystal>();
                    addLoot = true;
                    break;
                case NPCID.BrainofCthulhu:
                case NPCID.EaterofWorldsHead:
                case NPCID.EaterofWorldsBody:
                case NPCID.EaterofWorldsTail:
                    itemID = ModContent.ItemType<PurpleCrystal>();
                    addLoot = true;
                    break;
                case NPCID.QueenBee:
                    itemID = ModContent.ItemType<OrangeCrystal>();
                    addLoot = true;
                    break;
                case NPCID.SkeletronHead:
                    itemID = ModContent.ItemType<GrayCrystal>();
                    addLoot = true;
                    break;
                case NPCID.Deerclops:
                    itemID = ModContent.ItemType<CyanCrystal>();
                    addLoot = true;
                    break;
                case NPCID.WallofFlesh:
                    itemID = ModContent.ItemType<RedCrystal>();
                    addLoot = true;
                    break;
                case NPCID.QueenSlimeBoss:
                    itemID = ModContent.ItemType<PinkCrystal>();
                    addLoot = true;
                    break;
                case NPCID.SkeletronPrime:
                case NPCID.Retinazer:
                case NPCID.Spazmatism:
                case NPCID.TheDestroyer:
                    itemID = ModContent.ItemType<WhiteCrystal>();
                    addLoot = true;
                    break;
                case NPCID.Plantera:
                    itemID = ModContent.ItemType<LimeCrystal>();
                    addLoot = true;
                    break;
                case NPCID.DukeFishron:
                    itemID = ModContent.ItemType<GreenCrystal>();
                    addLoot = true;
                    break;
                default:
                    addLoot = false;
                    break;
            }
            if(addLoot)
            {
                Conditions.LegacyHack_IsABoss condition = new Conditions.LegacyHack_IsABoss();
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 1;
                int chanceDenominator = 1;
                ItemDropWithConditionRule drop = new ItemDropWithConditionRule(itemID, chanceDenominator, dropAmountMin, dropAmountMax, condition, chanceNumerator);
                npcLoot.Add(drop);
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }
    }
}

