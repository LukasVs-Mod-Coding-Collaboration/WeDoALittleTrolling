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
using ReLogic.Content;
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Buffs
{
    public class Gnomed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
        
        public override void Update(Player player, ref int buffIndex)
        {
            if(Gnomed_ShouldTurnToStone(player))
            {
                player.GetModPlayer<WDALTPlayer>().gnomedStonedDebuff = true;
            }
            else
            {
                player.GetModPlayer<WDALTPlayer>().gnomedStonedDebuff = false;
            }
        }

        public static bool Gnomed_ShouldTurnToStone(Player player)
        {
            if(player.buffImmune[BuffID.Stoned])
            {
                return false;
            }
            if(Main.remixWorld)
            {
                if((player.position.Y / 16f) > (float)(Main.maxTilesY - 350))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if(Main.dayTime)
            {
                if(WorldGen.InAPlaceWithWind(player.position, player.width, player.height))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
