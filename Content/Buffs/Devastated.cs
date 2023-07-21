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
        }

        public override void Update(Player player, ref int buffIndex)
        {
            float value = 0f;
            value = player.GetModPlayer<WDALTPlayerUtil>().finalEndurance * 0.5f;
            player.endurance -= value;
            player.GetModPlayer<WDALTPlayerUtil>().finalEnduranceIncrease -= value;
        }
    }
}
