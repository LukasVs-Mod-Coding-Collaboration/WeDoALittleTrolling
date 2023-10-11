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
using WeDoALittleTrolling.Common.ModPlayers;


namespace WeDoALittleTrolling.Content.Buffs
{
    public class WreckedAccuracy : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = tip+" "+(10+(Main.player[Main.myPlayer].GetModPlayer<WDALTPlayer>().wreckedAccuracyStack * 10))+"%";
            base.ModifyBuffText(ref buffName, ref tip, ref rare);
        }
        
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            if(player.GetModPlayer<WDALTPlayer>().wreckedAccuracyStack < 5)
            {
                player.GetModPlayer<WDALTPlayer>().wreckedAccuracyStack += 1;
            }
            return base.ReApply(player, time, buffIndex);
        }
    }
}
