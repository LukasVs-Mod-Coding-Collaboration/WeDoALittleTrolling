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
            if(npc.type == NPCID.Derpling)
            {
                modifiers.SourceDamage *= (float)0.75;
            }
            if(npc.type == NPCID.WalkingAntlion || npc.type == NPCID.GiantWalkingAntlion)
            {
                modifiers.SourceDamage *= (float)0.75;
            }
            if(npc.type == NPCID.TombCrawlerHead)
            {
                modifiers.SourceDamage *= (float)0.75;
            }
            if(npc.type == NPCID.GiantTortoise)
            {
                modifiers.SourceDamage *= (float)0.65;
            }
            if(npc.type == NPCID.JungleCreeper || npc.type == NPCID.JungleCreeperWall)
            {
                modifiers.SourceDamage *= (float)0.75;
            }
            if
            (
                npc.type == NPCID.GiantMossHornet ||
                npc.type == NPCID.BigMossHornet ||
                npc.type == NPCID.MossHornet ||
                npc.type == NPCID.LittleMossHornet ||
                npc.type == NPCID.TinyMossHornet
            )
            {
                modifiers.SourceDamage *= (float)0.75;
            }
            base.ModifyHitPlayer(npc, target, ref modifiers);
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if(npc.type == NPCID.AngryTrapper)
            {
                target.AddBuff(BuffID.Venom, 120, false);
            }
            base.OnHitPlayer(npc, target, hurtInfo);
        }
    }
}
