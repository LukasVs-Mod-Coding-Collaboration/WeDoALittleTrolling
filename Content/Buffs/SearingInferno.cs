using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;


namespace WeDoALittleTrolling.Content.Buffs
{
    public class SearingInferno : ModBuff
    {

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }


        public static void NPCEffects(NPC npc)
        {
            npc.damage = (int)(npc.damage * 0.75f);
            
        }

        public static void NPCEffectsEnd(NPC npc, int buffType)
        {
            npc.color = default(Color); 
        }       
    }
}