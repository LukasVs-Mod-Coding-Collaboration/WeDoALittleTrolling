using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.NPCs;


namespace WeDoALittleTrolling.Content.Buffs
{
    public class SearingInferno : ModBuff
    {

        public static float damageNerfMultiplier = 0.25f;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            int dotDPS = 15;
            npc.lifeRegenExpectedLossPerSecond = 5;
            npc.lifeRegen -= (dotDPS*2);
            base.Update(npc, ref buffIndex);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            int dotDPS = 15;
            player.lifeRegen -= (dotDPS*2);
            base.Update(player, ref buffIndex);
        }
    }
}