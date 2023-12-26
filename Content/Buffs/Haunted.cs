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
    public class Haunted : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public static void AnimateHaunted(Player player)
        {
            for (int i = 0; i < 60; i++)
            {
                int rMax = (int)player.width;
                double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                double angle = Main.rand.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = player.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                Vector2 dustVelocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                dustVelocity.Normalize();
                dustVelocity *= 16f;
                Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Shadowflame, dustVelocity, 0, default);
                newDust.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.NPCHit55, player.Center);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<WDALTPlayer>().hauntedDebuffTicksLeft > 0)
            {
                player.GetModPlayer<WDALTPlayer>().hauntedDebuffTicksLeft--;
            }
            else
            {
                player.GetModPlayer<WDALTPlayer>().hauntedDebuffTicksLeft = 0;
                player.GetModPlayer<WDALTPlayer>().hauntedDebuff = false;
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
