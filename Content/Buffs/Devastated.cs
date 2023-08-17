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
using WeDoALittleTrolling.Common.Utilities;


namespace WeDoALittleTrolling.Content.Buffs
{
    public class Devastated : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = (10+(Main.player[Main.myPlayer].GetModPlayer<WDALTPlayerUtil>().devastatedStack * 10))+"% "+tip;
            base.ModifyBuffText(ref buffName, ref tip, ref rare);
        }
        
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            if(player.GetModPlayer<WDALTPlayerUtil>().devastatedStack < 2)
            {
                player.GetModPlayer<WDALTPlayerUtil>().devastatedStack += 1;
            }
            return base.ReApply(player, time, buffIndex);
        }
    }
}
