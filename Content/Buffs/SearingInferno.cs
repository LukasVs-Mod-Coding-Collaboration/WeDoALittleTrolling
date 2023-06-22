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
        public static void NPCEffects(NPC npc)
        {            
            Color color = new Color(0, 255, 165, 0);
            npc.color = color;
        }
        public static void NPCEffectsEnd(NPC npc, int buffType)
        {
            npc.color = default(Color);
        }

        public override void Update(NPC npc,ref int buffIndex)
        {
            npc.damage = (int)(npc.damage * 0.75f);
        }

    }
}