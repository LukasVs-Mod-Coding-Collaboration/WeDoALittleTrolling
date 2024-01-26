/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2024 LukasV-Coding

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

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
            tip = (10+(Main.player[Main.myPlayer].GetModPlayer<WDALTPlayer>().devastatedStack * 10))+"% "+tip;
            base.ModifyBuffText(ref buffName, ref tip, ref rare);
        }
        
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            if(player.GetModPlayer<WDALTPlayer>().devastatedStack < 3)
            {
                player.GetModPlayer<WDALTPlayer>().devastatedStack += 1;
            }
            return base.ReApply(player, time, buffIndex);
        }

        public static void AnimateDevastated(Player player)
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
            SoundEngine.PlaySound(SoundID.NPCHit43, player.Center);
        }
    }
}
