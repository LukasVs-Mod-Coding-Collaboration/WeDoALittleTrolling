using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;

namespace WeDoALittleTrolling.Content.NPCs
{
    internal class GlobalNPCs : GlobalNPC
    {
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            base.ModifyHitPlayer(npc, target, ref modifiers);
        }

        public override void SetDefaults(NPC npc)
        {
            int[] NerfGroup25Percent =
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
            int[] NerfGroup35Percent =
            {
                NPCID.GiantTortoise,
                NPCID.IceTortoise
            };
            if(NerfGroup25Percent.Contains(npc.type))
            {
                npc.damage = (int)Math.Round(npc.damage * (float)0.75);
            }
            if(NerfGroup35Percent.Contains(npc.type))
            {
                npc.damage = (int)Math.Round(npc.damage * (float)0.65);
            }
            base.SetDefaults(npc);
        }

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            Random random = new Random();
            int[] InflictVenomDebuff1In1Group =
            {
                NPCID.AngryTrapper,
                NPCID.Moth
            };
            int[] InflictBleedingDebuff1In1Group =
            {
                NPCID.Shark,
                NPCID.SandShark
            };
            int[] InflictBleedingDebuff1In8Group =
            {
                NPCID.Herpling,
                NPCID.Wolf,
                NPCID.PirateCorsair,
                NPCID.PirateGhost
            };

            if(InflictVenomDebuff1In1Group.Contains(npc.type))
            {
                if(random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Venom, 240, false); //4s, X2 in Expert, X2.5 in Master
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
    }
}
